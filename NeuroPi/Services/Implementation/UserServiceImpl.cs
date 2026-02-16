using Azure.Storage.Blobs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.User;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class UserServiceImpl : IUserService
    {
        private readonly NeuroPiDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;

        public UserServiceImpl(NeuroPiDbContext context, IConfiguration configuration, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _configuration = configuration;
            _blobServiceClient = blobServiceClient;
        }


        // login
        public UserLogInSucessVM LogIn(string identifier, string password)
        {
            Console.WriteLine($"[INFO] Login attempt for identifier: {identifier}");

            var user = _context.Users.FirstOrDefault(u =>
                !u.IsDeleted && (u.Username == identifier || u.Email == identifier || u.MobileNumber == identifier) && u.Password == password);

            if (user == null)
            {
                Console.WriteLine($"[WARN] Login failed: User not found or incorrect password for identifier: {identifier}");
                return null;
            }

            Console.WriteLine($"[INFO] User found: UserId = {user.UserId}, fetching roles and departments...");

            var role = _context.UserRoles
                .Include(r => r.Role)
                .Include(u => u.User)
                .FirstOrDefault(r => !r.IsDeleted && r.UserId == user.UserId && r.TenantId == user.TenantId);

            if (role == null)
            {
                Console.WriteLine($"[WARN] Role is NULL for UserId: {user.UserId}, TenantId: {user.TenantId}");
            }
            else
            {
                 Console.WriteLine($"[INFO] Role Found: {role.Role?.Name} (ID: {role.RoleId})");
            }

            var department = _context.UserDepartments
                .FirstOrDefault(d => d.UserId == user.UserId && !d.IsDeleted);

            Console.WriteLine($"[INFO] Login successful: Role = {role?.Role?.Name}, DepartmentId = {department?.DepartmentId}");

            var response = new UserLogInSucessVM
            {
                UserName = user.Username,
                FirstName = user.FirstName, // Use user object directly
                LastName = user.LastName,   // Use user object directly
                TenantId = user.TenantId,
                UserId = user.UserId,
                token = GenerateJwtToken(user),
                UserProfile = UserResponseVM.ToViewModel(user),
                RoleId = role?.Role?.RoleId ?? 0,
                RoleName = role?.Role?.Name,
                departmentId = department?.DepartmentId ?? 0,
                UserImageUrl = user.UserImageUrl,
                LinkedStudents = (role?.Role?.Name?.ToUpper() == "PARENT") ? GetLinkedStudents(user.UserId, user.TenantId) : null
            };

            // ✅ Generate SAS Token for Login response too (Sidebar/Header usually use this)
            if (!string.IsNullOrEmpty(response.UserImageUrl))
            {
                try
                {
                    var uri = new Uri(response.UserImageUrl);
                    var blobName = System.IO.Path.GetFileName(uri.LocalPath);
                    var containerClient = _blobServiceClient.GetBlobContainerClient("user-profiles");
                    var blobClient = containerClient.GetBlobClient(blobName);

                    if (blobClient.CanGenerateSasUri)
                    {
                        var sasBuilder = new Azure.Storage.Sas.BlobSasBuilder
                        {
                            BlobContainerName = "user-profiles",
                            BlobName = blobName,
                            Resource = "b",
                            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
                        };
                        sasBuilder.SetPermissions(Azure.Storage.Sas.BlobSasPermissions.Read);
                        response.UserImageUrl = blobClient.GenerateSasUri(sasBuilder).ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Failed to generate SAS token for login image: {ex.Message}");
                }
            }

            return response;
        }

        private string GenerateJwtToken(MUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Username", user.Username ?? " "),
                new Claim("Email", user.Email ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(8), // 8 hours to support activity-based session extension
                signingCredentials: creds);

            return "bearer " + new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<UserResponseVM> GetAllUsers() =>
            UserResponseVM.ToViewModelList(_context.Users.Where(u => !u.IsDeleted).ToList());

        public List<UserResponseVM> GetAllUsersByTenantId(int tenantId) =>
            UserResponseVM.ToViewModelList(_context.Users.Where(u => u.TenantId == tenantId && !u.IsDeleted).ToList());

        public List<UserResponseVM> GetAllUsersByTenantIdWithRoles(int tenantId)
        {
            var users = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.TenantId == tenantId && !u.IsDeleted)
                .ToList();

            var userVMs = UserResponseVM.ToViewModelList(users);

            // Manual mapping for RoleName since AutoMapper isn't being used here
            // Bulk fetch linked students for all parents in this tenant
            var linkedStudentsMap = GetAllLinkedStudentsForTenant(tenantId);
            
            // Fetch all UserIds and ParentIds from parents table for this tenant
            // Dictionary<UserId, ParentId>
            var parentUserMap = new Dictionary<int, int>();
            try
            {
                var parentSql = @"SELECT ""user_id"", ""id"" FROM ""parents"" WHERE ""tenant_id"" = {0} AND ""is_deleted"" = false";
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = parentSql.Replace("{0}", tenantId.ToString());
                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0) && !reader.IsDBNull(1))
                            {
                                int uId = reader.GetInt32(0);
                                int pId = reader.GetInt32(1);
                                if (!parentUserMap.ContainsKey(uId))
                                {
                                    parentUserMap[uId] = pId;
                                }
                            }
                        }
                    }
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to fetch parent user IDs: {ex.Message}");
            }

            foreach (var vm in userVMs)
            {
                var user = users.FirstOrDefault(u => u.UserId == vm.UserId);
                if (user != null)
                {
                    var role = user.UserRoles.FirstOrDefault(ur => !ur.IsDeleted);
                    vm.RoleName = role?.Role?.Name;
                    
                    // Override rule: IF user is in parents table, they ARE a PARENT
                    if (parentUserMap.ContainsKey(vm.UserId))
                    {
                        vm.RoleName = "PARENT";
                        vm.ParentId = parentUserMap[vm.UserId];
                    }

                    if (vm.RoleName?.ToUpper() == "PARENT" && linkedStudentsMap.ContainsKey(vm.UserId))
                    {
                        vm.LinkedStudents = linkedStudentsMap[vm.UserId];
                    }
                }
            }

            return userVMs;
        }

        public UserResponseVM GetUser(int id, int tenantId)
        {
            var user=_context.Users.FirstOrDefault(u=>u.UserId==id && u.TenantId==tenantId && !u.IsDeleted);
            if(user != null)
            {
                var response = UserResponseVM.ToViewModel(user);
                
                // Get Role for this user to check if PARENT
                var role = _context.UserRoles.Include(r => r.Role)
                            .FirstOrDefault(ur => ur.UserId == id && ur.TenantId == tenantId && !ur.IsDeleted);
                            
                if (role?.Role?.Name?.ToUpper() == "PARENT")
                {
                    response.LinkedStudents = GetLinkedStudents(id, tenantId);
                }
                
                return response;
            }
            return null;
        }

        private Dictionary<int, List<object>> GetAllLinkedStudentsForTenant(int tenantId)
        {
            try
            {
                var sql = @"
                    SELECT 
                        p.""user_id"" as ""ParentUserId"",
                        s.""id"" as ""StudentId"", 
                        s.""first_name"" as ""ResultName"", 
                        s.""last_name"" as ""ResultLastName"",
                        s.""reg_number"" as ""AdmissionNumber"",
                        c.""name"" as ""CourseName"", 
                        b.""name"" as ""BranchName"",
                        s.""course_id"" as ""CourseId"",
                        s.""branch_id"" as ""BranchId"" 
                    FROM ""parents"" p 
                    JOIN ""parent_student"" ps ON ps.""parent_id"" = p.""id"" 
                    JOIN ""students"" s ON s.""id"" = ps.""student_id"" 
                    LEFT JOIN ""course"" c ON c.""id"" = s.""course_id"" 
                    LEFT JOIN ""branch"" b ON b.""id"" = s.""branch_id"" 
                    WHERE p.""tenant_id"" = {0} AND p.""is_deleted"" = false AND ps.""is_deleted"" = false AND s.""is_deleted"" = false";

                var result = new Dictionary<int, List<object>>();

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandText = command.CommandText.Replace("{0}", tenantId.ToString());

                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            int parentUserId = 0;
                            // Read fields
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var name = reader.GetName(i);
                                var value = reader.GetValue(i);
                                if (name == "ParentUserId")
                                {
                                    parentUserId = value == DBNull.Value ? 0 : Convert.ToInt32(value);
                                }
                                else
                                {
                                    row[name] = value == DBNull.Value ? null : value;
                                }
                            }

                            if (parentUserId != 0)
                            {
                                if (!result.ContainsKey(parentUserId))
                                {
                                    result[parentUserId] = new List<object>();
                                }
                                result[parentUserId].Add(row);
                            }
                        }
                    }
                    _context.Database.CloseConnection();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to fetch all linked students: {ex.Message}");
                return new Dictionary<int, List<object>>();
            }
        }

        private object GetLinkedStudents(int userId, int tenantId)
        {
            try 
            {
                var sql = @"
                    SELECT 
                        s.""id"" as ""StudentId"", 
                        s.""first_name"" as ""ResultName"", 
                        s.""last_name"" as ""ResultLastName"",
                        s.""reg_number"" as ""AdmissionNumber"",
                        c.""name"" as ""CourseName"", 
                        b.""name"" as ""BranchName"",
                        s.""course_id"" as ""CourseId"",
                        s.""branch_id"" as ""BranchId"" 
                    FROM ""parents"" p 
                    JOIN ""parent_student"" ps ON ps.""parent_id"" = p.""id"" 
                    JOIN ""students"" s ON s.""id"" = ps.""student_id"" 
                    LEFT JOIN ""course"" c ON c.""id"" = s.""course_id"" 
                    LEFT JOIN ""branch"" b ON b.""id"" = s.""branch_id"" 
                    WHERE p.""user_id"" = {0} AND p.""tenant_id"" = {1} AND p.""is_deleted"" = false AND ps.""is_deleted"" = false AND s.""is_deleted"" = false";
                    
                var result = new List<Dictionary<string, object>>();
                
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    // Parameter replacement for Raw SQL with {0} only works with FromSqlRaw, 
                    // for generic command execution we need to replace manually or use parameters.
                    // Since specific DbContext ExecuteSqlRaw doesn't return results easily without a type,
                    // we'll construct the query string safely or use a DTO.
                    // But here we need DYNAMIC structure.
                    
                    // Actually, let's use a simpler approach: 
                    // Use FromSqlRaw with a dummy type? No.
                    
                    // Let's manually replace parameters since they are integers (safe-ish).
                    command.CommandText = command.CommandText.Replace("{0}", userId.ToString()).Replace("{1}", tenantId.ToString());
                    
                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            result.Add(row);
                        }
                    }
                    _context.Database.CloseConnection();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to fetch linked students: {ex.Message}");
                return null;
            }
        }


        public UserResponseVM AddUser(UserRequestVM request, out string message)
        {
            message = string.Empty;

            try
            {
                // 1. Check if username already exists
                if (CheckUsernameExists(request.Username))
                {
                    message = "Username already exists";
                    return null;
                }

                // 2. Check if email already exists
                if (CheckEmailExists(request.Email))
                {
                    message = "Email already exists";
                    return null;
                }

                var user = UserRequestVM.ToModel(request);
                _context.Users.Add(user);
                _context.SaveChanges();

                // Handle Role Assignment
                if (!string.IsNullOrEmpty(request.RoleName))
                {
                    var role = _context.Roles.FirstOrDefault(r => r.Name.ToLower() == request.RoleName.ToLower() && r.TenantId == request.TenantId);
                    if (role != null)
                    {
                        var newUserRole = new MUserRole
                        {
                            UserId = user.UserId,
                            RoleId = role.RoleId,
                            TenantId = request.TenantId,
                            CreatedBy = request.CreatedBy,
                            CreatedOn = DateTime.UtcNow
                        };
                        _context.UserRoles.Add(newUserRole);

                        // Handles TEACHER logic
                        if (request.RoleName.ToUpper() == "TEACHER")
                        {
                            var userDept = new MUserDepartment
                            {
                                UserId = user.UserId,
                                DepartmentId = 1, // Hardcoded
                                TenantId = request.TenantId,
                                CreatedBy = request.CreatedBy,
                                CreatedOn = DateTime.UtcNow
                            };
                            _context.UserDepartments.Add(userDept);
                        }

                        // Handles PARENT logic
                        if (request.RoleName.ToUpper() == "PARENT")
                        {
                            // Create parent entry
                            var sql = @"INSERT INTO parents (user_id, tenant_id, created_by, created_on, is_deleted)
                                        VALUES ({0}, {1}, {2}, {3}, false)";

                            _context.Database.ExecuteSqlRaw(sql, user.UserId, request.TenantId, request.CreatedBy, DateTime.UtcNow);

                            // Handle Linked Students
                            if (request.LinkedStudents != null && request.LinkedStudents.Count > 0)
                            {
                                foreach (var student in request.LinkedStudents)
                                {
                                    var linkSql = @"INSERT INTO parent_student (parent_id, student_id, tenant_id, created_by, created_on, is_deleted)
                                                    SELECT id, {1}, {2}, {3}, {4}, false
                                                    FROM parents
                                                    WHERE user_id = {0} AND tenant_id = {2} AND is_deleted = false";
                                    _context.Database.ExecuteSqlRaw(linkSql, user.UserId, student.StudentId, request.TenantId, request.CreatedBy, DateTime.UtcNow);
                                }
                            }
                        }

                        _context.SaveChanges();
                    }
                }

                message = "User added successfully";
                return UserResponseVM.ToViewModel(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] AddUser failed: {ex.Message}");
                // Check if it's an inner exception from DB updates
                if (ex.InnerException != null)
                {
                    message = $"Failed to add user: {ex.InnerException.Message}";
                }
                else
                {
                    message = $"Failed to add user: {ex.Message}";
                }
                return null;
            }
        }

        public UserResponseVM UpdateUser(int id, int tenantId, UserUpdateRequestVM userUpdate)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == id && u.TenantId == tenantId && !u.IsDeleted);
                if (user == null) return null;

                // Safe UpdatedBy
                int safeUpdatedBy = (userUpdate.UpdatedBy.HasValue && userUpdate.UpdatedBy.Value > 0) ? userUpdate.UpdatedBy.Value : id;

                user.Username = userUpdate.Username ?? user.Username;
                user.FirstName = userUpdate.FirstName ?? user.FirstName;
                user.MiddleName = userUpdate.MiddleName ?? user.MiddleName;
                user.LastName = userUpdate.LastName ?? user.LastName;
                user.Email = userUpdate.Email ?? user.Email;
                if (!string.IsNullOrEmpty(userUpdate.Password))
                {
                    user.Password = userUpdate.Password;
                }
                user.MobileNumber = userUpdate.MobileNumber ?? user.MobileNumber;
                user.AlternateNumber = userUpdate.AlternateNumber ?? user.AlternateNumber;
                user.DateOfBirth = userUpdate.DateOfBirth;
                user.Address = userUpdate.Address ?? user.Address;
                user.UpdatedBy = safeUpdatedBy;
                user.UpdatedOn = DateTime.UtcNow;
                if (!string.IsNullOrEmpty(userUpdate.UserImageUrl))
                {
                    user.UserImageUrl = userUpdate.UserImageUrl;
                }

                // Handle Role Update/Assignment
                if (!string.IsNullOrEmpty(userUpdate.RoleName))
                {
                    var role = _context.Roles.FirstOrDefault(r => r.Name.ToLower() == userUpdate.RoleName.ToLower() && r.TenantId == tenantId);
                    if (role != null)
                    {
                        var existingUserRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == id && ur.TenantId == tenantId && !ur.IsDeleted);
                        if (existingUserRole != null)
                        {
                            // Update existing role if different
                            if (existingUserRole.RoleId != role.RoleId)
                            {
                                existingUserRole.RoleId = role.RoleId;
                                existingUserRole.UpdatedBy = safeUpdatedBy;
                                existingUserRole.UpdatedOn = DateTime.UtcNow;
                                _context.UserRoles.Update(existingUserRole);
                            }
                        }
                        else
                        {
                            // Add new role
                            var newUserRole = new MUserRole
                            {
                                UserId = id,
                                RoleId = role.RoleId,
                                TenantId = tenantId,
                                CreatedBy = safeUpdatedBy, // Fallback for created by if new
                                CreatedOn = DateTime.UtcNow
                            };
                            _context.UserRoles.Add(newUserRole);
                        }

                        // Handles TEACHER logic
                        if (userUpdate.RoleName.ToUpper() == "TEACHER")
                        {
                            var existingDept = _context.UserDepartments.FirstOrDefault(ud => ud.UserId == id && ud.DepartmentId == 1 && ud.TenantId == tenantId && !ud.IsDeleted);
                            if (existingDept == null)
                            {
                                var userDept = new MUserDepartment
                                {
                                    UserId = id,
                                    DepartmentId = 1, // Hardcoded
                                    TenantId = tenantId,
                                    CreatedBy = safeUpdatedBy,
                                    CreatedOn = DateTime.UtcNow
                                };
                                _context.UserDepartments.Add(userDept);
                            }
                        }

                        // Handles PARENT logic
                        Console.WriteLine($"[DEBUG] Checking Parent Logic. RoleName: {userUpdate.RoleName}");
                        if (userUpdate.RoleName.ToUpper() == "PARENT")
                        {
                            Console.WriteLine($"[DEBUG] Entering Parent Logic. TenantId: {tenantId}, UserId: {id}");
                            // formatting: create parent if not exists
                            // Using Raw SQL because MParent is in SchoolManagement namespace which is not referenced here
                            var sql = @"INSERT INTO parents (user_id, tenant_id, created_by, created_on, is_deleted)
                                    SELECT {0}, {1}, {2}, {3}, false
                                    WHERE NOT EXISTS (SELECT 1 FROM parents WHERE user_id = {0} AND tenant_id = {1} AND is_deleted = false);";

                            _context.Database.ExecuteSqlRaw(sql, id, tenantId, safeUpdatedBy, DateTime.UtcNow);

                            // Handle Linked Students
                            if (userUpdate.LinkedStudents != null)
                            {
                                Console.WriteLine($"[DEBUG] Processing LinkedStudents. Count: {userUpdate.LinkedStudents.Count}");

                                int parentIdToUse = 0;

                                // 1. Try using explicitly passed ParentId
                                if (userUpdate.ParentId.HasValue && userUpdate.ParentId.Value > 0)
                                {
                                    parentIdToUse = userUpdate.ParentId.Value;
                                }
                                else
                                {
                                    // 2. Fallback: Lookup parent_id from parents table using user_id
                                    // This is needed if frontend didn't pass it, or if we just created the parent above
                                    try 
                                    {
                                        var pIdHelperSql = "SELECT id FROM parents WHERE user_id = {0} AND tenant_id = {1} AND is_deleted = false LIMIT 1";
                                        using (var command = _context.Database.GetDbConnection().CreateCommand())
                                        {
                                            command.CommandText = pIdHelperSql.Replace("{0}", id.ToString()).Replace("{1}", tenantId.ToString());
                                            _context.Database.OpenConnection();
                                            var result = command.ExecuteScalar();
                                            if (result != null && result != DBNull.Value)
                                            {
                                                parentIdToUse = Convert.ToInt32(result);
                                            }
                                            _context.Database.CloseConnection();
                                        }
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine($"[ERROR] Failed to lookup parent ID: {ex.Message}");
                                    }
                                }

                                if (parentIdToUse > 0)
                                {
                                    Console.WriteLine($"[DEBUG] Using ParentId: {parentIdToUse}");

                                    // Soft delete existing links for this parent
                                    var deleteSql = @"UPDATE parent_student 
                                                   SET is_deleted = true, updated_by = {0}, updated_on = {1}
                                                   WHERE parent_id = {2} AND tenant_id = {3} AND is_deleted = false";
                                    _context.Database.ExecuteSqlRaw(deleteSql, safeUpdatedBy, DateTime.UtcNow, parentIdToUse, tenantId);

                                    // Insert new links or Activate existing ones
                                    foreach (var student in userUpdate.LinkedStudents)
                                    {
                                        Console.WriteLine($"[DEBUG] Linking StudentId: {student.StudentId} to ParentId: {parentIdToUse}");
                                        // Postgres Upsert (ON CONFLICT)
                                        var linkSql = @"INSERT INTO parent_student (parent_id, student_id, tenant_id, created_by, created_on, is_deleted)
                                                     VALUES ({0}, {1}, {2}, {3}, {4}, false)
                                                     ON CONFLICT (parent_id, student_id) 
                                                     DO UPDATE SET is_deleted = false, updated_by = {3}, updated_on = {4}";
                                     
                                        _context.Database.ExecuteSqlRaw(linkSql, parentIdToUse, student.StudentId, tenantId, safeUpdatedBy, DateTime.UtcNow);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"[ERROR] Could not determine ParentId for User {id}. Linking skipped.");
                                }
                            }
                            else 
                            {
                                Console.WriteLine("[DEBUG] LinkedStudents is NULL");
                            }
                   }
                }
            }

            _context.SaveChanges();

            return UserResponseVM.ToViewModel(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UpdateUser failed: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[ERROR] Inner Exception: {ex.InnerException.Message}");
                }
                return null;
            }
        }

       
        



        public UserResponseVM DeleteUser(int id, int tenantId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id && u.TenantId == tenantId && !u.IsDeleted);
            if (user == null) return null;

            user.IsDeleted = true;
            _context.SaveChanges();

            return UserResponseVM.ToViewModel(user);
        }

        public async Task<string> UpdateUserImageAsync(int id, int tenantId, UserImageUploadVM request)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id && u.TenantId == tenantId && !u.IsDeleted);
            if (user == null || request.Image == null || request.Image.Length == 0)
                return null;

            var containerClient = _blobServiceClient.GetBlobContainerClient("user-profiles");
            await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.None);

            var blobName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
            var blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = request.Image.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            var imageUrl = blobClient.Uri.ToString();

            //user.UserImageUrl  = imageUrl;
            user.UpdatedBy = request.UpdatedBy;
            user.UpdatedOn = DateTime.UtcNow;
            user.UserImageUrl = imageUrl;
            
            _context.SaveChanges();

            // ✅ Generate SAS Token for return value
            if (blobClient.CanGenerateSasUri)
            {
                var sasBuilder = new Azure.Storage.Sas.BlobSasBuilder
                {
                    BlobContainerName = "user-profiles",
                    BlobName = blobName,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
                };
                sasBuilder.SetPermissions(Azure.Storage.Sas.BlobSasPermissions.Read);
                return blobClient.GenerateSasUri(sasBuilder).ToString();
            }

            return imageUrl;
        }



        //UserUpdatePasswordVM

        public bool UpdateUserPassword(int id, int tenantId, UserUpdatePasswordVM request, out string message)
        {
            message = string.Empty;

            var user = _context.Users.FirstOrDefault(u => u.UserId == id && u.TenantId == tenantId && !u.IsDeleted);
            if (user == null)
            {
                message = "User not found";
                return false;
            }

            // Simple check (no hash)
            if (user.Password != request.CurrentPassword)
            {
                message = "Current password is incorrect";
                return false;
            }

            user.Password = request.NewPassword;
            user.Password = request.NewPassword;
            user.UpdatedOn = DateTime.UtcNow;
            user.FirstTimeLogin = false; // Reset first time login flag

            try
            {
                _context.SaveChanges();
                message = "Password updated successfully";
                return true;
            }
            catch
            {
                message = "Failed to update password";
                return false;
            }
        }

        public bool ResetUserPassword(int id, int tenantId, AdminResetPasswordVM request, out string message)
        {
            message = string.Empty;

            var user = _context.Users.FirstOrDefault(u => u.UserId == id && u.TenantId == tenantId && !u.IsDeleted);
            if (user == null)
            {
                message = "User not found";
                return false;
            }

            user.Password = request.NewPassword;
            user.Password = request.NewPassword;
            user.UpdatedOn = DateTime.UtcNow;
            user.FirstTimeLogin = true; // Force password change on next login

            try
            {
                _context.SaveChanges();
                message = "Password reset successfully";
                return true;
            }
            catch
            {
                message = "Failed to reset password";
                return false;
            }
        }



        public UsersProfileSummaryVM GetUserProfileSummary(int id, int tenantId)
        {
            var sql = @"
    SELECT 
        u.user_id AS UserId,
        u.username AS Username,
        CONCAT(u.first_name, ' ', u.last_name) AS FullName,
        u.email AS Email,
        u.mobile_number AS MobileNumber,
        u.user_image_url AS UserImageUrl,
        COALESCE(STRING_AGG(DISTINCT r.name, ', '), 'N/A') AS Roles,
        COUNT(DISTINCT c.id) AS TotalCourses,
        COALESCE(STRING_AGG(DISTINCT c.name, ', '), 'N/A') AS CoursesTaught,
        COUNT(DISTINCT b.id) AS TotalBranches,
        COALESCE(STRING_AGG(DISTINCT b.name, ', '), 'N/A') AS Branches,
        
        -- ✅ return native types, not text
        u.joining_date AS JoiningDate,
        u.working_start_time AS WorkingStartTime,
        u.working_end_time AS WorkingEndTime,
        
        CASE WHEN u.is_deleted = FALSE THEN 'Active' ELSE 'Inactive' END AS UserStatus,
        u.created_on AS UserCreatedOn,
        u.updated_on AS UserLastUpdated,
        ur.created_on AS RoleAssignedOn
    FROM user_roles ur
    JOIN users u ON u.user_id = ur.user_id
    JOIN roles r ON r.role_id = ur.role_id
    LEFT JOIN course_teacher ct ON ct.teacher_id = u.user_id
    LEFT JOIN course c ON c.id = ct.course_id
    LEFT JOIN branch b ON b.id = ct.branch_id
    WHERE ur.user_id = {0} AND ur.tenant_id = {1}
    GROUP BY u.user_id, u.username, u.first_name, u.last_name, 
             u.email, u.mobile_number, u.user_image_url,
             u.joining_date, u.working_start_time, u.working_end_time,
             u.is_deleted, u.created_on, u.updated_on, ur.created_on;";

            var summary = _context.Set<UsersProfileSummaryVM>()
                .FromSqlRaw(sql, id, tenantId)
                .AsEnumerable()
                .FirstOrDefault();

            if (summary != null && !string.IsNullOrEmpty(summary.UserImageUrl))
            {
                try
                {
                    // Extract blob name from URL
                    // URL format: https://<account>.blob.core.windows.net/<container>/<blobname>
                    var uri = new Uri(summary.UserImageUrl);
                    var blobName = System.IO.Path.GetFileName(uri.LocalPath);

                    var containerClient = _blobServiceClient.GetBlobContainerClient("user-profiles");
                    var blobClient = containerClient.GetBlobClient(blobName);

                    if (blobClient.CanGenerateSasUri)
                    {
                        var sasBuilder = new Azure.Storage.Sas.BlobSasBuilder
                        {
                            BlobContainerName = "user-profiles",
                            BlobName = blobName,
                            Resource = "b",
                            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
                        };

                        sasBuilder.SetPermissions(Azure.Storage.Sas.BlobSasPermissions.Read);

                        summary.UserImageUrl = blobClient.GenerateSasUri(sasBuilder).ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Failed to generate SAS token for image: {ex.Message}");
                    // Keep original URL on error
                }
            }

            return summary;
        }

        public bool CheckUsernameExists(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            return _context.Users.Any(u => u.Username.ToLower() == username.ToLower() && !u.IsDeleted);
        }

        public bool CheckEmailExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return _context.Users.Any(u => u.Email.ToLower() == email.ToLower() && !u.IsDeleted);
        }

        public bool CheckFirstTimeLogin(string username, int tenantId)
        {
             var user = _context.Users.FirstOrDefault(u => 
                u.Username.ToLower() == username.ToLower() && 
                u.TenantId == tenantId && 
                !u.IsDeleted);
            
            if (user == null) return false; 
            
            return user.FirstTimeLogin;
        }

        public string SendMessage(string email)
        {
            //var today = DateOnly.FromDateTime(DateTime.Today);
            //var students = _context.Students
            //                .Where(s => s.Dob.Month == today.Month && s.Dob.Day == today.Day)
            //                .ToList();
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("kakanimohithkrishnasai@gmail.com", "asqevdthoegfynbb"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("kakanimohithkrishnasai@gmail.com"),
                Subject = "Birthday Wishes from Neuropi Tech Private Limited",
                Body = "<h1>Hello, " +
                "we are inviting you to birthday wish.</h1>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
            return "success";
        }
    }
}
