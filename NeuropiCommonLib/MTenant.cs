﻿//using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using NeuroPi.CommonLib.Model;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;


namespace NeuroPi.CommonLib.Model
{
    
    public class MTenant : MBaseModel
    {
       
        public int TenantId { get; set; }

       
        public string Name { get; set; } = string.Empty;       

        //public virtual ICollection<MUser> Users { get; set; } = new List<MUser>();   



  }
}