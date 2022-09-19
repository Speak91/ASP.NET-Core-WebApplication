using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork1
{
    internal class Program
    {
        private static HttpClient client = new HttpClient();
        private static readonly CancellationToken cts = new CancellationToken();
        private static readonly string path = "result.txt";

        static async Task Main(string[] args)
        {
            await GetPosts(cts);
        }

        public static async Task GetPosts(CancellationToken cancelationToken)
        {

            for (int i = 1; i < 11; i++)
            {
                var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{i}", cancelationToken);
                var jsonModel = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PostModel>(jsonModel);
                await SavePost(result);
                Console.WriteLine("Все сохранено");
            }

        }

        public static async Task SavePost(PostModel model)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync(model.ToString());
            }
        }
    }
}
