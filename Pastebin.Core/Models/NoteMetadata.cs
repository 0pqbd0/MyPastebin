namespace Pastebin.Core.Models
{
    public class NoteMetadata
    {
        public const int MAX_TITLE_LENGTH = 250;
        private NoteMetadata(Guid id, string noteTitle, string noteUrl, DateTime expirationDate)
        {
            Id = id;
            NoteTitle = noteTitle;
            NoteUrl = noteUrl;
            ExpirationTime = expirationDate;

        }

        public Guid Id { get; }

        public string NoteTitle { get; } = string.Empty;

        public string NoteUrl { get; }

        public DateTime ExpirationTime { get; }

        public static (NoteMetadata Note, string Error) Create(Guid id, string noteTitle, string noteUrl, DateTime expirationDate)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(noteTitle) || noteTitle.Length > MAX_TITLE_LENGTH)
            {
                error = "Title cannot be empty or  longer than 250 symbols";
            }

            var noteMetadat = new NoteMetadata(id, noteTitle, noteUrl, expirationDate);

            return (noteMetadat, error);
        }
    }
}
