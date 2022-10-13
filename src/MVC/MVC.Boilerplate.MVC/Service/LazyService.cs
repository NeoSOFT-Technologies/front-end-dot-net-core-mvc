using Microsoft.Extensions.Hosting.Internal;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Lazy;

namespace MVC.Boilerplate.Service
{
    public class LazyService:ILazyService
    {
        private readonly ILogger<LazyService> _logger;

        public LazyService(ILogger<LazyService> logger)
        {
            _logger = logger;
        }
        public async Task<List<Person>> PersonList()
        {
            _logger.LogInformation("PersonList of Lazy Service executed");
            //Setting path for persons txt file
            string path = (System.IO.Directory.GetCurrentDirectory() + "\\Static\\Persons.txt");
            string path2 = AppDomain.CurrentDomain.BaseDirectory;
            List<Person> PersonList = new List<Person>();
            string[] Persons = await File.ReadAllLinesAsync(path);
            foreach (string line in Persons)
            {
                var data = line.Split(',');
                PersonList.Add(new Person()
                {
                    Id = Convert.ToInt32(data[0]),  
                    Name = data[1],
                    Email = data[2]
                }) ;
            }
            _logger.LogInformation("PersonList of Lazy Service completed");
            return PersonList;
        }


        // Read File and get data in string
        //async static Task<string> ReadFromFile(string DirectoryPath, string FileName)
        //{
        //    if (Directory.Exists(DirectoryPath))
        //    {
        //        string FilePath = DirectoryPath + "\\" + FileName;
        //        if (File.Exists(FilePath))
        //        {
        //            return await File.ReadAllTextAsync(FilePath);
        //        }
        //    }
        //    return "";
        //}
    }
}
