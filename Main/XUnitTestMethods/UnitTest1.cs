using Pizza.Library.Pizza;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestMethods
{
    public class UnitTest1
    {
        
        public void Test1()
        {




        }


        List<Ingredients> ingredients = new List<Ingredients>();

        [Fact]
        public void PopulateInventory()
        {

            ingredients.Add(new Ingredients
            {

                topping = "Pepperonie",
                qty = 25

            });

            ingredients.Add(new Ingredients
            {

                topping = "Sausagge",
                qty = 25

            });

            ingredients.Add(new Ingredients
            {

                topping = "Bacon",
                qty = 25

            });

            Console.WriteLine("Inventory Populated!");
            Console.WriteLine("");

        }
        [Fact]
        public void PrintIngredients()
        {
            foreach (var item in ingredients)
            {
                Console.WriteLine("The topping: " + item.topping + " The Quantity: " + item.qty);
            }
        }



    }
}
