using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public AccountsController(
            ICathedralKitchenRepository cathedralKitchenRepository,
            CathedralKitchenContext ctx,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountsController> logger)
        {
            _ctx = ctx;
            _cathedralKitchenRepository = cathedralKitchenRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] PersonRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var person = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Cell = model.Cell,
                    Active = true,
                    CreateBy = 1,
                    CreateTime = DateTime.UtcNow,
                    Email = model.Email,
                    SendEmail = model.SendEmail,
                    UpdateBy = 1,
                    UpdateTime = DateTime.UtcNow
                };

                await _ctx.AddAsync(person);
                await _ctx.SaveChangesAsync();

                var user = new User { Email = model.Email.ToUpper(), PersonId = person.Id };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email.ToUpper(), model.Password, true, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Ok();
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return BadRequest();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Ok(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return Ok(model);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody]PersonRegisterViewModel model , long id)
        {
            if (ModelState.IsValid)
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

            // If we got this far, something failed, redisplay form
            return BadRequest(model);
        }

    }
}
