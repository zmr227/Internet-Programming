using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Stories story = await GetAPIAsync("https://localhost:44320/api/Story/1");

            String sResult = "API Result on /api/Story: " + Environment.NewLine
                            + "Title=" + story.Title + Environment.NewLine + "Description=" + story.Description;

            sResult += Environment.NewLine + "Create Time=" + story.CreateTime;
            sResult += Environment.NewLine + "Image Dir=" + story.Image;

            txt1.Text = sResult;
        }

        public class Stories
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            public DateTime CreateTime { get; set; }
        }

        static async Task<Stories> GetAPIAsync(string path)
        {
            Stories story = null;
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                story = JsonConvert.DeserializeObject<Stories>(
                        await response.Content.ReadAsStringAsync());
            }
            return story;
        }
        
    }
}

