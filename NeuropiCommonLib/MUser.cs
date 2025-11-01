using System;
using System.Collections.Generic;

namespace NeuroPi.CommonLib.Model
{
    public class MUser : MBaseModel
    {
        
        public int UserId { get; set; }       
        public string Username { get; set; } = default!;        
        public string FirstName { get; set; } = default!;       
        public string? MiddleName { get; set; }        
        public string LastName { get; set; } = default!;        
        public string Password { get; set; } = default!;        
        public string Email { get; set; } = default!;       
        public string? MobileNumber { get; set; }        
        public string? AlternateNumber { get; set; }        
        public DateOnly? DateOfBirth { get; set; }       
        public string? Address { get; set; }        
        public int TenantId { get; set; }       
        public byte[]? UserImageUrl { get; set; }

        // ----------------------------
        // Extended profile info
        // ----------------------------
        
        public string? FatherName { get; set; }       
        public string? MotherName { get; set; }       
        public string? Gender { get; set; }        
        public string? MaritalStatus { get; set; }        
        public string? SpouseName { get; set; }        
        public DateOnly? WeddingAnniversaryDate { get; set; }        
        public DateOnly? JoiningDate { get; set; }      
        public TimeOnly? WorkingStartTime { get; set; }      
        public TimeOnly? WorkingEndTime { get; set; }        
        public int? RoleTypeId { get; set; }       
       }
}
