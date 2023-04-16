using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProyPRAD.Models
{
    public class Sites
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nombre_sitio { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public byte[] Foto { get; set; }
        public string Pais { get; set; }

        [MaxLength(70)]
        public string Nota { get; set; }

    }
}
