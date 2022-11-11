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
            db.CreateTableAsync<producto>().Wait();            
            db.CreateTableAsync<cliente>().Wait();            
            db.CreateTableAsync<encFactura>().Wait();            
            db.CreateTableAsync<detFac>().Wait();            

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
            return db.Table<categoria>().Where(cat=>cat.estadoCat=="Activo").ToListAsync(); 
        }

        /// <summary>
        /// Recupera todas las categorias INACTIVAS
        /// </summary>
        /// <returns></returns>
        public Task<List<categoria>> recuperarCategoriaDactvAsync()
        {
            return db.Table<categoria>().Where(cat => cat.estadoCat == "Inactivo").ToListAsync();
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

        /// <summary>
        /// Eliminar categoria
        /// </summary>
        /// <param name="Categoria"></param>
        /// <returns></returns>
        public Task<int> eliminiarCategoriaAsync(categoria Categoria)
        {
            return db.DeleteAsync(Categoria);
        }
        #endregion

        #region Empresa
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

        /// <summary>
        /// Eliminar empresa
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        public Task<int> eliminiarEmpresaAsync(empresa Empresa)
        {
            return db.DeleteAsync(Empresa);
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

        /// <summary>
        /// Eliminar proveedor
        /// </summary>
        /// <param name="Proveedor"></param>
        /// <returns></returns>
        public Task<int> eliminiarProveedorAsync(proveedor Proveedor)
        {
            return db.DeleteAsync(Proveedor);
        }
        #endregion

        #region Productos

        /// <summary>
        /// Crear Productos
        /// </summary>
        /// <param name="Producto"></param>
        /// <returns></returns>
        public Task<int> crearProductoAsync(producto Producto)
        {
            if (Producto.idProducto == 0)
            {

                return db.InsertAsync(Producto);

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// REcuperar productos activos
        /// </summary>
        /// <returns></returns>
        public Task<List<producto>> recuperarProductoACTVAsync()
        {
            return db.Table<producto>().Where(prod => prod.estadoProd == "Activo").ToListAsync();
        }

        /// <summary>
        /// REcueperar productos inactivos
        /// </summary>
        /// <returns></returns>
        public Task<List<producto>> recuperarProductoINACTVsync()
        {
            return db.Table<producto>().Where(prod => prod.estadoProd == "Inactivo").ToListAsync();
        }

        /// <summary>
        /// Recuperar productos por ID
        /// </summary>
        /// <param name="idProd"></param>
        /// <returns></returns>
        public Task<producto> recuperarProductoXidAsync(int idProd)
        {
            return db.Table<producto>().Where(prod => prod.idProducto == idProd).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Recuperar productos por nombre
        /// </summary>
        /// <param name="nomProd"></param>
        /// <returns></returns>
        public Task<producto> recuperarProductoXNombreAsync(string nomProd)
        {
            return db.Table<producto>().Where(prod => prod.nomProducto==nomProd).FirstOrDefaultAsync();
        }

        public Task<producto> recuperarProductoXCodigoAsync(string codProd)
        {
            return db.Table<producto>().Where(prod => prod.barCodeProd==codProd).FirstOrDefaultAsync();
        }

        /// <summary>
        /// EDitar productos
        /// </summary>
        /// <param name="Producto"></param>
        /// <returns></returns>
        public Task<int> editarProductoAsync(producto Producto)
        {
            if (Producto.idProducto != 0)
            {
                return db.UpdateAsync(Producto);
            }
            else
            {
                return db.InsertAsync(Producto);
            }
        }

        /// <summary>
        /// Eliminar producto
        /// </summary>
        /// <param name="Producto"></param>
        /// <returns></returns>
        public Task<int> eliminiarProductoAsync(producto Producto)
        {
            return db.DeleteAsync(Producto);
        }

       
        #endregion

        #region Cliente

        /// <summary>
        /// Crear cleintes
        /// </summary>
        /// <param name="Cliente"></param>
        /// <returns></returns>
        public Task<int> crearClienteAsync(cliente Cliente)
        {
            if (Cliente.idCliente == 0)
            {

                return db.InsertAsync(Cliente);

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Recuperar cleintes acivos
        /// </summary>
        /// <returns></returns>
        public Task<List<cliente>> recuperarClienteACTVAsync()
        {
            return db.Table<cliente>().Where(cli=>cli.estadoCliente == "Activo").ToListAsync();
        }

        /// <summary>
        /// Recuperar clientes inactivos
        /// </summary>
        /// <returns></returns>
        public Task<List<cliente>> recuperarClienteINACTVsync()
        {
            return db.Table<cliente>().Where(cli => cli.estadoCliente == "Inactivo").ToListAsync();
        }

        /// <summary>
        /// Recuperar cliente por Id
        /// </summary>
        /// <param name="idCli"></param>
        /// <returns></returns>
        public Task<cliente> recuperarClienteXidAsync(int idCli)
        {
            return db.Table<cliente>().Where(cli => cli.idCliente == idCli).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Recuperar clientes por Cedula
        /// </summary>
        /// <param name="cedCli"></param>
        /// <returns></returns>
        public Task<cliente> recuperarClienteXCedAsync(string cedCli)
        {
            return db.Table<cliente>().Where(cli => cli.cedCliente == cedCli).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Recuperar cliente por nombre
        /// </summary>
        /// <param name="nomCli"></param>
        /// <returns></returns>
        public Task<cliente> recuperarClienteXNomAsync(string nomCli)
        {
            return db.Table<cliente>().Where(cli => cli.nomCliente == nomCli).FirstOrDefaultAsync();

        }

        /// <summary>
        /// Editar clientes
        /// </summary>
        /// <param name="Cliente"></param>
        /// <returns></returns>
        public Task<int> editarClienteAsync(cliente Cliente)
        {
            if (Cliente.idCliente != 0)
            {
                return db.UpdateAsync(Cliente);
            }
            else
            {
                return db.InsertAsync(Cliente);
            }
        }

        /// <summary>
        /// Eliminar cliente
        /// </summary>
        /// <param name="Cliente"></param>
        /// <returns></returns>
        public Task<int> eliminiarClienteAsync(cliente Cliente)
        {
            return db.DeleteAsync(Cliente);
        }

        #endregion

        #region Facturas

        /// <summary>
        /// Crear encabezado de la factura
        /// </summary>
        /// <param name="encanbezado"></param>
        /// <returns></returns>

        public Task<int> crearEncFActuraeAsync(encFactura encanbezado)
        {
            if (encanbezado.idEnc == 0)
            {

                return db.InsertAsync(encanbezado);

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Recuperar encabezado por numero
        /// </summary>
        /// <param name="nFAc"></param>
        /// <returns></returns>
        public Task<encFactura> recuperarEcnabezadoXnumFAcAsync(string nFAc)
        {
            return db.Table<encFactura>().Where(fac => fac.numFac == nFAc).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Recupera todos las facturas NO PAGADAS
        /// </summary>
        /// <returns></returns>
        public Task<List<encFactura>> recuperarFacturasAbiertassync()
        {
            return db.Table<encFactura>().Where(fac => fac.estadoEnc == "Abierta").ToListAsync();
        }

        /// <summary>
        /// REcuperar facturas PAGADAS
        /// </summary>
        /// <returns></returns>
        public Task<List<encFactura>> recuperarFacturasPagadaAssync()
        {
            return db.Table<encFactura>().Where(fac => fac.estadoEnc == "Pagada").ToListAsync();
        }

        public Task<int> editarEncabezadoAsync(encFactura encabezado)
        {
            if (encabezado.idEnc != 0)
            {
                return db.UpdateAsync(encabezado);
            }
            else
            {
                return db.InsertAsync(encabezado);
            }
        }


        /// <summary>
        /// Ingresar Productos al carrito
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public Task<int> facturarProductoAsync(detFac detalle)
        {
            if (detalle.idDet == 0)
            {

                return db.InsertAsync(detalle);

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Recuperar productos del carrito por NF
        /// </summary>
        /// <param name="numF"></param>
        /// <returns></returns>
        public Task<List<detFac>> recuperarDetallesAsync(string numF)
        {
            return db.Table<detFac>().Where(fac => fac.numFac == numF).ToListAsync();
        }
        /// <summary>
        /// Recupera los productos por ID y NF
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nF"></param>
        /// <returns></returns>

        public Task<detFac> recuperarDetalleXIdAsync(int id, string nF)
        {
            return db.Table<detFac>().Where(det => det.idDet==id && det.numFac==nF).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Editar productos del carrito
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public Task<int> editarDetallesAsync(detFac detalle)
        {
            if (detalle.idDet != 0)
            {
                return db.UpdateAsync(detalle);
            }
            else
            {
                return db.InsertAsync(detalle);
            }
        }

        /// <summary>
        /// Eliminar Carrito en CASO DE NO VENTA
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public Task<int> eliminiarCarritoAsync(detFac detalle)
        {
            return db.DeleteAsync(detalle);
        }


        /// <summary>
        /// Eliminar el encabezado en CASO DE NO VENTA
        /// </summary>
        /// <param name="encabezado"></param>
        /// <returns></returns>
        public Task<int> eliminiarEncabezadoAsync(encFactura encabezado)
        {
            return db.DeleteAsync(encabezado);
        }


        #endregion
    }
}
