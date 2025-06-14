﻿using SchoolManagement.Model;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace SchoolManagement.ViewModel.Master
{
    //Sai Vardhan
    public class MasterResponseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        

        public int TenantId { get; set; }


        public int? MasterTypeId { get; set; }

        public string Code { get; set; }


        public static MasterResponseVM ToViewModel(MMaster master) =>
             new MasterResponseVM
             {
                Id = master.Id,
                Name = master.Name,
                Code = master.Code,
                TenantId = master.TenantId,
                MasterTypeId=master.MasterTypeId,
             };
        public static List<MasterResponseVM> ToViewModelList(List<MMaster> masters)
        {
            return masters.Select(ToViewModel).ToList();
        }

    }
}
