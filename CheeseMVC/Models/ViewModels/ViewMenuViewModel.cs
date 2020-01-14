using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models.ViewModels
{
    public class ViewMenuViewModel
    {

        public Menu Menu { get; set; }
        public IList<CheeseMenu> Items { get; set; }
        
        public ViewMenuViewModel()
        {}

        public ViewMenuViewModel(CheeseMenu cheeseMenu)
        {
        } 
    
        
    }
}
