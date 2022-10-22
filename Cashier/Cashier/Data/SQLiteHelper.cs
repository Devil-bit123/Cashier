using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Cashier.Models;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;
using SQLiteNetExtensionsAsync.Extensions;

namespace Cashier.Data
{
    public class SQLiteHelper
    {
        public SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db=new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<categoria>().Wait();
            db.CreateTableAsync<empresa>().Wait();
            db.CreateTableAsync<proveedor>().Wait();            

        }

        #region Categoria       

        /// <summary>
        /// Crea una catrgoria
        /// </summary>
        /// <param name="Categoria"></param>
        /// <returns></returns>
        public Task<int> crearCategoriaAsync(categoria Categoria)
        {
            if (Categoria.Id==0)
            {
                return db.InsertAsync(Categoria);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Recupera todas las categorias ACTIVAS
        /// </summary>
        /// <returns></returns>
        public Task<List<categoria>> recuperarCategoriaACTVAsync()
        {
            return db.Table<categoria>().Where(cat=>cat.estadoCat=="Activado").ToListAsync(); 
        }

        /// <summary>
        /// Recupera todas las categorias INACTIVAS
        /// </summary>
        /// <returns></returns>
        public Task<List<categoria>> recuperarCategoriaDactvAsync()
        {
            return db.Table<categoria>().Where(cat => cat.estadoCat == "Desactivado").ToListAsync();
        }


        /// <summary>
        /// Recuperar categorias por nombre
        /// </summary>
        /// <param name="nombreCat">el nombre de la categoria</param>
        /// <returns></returns>
        public Task<categoria> recuperarCategoriaXnombre(string nombreCat)
        {
            return db.Table<categoria>().Where(cat => cat.nomCat.Contains(nombreCat)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Recuperar categorais por ID
        /// </summary>
        /// <param name="idcat"></param>
        /// <returns></returns>
        public Task<categoria> recuperarCategoriaXid(int idcat)
        {
            return db.Table<categoria>().Where(cat => cat.Id==idcat).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Editar categorias
        /// </summary>
        /// <param name="Categoria"></param>
        /// <returns></returns>
        public Task<int> editarCategoriaAsync(categoria Categoria)
        {
            if (Categoria.Id != 0)
            {
                return db.UpdateAsync(Categoria);
            }
            else
            {
                return db.InsertAsync(Categoria);
            }
        }
        #endregion

        #region empresa
        /// <summary>
        /// Crear empresa
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        public Task<int> crearEmpresaAsync(empresa Empresa)
        {
            if (Empresa.idEmpresa== 0)
            {
                
                return db.InsertAsync(Empresa);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Recuperar Todas las Empresas Activas
        /// </summary>
        /// <returns></returns>
        public Task<List<empresa>> recuperarEmpresaACTVAsync()
        {
            return db.Table<empresa>().Where(emp => emp.estadoEmpresa == "Activo").ToListAsync();
        }
        /// <summary>
        /// Recuperar Empresas Inactivadas
        /// </summary>
        /// <returns></returns>
        public Task<List<empresa>> recuperarEmpresaDACTVAsync()
        {
            return db.Table<empresa>().Where(emp => emp.estadoEmpresa == "Inactivo").ToListAsync();
        }

        /// <summary>
        /// Recuperar las empresas por id
        /// </summary>
        /// <param name="idcat"></param>
        /// <returns></returns>
        public Task<empresa> recuperarEmpresaXid(int idEmp)
        {
            return db.Table<empresa>().Where(emp => emp.idEmpresa == idEmp).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Editar Empresas
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        public Task<int> editarEmpresaAsync(empresa Empresa)
        {
            if (Empresa.idEmpresa != 0)
            {
                return db.UpdateAsync(Empresa);
            }
            else
            {
                return db.InsertAsync(Empresa);
            }
        }
           

        #endregion

        #region Proveedores
        /// <summary>
        /// Crear Proveedores
        /// </summary>
        /// <param name="Proveedor"></param>
        /// <returns></returns>
        public Task<int> crearProveedorAsync(proveedor Proveedor)
        {
            if (Proveedor.idProveedor == 0)            {
               
                return db.InsertAsync(Proveedor);

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// REcuperar Proveedores Activos
        /// </summary>
        /// <returns></returns>
        public Task<List<proveedor>> recuperarProveedorACTVAsync()
        {
            return db.Table<proveedor>().Where(prov => prov.estadoProveedor == "Activo").ToListAsync();
        }

    


        /// <summary>
        /// REcuperar Proveedores Inactivos
        /// </summary>
        /// <returns></returns>
        public Task<List<proveedor>> recuperarProveedorDACTVAsync()
        {
            return db.Table<proveedor>().Where(prov => prov.estadoProveedor == "Inactivo" || prov.estadoProveedor == "Empresa Desactivada").ToListAsync();
        }

        /// <summary>
        /// REcuperar proveedores por id
        /// </summary>
        /// <param name="idProv"></param>
        /// <returns></returns>
        public Task<proveedor> recuperarProveedorXidAsync(int idProv)
        {
            return db.Table<proveedor>().Where(prov => prov.idProveedor==idProv).FirstOrDefaultAsync();
        }

 

        /// <summary>
        /// Editar Proveedores
        /// </summary>
        /// <param name="Proveedor"></param>
        /// <returns></returns>
        public Task<int> editarProveedoresAsync(proveedor Proveedor)
        {
            if (Proveedor.idProveedor != 0)
            {
                return db.UpdateAsync(Proveedor);
            }
            else
            {
                return db.InsertAsync(Proveedor);
            }
        }

        #endregion

    }
}
