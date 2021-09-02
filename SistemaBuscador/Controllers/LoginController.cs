using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepositories _loginRepositories;

        public LoginController(ILoginRepositories loginRepositories)
        {
            _loginRepositories = loginRepositories;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)//En esta parte el código analiza si el usuario y password son validos, de serlo nos envían a un lado u otro si no (validador)
            {
                if (await _loginRepositories.UserExist(model.Usuario, model.Password))//Aquí esperamos el resultado de una función aislada en el "var repo = new LoginRepositories();
                {
                    _loginRepositories.SetSessionAndCookie(HttpContext);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El usuario o contraseña no es valido");
                }

            }
            return View("Index", model);
        }
    }
}
