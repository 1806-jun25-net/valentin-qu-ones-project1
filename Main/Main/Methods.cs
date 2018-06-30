using Pizza.Library;
using Pizza.Library.Pizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main
{
    public class Methods
    {

        public void PrintAll()
        {
            Console.WriteLine("");
            Console.WriteLine("Welcome to the Pizza Palace");
            Console.WriteLine("Choose an option");
            Console.WriteLine("1. Populate inventory");
            Console.WriteLine("2. Order Pizza");
            Console.WriteLine("3. Get a suggested order for a user based on his order history");
            Console.WriteLine("4. Search users by name");
            Console.WriteLine("5. Display details of an order");
            Console.WriteLine("6. Display all order history of a location");
            Console.WriteLine("7. Display all order history of a user");
            Console.WriteLine("8. Display order history sorted by earliest, latest, cheapest, most expensive");
            Console.WriteLine("9. Save all data to disk in XML format");
            Console.WriteLine("10. Load all data from disk");
            Console.WriteLine("0. Exit Application");
            Console.WriteLine("");

        }

        public List<Ingredients> ingredients = new List<Ingredients>();

        public List<Orders> order = new List<Orders>();
        IEnumerable<Orders> result = new List<Orders>();


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

        public void PrintIngredients()
        {


            for (int i = 0; i < ingredients.Count; i++)
            {
                Console.WriteLine(i + " The topping: " + ingredients[i].topping + " The Quantity: " + ingredients[i].qty);
                Console.WriteLine();

            }
        }





        public void OrderPizza(int idOrder, string location,int idUser, string fName, string lName, string adLine, string adCity, string state, int topp, int qty1)
        {
            try
            {
                int range = 25;
                Random r = new Random();
                double rDouble = r.NextDouble() * range; //for the cost of the pizza in total.

                order.Add(new Orders
                {
                    Id = idOrder,
                    location = location,
                    User = new User
                    {

                        Id = idUser,
                        Name = new Name
                        {
                            First = fName,
                            Last = lName
                        },
                        Address = new Address
                        {
                            Line1 = adLine,
                            City = adCity,
                            State = state
                        }

                    },


                    Pizza = new Pizza.Library.Pizza.Pizza
                    {
                        toppings = new Ingredients
                        {
                            topping = ingredients[topp].topping.ToString(),



                        },
                        cost = rDouble


                    },
                    amountOfPizza = qty1




                });

                ingredients[topp].qty -= qty1;
                Console.WriteLine(ingredients[topp].qty.ToString() + " Amount of " + ingredients[topp].topping.ToString() + " left.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }
        }


        public void PrintSuggestedOrder(int suggestion)
        {
            int countTop1 = 0;
            int countTop2 = 0;



            for (int i = 0; i < order.Count; i++)
            {
                if (order[i].User.Id == suggestion)
                {

                    //order.Where(s => s != null && s.Pizza.toppings.ToString().StartsWith(order[i].Pizza.toppings.ToString())).Count();

                    // order.Average(s => s.Pizza.toppings.ToString().Length);



                    Console.WriteLine();

                }
            }




        }

        public void SearchUser(string Name)
        {

            for (int i = 0; i < order.Count; i++)
            {
                if (order[i].User.Name.First.ToString() == Name)
                {
                    Console.WriteLine("Information that was found: " + order[i].User.Name.First.ToString() + " " + order[i].User.Name.Last.ToString() + " Adress: " + order[i].User.Address.Line1.ToString() + " " + order[i].User.Address.State.ToString());

                }
            }


        }

        public void DisplayOrderByID(int id)
        {

            for (int i = 0; i < order.Count; i++)
            {
                if (int.Parse(order[i].Id.ToString()) == id)
                {

                    Console.WriteLine("Order details: \n " + "Order ID: " + id + "\n Location of Store: " + order[i].location.ToString() + "\n Pizza was: " + order[i].Pizza.toppings.topping.ToString() +"\n Amount: " + order[i].amountOfPizza.ToString() + "\n The total was: " + order[i].Pizza.cost.ToString());

                }
            }


        }

        public void DisplayOrderByLocation(string location)
        {

            for (int i = 0; i < order.Count; i++)
            {
                if (order[i].location.ToString() == location)
                {

                    Console.WriteLine("Order details: \n " + "Order ID: " + order[i].Id.ToString() + "\n Location of Store: " + order[i].location.ToString() + "\n Pizza was: " + order[i].Pizza.toppings.topping.ToString() + "\n Amount: " + order[i].amountOfPizza.ToString() + "\n The total was: " + order[i].Pizza.cost.ToString());

                }
            }


        }


    }
}
