using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cashier.Models
{
    public class detFac
    {
        [PrimaryKey,AutoIncrement]
        public int idDet { get; set; }
        public string numFac { get; set; }    
        public string nomProducto { get; set; }
        public decimal preVentaProducto { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }       
    }
}
