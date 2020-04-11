using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //conexion base de datos en el, json se completo

        }
        
        public DbSet<Owner> Owners { get; set; }  //se crea la tabla owners a partir del objeto owner  
        public DbSet<Contract> Contracts { get; set; }  //se crea la tabla owners a partir del objeto owner  
        public DbSet<Lessee> Lessees { get; set; }  //se crea la tabla owners a partir del objeto owner  
        public DbSet<Property>Properties { get; set; }  //se crea la tabla owners a partir del objeto owner  
        public DbSet<PropertyImage> PropertyImages { get; set; }  //se crea la tabla owners a partir del objeto owner  
        public DbSet<PropertyType> PropertyTypes { get; set; }  //se crea la tabla owners a partir del objeto owner  

    }
}
