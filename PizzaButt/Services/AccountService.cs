using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using CathedralKitchen.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.Service
{
    public class AccountService
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountService> _logger;

        public AccountService(CathedralKitchenContext ctx,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<AccountService> logger
            )
        {
            _ctx = ctx;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
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

        public async Task<string> Login(LoginViewModel model)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email.ToUpper(), model.Password, true, false);
            if (result.Succeeded)
            {
                return "User logged in.";
            }
            if (result.IsLockedOut)
            {
                return "User account locked out.";
            }
            else
            {
                return "Invalid attempt";
            }
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
                var user = new User { Email = model.Email.ToUpper() };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                return true;
                }

            return false;
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
