using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Cashier.Models
{
    public class categoria
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [MaxLength(32)]
        public string nomCat { get; set; }
        [MaxLength(128)]
        public string descCat { get; set; }
        [MaxLength(16)]
        public string estadoCat { get; set; }
    }
}
