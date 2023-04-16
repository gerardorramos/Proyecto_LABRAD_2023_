using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ProyPRAD.Models;
using System.Threading.Tasks;

namespace ProyPRAD.Controllers
{
    public class DateBase
    {
        public static SQLiteAsyncConnection dbconexion;
        public static void Conexion(string dbpath)
        {
            dbconexion = new SQLiteAsyncConnection(dbpath);
            dbconexion.CreateTableAsync<Contacts>();
            dbconexion.CreateTableAsync<Sites>();
        }
        public static Task<List<Contacts>> ObtenerListaContactos()
        {
            return dbconexion.Table<Contacts>().ToListAsync();
        }
        public static Task<int> AddContacto(Contacts contacto)
        {
            if (contacto.Id != 0)
            {
                return dbconexion.UpdateAsync(contacto);
            }
            else
            {
                return dbconexion.InsertAsync(contacto);
            }
        }
        public static Task<Contacts> ObtenerContacto(int pid)
        {
            return dbconexion.Table<Contacts>()
                .Where(i => i.Id == pid)
                .FirstOrDefaultAsync();
        }
        public static Task<int> DelContacto(Contacts contacto)
        {
            return dbconexion.DeleteAsync(contacto);
        }
        public static Task<List<Sites>> ObtenerListaSitios()
        {
            return dbconexion.Table<Sites>().ToListAsync();
        }
        public static Task<int> AddSitio (Sites sitio)
        {
            if (sitio.Id != 0)
            {
                return dbconexion.UpdateAsync(sitio);

            }
            else
            {
                return dbconexion.InsertAsync(sitio);
            }
        }
        public static Task<Sites> ObtenerSitio(int pid)
        {
            return dbconexion.Table<Sites>().Where(i=>i.Id==pid).FirstOrDefaultAsync();
        }
        public static Task<int> DelSitio(Sites sitio)
        {
            return dbconexion.DeleteAsync(sitio);
        }

        
    }
}

