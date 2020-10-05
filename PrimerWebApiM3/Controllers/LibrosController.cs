using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerWebApiM3.Context;
using PrimerWebApiM3.Entities;

namespace PrimerWebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.Include(x => x.Autor).ToList();
        }

        [HttpGet("{id}", Name ="getLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);

            if (libro == null)
            {
                return BadRequest();
            }
            return libro ;
        }

        [HttpPost]
        public ActionResult<Libro> Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();

            return new CreatedAtRouteResult("getLibro", new { id = libro.Id }, libro);
        }

        [HttpPut("{id}")]
        public ActionResult<Autor> Put([FromBody] Libro libro, int id)
        {

            if (id != libro.Id)
            {
                return BadRequest();
            }

            context.Entry(libro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var libro = context.Libros.FirstOrDefault(x => x.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            context.Libros.Remove(libro);
            context.SaveChanges();
            return Ok(libro);

        }

    }
}