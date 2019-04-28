using System;

namespace ModulSchool.Models
{
    public class User
    {
        public Guid Id { set; get; }
        public string Email { set; get; }
        public string Nickname     { set; get; }
        public string Phone { set; get; }
    }
}