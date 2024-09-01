using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pastebin.DataAccess.Entites
{
    public class NoteMetadataEntity
    {
        public Guid Id { get; set; }

        public string NoteTitle { get; set; } = string.Empty;

        public string NoteUrl { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
