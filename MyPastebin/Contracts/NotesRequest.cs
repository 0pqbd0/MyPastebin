namespace MyPastebin.Contracts
{
    public record NotesRequest(
        string Text,
        DateTime ExpirationTime);
}
