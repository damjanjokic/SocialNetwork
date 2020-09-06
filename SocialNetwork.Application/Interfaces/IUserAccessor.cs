using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Application.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUsername();
        int GetCurrentUserId();
    }
}
