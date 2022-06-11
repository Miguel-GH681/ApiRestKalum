using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers{
    [ApiController]
    [Route("v1/KalumManagement/Alumnos")]
    public class AlumnoController : ControllerBase{

        public AlumnoController(KalumDbContext _DbContext){
            this.DbContext = _DbContext;
        }

        private readonly KalumDbContext DbContext;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> Get(){
            List<Alumno> alumnos = null;
            alumnos = await DbContext.Alumno.ToListAsync();
            if(alumnos == null || alumnos.Count == 0){
                return new NoContentResult();
            } else{
                return Ok(alumnos);
            }
        }

        [HttpGet("{carne}")]
        public async Task<ActionResult<Alumno>> GetAlumno(string carne){
            Alumno alumno = null;
            alumno = await DbContext.Alumno.FirstOrDefaultAsync(a => a.Carne == carne);
            if(alumno == null){
                return NotFound();
            } else{
                return Ok(alumno);
            }
        }
    }
}