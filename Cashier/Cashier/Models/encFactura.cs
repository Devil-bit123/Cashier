using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cashier.Models
{
    public class encFactura
    {
        [PrimaryKey,AutoIncrement]
        public int idEnc { get; set; }
        public string numFac { get; set; }
        public string fechaEnc { get; set; }
        public string nomCli { get; set; }
        public string apeCli { get; set; }
        public string cedCli { get; set; }
        public string dirCliente { get; set; }
        public string telfCliente { get; set; }        
        public string estadoEnc { get; set; }

    }
}
