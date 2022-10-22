using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cashier.Models
{
    public class empresa
    {
        [PrimaryKey,AutoIncrement]
        public int idEmpresa { get; set; }
        [MaxLength(64)]
        public string nomEmpresa { get; set; }
        [MaxLength(128)]
        public string dirEmpresa { get; set; }
        [MaxLength(10)]
        public string teflEmpresa { get; set; }
        [MaxLength(16)]
        public string estadoEmpresa { get; set; }
       
    }
}
