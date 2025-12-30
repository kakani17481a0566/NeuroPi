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
        public UserLogInSucessVM LogIn(string username, string password)
        {
            Console.WriteLine($"[INFO] Login attempt for username: {username}");

            var user = _context.Users.FirstOrDefault(u =>
                !u.IsDeleted && u.Username == username && u.Password == password);

            if (user == null)
            {
                Console.WriteLine($"[WARN] Login failed: User not found or incorrect password for username: {username}");
                return null;
            }

            Console.WriteLine($"[INFO] User found: UserId = {user.UserId}, fetching roles and departments...");

            var role = _context.UserRoles
                .Include(r => r.Role)
                .Include(u => u.User)
                .FirstOrDefault(r => !r.IsDeleted && r.UserId == user.UserId && r.TenantId == user.TenantId);

            var department = _context.UserDepartments
                .FirstOrDefault(d => d.UserId == user.UserId && !d.IsDeleted);

            Console.WriteLine($"[INFO] Login successful: Role = {role?.Role?.Name}, DepartmentId = {department?.DepartmentId}");

            var response = new UserLogInSucessVM
            {
                UserName = username,
                FirstName = role.User.FirstName,
                LastName = role.User.LastName,
                TenantId = user.TenantId,
                UserId = user.UserId,
                token = GenerateJwtToken(user),
                UserProfile = UserResponseVM.ToViewModel(user),
                RoleId = role.Role.RoleId,
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
                expires: DateTime.UtcNow.AddMinutes(30),
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

            foreach (var vm in userVMs)
            {
                var user = users.FirstOrDefault(u => u.UserId == vm.UserId);
                if (user != null)
                {
                    var role = user.UserRoles.FirstOrDefault(ur => !ur.IsDeleted);
                    vm.RoleName = role?.Role?.Name;

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


        public UserResponseVM AddUser(UserRequestVM request)
        {
            var user = UserRequestVM.ToModel(request);
            _context.Users.Add(user);
            _context.SaveChanges();
            return UserResponseVM.ToViewModel(user);
        }

        public UserResponseVM UpdateUser(int id, int tenantId, UserUpdateRequestVM userUpdate)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id && u.TenantId == tenantId && !u.IsDeleted);
            if (user == null) return null;

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
            user.UpdatedBy = userUpdate.UpdatedBy;
            user.UpdatedOn = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(userUpdate.UserImageUrl))
            {
                user.UserImageUrl = userUpdate.UserImageUrl;
            }

            // Handle Role Update/Assignment
            if (!string.IsNullOrEmpty(userUpdate.RoleName))
            {
                var role = _context.Roles.FirstOrDefault(r => r.Name == userUpdate.RoleName && r.TenantId == tenantId);
                if (role != null)
                {
                    var existingUserRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == id && ur.TenantId == tenantId && !ur.IsDeleted);
                    if (existingUserRole != null)
                    {
                        // Update existing role if different
                        if (existingUserRole.RoleId != role.RoleId)
                        {
                            existingUserRole.RoleId = role.RoleId;
                            existingUserRole.UpdatedBy = userUpdate.UpdatedBy;
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
                            CreatedBy = userUpdate.UpdatedBy ?? 0,
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
                                CreatedBy = userUpdate.UpdatedBy ?? 0,
                                CreatedOn = DateTime.UtcNow
                            };
                            _context.UserDepartments.Add(userDept);
                        }
                   }
                   
                   // Handles PARENT logic
                   if (userUpdate.RoleName.ToUpper() == "PARENT")
                   {
                        // formatting: create parent if not exists
                        // Using Raw SQL because MParent is in SchoolManagement namespace which is not referenced here
                        var sql = @"INSERT INTO parents (user_id, tenant_id, created_by, created_on, is_deleted)
                                    SELECT {0}, {1}, {2}, {3}, false
                                    WHERE NOT EXISTS (SELECT 1 FROM parents WHERE user_id = {0} AND tenant_id = {1} AND is_deleted = false);";
                        
                         _context.Database.ExecuteSqlRaw(sql, id, tenantId, userUpdate.UpdatedBy ?? 0, DateTime.UtcNow);
                   }
                }
            }

            _context.SaveChanges();

            return UserResponseVM.ToViewModel(user);
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
            user.UpdatedOn = DateTime.UtcNow;

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


    }
}
