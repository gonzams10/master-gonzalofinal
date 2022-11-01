using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repositorio;

namespace WebApplication2.Controllers
{
    public class ConfigController
    {
        public string connectionstring { get; set; }
        public ConfigController(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

    }
}
