using PrimerWebApiM3.Entities;
using PrimerWebApiM3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPruebasDeIntegracion.Mocks
{
    public class AutoresRepositoryMock : IAutoresRepository
    {
        Autor IAutoresRepository.GetAutorById(int autorId)
        {
            if(autorId == 0)
            {
                return null;
            }

            return new Autor() { Id = autorId, Nombre = "Trafalgar Law", Libros = null };
        }

        Task<List<Autor>> IAutoresRepository.GetAutores()
        {
            throw new NotImplementedException();
        }
    }
}
