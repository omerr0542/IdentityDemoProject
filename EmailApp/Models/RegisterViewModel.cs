﻿namespace EmailApp.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        // AppUser sınıfındaki zorunlu property'ler
        public string FirstName { get; set; } 
        public string LastName { get; set; }
    }
}
