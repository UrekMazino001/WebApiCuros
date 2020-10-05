﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerWebApiM3.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
