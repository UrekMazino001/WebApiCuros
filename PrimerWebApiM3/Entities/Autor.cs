using PrimerWebApiM3.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerWebApiM3.Entities
{
    public class Autor
    {
        public int Id { get; set; }

        [PrimeraLetraMayuscula]
        [Required(ErrorMessage = "Campo es requerido")]
        public string Nombre { get; set; }
        public List<Libro> Libros { get; set; }

    }
}
