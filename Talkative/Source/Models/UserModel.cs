﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkative.Source.Models
{
    public class UserModel
    {
        public string ID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string? Adress { get; set; }
        public string? PhoneForEmergency { get; set; }

    }
}
