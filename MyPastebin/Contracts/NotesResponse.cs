namespace MyPastebin.Contracts
{
    public record NotesResponse(
        Guid Id,
        string NoteTitle,
        string NoteUrl,
        DateTime ExpirationTime);
}
