using SocialNetwork.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
