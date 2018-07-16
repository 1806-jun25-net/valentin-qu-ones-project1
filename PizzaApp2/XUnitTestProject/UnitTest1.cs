using Microsoft.AspNetCore.Mvc;
using PizzaApp2.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        private readonly OrderContext _context;
        [Theory]
        [InlineData("1")]
        public IActionResult Index(string sortOrder)
        {


            var qty = String.IsNullOrEmpty(sortOrder) ? "Quantity" : "";
            var date = sortOrder == "Date" ? "date_desc" : "Date";
            var ord = from s in _context.Orders
                      select s;
            switch (sortOrder)
            {

                case "Date":
                    ord = ord.OrderBy(s => s.DateOrder);
                    break;
                case "Quantity":
                    ord = ord.OrderBy(s => s.AmountPizza);
                    break;

            }

            return date;
        }


        public List<Pizza> order = new List<Pizza();
        [Theory]
        [InlineData(1)]
        public void DisplayOrderByID(int id)
        {
            //Display order by id.

            for (int i = 0; i < order.Count; i++)
            {
                if (int.Parse(order[i].Id.ToString()) == id)
                {

                    Console.WriteLine("Order details: \n " + order[i].Date.ToShortDateString() + "\n Order ID: " + id + "\n Location of Store: " + order[i].Location.ToString() + "\n Pizza was: " + order[i].Pizza.NamePizza.ToString() + "\n Amount: " + order[i].AmountOfPizza.ToString() + "\n The total was: " + order[i].Pizza.CostPizza.ToString());

                }
            }


        }


    }
}
