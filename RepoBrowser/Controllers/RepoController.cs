using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Web.Http.Cors;

namespace RepoBrowser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoController : ControllerBase
    {
        static HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<RepositoryList> searchRepo(string repoName, string page)
        {
            string repositoriesTemp = null;
            RepositoryList repositories = null;

            //it didnt work without this header
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " + "Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            var response = await client.GetAsync("https://api.github.com/search/repositories?q=" + repoName + "&page=" + page + "&per_page=8");
            
            if (response.IsSuccessStatusCode) {
                repositoriesTemp = await response.Content.ReadAsStringAsync();
            }

            if (repositoriesTemp != null) {
                repositories = JsonSerializer.Deserialize<RepositoryList>(repositoriesTemp);
            }

            return repositories;
        }
    }
}
