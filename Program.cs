using System;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DominandoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
          //EnsureCreatedAndDeleted();
          //GapDoEnsureCretead();
          HealthCheckBancoDeDados();
        }
        static void HealthCheckBancoDeDados()
        {
            using var db = new Curso.Data.ApplicationContext();
            var canConnect = db.Database.CanConnect();

            if(canConnect)
            {;
                Console.WriteLine("Posso me conectar");
            }
            else
            {
                Console.WriteLine("Não posso me conectar");
            }
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
