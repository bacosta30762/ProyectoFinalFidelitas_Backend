﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Resena
    {
        [Key]
        public int Id { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int BoletinId { get; set; }
        public Boletin Boletin { get; set; }
    }
}
