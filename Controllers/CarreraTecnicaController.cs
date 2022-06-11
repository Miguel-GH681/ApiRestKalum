using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers{

    [ApiController]
    [Route("v1/KalumManagement/CarrerasTecnicas")]
    public class CarreraTecnicaController : ControllerBase{
               
        public CarreraTecnicaController(KalumDbContext _DbContext, ILogger<CarreraTecnicaController> _Logger){
            this.DbContext = _DbContext;
            this.Logger = _Logger;
        }

        private readonly KalumDbContext DbContext;
        private readonly ILogger<CarreraTecnicaController> Logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarreraTecnica>>> Get(){
            List<CarreraTecnica> carrerasTecnicas = null;
            Logger.LogDebug("Iniciando proceso de consulta de carreras tecnicas en la base de datos");
            carrerasTecnicas = await DbContext.CarreraTecnica.Include(c => c.Aspirantes).Include(c => c.Inscripciones).ToListAsync();
            if(carrerasTecnicas == null || carrerasTecnicas.Count == 0){
                Logger.LogWarning("No existe carraras tecnicas");
                return new NoContentResult();
            }
            Logger.LogInformation("Se ejecutó la petición de forma exitosa");
            return Ok(carrerasTecnicas);
        } 

        [HttpGet("{id}", Name = "GetCarreraTecnica")]
        public async Task<ActionResult<CarreraTecnica>> GetCarreraTecnica(string id){
            Logger.LogDebug("Iniciando el proceso de búsqueda con el id " + id);
            var carrera = await  DbContext.CarreraTecnica.Include(c => c.Aspirantes).FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carrera == null){
                Logger.LogWarning("No existe la carrera técnica con el id" + id);
                //return new NoContentResult();
                return NotFound();
            }
            Logger.LogInformation("Finalizando el proceso de búsqueda de forma exitosa");
            return Ok(carrera);
        }
    }
}
