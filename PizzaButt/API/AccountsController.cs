using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.Service;
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
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        public AccountsController(
            ICathedralKitchenRepository cathedralKitchenRepository,
            CathedralKitchenContext ctx,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAccountService accountService,
            ILogger<AccountsController> logger)
        {
            _ctx = ctx;
            _cathedralKitchenRepository = cathedralKitchenRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
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

                    var createdUser =  await _accountService.Login(loginModel);
                    if (createdUser.Email != null)
                    {
                        return Ok(createdUser);
                    }
                }

                return Ok(result.Errors);
            }
            return BadRequest();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            var user = await _accountService.Login(model);

            return Json(user);
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
