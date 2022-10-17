using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Cashier.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Cashier.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db=new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<categoria>().Wait();

        }
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
    }
}
