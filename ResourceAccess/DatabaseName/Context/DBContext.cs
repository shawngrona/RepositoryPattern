using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourceAccess.DatabaseName.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data.SqlClient;

namespace ResourceAccess.DatabaseName.Context
{


    [DbConfigurationType(typeof(DBConfiguration))]
    public partial class DBContext : System.Data.Entity.DbContext
    {

        public DBContext()
        {

        }

        public DBContext(string connectionString)
            : base(connectionString)
        {

        }

        //needed for unit testing with effort - passing in our dbconnection to in memory db
        public DBContext(DbConnection conn)
            : base(conn, true)
        { }

        public virtual DbSet<SampleEntity> SampleDBSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (!(Database.Connection is SqlConnection)) return;

            //add table configurations
            modelBuilder.Configurations.Add(new SampleEntityConfiguration());

        }
    }
}
