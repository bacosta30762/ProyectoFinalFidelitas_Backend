using Aplicacion.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class InventarioController : Controller
    {
        
            [ApiController]
            [Route("api/[controller]")]
            public class InventarioController : ControllerBase
        {
            private readonly IInventarioService _inventarioService;

            public InventarioController(IInventarioService inventarioService)
            {
                _inventarioService = inventarioService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var inventarios = await _inventarioService.GetAllAsync();
                return Ok(inventarios);
            }
        }

       
        }
    }

