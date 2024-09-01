using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pastebin.Core.Abstractions
{
    public interface IS3Repository
    {
        Task<string> SaveTextAsync(string text, string fileName);

        Task<string> GetTextAsync(string url);

        Task DeleteTextAsync(string key);
    }
}
