using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
          //HealthCheckBancoDeDados();
          new Curso.Data.ApplicationContext().Departamentos.AsNoTracking().Any();
          _count=0;
          GerenciadorEstadoDaConexao(false);
          _count=0;
          GerenciadorEstadoDaConexao(true);
        }
        static void ExecuteSQL()
        {
           
            using var db = new Curso.Data.ApplicationContext();
            //Primeira opcao
            using(var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT 1"; 
                cmd.ExecuteNonQuery();
            }
            //segunda opcao
            var descricao = "Teste";
            db.Database.ExecuteSqlRaw("update departamentos set descricao={0} where id=1", descricao);
           
           //Terceira opcao
            db.Database.ExecuteSqlInterpolated($"update departamentos set descricao={0} where id=1");
           
            

            

            

        }

        static int _count;
        static void GerenciadorEstadoDaConexao(bool GerenciadorEstadoDaConexao)
        {
            using var db = new Curso.Data.ApplicationContext();
            var time = System.Diagnostics.Stopwatch.StartNew();

            var conexao = db.Database.GetDbConnection();
            conexao.StateChange += (_, __) => ++ _count;
            if(GerenciadorEstadoDaConexao)
            {
                conexao.Open();
            }
            for(var i = 0; i < 200; i++)
            {
                db.Departamentos.AsTracking().Any();
            }
            time.Stop();
            var menssagem = $"tempo: {time.Elapsed.ToString()}, {GerenciadorEstadoDaConexao}, Contador: {_count}";

            System.Console.WriteLine(menssagem);
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
