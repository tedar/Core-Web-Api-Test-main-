using System.Text;
using Core_Web_Api_Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextStatsController : ControllerBase
    {
        /// <summary>
        /// Gets statistical details from a string provided
        /// </summary>
        /// <param name="requestedText">The text to be analyzed.</param>
        /// <returns>A set of statistics</returns>
        [HttpPost("GetTextStats", Name = "GetTextStats")]
        public ActionResult GetTextStats([FromBody]string requestedText)
        {
            return Ok(Summary(requestedText).ToString());
        }

        /// <summary>
        /// Gets statistical details from a file provided
        /// </summary>
        /// <param name="textFile">The file to be analyzed.</param>/// 
        /// <returns>A set of statistics</returns>

        [HttpPost("GetTextFileStats", Name = "GetTextFileStats")]
        public async Task<ActionResult> GetTextFileStats(IFormFile textFile)
        {
            if (textFile == null || textFile.Length == 0)
            {
                return BadRequest();
            }

            var fileContents = String.Empty;

            using (var reader = new StreamReader(textFile.OpenReadStream()))
            {
                fileContents = await reader.ReadToEndAsync();
            }

            return Ok(Summary(fileContents).ToString());
        }

        private static StringBuilder Summary(string requestedText)
        {
            var service = new TextStatisticService();
            service.LoadString(requestedText);

            var sb = new StringBuilder();

            sb.AppendLine("Character count: " + service.GetCharacterCount());
            sb.AppendLine("Line count: " + service.GetLineCount());
            sb.AppendLine("Paragraph count: " + service.GetParagraphCount());
            sb.AppendLine("Sentence count: " + service.GetSentenceCount());
            sb.AppendLine("Top Ten Words:");

            var topTenWords = service.GetTopTenWords();

            foreach (var entry in topTenWords)
            {
                sb.AppendLine(entry.Key + " : " + entry.Value);
            }

            return sb;
        }
    }
}
