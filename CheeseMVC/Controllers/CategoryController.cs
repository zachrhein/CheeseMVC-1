using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }
        //GET /<controller>/
        public IActionResult Index()
        {
            return View(context.Categories.ToList());
        }
        //GET
        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }

        //POST /Category/Add
        [HttpPost]
        public object Add(AddCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory
                {
                    Name = viewModel.Name
                };
                context.Categories.Add(newCategory);
                context.SaveChanges();

                return Redirect("Index");
            }
            return View(viewModel);
        }
    }
}