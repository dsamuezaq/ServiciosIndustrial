using DaemonIndustialMolinera.Base.Tablas;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DaemonIndustialMolinera.Base
{
    public class TestContext : DbContext
    {
        private readonly string _connectionString;
        protected SqlConnection Connection;
        protected SqlConnection connection => Connection ?? (Connection = GetOpenConnection());

        public TestContext(DbContextOptions<TestContext> options)
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
        public SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(_connectionString);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }
        /// <summary>
        /// Table Tasks
        /// Creation :202004016
        /// </summary>
        public DbSet<FacturasApi> FacturasApiTabla { get; set; }
        public DbSet<Product> MaestroProductos { get; set; }

        public DbSet<ResumenPedidos> ResumenPedidos { get; set; }
        public DbSet<ResumenPedidos_API> ResumenPedidos_APIs { get; set; }

        public DbSet<Visita> Visitalocales { get; set; }
        public DbSet<ClienteNuevo> ClienteNuevos { get; set; }
        public DbSet<DevolucionFactura> DevolucionFacturas { get; set; }
        public DbSet<DevolucionFacturaAPI> DevolucionFacturaAPIs { get; set; }
        public IEnumerable<T> Query<T>(string query) where T : class
        {
            return connection.Query<T>(query);
        }


    }
}
