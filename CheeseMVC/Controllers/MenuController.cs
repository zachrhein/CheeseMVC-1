﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            return View(context.Menus.ToList());
        }
        //GET
        public IActionResult Add()
        {
            AddMenuViewModel vm = new AddMenuViewModel();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Add(AddMenuViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = vm.Name
                };
                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect("ViewMenu/" + newMenu.ID);
            }
            return View(vm);
        }
        public IActionResult ViewMenu(int id)
        {
            Menu menu =
                 context.Menus.Single(c => c.ID == id);
            List<CheeseMenu> items = context
                    .CheeseMenus
                    .Include(item => item.Cheese)
                    .Where(cm => cm.MenuID == id)
                    .ToList();

            ViewMenuViewModel vm = new ViewMenuViewModel()
            {
                Menu = menu,
                Items = items
            };
            return View(vm);
        }
        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            List<Cheese> cheeses = context.Cheeses.ToList();
            return View(new AddMenuItemViewModel(menu, cheeses));
        }
        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var cheeseID = addMenuItemViewModel.CheeseID;
                var menuID = addMenuItemViewModel.MenuID;

                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where (cm => cm.MenuID == menuID).ToList();
                if (existingItems.Count == 0)
                {
                    CheeseMenu menuItem = new CheeseMenu
                    {
                        Cheese = context.Cheeses.Single(c => c.Id == cheeseID),
                        Menu = context.Menus.Single(m => m.ID == menuID)
                    };
                    context.CheeseMenus.Add(menuItem);
                    context.SaveChanges();
                }
                return Redirect(string.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.MenuID));
            }
            return View(addMenuItemViewModel);
        }
    }

}