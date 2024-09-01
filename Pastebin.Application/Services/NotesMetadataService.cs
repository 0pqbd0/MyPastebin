using Pastebin.Core.Abstractions;
using Pastebin.Core.Models;
using Pastebin.DataAccess.Repositories;
using Pastebin.Application.Utils;

namespace Pastebin.Application.Services
{
    public class NotesMetadataService : INotesMetadataService
    {
        private readonly INotesMetadataRepository _notesMetadataRepository;
        private readonly IS3Repository _s3Repository;

        public NotesMetadataService(INotesMetadataRepository notesMetadataRepository, IS3Repository s3Repository)
        {
            _notesMetadataRepository = notesMetadataRepository;
            _s3Repository = s3Repository;
        }

        public async Task<string> CreateAsync(string text, DateTime expirationTime)
        {
            var id = Guid.NewGuid();
            var fileName = $"{id}.txt";
            var noteUrl = await _s3Repository.SaveTextAsync(text, fileName);

            var noteMetadata = NoteMetadata.Create(id, Hash.GeneratedHash(id.ToString()), noteUrl, expirationTime);

            await _notesMetadataRepository.CreateAsync(noteMetadata.Note);

            return $"https://MyPastebin.com/notes/{noteMetadata.Note.NoteTitle}";
        }

        public async Task<NoteMetadata?> GetByHashAsync(string hash)
        {
            return await _notesMetadataRepository.GetByHashAsync(hash);
        }

        public async Task<NoteMetadata?> GetByIdAsync(Guid id)
        {
            return await _notesMetadataRepository.GetByIdAsync(id);
        }

        public async Task<string> GetTextByHashAsync(string hash)
        {
            var noteMetadata = await _notesMetadataRepository.GetByHashAsync(hash);

            var fileName = noteMetadata.NoteUrl.Split('/').Last();
            return await _s3Repository.GetTextAsync(fileName);
        }

        public async Task<List<Guid>> GetExpiredNotesAsync(DateTime currentDate)
        {
            return await _notesMetadataRepository.GetExpiredNotesAsync(currentDate.ToUniversalTime());
        }

        public async Task<Guid> UpdateAsync(string hash, string text, DateTime expirationTime)
        {
            var note = await _notesMetadataRepository.GetByHashAsync(hash);
            await _s3Repository.SaveTextAsync(text, $"{note.Id}.txt");
            return await _notesMetadataRepository.UpdateAsync(note.Id, expirationTime);
        }

        public async Task<Guid> DeleteAsync(string hash)
        {
            var note = await _notesMetadataRepository.GetByHashAsync(hash);
            await _s3Repository.DeleteTextAsync($"{note.Id}.txt");
            return await _notesMetadataRepository.DeleteAsync(note.Id);
        }

        public async Task DeleteExpiredNotesAsync()
        {
            var expiriedNotesIdList = await _notesMetadataRepository.GetExpiredNotesAsync(DateTime.Now.ToUniversalTime());

            foreach (var noteId in  expiriedNotesIdList)
            {
                await _s3Repository.DeleteTextAsync($"{noteId}.txt");
                _notesMetadataRepository.DeleteAsync(noteId);
            }
        }

    }
}
