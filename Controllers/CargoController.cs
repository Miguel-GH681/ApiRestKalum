using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers{
    [ApiController]
    [Route("v1/KalumManagement/Cargos")]
    public class CargoController : ControllerBase{
        public CargoController(KalumDbContext _DbContext){
            this.DbContext = _DbContext;
        }
        private readonly KalumDbContext DbContext;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> Get(){
            List<Cargo> cargos = null;
            cargos = await DbContext.Cargo.ToListAsync();
            if(cargos == null || cargos.Count == 0){
                return new NoContentResult();
            } else {
                return Ok(cargos);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(string id){
            Cargo cargo = null;
            cargo = await DbContext.Cargo.FirstOrDefaultAsync(c => c.CargoId == id);
            if(cargo == null){
                return NotFound();
            } else {
                return Ok(cargo);
            }
        }
    }
}

