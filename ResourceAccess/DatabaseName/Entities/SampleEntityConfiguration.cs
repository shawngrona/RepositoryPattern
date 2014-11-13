using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace ResourceAccess.DatabaseName.Entities
{
    public partial class SampleEntityConfiguration : EntityTypeConfiguration<SampleEntity>
    {
        public SampleEntityConfiguration()
        {
            ToTable("SamplEntity");
            HasKey(key => key.theInt);
            Property(p => p.theInt).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(p => p.theString).HasMaxLength(128).IsRequired();
            MapToStoredProcedures();
        }



    }
}
