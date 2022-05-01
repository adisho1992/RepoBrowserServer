using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace RepoBrowser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkController : ControllerBase
    {
        [HttpPost]
        public void SaveBookMark([FromBody] Item bookMark)
        {
            RepositoryList bookMarks = null;
            var session = HttpContext.Session;
            string bookMarksStr = session.GetString("BookMarks");

            if (bookMarksStr == null) {
                bookMarks = new RepositoryList();
                bookMarks.items = new List<Item>();
            }
            else
                bookMarks = JsonSerializer.Deserialize<RepositoryList>(bookMarksStr);

            bookMarks.items.Add(bookMark);
            session.SetString("BookMarks", JsonSerializer.Serialize(bookMarks)); // saves the list to the session
        }

        [HttpGet]
        public RepositoryList GetBookMarks()
        {
            var session = HttpContext.Session;
            string bookMarksStr = session.GetString("BookMarks");
            if (bookMarksStr == null) {
                return new RepositoryList();
            }
            RepositoryList repoList = JsonSerializer.Deserialize<RepositoryList>(bookMarksStr);
            return repoList;   
        }
    }
}
