using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
/*
namespace ConsoleClient
{
    class RestClient
    {
        private string BaseUrl = "https://localhost:44305/api/Demo/";

        public Task<HttpResponseMessage> Demo1()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new 
                    MediaTypeWithQualityHeaderValue("text/plain"));
                return client.GetAsync("demo1");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> Demo2()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("text/html"));
                return client.GetAsync("demo2");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> Demo3(int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("text/html"));
                return client.GetAsync("demo3/" + id);
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> Demo4(int a, string b)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("text/html"));
                return client.GetAsync("demo4/" + a + "/" + b);
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> Demo5()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));
                return client.GetAsync("demo5");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> Demo6()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));
                return client.GetAsync("demo6");
            }
            catch
            {
                return null;
            }
        }
    }
}*/
