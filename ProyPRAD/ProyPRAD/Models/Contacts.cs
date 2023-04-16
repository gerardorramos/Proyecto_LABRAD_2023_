using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProyPRAD.Models
{
    public class Contacts
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Nombres { get; set; }
        [MaxLength(20)]
        public string Apellidos { get; set; }
        public double Telefono { get; set; }
        public double Edad { get; set; }
        public string Pais { get; set; }
        public byte[] Foto { get; set; }
        public string Nota { get; set; }
    }
}
