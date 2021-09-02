using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
   public interface ILoginRepositories
    {
        Task<bool> UserExist(string usuario, string password);

        void SetSessionAndCookie(HttpContext context);
    }
}
