using Microsoft.EntityFrameworkCore;
using Pastebin.Core.Models;
using Pastebin.DataAccess.Entites;

namespace Pastebin.DataAccess.Repositories
{
    public class NotesMetadataRepository : INotesMetadataRepository
    {
        private readonly PastebinDbContext _context;
        public NotesMetadataRepository(PastebinDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(NoteMetadata note)
        {
            var noteMetadataEntity = new NoteMetadataEntity
            {
                Id = note.Id,
                NoteTitle = note.NoteTitle,
                NoteUrl = note.NoteUrl,
                ExpirationTime = note.ExpirationTime,
            };

            await _context.AddAsync(noteMetadataEntity);
            await _context.SaveChangesAsync();
            return noteMetadataEntity.Id;
        }

        public async Task<NoteMetadata?> GetByIdAsync(Guid id)
        {
            var noteMetadataEntity = await _context.NotesMetadata.FindAsync(id);

            return noteMetadataEntity != null ? NoteMetadata.Create(noteMetadataEntity.Id, noteMetadataEntity.NoteTitle,
                noteMetadataEntity.NoteUrl, noteMetadataEntity.ExpirationTime).Note : null;
        }

        public async Task<NoteMetadata?> GetByHashAsync(string hash)
        {
            var noteMetadataEntity = await _context.NotesMetadata
        .FirstOrDefaultAsync(b => b.NoteTitle == hash);

            return noteMetadataEntity != null ? NoteMetadata.Create(noteMetadataEntity.Id, noteMetadataEntity.NoteTitle,
                noteMetadataEntity.NoteUrl, noteMetadataEntity.ExpirationTime).Note : null;
        }

        public async Task<Guid> UpdateAsync(Guid id, DateTime expirationTime)
        {
            await _context.NotesMetadata
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.ExpirationTime, expirationTime));
            return id;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _context.NotesMetadata.Where(b => b.Id == id).ExecuteDeleteAsync();
            return id;
        }

        public async Task<List<Guid>> GetExpiredNotesAsync(DateTime currentDate)
        {
            return await _context.NotesMetadata.Where(b => b.ExpirationTime <= currentDate).Select(b => b.Id).ToListAsync();
        }
    }
}
