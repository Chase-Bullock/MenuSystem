using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using CathedralKitchen.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.Service
{
    public class AccountService : IAccountService
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountService> _logger;
        private readonly IConfiguration _configuration;

        public AccountService(CathedralKitchenContext ctx,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<AccountService> logger,
            IConfiguration configuration
            )
        {
            _ctx = ctx;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        public async void UpdateUser(PersonRegisterViewModel model, long id)
        {
            var user = _ctx.User.FirstOrDefault(x => x.Id == id);

            var person = _ctx.Person.FirstOrDefault(x => x.Id == user.PersonId);

            user.Email = model.Email;
            user.BuilderId = model.Builder;
            user.UpdateBy = id;
            user.UpdateTime = DateTime.UtcNow;

            person.LastName = model.LastName;
            person.FirstName = model.FirstName;
            person.Cell = model.Cell;
            person.UpdateTime = DateTime.UtcNow;
            person.UpdateBy = id;

            _ctx.Update(user);
            _ctx.Update(person);

            await _ctx.SaveChangesAsync();
        }

        public async Task<User> Login(LoginViewModel model)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email.ToUpper(), model.Password, true, false);
            if (result.Succeeded)
            {
                var user = _ctx.User.Include(x => x.City).FirstOrDefault(x => x.Email.ToUpper() == model.Email.ToUpper());

                if (user == null) return null;

                var claims = new List<Claim>();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: claims.Any() ? claims.ToArray() : new Claim[0],
                    expires: DateTime.UtcNow.AddMinutes(3000),
                    signingCredentials: creds
                );
                user.Hash = null;
                user.Token = new JwtSecurityTokenHandler().WriteToken(token);

                return user;
            }

            if (result.IsLockedOut)
            {
                return null;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> Register(RegisterViewModel model)
        {
            var user = new User { 
                Email = model.Email.ToUpper(),
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                BuilderId = model.BuilderId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Zipcode = model.Zipcode,
                CityId = model.CityId,
                Number = model.Number,

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var loginModel = new LoginViewModel
                {
                    Email = model.Email,
                    Password = model.Password
                };

                return await Login(loginModel);
            }

            return new User();

        }


        public async void Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }

        //public async void ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return RedirectToAction(nameof(HomeController.Index), "Home");
        //    }
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        throw new ApplicationException($"Unable to load user with ID '{userId}'.");
        //    }
        //    var result = await _userManager.ConfirmEmailAsync(user, code);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        //Todo Fix password
        public async Task<bool> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return true;
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);

            // If we got this far, something failed, redisplay form
            return false;
        }

    }
}
