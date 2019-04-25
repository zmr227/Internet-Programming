using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Storyphase.Models;

namespace Storyphase.Controllers
{
    public class StoryBlocksController : Controller
    {
        public IActionResult Index()
        {
            var blocks = GetBlocksFromAPI();
            return View();
        }

        private List<StoryBlocks> GetBlocksFromAPI()
        {
            try
            {
                var client = new HttpClient();
                var blockList = new List<StoryBlocks>();

                var getDataTask = client.GetAsync("https://localhost:44307/api/StoryBlocks")
                    .ContinueWith(response => {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<List<StoryBlocks>>();
                            readResult.Wait();
                            blockList = readResult.Result;
                        }
                    });
                // wait for async task to complete
                getDataTask.Wait();
                return blockList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}