using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkative.Source.Models
{
    public static class ActiveUser
    {
        public static string ID { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string? Adress { get; set; }
        public static string? PhoneForEmergency { get; set; }
    }
}
