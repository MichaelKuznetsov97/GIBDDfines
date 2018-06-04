using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GIBDDfines.Models;
using Microsoft.AspNetCore.Identity;

namespace GIBDDfines.Controllers
{
    [Produces("application/json")]
    //[Route("api/Account")]
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly modeldbGIBDD2Context _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, modeldbGIBDD2Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            bool flag = false;
            _context.AutoOwners.Load();
            foreach (var t in _context.AutoOwners)
            {
                if (t.Number == model.Number) { flag = true; break; }
            }

            foreach(var t in _userManager.Users)
            {
                if(t.Number == model.Number) { flag = false; break; }
            }

            if (ModelState.IsValid && flag)
            {

                User user = new User { Email = model.Email, UserName = model.Email, nameSurname = model.UserName, Number = model.Number };
                // Добавление нового пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, "user");
                    var msg = new
                    {
                        message = "Добавлен новый пользователь: " + user.nameSurname                  
                    };
                    return Ok(msg);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    var errorMsg = new
                    {
                        message = "Пользователь не добавлен.",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Ok(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные. Возможно введен неверный номер ВУ или пользователь с таким ВУ уже существует",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er =>
                    er.ErrorMessage))
                };
                return Ok(errorMsg);
            }
        }

        //Вход в учетку
        [HttpPost]
        [Route("api/Account/Login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                User user = await _userManager.FindByEmailAsync(model.Email);
                if (result.Succeeded)
                {  
                    var msg = new
                    {
                        message = "Выполнен вход пользователем: " + user.nameSurname,
                        message1 = "Вы вошли как: "+ model.Email,
                        enter = true
                    };
                    return Ok(msg);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    var errorMsg = new
                    {
                        message = "Вход не выполнен.",
                        enter = false,
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Ok(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен.",
                    enter = false,
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er =>
                    er.ErrorMessage))
                };
                return Ok(errorMsg);
            }
        }

        //Выход из учетки
        [HttpPost]
        [Route("api/Account/LogOff")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // Удаление куки
            await _signInManager.SignOutAsync();

            var msg = new
            {
                message = "Вы гость."
            };
            return Ok(msg);
        }

        [HttpPost]
        [Route("api/Account/isAuthenticated")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogisAuthenticatedOff()
        {
            User usr = await GetCurrentUserAsync();

            var message = usr == null ? "Вы Гость." : "Вы вошли как: " + usr.nameSurname;
            bool guest = usr == null ? true : false;
            var msg = new
            {
                message,
                guest
            };
            return Ok(msg);
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}