using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore
{
    public static class ShoppingCart
    {
        public static List<goods> Items { get; private set; } = new List<goods>();

        public static void AddToCart(goods item)
        {
            Items.Add(item);
        }
    }

}

