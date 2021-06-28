using OrderManagementSystem.DAL;
using OrderManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    public class StockItemController
    {
        private readonly StockRepo _stockRepo = new StockRepo();

        //singleton pattern
        private static StockItemController _instance;

        private StockItemController() { }

        public static StockItemController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StockItemController();
                }
                return _instance;
            }
        }

        public IEnumerable<StockItem> GetStockItems()
        {
            return _stockRepo.GetStockItems();
        }

    }
}
