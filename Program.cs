using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DominandoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
          //EnsureCreatedAndDeleted();
          GapDoEnsureCretead();
        }
        static void EnsureCreatedAndDeleted()
        {
            using var db = new Curso.Data.ApplicationContext();
            //db.Database.EnsureCreated();
            db.Database.EnsureDeleted();
        }
        static void GapDoEnsureCretead()
        {
           using var db1 = new Curso.Data.ApplicationContext();
           using var db2 = new Curso.Data.ApplicationContextCIdade();

           db1.Database.EnsureCreated();
           db2.Database.EnsureCreated();

           var databaseCreator = db2.GetService<IRelationalDatabaseCreator>();
           databaseCreator.CreateTables();
        }
       
    }
}
