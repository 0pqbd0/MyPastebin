using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pastebin.DataAccess.Entites;
using Pastebin.Core.Models;

namespace Pastebin.DataAccess.Configuration
{
    public class NoteMetadataConfiguration : IEntityTypeConfiguration<NoteMetadataEntity>
    {
        public void Configure(EntityTypeBuilder<NoteMetadataEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.NoteTitle)
                .IsRequired();

            builder.Property(b => b.NoteUrl)
                .HasMaxLength(NoteMetadata.MAX_TITLE_LENGTH)
                .IsRequired();
        }
    }
}
