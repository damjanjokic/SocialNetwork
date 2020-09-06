using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.IRepositories;
using SocialNetwork.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;

        public AuthRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailure)
        {
            return _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
           return await _userManager.CreateAsync(user, password);
        }

        public async Task<AppUser> FindByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public Task UpdateAsync(AppUser user)
        {
            return _userManager.UpdateAsync(user);
        }

        public async Task<bool> UserExistsQuery(Expression<Func<AppUser, bool>> filter = null)
        {
            if (await _context.Users.AnyAsync(filter))
                return true;

            return false;
        }

    }
}
