﻿using System.ComponentModel.DataAnnotations;

namespace DoList.Common.Models.User
{
    public class LoginUserModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
