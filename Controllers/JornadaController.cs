using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers{
    [ApiController]
    [Route("v1/KalumManagement/Jornadas")]
    public class JornadaController : ControllerBase{
        public JornadaController(KalumDbContext _DbContext, ILogger<JornadaController> _Logger){
            this.DbContext = _DbContext;
            this.Logger = _Logger;
        }
        private readonly KalumDbContext DbContext;
        private readonly ILogger<JornadaController> Logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jornada>>> Get(){
            List<Jornada> jornadas = null;
            jornadas = await DbContext.Jornada.Include(j => j.Aspirantes).ToListAsync();
            if(jornadas == null || jornadas.Count == 0){
                return new NoContentResult();
            } else{
                return Ok(jornadas);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Jornada>> GetJornada(string id){
            Jornada jornada = null;
            jornada = await DbContext.Jornada.Include(j => j.Aspirantes).FirstOrDefaultAsync(j => j.JornadaId == id);
            if(jornada == null){
                return NotFound();
            } else{
                return Ok(jornada);
            }
        }
    }
}