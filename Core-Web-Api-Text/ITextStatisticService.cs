using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Web_Api_Text
{
    public interface ITextStatisticService
    {
        void LoadString(string? textToLoad);
        int GetSentenceCount();
        int GetLineCount();
        int GetParagraphCount();
        int GetCharacterCount();
        Dictionary<string, int> GetTopTenWords();
    }
}
