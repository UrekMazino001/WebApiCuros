using PrimerWebApiM3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerWebApiM3.Services
{
    public interface IAutoresRepository
    {
        Task<List<Autor>> GetAutores();
        Autor GetAutorById(int autorId);
    }
}
