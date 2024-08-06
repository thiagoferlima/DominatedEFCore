using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos {get; set;}
        public DbSet<Funcionario> Funcionarios {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const String strconnection="Data source=(localdb)\\mssqlocaldb; Initial Catalog=C002; Integrated Security = True;";
            optionsBuilder
                 .UseSqlServer(strconnection)
                 .EnableSensitiveDataLogging()
                 .LogTo(Console.WriteLine, LogLevel.Information)
        }
    }
}