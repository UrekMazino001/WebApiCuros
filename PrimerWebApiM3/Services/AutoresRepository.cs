using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimerWebApiM3.Context;
using PrimerWebApiM3.Entities;

namespace PrimerWebApiM3.Services
{
    public class AutoresRepository : IAutoresRepository
    {
        private readonly ApplicationDbContext context;

        public AutoresRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        Autor IAutoresRepository.GetAutorById(int autorId)
        {
            return context.Autores.Find(autorId);
        }

        async Task<List<Autor>> IAutoresRepository.GetAutores()
        {
            return await context.Autores.ToListAsync();
        }
    }
}
