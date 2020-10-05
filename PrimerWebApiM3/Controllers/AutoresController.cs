using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PrimerWebApiM3.Context;
using PrimerWebApiM3.Entities;
using PrimerWebApiM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerWebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        ApplicationDbContext _context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AutoresController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }


        [HttpGet]
        //[HttpGet("listado")]    //Modifica la ruta del endpoint a api/autores/listado
        //[HttpGet("/listado")]  //sustituye la base de api/Autores a /listado
        public ActionResult<IEnumerable<AutorDTO>> Get(int cantidad =1, int pagina = 1)
        {
            var query = _context.Autores.AsQueryable();
            var autores = query.Include(x => x.Libros).Skip(cantidad * (pagina - 1)).Take(cantidad).ToList();
            var totalRegistros = query.Count();

            //Respuesatas enviadas a través del header
            Response.Headers["totalRegistros"] = totalRegistros.ToString();
            Response.Headers["totalPaginas"] = Math.Ceiling((double)(totalRegistros / cantidad)).ToString(); ;

            var autoresDTO = mapper.Map<List<AutorDTO>>(autores);
            return autoresDTO;
        }

        [HttpGet("{id}/{param2?}")]     //Párametro opcional ó /{param2}
        [HttpGet("{id}", Name = "getAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await _context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorDTO>(autor);
            return autorDTO;
        }


        [HttpPost]
        public ActionResult Post([FromBody] AutorCreacionDTO autorCreacion)
        {

            var autor = mapper.Map<Autor>(autorCreacion);
            _context.Autores.Add(autor);
            _context.SaveChanges();

            var autorDTO = mapper.Map<AutorDTO>(autor);

            return new CreatedAtRouteResult("getAutor", new { id = autor.Id }, autorDTO);
        }

        [HttpPut("{id}")]
        public ActionResult<Autor> Put([FromBody] AutorCreacionDTO autorEdicion, int id)
        {
            var autor = mapper.Map<Autor>(autorEdicion);
            autor.Id = id;

            _context.Entry(autor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var autorId = _context.Autores.Select(x => x.Id).FirstOrDefault(x => x == id);
            //var autor = _context.Autores.FirstOrDefault(x => x.Id == id);

            //if (autor == null)
            //{
            //    return NotFound();
            //}

            if (autorId == default)
            {
                return NotFound();
            }

            _context.Autores.Remove(new Autor { Id = autorId });
            _context.SaveChanges();
            return NoContent();

        }

        //Variables de sistema
        [HttpGet("variable")]
        public ActionResult<object> getVariable()
        {
            string lol = configuration["Test"];
            string lol2 = configuration["newTest"];

            return new { variable1 = configuration["Test"], variable2 = configuration["newTest"] } ;
        }
        
    }
}
