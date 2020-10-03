using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dojo_netcore.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace dojo_netcore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        FireBaseAccount Instancia = FireBaseAccount.Instancia;
        [HttpGet]
        public async Task<List<Usuario>> Get() {
            return await Instancia.GetUser();
        }

        [HttpPost]
        public async Task<String> Post(Usuario user) {
            return await Instancia.AddUser(user);
        }

        [HttpDelete]
        public async Task<String> Delete([FromQuery]String id) {
            return await Instancia.DeleteUser(id);
        }
    }
}