﻿namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class SignUpDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
    }
}
