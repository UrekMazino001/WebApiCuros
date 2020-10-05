using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimerWebApiM3.Entities;
using PrimerWebApiM3.Services;

namespace PrimerWebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutoresRepository repository;

        public AutorController(IAutoresRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<Autor> GetAutorById(int id)
        {
            var autor = repository.GetAutorById(id);

            if(autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> Get()
        {
            return await repository.GetAutores();
        }
    }
}