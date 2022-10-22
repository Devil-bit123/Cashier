using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Cashier.Models;
using System.Runtime.Serialization;
using SQLiteNetExtensions.Attributes;


namespace Cashier.Models
{
    public class proveedor
    {
        [PrimaryKey,AutoIncrement]
        public int idProveedor { get; set; }   
        public string nomProveedor { get; set; }    
        public string apeProveedor { get; set; }      
        public string cedProveedor { get; set; }      
        public string telfProveedor { get; set; }    
        public string  estadoProveedor { get; set; }       
        public string idEmpresa { get; set; }

    }
}
