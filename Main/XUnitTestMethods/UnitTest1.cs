using Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pizza.Library;
using Pizza.Library.Pizza;
using Store.Data;
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
        public List<Pizza.Library.Orders> order = new List<Pizza.Library.Orders>();
        IEnumerable<Pizza.Library.Orders> result = new List<Pizza.Library.Orders>();
        public List<Pizza.Library.Pizza.Pizza> piz = new List<Pizza.Library.Pizza.Pizza>();
        // private XMLMethods xml = new XMLMethods();

        public List<Address> location = new List<Address>();

        public List<User> users = new List<User>();
        List<OrderHasPizza> orderHasPizzas = new List<OrderHasPizza>();
        List<Store.Data.Orders> stOrder = new List<Store.Data.Orders>();



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
        [InlineData(1, "Randall", "Valentin", "Barr. Daguey", "Anasco", "PR", 1, 1)]
        public void OrderPizza(int id, string fName, string lName, string adLine, string adCity, string state, int topp, int qty1)
        {
            try
            {
                //arange
                int range = 100;
                Random r = new Random();
                double rDouble = r.NextDouble() * range; //for the cost of the pizza in total.

                //act
                order.Add(new Pizza.Library.Orders
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

        [Theory]
        [InlineData("Randall", "Valentin")]
        public void SearchUser(string name, string lastname)
        {
            List<User> users = new List<User>();
            //Search a user with his primary name i know is not efficient and is not safe to do this.
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Name.First.ToString() == name)
                {
                    if (users[i].Name.Last.ToString() == lastname)
                    {
                        Console.WriteLine("Information that was found: " + users[i].Name.First.ToString() + " " + users[i].Name.Last.ToString() + " Adress: " + users[i].Address.Line1.ToString() + " ");
                    }
                }
            }


        }


        [Theory]
        [InlineData(1, 1, 1, 1)]
        public void OrderPizza1(int userID, int locationID, int idPizza, int qty1Pizza)
        {










            /*
            foreach (var item in pizza)
            {
                if (item.IdPizza == idPizza)
                {
                    pizName = item.PizzaName;
                    price = decimal.Parse(item.PizzaPrice.ToString());

                }

            }
            */

            try
            {
                if (qty1Pizza <= 12)
                {




                    order.Add(new Pizza.Library.Orders
                    {
                        Id = 1 + int.Parse(order[order.Count - 1].ToString()),
                        Location = locationID,
                        User = new User
                        {

                            Id = int.Parse(users[userID].Id.ToString()),
                            Name = new Name
                            {
                                First = users[userID].Name.First.ToString(),
                                Last = users[userID].Name.Last.ToString()
                            },

                            Address = new Address
                            {
                                IdAddress = int.Parse(users[userID].Address.IdAddress.ToString()),
                                Line1 = users[userID].Address.Line1.ToString()
                            }

                        },


                        Pizza = new Pizza.Library.Pizza.Pizza
                        {
                            IdPizza = int.Parse(piz[idPizza].IdPizza.ToString()),
                            NamePizza = piz[idPizza].NamePizza.ToString(),
                            CostPizza = decimal.Parse(piz[idPizza].CostPizza.ToString()),
                            CountPizza = qty1Pizza




                        },
                        AmountOfPizza = qty1Pizza,
                        Date = DateTime.Now




                    });

                    piz[idPizza].CountPizza -= qty1Pizza;
                    Console.WriteLine(piz[idPizza].CountPizza + " Amount of " + piz[idPizza].NamePizza + " left.");
                }
                else
                {
                    Console.WriteLine("You have ordered " + qty1Pizza + " and that surpasses the amount we allow to order.");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }

        }


        [Theory]
        [InlineData(1, 1, 1, 1)]
        public void PlaceOrder(int userID, int locationID, int pizzaOption, int pizzaQty)
        {
            Methods m = new Methods();
            int option = 2;


            m.PrintAll();


            //This if will call a method an populate the inventory.
            if (option == 1)
            {
                m.LoadFromDB();

            }

            //This if will contain the order info to pass it to parameters to another class so it can be processed.
            if (option == 2)
            {



                Console.WriteLine("These are the options for your pizza. Choose one: ");
                m.PrintPizza();





                int i;
                int count = 0;

                if (int.TryParse(pizzaOption.ToString(), out i))
                {
                    count++;
                    if (count == 1)
                    {
                        if (pizzaOption >= 0)
                        {


                            #region UserInfo
                            Console.WriteLine("Lets place a order but first. Fill the require  information about you. ");
                            Console.WriteLine("Enter the First Name: ");

                            Console.WriteLine("Enter the Last Name: ");


                            Console.WriteLine("Enter the user ID you want to use the order with: ");


                            m.PrintLocations();
                            Console.WriteLine("What is the location ID of the store: ");



                            #endregion

                            Console.WriteLine("How many " + m.piz[pizzaOption].NamePizza.ToString() + " Pizza do you want? ");


                            if (int.TryParse(pizzaQty.ToString(), out i))
                            {
                                if (pizzaQty <= int.Parse(m.piz[pizzaOption].CountPizza.ToString()))
                                {
                                    m.OrderPizza(userID, locationID, pizzaOption, pizzaQty);

                                }
                                else
                                {
                                    Console.WriteLine("We are low on stock at the moment. Please try again later. Thanks.");
                                }


                            }

                        }


                    }
                }
            }










        }

        [Fact]
        public void PrintPizza()
        {


            for (int i = 0; i < piz.Count; i++)
            {
                Console.WriteLine(i + " The Pizza is : " + piz[i].NamePizza + " and the Quantity: " + piz[i].CountPizza);
                Console.WriteLine();

            }
        }

        [Fact]
        public void PrintLocations()
        {
            foreach (var item in location)
            {
                Console.WriteLine("Location ID: " + item.IdAddress.ToString() + "Location Name: " + item.Line1.ToString());
            }
        }


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

        private readonly PizzaPalaceContext _db;

        [Fact]
        public IEnumerable<Store.Data.Pizza> GetPizzas()
        {
             
        //we dont need to track changes to these
        //so skip the overhead of doing so
        var pizza = _db.Pizza.AsNoTracking().ToList();
            return pizza;

        }


        [Theory]
        [InlineData(1,2,3,4,2,2)]
        public void AddOrder(int location, int userID, int userLocation,  int iD, int idPizza, int amountPizza)
        {

            //LINQ first fails by thrrowing exception
            //FirstOrDefault fails to just null
            var id = _db.Orders.FirstOrDefault(g => g.IdOrder == iD);
            if (id == null)
            {
                throw new ArgumentException("order not added", nameof(id));

            }
            var order = new Store.Data.Orders
            {

                LocationIdLocation = location,
                UserIdUser = userID,
                UserLocationIdLocation = userLocation,
                




            };
            _db.Add(order);

            

            var id1 = _db.OrderHasPizza.FirstOrDefault(g => g.OrderIdOrder == iD);
            if (id == null)
            {
                throw new ArgumentException("order not added", nameof(id));

            }
            var orderHasPizza = new OrderHasPizza
            {
                OrderIdOrder = 4,
                OrderLocationIdLocation = location,
                OrderUserIdUser = userID,
                OrderUserLocationIdLocation = userLocation,
                PizzaIdPizza = idPizza,
                AmountOfPizzaInOrder = amountPizza





            };




            _db.Add(orderHasPizza);

        }


    }
}
