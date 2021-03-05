using DaemonIndustialMolinera.Base.Tablas;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaemonIndustialMolinera.baseChariot
{
    public class EngineV2Context : DbContext
    {
        private readonly string _connectionString;
        protected SqlConnection Connection;
        protected SqlConnection connection => Connection ?? (Connection = GetOpenConnection());

        public EngineV2Context(DbContextOptions<EngineV2Context> options)
                  : base(options)
        {
            _connectionString = options.FindExtension<SqlServerOptionsExtension>().ConnectionString;


            //extension.ConnectionString = connectionString;
        }
        public SqlConnection GetOpenConnection(bool mars = false)
        {

            var cs = _connectionString;
            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }
            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }
        /// <summary>
        /// Table Tasks
        /// Creation :202004016
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        public DbSet<VistaDetallePedidos> VistaDetallePedidos { get; set; }

        public DbSet<VistaVisitas> VistaVisitalocales { get; set; }

        public DbSet<VisitaUIO> VisitaUIOs { get; set; }
        public DbSet<Locales> Branchess { get; set; }
        public DbSet<VistaLocalesNuevos> VistaLocalesNuevos { get; set; }

        public DbSet<VistaDevolucion> VistaDevoluciones { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }

        
        internal void LocaleNUevo()
        {
            throw new NotImplementedException();
        }

        //  public DbSet<VistaLocalesNuevos> Branchess 


        //public SqlConnection GetClosedConnection()
        //{
        //    var conn = new SqlConnection(_connectionString);
        //    if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");

        //}


    }
}
