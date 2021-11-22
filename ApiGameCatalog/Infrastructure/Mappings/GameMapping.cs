using ApiGameCatalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiGameCatalog.Infrastructure.Mappings
{
    public class GameMapping : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name);            
            builder.HasOne(p => p.Publisher).WithMany().HasForeignKey(fk => fk.PublisherId);
            builder.Property(p => p.Price);
        }
    }
}
