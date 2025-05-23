using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Model;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    public class ContactController1 : Controller
    {
        private readonly IContactService _contactService;

        public ContactController1(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: /Contact/GetAll
      
    }
}