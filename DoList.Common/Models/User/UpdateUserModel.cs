﻿using System.ComponentModel.DataAnnotations;

namespace DoList.Common.Models.User
{
    public class UpdateUserModel
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }

    }
}
