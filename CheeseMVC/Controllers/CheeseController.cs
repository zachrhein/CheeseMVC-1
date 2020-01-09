using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext) 
        {
            context = dbContext;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Cheese> cheeses = context.Cheeses.ToList();
            

            return View(cheeses);
        }
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.Id == cheeseId);
                context.Cheeses.Remove(theCheese);
            }
            context.SaveChanges(); 
            return Redirect("/Cheese");
        }
        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }


        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description
                };
                context.Cheeses.Add(newCheese);
                context.SaveChanges();
                return Redirect("/Cheese");
            }
            return View(addCheeseViewModel);
        }


        public IActionResult Edit(int cheeseId)
        {
            Cheese ch = context.Cheeses.Single(c => c.Id == cheeseId);
            ViewBag.cheese = ch;
            return View();
        }


        [HttpPost]
        public IActionResult Edit (int id, string name, string description)
        {
            Cheese ch = context.Cheeses.Single(c => c.Id == id);
            ch.Name = name;
            ch.Description = description;
            context.SaveChanges();
            
            return Redirect("/Cheese");
        }

      
    }

    
}


