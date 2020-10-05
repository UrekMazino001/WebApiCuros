using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerWebApiM3.Models
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<LibroDTO> Libros { get; set; }
    }
}
