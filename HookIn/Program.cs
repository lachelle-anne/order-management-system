using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.Controllers;
using OrderManagementSystem.Domain;

namespace HookIn
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderController.Instance.ResetDatabase();
        }
    }
}
