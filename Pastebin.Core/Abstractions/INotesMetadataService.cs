using Pastebin.Core.Models;

namespace Pastebin.Application.Services
{
    public interface INotesMetadataService
    {
        Task<string> CreateAsync(string text, DateTime expirationTime);
        Task<NoteMetadata?> GetByIdAsync(Guid id);
        Task<string> GetTextByHashAsync(string hash);
        Task<NoteMetadata?> GetByHashAsync(string hash);
        Task<List<Guid>> GetExpiredNotesAsync(DateTime currentDate);
        Task<Guid> UpdateAsync(string noteUrl, string text, DateTime expirationTime);
        Task<Guid> DeleteAsync(string hash);
        Task DeleteExpiredNotesAsync();
    }
}