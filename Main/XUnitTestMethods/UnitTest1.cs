using Pizza.Library;
using Pizza.Library.Pizza;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestMethods
{
    public class UnitTest1
    {
        
        public void Test1()
        {




        }


        List<Ingredients> ingredients = new List<Ingredients>();
        public List<Orders> order = new List<Orders>();
        IEnumerable<Orders> result = new List<Orders>();

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

        [Theory]
        [InlineData(1, "Randall", "Valentin", "Barr. Daguey", "Anasco", "PR", 1,1)]
        public void OrderPizza(int id, string fName, string lName, string adLine, string adCity, string state, int topp, int qty1)
        {
            try
            {
                //arange
                int range = 100;
                Random r = new Random();
                double rDouble = r.NextDouble() * range; //for the cost of the pizza in total.

                //act
                order.Add(new Orders
                {
                    User = new User
                    {

                        Id = id,
                        Name = new Name
                        {
                            First = fName,
                            Last = lName
                        },
                        Address = new Address
                        {
                            Line1 = adLine,
                           
                        }

                    },


                    Pizza = new Pizza.Library.Pizza.Pizza
                    {
                        


                    },
                    AmountOfPizza = qty1




                });

                ingredients[topp].qty -= qty1;
                Console.WriteLine(ingredients[topp].qty.ToString() + " Amount of " + ingredients[topp].ToString() + " left.");
                //assert is handle by the catch block if we encounter a error of some sort.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }
        }

        [Theory]
        [InlineData(1)]
        public void PrintSuggestedOrder(int suggestion)
        {

            var result = Enumerable.Range(0, order.Count).Where(i => order[i].User.Id == suggestion).ToList();
            //assert
           // Assert.AreEqual(expected, suggestion);

        }
        [Theory]
        [InlineData("randall@yahoo.com")]
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public void PopulateInvenentory()
        {
            

        }

       




    }
}
