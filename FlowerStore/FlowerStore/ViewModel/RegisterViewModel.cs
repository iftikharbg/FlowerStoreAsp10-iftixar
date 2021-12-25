﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerStore.ViewModel
{
    public class RegisterViewModel
    {
        [Required, MaxLength(100),MinLength(6) ]

        public string FullName { get; set; }

        [Required,MaxLength(100),MinLength(6)]

        public string Username { get; set; }

        [Required,EmailAddress,DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Required,DataType(DataType.Password)]

        public string Password { get; set; }

        [Required, DataType(DataType.Password),Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }
    }
}
