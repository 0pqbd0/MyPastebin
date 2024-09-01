using Pastebin.Core.Models;

namespace Pastebin.DataAccess.Repositories
{
    public interface INotesMetadataRepository
    {
        Task<Guid> CreateAsync(NoteMetadata note);
        Task<Guid> DeleteAsync(Guid id);
        Task<NoteMetadata?> GetByIdAsync(Guid id);
        Task<NoteMetadata?> GetByHashAsync(string hash);
        Task<List<Guid>> GetExpiredNotesAsync(DateTime currentDate);
        Task<Guid> UpdateAsync(Guid id, DateTime expirationTime);
    }
}