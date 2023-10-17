using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileGroupProject.Data;
using AgileGroupProject.Data.Entities;
using AgileGroupProject.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgileGroupProject.Services.User
{
    public class UserService : IUserService
    {
        private readonly AgpDbContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserService(AgpDbContext context,
                           UserManager<UserEntity> userManager,
                           SignInManager<UserEntity> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> RegisterUserAsync(UserRegister model)
        {
            if (await UserNameNotAvailable(model.UserName) || await EmailNotAvailable(model.Email))
            {
                return false;
            }
            
            UserEntity entity = new UserEntity()
            {
                Email = model.Email,
                UserName = model.UserName,
                DateCreated = DateTime.Now
            };

            IdentityResult registerResult = await _userManager.CreateAsync(entity, model.Password);
            return registerResult.Succeeded;
        }

        private Task<bool> UserNameNotAvailable(string userName)
        {
            return _context.Users.AnyAsync(u => u.UserName == userName);
        }

        private Task<bool> EmailNotAvailable(string email)
        {
            return _context.Users.AnyAsync(e => e.Email == email);
        }
    }
}