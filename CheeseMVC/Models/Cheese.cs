using System;
using System.Collections.Generic;

namespace CheeseMVC.Models
{
    public class Cheese
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public CheeseCategory Category { get; set; }
        public int Id { get; set; }
        public int CategoryID { get; set; }
        public IList<CheeseMenu> CheeseMenus { get; set; }




    }
}
