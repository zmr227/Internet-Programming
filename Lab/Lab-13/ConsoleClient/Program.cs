using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
/*
namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Demo6();
            Console.ReadLine();
        }

        static void Demo1()
        {
            RestClient restClient = new RestClient();
            var result = restClient.Demo1().Result;
            Console.WriteLine("Status: " + result.StatusCode);
            var content = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Content: " + content);
        }

        static void Demo2()
        {
            RestClient restClient = new RestClient();
            var result = restClient.Demo2().Result;
            Console.WriteLine("Status: " + result.StatusCode);
            var content = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Content: " + content);
        }

        static void Demo3()
        {
            RestClient restClient = new RestClient();
            var result = restClient.Demo3(123).Result;
            Console.WriteLine("Status: " + result.StatusCode);
            var content = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Content: " + content);
        }

        static void Demo4()
        {
            RestClient restClient = new RestClient();
            var result = restClient.Demo4(123, "hello").Result;
            Console.WriteLine("Status: " + result.StatusCode);
            var content = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Content: " + content);
        }

        static void Demo5()
        {
            RestClient restClient = new RestClient();
            var result = restClient.Demo5().Result;
            Console.WriteLine("Status: " + result.StatusCode);
            var block = result.Content.ReadAsAsync<StoryBlocks>().Result;
            Console.WriteLine("Story Block Info: ");
            Console.WriteLine("Id: " + block.Id);
            Console.WriteLine("Content: " + block.Content);
            Console.WriteLine("StoryId: " + block.StoryId);
        }

        static void Demo6()
        {
            RestClient restClient = new RestClient();
            var result = restClient.Demo6().Result;
            Console.WriteLine("Status: " + result.StatusCode);
            var blocks = result.Content.ReadAsAsync<List<StoryBlocks>>().Result;
            Console.WriteLine("Story Block List: ");
            foreach (var block in blocks)
            {
                Console.WriteLine("Id: " + block.Id);
                Console.WriteLine("Content: " + block.Content);
                Console.WriteLine("StoryId: " + block.StoryId);
                Console.WriteLine("=========================");
            }
        }
    }
}
*/