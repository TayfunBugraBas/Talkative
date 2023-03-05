using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkative.Source.Models
{
    public static class ActiveUser
    {
      public static Models.UserModel CurUser { get; set; }
      
      public static FirebaseAuthLink FbUser { get; set; }

    }
}
