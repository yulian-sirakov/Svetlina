﻿using System.Composition;
using Microsoft.AspNetCore.Identity;


namespace Svetlina.Data.Models
{
    public class Customer : IdentityUser
    {
        public List<Report> Reports { get; set; }
        public List<Project> Projects { get; set; }


        private Customer()
        {
            Reports = new List<Report>();
            Projects = new List<Project>();
        }
        public Customer(string CustomerName, string Email, string PhoneNumber)
        {
            UserName = CustomerName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            Reports = new List<Report>();
            Projects = new List<Project>();


        }
    }
}
