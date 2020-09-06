using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Core.Entities
{
    public class UserFollowing
    {
        public int ObserverId { get; set; }
        public  AppUser Observer { get; set; }
        public int TargetId { get; set; }
        public  AppUser Target { get; set; }
    }
}
