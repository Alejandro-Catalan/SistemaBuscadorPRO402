using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBuscador.Controllers;
using SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Test.PruebasUnitarias.Controladores
{

    [TestClass]
    public class LoginControllerTest
    {
        
        [TestMethod]
        public async Task LoginModeloInvalido()
        {
            //Preparación
            var loginRepositories = new LoginRepositoriesEFFalse();
            var model = new LoginViewModel() { Usuario = "", Password="" };
            //Ejecución
            var controller = new LoginController(loginRepositories);
            controller.ModelState.AddModelError(string.Empty, "Datos inválidos");
            var resultado = await controller.Login(model) as ViewResult;

            //Validación

            Assert.AreEqual(resultado.ViewName, "Index");
        }

        [TestMethod]
        public async Task LoginUsuarioNoExiste()
        {
            //Preparación
            var loginService = new LoginRepositoriesEFFalse();
            var model = new LoginViewModel() { Usuario = "Usuario1", Password = "Password1" };


            //Ejecución
            var controller = new LoginController(loginService);
            var resultado = await controller.Login(model) as ViewResult;


            //Validación
            Assert.AreEqual(resultado.ViewName, "Index");

        }

        [TestMethod]
        public async Task LoginUsuarioExiste()
        {
            //Preparación
            var loginService = new LoginRepositoriesEFTrue();
            var model = new LoginViewModel() { Usuario = "Usuario1", Password = "Password1" };


            //Ejecución
            var controller = new LoginController(loginService);
            var resultado = await controller.Login(model) as RedirectToActionResult;

            //Validación
            Assert.AreEqual(resultado.ActionName, "Index");
            Assert.AreEqual(resultado.ControllerName, "Home");



        }


    }
}
