﻿using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Lazy;

namespace MVC.Boilerplate.Component
{
    public class AnimalListViewComponent:ViewComponent
    {
        private readonly ILazyService _lazyService;
        int recordSize = 10;
        public AnimalListViewComponent(ILazyService lazyService)
        {
            _lazyService = lazyService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int componentNum)
        {
            List<Animal> animalList = await _lazyService.AnimalList();
            animalList = animalList.Skip(componentNum * recordSize).Take(recordSize).ToList();
            //Thread.Sleep(1000);
            return View("AnimalList",animalList);
        }
    }
}
