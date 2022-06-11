using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers{
    
    [ApiController]
    [Route("v1/KalumManagement/ExamenesAdmision")]
    public class ExamenAdmisionController : ControllerBase{
        
        public ExamenAdmisionController(KalumDbContext _DbContext){
            this.DbContext = _DbContext;
        }
        private readonly KalumDbContext DbContext;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamenAdmision>>> Get(){
            List<ExamenAdmision> examenes = null;

            examenes = await DbContext.ExamenAdmision.Include(ea => ea.Aspirantes).ToListAsync();
            if(examenes == null || examenes.Count == 0){
                return new NoContentResult();
            }else{
                return Ok(examenes);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamenAdmision>> GetExamenAdmision(string id){
            ExamenAdmision examen = null;

            examen = await DbContext.ExamenAdmision.FirstOrDefaultAsync(ea => ea.ExamenId == id);
            if(examen == null){
                return NotFound();
            } else {
                return Ok(examen);
            }
        }
    }
}