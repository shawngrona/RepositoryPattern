using System.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;



namespace ResourceAccess.DatabaseName.Context
{
    public partial class DBConfiguration : DbConfiguration
    {

        public DBConfiguration()
        {
            this.SetDefaultConnectionFactory(new SqlConnectionFactory("<DB Connection String>"));
        }
    }
}
