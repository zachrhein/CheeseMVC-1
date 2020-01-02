using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.cheeses = CheeseData.GetAll();

            return View();
        }


        
        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult Remove (int[] cheeseIds) 
        {
            foreach (int cheeseId in cheeseIds)
            {
                CheeseData.Remove(cheeseId);
            }
            
            return Redirect("/Cheese"); 
        }





        public IActionResult Add()
        {
            return View();
        }




        
        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(Cheese newCheese)
        {
            //Add the new cheese to my existing cheeses

            CheeseData.Add(newCheese); 
            return Redirect("/Cheese");
        }


        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheeses = CheeseData.; 

        }


        [HttpPost]
        public IActionResult Edit (int cheeseId, string name, string description)
        {

        }

      
    }

    
}


