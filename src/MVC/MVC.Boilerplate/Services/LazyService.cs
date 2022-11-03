﻿using Microsoft.Extensions.Hosting.Internal;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Lazy;

namespace MVC.Boilerplate.Services
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

        public async Task<List<Animal>> AnimalList()
        {
            _logger.LogInformation("AnimalList of Lazy Service executed");

            List<Animal> AnimalList = new List<Animal>();
            string[] Animals = await AnimalArr();
            foreach (string line in Animals)
            {
                var data = line.Split(',');
                AnimalList.Add(new Animal()
                {
                    Id = Convert.ToInt32(data[0]),
                    Name = data[1],
                    Type = data[2]
                });
            }
            _logger.LogInformation("AnimalList of Lazy Service completed");
            return AnimalList;
        }

        public async Task<int> AnimalsCount()
        {
            _logger.LogInformation("AnimalsCount of Lazy Service executed");
            return (await AnimalArr()).Length;
        }
        async Task<string[]> AnimalArr()
        {

            //Setting path for persons txt file
            string path = (System.IO.Directory.GetCurrentDirectory() + "\\Static\\Animals.txt");
            string path2 = AppDomain.CurrentDomain.BaseDirectory;
            return await File.ReadAllLinesAsync(path);
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