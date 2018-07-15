using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using Pizza.Library;
using Pizza.Library.Pizza;
using Store.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Main
{
    public class Methods
    {
        #region Database





        #endregion

        #region list And Variables
        public List<Pizza.Library.Pizza.Pizza> piz = new List<Pizza.Library.Pizza.Pizza>();
        // private XMLMethods xml = new XMLMethods();
        public List<Pizza.Library.Orders> order = new List<Pizza.Library.Orders>();
        public List<Address> location = new List<Address>();
        IEnumerable<Pizza.Library.Orders> result = new List<Pizza.Library.Orders>();
        public List<User> users = new List<User>();
        List<OrderHasPizza> orderHasPizzas = new List<OrderHasPizza>();
        List<Store.Data.Orders> stOrder = new List<Store.Data.Orders>();

        Task<IEnumerable<Pizza.Library.Orders>> desListTask = DeserializeFromFileAsync(@"C:\Users\Revature\Desktop\data.xml");

        #endregion

        public void PrintAll()
        {
            Console.WriteLine("");
            Console.WriteLine("Welcome to the Pizza Palace");
            Console.WriteLine("Choose an option");
            Console.WriteLine("1. Populate inventory");
            Console.WriteLine("2. Order Pizza");
            Console.WriteLine("3. Get a suggested order for a user based on his order history (Not working WIP)");
            Console.WriteLine("4. Search users by ID ");
            Console.WriteLine("5. Display details of an order");
            Console.WriteLine("6. Display all order history of a location");
            Console.WriteLine("7. Display all order history of a user");
            Console.WriteLine("8. Display order history sorted by earliest, latest, cheapest, most expensive");
            Console.WriteLine("9. Save all data to disk in XML format");
            Console.WriteLine("10. Load all data from disk");
            Console.WriteLine("0. Exit Application");
            Console.WriteLine("");

        }





        public void LoadFromDB()
        {
            #region Not in use

            /*ingredients.Add(new Ingredients
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
            Console.WriteLine("");*/

            #endregion

            #region Database

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            Console.WriteLine(configuration.GetConnectionString("PizzaPalace"));



            #endregion

            var optionsBuilder = new DbContextOptionsBuilder<PizzaPalaceContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaPalace"));
            var repo = new PizzaStoreRepository(new PizzaPalaceContext(optionsBuilder.Options));

            var pizza = repo.GetPizzas();//*Pizza
            var userin = repo.GetUserInfo();//UserInformation
            var locations = repo.GetLocations();//Locations
            var orderHas = repo.GetOrderHasPizzas();//Order_Has_Pizza
            var orderDb = repo.GetOrders();//Orders

            foreach (var item in locations)
            {


                location.Add(new Address
                {
                    IdAddress = item.IdLocation,
                    Line1 = item.LocationName
                }

                    );

            }

            foreach (var item in userin)
            {
                foreach (var item2 in locations)
                {
                    if (item.LocationIdLocation == item2.IdLocation)
                    {

                        //add user info here to a list make a new list is what i need to do dont forget.
                        users.Add(new User
                        {

                            Id = item.IdUser,
                            Name = new Name
                            {
                                First = item.FirstName,
                                Last = item.LastName

                            },

                            Address = new Address
                            {
                                IdAddress = int.Parse(item2.IdLocation.ToString()),
                                Line1 = item2.LocationName.ToString()
                            }




                        });
                    }

                }
            }


            foreach (var item in pizza)
            {


                piz.Add(new Pizza.Library.Pizza.Pizza
                {
                    IdPizza = item.IdPizza,
                    NamePizza = item.PizzaName.ToString(),
                    CountPizza = int.Parse(item.PiizaCount.ToString()),
                    CostPizza = decimal.Parse(item.PizzaPrice.ToString())


                });




            }


            foreach (var item in orderHas)
            {
                orderHasPizzas.Add(new OrderHasPizza
                {

                    IdOrderHasPizza = item.IdOrderHasPizza,
                    OrderIdOrder = item.OrderIdOrder,
                    OrderLocationIdLocation = item.OrderLocationIdLocation,
                    OrderUserIdUser = item.OrderUserIdUser,
                    OrderUserLocationIdLocation = item.OrderUserLocationIdLocation,
                    PizzaIdPizza = item.PizzaIdPizza,
                    AmountOfPizzaInOrder = item.AmountOfPizzaInOrder


                });


            }

            foreach (var item in orderDb)
            {

                stOrder.Add(new Store.Data.Orders
                {

                    IdOrder = item.IdOrder,
                    LocationIdLocation = item.LocationIdLocation,
                    UserIdUser = item.UserIdUser,
                    UserLocationIdLocation = item.UserLocationIdLocation,
                    DateOfOrders = item.DateOfOrders,




                });


            }



            try
            {
                //Method used to convert all database informtaion to process it into a List
                int count = 0;
                foreach (var itemOrd in orderDb)
                {

                    if (itemOrd.IdOrder == orderHasPizzas[count].OrderIdOrder)
                    {

                        order.Add(new Pizza.Library.Orders
                        {


                            Id = int.Parse(orderHasPizzas[count].OrderIdOrder.ToString()),
                            Location = int.Parse(orderHasPizzas[count].OrderLocationIdLocation.ToString()),
                            User = new User
                            {

                                Id = int.Parse(users[count].Id.ToString()),
                                Name = new Name
                                {
                                    First = users[count].Name.First.ToString(),
                                    Last = users[count].Name.Last.ToString()
                                },
                                Address = new Address
                                {
                                    IdAddress = int.Parse(location[count].IdAddress.ToString()),
                                    Line1 = location[count].Line1.ToString()

                                }

                            },
                            Pizza = new Pizza.Library.Pizza.Pizza
                            {
                                IdPizza = int.Parse(piz[count].IdPizza.ToString()),
                                NamePizza = piz[count].NamePizza.ToString(),
                                CountPizza = int.Parse(piz[count].CountPizza.ToString()),
                                CostPizza = decimal.Parse(piz[count].CostPizza.ToString())


                            },
                            AmountOfPizza = int.Parse(orderHasPizzas[count].AmountOfPizzaInOrder.ToString()),
                            Date = DateTime.Parse(stOrder[count].DateOfOrders.ToString())

                        });

                        count++;

                    }


                }
            }
            catch (FormatException ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.ErrorException("Format Error", ex);
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }
            catch (Exception ex)
            {

                Logger logger = LogManager.GetCurrentClassLogger();
                logger.ErrorException("Format Error", ex);
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }


        }

        public void PrintPizza()
        {


            for (int i = 0; i < piz.Count; i++)
            {
                Console.WriteLine(piz[i].IdPizza.ToString() + " The Pizza is : " + piz[i].NamePizza + " and the Quantity: " + piz[i].CountPizza);
                Console.WriteLine();

            }
        }



        public void PrintLocations()
        {
            foreach (var item in location)
            {
                Console.WriteLine("Location ID: " + item.IdAddress.ToString() + "Location Name: " + item.Line1.ToString());
            }
        }


        public void OrderPizza(int userID, int locationID, int idPizza, int qty1Pizza)
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

           /* try
            {
                if (qty1Pizza <= 12)
                {

                    int lastIndex = int.Parse(stOrder[stOrder.Count - 1].IdOrder.ToString());



                    order.Add(new Pizza.Library.Orders
                    {

                        Id = lastIndex,
                        Location = locationID,
                        User = new User
                        {
                            Id = userID,
                            Name = new Name
                            {
                                First = users[lastIndex].Name.First.ToString(),
                                Last = users[lastIndex].Name.Last.ToString()
                            },
                            Address = new Address
                            {

                                IdAddress = locationID,
                                // Line1 = location[lastIndex]

                            }

                        }

                    });



                    /*
                    order.Add(new Pizza.Library.Orders
                    {
                        Id = 2 + int.Parse(order[order.Count - 1].ToString()),
                        Location = locationID,
                        User = new User
                        {

                            Id = userID,
                            Name = new Name
                            {
                                First = users[userID - 1].Name.First.ToString(),
                                Last = users[userID - 1].Name.Last.ToString()
                            },

                            Address = new Address
                            {
                                
                                IdAddress = int.Parse(users[userID - 1].Address.IdAddress.ToString()),
                                Line1 = users[userID - 1].Address.Line1.ToString()
                            }

                        },


                        Pizza = new Pizza.Library.Pizza.Pizza
                        {
                            IdPizza = int.Parse(piz[idPizza].IdPizza.ToString()),
                            NamePizza = piz[idPizza].NamePizza.ToString(),
                            CostPizza = piz[idPizza].CostPizza,
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
            */
               
            }


        public void PrintSuggestedOrder(int suggestion)
        {

            // Method is not currently working.
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

        public void SearchUser(string name, string lastname)
        {
            //Search a user with his primary name i know is not efficient and is not safe to do this.
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Name.First.ToString() == name)
                {
                    if (users[i].Name.Last.ToString() == lastname)
                    {
                        Console.WriteLine();
                        Console.WriteLine("ID of User: " + users[i].Id.ToString() + " \n Information that was found: " + users[i].Name.First.ToString() + " " + users[i].Name.Last.ToString() + " \n Adress: " + users[i].Address.Line1.ToString() + " ");

                    }
                }
            }


        }

        public void DisplayOrderByID(int id)
        {
            //Display order by id.
            try
            {
                for (int i = 0; i < order.Count; i++)
                {
                    if (int.Parse(order[i].Id.ToString()) == id)
                    {

                        Console.WriteLine("Order details: \n " + order[i].Date.ToShortDateString() + "\n Order ID: " + id + "\n Location of Store: " + order[i].Location.ToString() + "\n Pizza was: " + order[i].Pizza.NamePizza.ToString() + "\n Amount: " + order[i].AmountOfPizza.ToString() + "\n The total was: " + order[i].Pizza.CostPizza.ToString());

                    }
                }
            }
            catch (FormatException ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.ErrorException("Format Error", ex);
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }
            catch (Exception ex)
            {

                Logger logger = LogManager.GetCurrentClassLogger();
                logger.ErrorException("Format Error", ex);
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }


        }

        public void DisplayOrderByLocation(int id)
        {
            //Method to display orders of a location
            for (int i = 0; i < order.Count; i++)
            {
                if (int.Parse(order[i].Location.ToString()) == id)
                {

                    Console.WriteLine("Order details: \n " + order[i].Date.ToShortDateString() + "\n Order ID: " + order[i].Id.ToString() + "\n Location of Store: " + order[i].Location.ToString() + "\n Pizza was: " + order[i].Pizza.NamePizza.ToString() + "\n Amount: " + order[i].AmountOfPizza.ToString() + "\n The total was: " + order[i].Pizza.CostPizza.ToString());

                }
            }


        }

        public void DisplayOrdersByUser(string id)
        {
            for (int i = 0; i < order.Count; i++)
            {
                if (order[i].User.Id.ToString() == id)
                {
                    Console.WriteLine($"UserID: {order[i].User.Id}");
                    Console.WriteLine("User: " + order[i].User.Name.First.ToString() + " " + order[i].User.Name.Last.ToString());
                    Console.WriteLine("Order details: \n " + order[i].Date.ToShortDateString() + "\n Order ID: " + order[i].Id.ToString() + "\n Location of Store: " + order[i].Location.ToString() + "\n Pizza was: " + order[i].Pizza.NamePizza.ToString() + "\n Amount: " + order[i].AmountOfPizza.ToString() + "\n The total was: " + order[i].Pizza.CostPizza.ToString());

                }
            }
        }

        public void SortByAll()
        {
            //List contains a method call OrderBy if given the correct parameters u can order the list in any way you want.   
            try
            {
                Console.WriteLine("\n Sorted by Acending Date: \n");
                List<Pizza.Library.Orders> SortedList = order.OrderBy(o => o.Date).ToList();

                foreach (var o in SortedList)
                {
                    Console.Write(o.Date.ToString() + " Order ID: " + o.Id.ToString() + "\n Name of User: " + o.User.Name.First.ToString() + " " + o.User.Name.Last.ToString() + "\n Order Cost: " + o.Pizza.CostPizza.ToString("C2"));
                    Console.WriteLine();
                }
                Console.WriteLine();


                Console.WriteLine("\n Sorted by Decending Date: \n");
                List<Pizza.Library.Orders> SortedListDescending = order.OrderByDescending(o => o.Date).ToList();

                foreach (var o in SortedListDescending)
                {
                    Console.Write(o.Date.ToString() + " Order ID: " + o.Id.ToString() + "\n Name of User: " + o.User.Name.First.ToString() + " " + o.User.Name.Last.ToString() + "\n Order Cost: " + o.Pizza.CostPizza.ToString("C2"));
                    Console.WriteLine();

                }
                Console.WriteLine();





                Console.WriteLine("\n Sorted by Acending Cost: \n");
                List<Pizza.Library.Orders> SortedListCost = order.OrderBy(o => o.Pizza.CostPizza.ToString()).ToList();

                foreach (var o in SortedListCost)
                {
                    Console.Write(o.Date.ToString() + " Order ID: " + o.Id.ToString() + "\n Name of User: " + o.User.Name.First.ToString() + " " + o.User.Name.Last.ToString() + "\n Order Cost: " + o.Pizza.CostPizza.ToString("C2"));
                    Console.WriteLine();
                }
                Console.WriteLine();

                Console.WriteLine("\n Sorted by Decending Cost: \n");
                List<Pizza.Library.Orders> SortedListDescendingCost = order.OrderByDescending(o => o.Pizza.CostPizza.ToString()).ToList();

                foreach (var o in SortedListDescendingCost)
                {
                    Console.Write(o.Date.ToString() + " Order ID: " + o.Id.ToString() + "\n Name of User: " + o.User.Name.First.ToString() + " " + o.User.Name.Last.ToString() + "\n Order Cost: " + o.Pizza.CostPizza.ToString("C2"));
                    Console.WriteLine();

                }
                Console.WriteLine();

            }
            catch (FormatException ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.ErrorException("Format Error", ex);
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }
            catch (Exception ex)
            {

                Logger logger = LogManager.GetCurrentClassLogger();
                logger.ErrorException("Format Error", ex);
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }





        }



        #region xml

        public void serializeToXML()
        {
            //USed to call the SerializeTOFile Method from another method.
            SerializeToFile(@"C:\Revature\Proyect 1\Main\Main\bin\Debug\netcoreapp2.1\data.xml", order);


        }

        public static void SerializeToFile(string fileName, List<Pizza.Library.Orders> order)
        {
            

            //Example give in training session to serilaize.
            var serializer = new XmlSerializer(typeof(List<Pizza.Library.Orders>));
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);
                serializer.Serialize(fileStream, order);
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine($"Path {fileName} was too long! {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Some other error with file I/O: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw; // re-throws the same exception
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }






        }


        public async Task DezerializedAsync()
        {
            //Was having proublems with the dezerialize but it was 
            //cause i was not moving the position back to 0 and i was not giving the result back to any variable to store it.
            FileStream fileStream = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Pizza.Library.Orders>));

                using (var memoryStream = new MemoryStream())
                {
                    using (fileStream = new FileStream(@"C:\Revature\Proyect 1\Main\Main\bin\Debug\netcoreapp2.1\data.xml", FileMode.Open))
                    {
                        await fileStream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Position = 0; // reset "cursor" of stream to beginning
                    order = (List<Pizza.Library.Orders>)serializer.Deserialize(memoryStream);
                }

            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Some other error with file I/O: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw; // re-throws the same exception
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }


        }


        #endregion



        #region Not Used Methods on Main
        // Method to deserialize a file from xlm format. Is not currently in use...
        public async static Task<IEnumerable<Pizza.Library.Orders>> DeserializeFromFileAsync(string fileName)
        {

            var serializer = new XmlSerializer(typeof(List<Pizza.Library.Orders>));

            using (var memoryStream = new MemoryStream())
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0; // reset "cursor" of stream to beginning

                return (List<Pizza.Library.Orders>)serializer.Deserialize(memoryStream);

            }
        }


        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);//Command to compare the string to a email format.
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion





    }
}
