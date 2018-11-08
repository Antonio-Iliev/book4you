using Autofac;
using LibrarySystem.ConsoleClient.Core.Contracts;
using LibrarySystem.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace LibrarySystem.ConsoleClient
{
    public class StartUp
    {
        static void Main()
        {

            var authors = JsonConvert.DeserializeObject<FileAuthors[]>(File.ReadAllText("../../../../LibrarySystem.Data/Files/AuthorNames.json"));

            Start(authors);

        }

        public static async void Start(FileAuthors[] authors)
        {
            string[] responses = await GetReleases(authors);
            


            foreach (var response in responses)
            {
                JObject json = JObject.Parse(response);
                foreach (var item in json["results"])
                {
                    System.Console.ReadLine();
                    Book book = new Book();
                    Author author = new Author();
                    author.Name = item["author"].ToString();
                    book.Title = item["title"].ToString();

                    System.Console.WriteLine(book.Title);
                    System.Console.WriteLine(author.Name);
                    System.Console.WriteLine(item["description"]);
                    System.Console.WriteLine(item["---------******---------"]);
                }
            }
        }

        public static async Task<string[]> GetReleases(FileAuthors[] authors)
        {
            var client = new HttpClient();
            var result = new List<string>();
            foreach (var author in authors)
            {
                string name = author.Name.Replace(" ", "%20");
                Task<string> response = client.GetStringAsync("https://api.nytimes.com/svc/books/v3/lists/best-sellers/history.json?api-key=f8e19acc6ac940daa7ae7456e943da68&author=" + name);

                var data = await response;

                result.Add(data);
            }


            return result.ToArray(); ;
        }
    }

    public class FileAuthors
    {
        public FileAuthors(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
