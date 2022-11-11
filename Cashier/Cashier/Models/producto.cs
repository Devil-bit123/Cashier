using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cashier.Models
{
    public class producto
    {
        [PrimaryKey,AutoIncrement]
        public int idProducto { get; set; }
        public string nomProducto  { get; set; }
        public string descProducto { get; set; }
        public decimal preCompraProd { get; set; }
        public decimal preVentaProd { get; set; }
        public int existProd { get; set; }
        public string idCategoria { get; set; }
        public string  idProveedor { get; set; }
        public string barCodeProd { get; set; }
        public string imageProd { get; set; }
        public string  estadoProd { get; set; }
        
    }
}
