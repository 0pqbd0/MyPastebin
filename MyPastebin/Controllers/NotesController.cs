using Microsoft.AspNetCore.Mvc;
using MyPastebin.Contracts;
using Pastebin.Application.Services;

namespace MyPastebin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INotesMetadataService _notesMetadataService;

        public NotesController(INotesMetadataService notesMetadataService)
        {
            _notesMetadataService = notesMetadataService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateNoteMetadata([FromBody] NotesRequest request)
        {
            var url = await _notesMetadataService.CreateAsync(request.Text, request.ExpirationTime.ToUniversalTime());
            return Ok(new { url });
        }

        [HttpGet("{hash}")]
        public async Task<ActionResult<string>> GetNoteMetadataByHash(string hash)
        {
            var text = await _notesMetadataService.GetTextByHashAsync(hash);
            return Ok(text);
        }

        [HttpPut("{hash}")]
        public async Task<ActionResult> UpdateNoteMetadata(string hash, [FromBody] NotesRequest request)
        {
            await _notesMetadataService.UpdateAsync(hash, request.Text, request.ExpirationTime.ToUniversalTime());
            return Ok();
        }

        [HttpDelete("{hash}")]
        public async Task<ActionResult> DeleteNoteMetadata(string hash)
        {
            var result = await _notesMetadataService.DeleteAsync(hash);
            return Ok();
        }
    }
}
