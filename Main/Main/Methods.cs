using Pizza.Library;
using Pizza.Library.Pizza;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

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
            Console.WriteLine("3. Get a suggested order for a user based on his order history (Not working WIP)");
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
        // private XMLMethods xml = new XMLMethods();
        public List<Orders> order = new List<Orders>();
        IEnumerable<Orders> result = new List<Orders>();

        Task<IEnumerable<Orders>> desListTask = DeserializeFromFileAsync(@"C:\Users\Revature\Desktop\data.xml");




        public void PopulateInventory()
        {
            #region Not in use
            /*
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
            */
            #endregion





        }

        public void PrintIngredients()
        {


            for (int i = 0; i < ingredients.Count; i++)
            {
                Console.WriteLine(i + " The topping: " + ingredients[i].topping + " The Quantity: " + ingredients[i].qty);
                Console.WriteLine();

            }
        }





        public void OrderPizza(int idOrder, string location, int idUser, string fName, string lName, string adLine, string adCity, string state, int topp, int qty1, DateTime date, string email)
        {
            try
            {
                if (qty1 <= 12)
                {
                    int range = 25;
                    Random r = new Random();
                    double rDouble = r.NextDouble() * range; //for the cost of the pizza in total.


                    order.Add(new Orders
                    {
                        Id = idOrder,
                        Location = location,
                        User = new User
                        {

                            Id = idUser,
                            Name = new Name
                            {
                                First = fName,
                                Last = lName
                            },
                            Email = email,
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
                            cost = Math.Round(rDouble, 2)


                        },
                        AmountOfPizza = qty1,
                        Date = date




                    });

                    ingredients[topp].qty -= qty1;
                    Console.WriteLine(ingredients[topp].qty.ToString() + " Amount of " + ingredients[topp].topping.ToString() + " left.");
                }
                else
                {
                    Console.WriteLine("You have ordered " + qty1 + " and that surpasses the amount we allow to order.");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");

            }

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

        public void SearchUser(string Name)
        {
            //Search a user with his primary name i know is not efficient and is not safe to do this.
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
            //Display order by id.

            for (int i = 0; i < order.Count; i++)
            {
                if (int.Parse(order[i].Id.ToString()) == id)
                {

                    Console.WriteLine("Order details: \n " + order[i].Date.ToShortDateString() + "\n Order ID: " + id + "\n Location of Store: " + order[i].Location.ToString() + "\n Pizza was: " + order[i].Pizza.toppings.topping.ToString() + "\n Amount: " + order[i].AmountOfPizza.ToString() + "\n The total was: " + order[i].Pizza.cost.ToString());

                }
            }


        }

        public void DisplayOrderByLocation(string location)
        {
            //Method to display orders of a location
            for (int i = 0; i < order.Count; i++)
            {
                if (order[i].Location.ToString() == location)
                {

                    Console.WriteLine("Order details: \n " + order[i].Date.ToShortDateString() + "\n Order ID: " + order[i].Id.ToString() + "\n Location of Store: " + order[i].Location.ToString() + "\n Pizza was: " + order[i].Pizza.toppings.topping.ToString() + "\n Amount: " + order[i].AmountOfPizza.ToString() + "\n The total was: " + order[i].Pizza.cost.ToString());

                }
            }


        }

        public void DisplayOrdersByUser(string email)
        {
            for (int i = 0; i < order.Count; i++)
            {
                if (order[i].User.Email.ToString() == email)
                {
                    Console.WriteLine($"UserID: {order[i].User.Id}");
                    Console.WriteLine("User: " + order[i].User.Name.First.ToString() + " " + order[i].User.Name.Last.ToString());
                    Console.WriteLine("Order details: \n " + order[i].Date.ToShortDateString() + "\n Order ID: " + order[i].Id.ToString() + "\n Location of Store: " + order[i].Location.ToString() + "\n Pizza was: " + order[i].Pizza.toppings.topping.ToString() + "\n Amount: " + order[i].AmountOfPizza.ToString() + "\n The total was: " + order[i].Pizza.cost.ToString());

                }
            }
        }

        public void SortByAll()
        {
            //List contains a method call OrderBy if given the correct parameters u can order the list in any way you want.   
            Console.WriteLine("\n Sorted by Acending Date: \n");
            List<Orders> SortedList = order.OrderBy(o => o.Date).ToList();

            foreach (var o in SortedList)
            {
                Console.Write(o.Date.ToString() + " Order ID: " + o.Id.ToString() + "\n Name of User: " + o.User.Name.First.ToString() + " " + o.User.Name.Last.ToString() + "\n Order Cost: " + o.Pizza.cost.ToString("C2"));
                Console.WriteLine();
            }
            Console.WriteLine();


            Console.WriteLine("\n Sorted by Decending Date: \n");
            List<Orders> SortedListDescending = order.OrderByDescending(o => o.Date).ToList();

            foreach (var o in SortedListDescending)
            {
                Console.Write(o.Date.ToString() + " Order ID: " + o.Id.ToString() + "\n Name of User: " + o.User.Name.First.ToString() + " " + o.User.Name.Last.ToString() + "\n Order Cost: " + o.Pizza.cost.ToString("C2"));
                Console.WriteLine();

            }
            Console.WriteLine();





            Console.WriteLine("\n Sorted by Acending Cost: \n");
            List<Orders> SortedListCost = order.OrderBy(o => o.Pizza.cost.ToString()).ToList();

            foreach (var o in SortedListCost)
            {
                Console.Write(o.Date.ToString() + " Order ID: " + o.Id.ToString() + "\n Name of User: " + o.User.Name.First.ToString() + " " + o.User.Name.Last.ToString() + "\n Order Cost: " + o.Pizza.cost.ToString("C2"));
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("\n Sorted by Decending Cost: \n");
            List<Orders> SortedListDescendingCost = order.OrderByDescending(o => o.Pizza.cost.ToString()).ToList();

            foreach (var o in SortedListDescendingCost)
            {
                Console.Write(o.Date.ToString() + " Order ID: " + o.Id.ToString() + "\n Name of User: " + o.User.Name.First.ToString() + " " + o.User.Name.Last.ToString() + "\n Order Cost: " + o.Pizza.cost.ToString("C2"));
                Console.WriteLine();

            }
            Console.WriteLine();






        }

        public void serializeToXML()
        {
            //USed to call the SerializeTOFile Method from another method.
            SerializeToFile(@"C:\Revature\Proyect 1\Main\Main\bin\Debug\netcoreapp2.1\data.xml", order);


        }


        public static void SerializeToFile(string fileName, List<Orders> order)
        {
            // XmlSerializer serializer = new XmlSerializer(typeof(Orders));

            //Example give in training session to serilaize.
            var serializer = new XmlSerializer(typeof(List<Orders>));
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Orders>));

                using (var memoryStream = new MemoryStream())
                {
                    using (fileStream = new FileStream(@"C:\Revature\Proyect 1\Main\Main\bin\Debug\netcoreapp2.1\data.xml", FileMode.Open))
                    {
                        await fileStream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Position = 0; // reset "cursor" of stream to beginning
                    order = (List<Orders>)serializer.Deserialize(memoryStream);
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


        #region Not Used Methods on Main
        // Method to deserialize a file from xlm format. Is not currently in use...
        public async static Task<IEnumerable<Orders>> DeserializeFromFileAsync(string fileName)
        {

            var serializer = new XmlSerializer(typeof(List<Orders>));

            using (var memoryStream = new MemoryStream())
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0; // reset "cursor" of stream to beginning

                return (List<Orders>)serializer.Deserialize(memoryStream);

            }
        }


        #endregion





    }
}
