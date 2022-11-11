using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cashier.Models
{
    public class cliente
    {
        [PrimaryKey,AutoIncrement]
        public int idCliente { get; set; }
        public string nomCliente { get; set; }
        public string apeCliente { get; set; }
        public string cedCliente { get; set; }
        public string dirCliente{ get; set; }
        public string telfCliente { get; set; }
        public string estadoCliente { get; set; }
    }
}
