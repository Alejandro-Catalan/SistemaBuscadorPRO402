﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBuscador.Entities;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Test.PruebasUnitarias.Servicios
{
    [TestClass]
    public class LoginRepositoriesEFTest : TestBase
    {
        [TestMethod]
        public async Task UsuarioNoExiste()
        {
            //Preparación
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContext(nombreBd);
            context.Usuarios.Add(new Usuario() { NombreUsuario = "Usuario1", Password = "Password1" });
            await context.SaveChangesAsync();

            var context2 = BuildContext(nombreBd);

            //Ejecución
            var nombreUsuario = "Usuario2";
            var password = "password2";
            var repo = new LoginRepositoriesEF(context2);
            var respuesta = await repo.UserExist(nombreUsuario, password);

            //Verificación

            Assert.IsFalse(respuesta);
        }

        [TestMethod]

        public async Task UsuarioExiste()
        {
            //Preparación
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContext(nombreBd);
            context.Usuarios.Add(new Usuario() { NombreUsuario = "Usuario1", Password = "Password1" });
            await context.SaveChangesAsync();

            var context2 = BuildContext(nombreBd);

            //Ejecución
            var nombreUsuario = "Usuario1";
            var password = "Password1";
            var repo = new LoginRepositoriesEF(context2);
            var respuesta = await repo.UserExist(nombreUsuario, password);

            //Verificación

            Assert.IsTrue(respuesta);
        }
    }
}
