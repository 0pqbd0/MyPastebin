using Microsoft.EntityFrameworkCore;
using Pastebin.DataAccess.Entites;

namespace Pastebin.DataAccess
{
    public class PastebinDbContext : DbContext
    {
        public PastebinDbContext(DbContextOptions<PastebinDbContext> options)
        : base(options)
        {
        }

        public DbSet<NoteMetadataEntity> NotesMetadata { get; set; }
    }
}
