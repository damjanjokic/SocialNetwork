using Microsoft.AspNetCore.Identity;
using SocialNetwork.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.IRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<SignInResult> CheckPasswordSignInAsync(AppUser User, string password, bool lockoutOnFailure);
        Task<AppUser> FindByUsername(string username);
        Task UpdateAsync(AppUser user);
        Task<bool> UserExistsQuery(Expression<Func<AppUser, bool>> filter = null);
    }
}
