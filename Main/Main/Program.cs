using Pizza.Library.Pizza;
using System;
using System.Collections.Generic;
using NLog;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Database

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            Console.WriteLine(configuration.GetConnectionString("PizzaPalace"));



            #endregion


            int option = 1;
            Methods m = new Methods();
            


            while (option != 0)
            {
                try
                {
                    m.PrintAll();
                    option = int.Parse(Console.ReadLine());

                    //This if will call a method an populate the inventory.
                    if (option == 1)
                    {
                        m.PopulateInventory();

                    }

                    //This if will contain the order info to pass it to parameters to another class so it can be processed.
                    if (option == 2)
                    {



                        Console.WriteLine("These are the options for your pizza. Choose one: ");
                        m.PrintIngredients();
                        Console.WriteLine();

                        try
                        {
                            int pizzaOption = int.Parse(Console.ReadLine());
                            int pizzaQty = 0;
                            int i;
                            int count = 0;

                            Random r = new Random();//For random integers that would be use to create random ID's.
                            int rInt = r.Next(1, 100);//With a range of 1 to 100.




                            if (int.TryParse(pizzaOption.ToString(), out i))
                            {
                                count++;
                                if (count == 1)
                                {
                                    if (pizzaOption >= 0 && pizzaOption <= 2)
                                    {
                                        try
                                        {

                                            #region UserInfo
                                            Console.WriteLine("Lets place a order but first. Fill the require  information about you. ");
                                            Console.WriteLine("Order ID: " + rInt);
                                            Console.WriteLine("What is the location of the store: ");
                                            string location = Console.ReadLine();
                                            Console.WriteLine("Enter your ID: ");
                                            string ID = Console.ReadLine();
                                            Console.WriteLine("Enter your Email: ");
                                            string email = Console.ReadLine();

                                            Console.WriteLine("Enter your First Name: ");
                                            string fName = Console.ReadLine();
                                            Console.WriteLine("Enter your Last Name");
                                            string lName = Console.ReadLine();
                                            Console.WriteLine("Enter your Address: ");
                                            string adLine = Console.ReadLine();
                                            Console.WriteLine("Enter your City: ");
                                            string adCity = Console.ReadLine();
                                            Console.WriteLine("Enter your State: ");
                                            string adState = Console.ReadLine();

                                            #endregion

                                            Console.WriteLine("How many " + m.ingredients[pizzaOption].topping.ToString() + " Pizza do you want? ");
                                            pizzaQty = int.Parse(Console.ReadLine());
                                            if (m.IsValidEmail(email) == true)
                                            {
                                                if (int.TryParse(pizzaQty.ToString(), out i))
                                                {
                                                    if (pizzaQty <= m.ingredients[pizzaOption].qty)
                                                    {
                                                        m.OrderPizza(rInt, location, int.Parse(ID), fName, lName, adLine, adCity, adState, pizzaOption, pizzaQty, DateTime.Now, email);

                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("We are low on stock at the moment. Please try again later. Thanks.");
                                                    }


                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid Email!");

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

                    if (option == 3)
                    {
                        Console.WriteLine("Enter a user ID to search the suggested pizza: ");
                        m.PrintSuggestedOrder(int.Parse(Console.ReadLine()));


                    }


                    if (option == 4)
                    {
                        Console.WriteLine("Enter a Name to display its data: ");
                        m.SearchUser(Console.ReadLine());

                    }


                    if (option == 5)
                    {
                        Console.WriteLine("Enter the order ID you want to display: ");
                        m.DisplayOrderByID(int.Parse(Console.ReadLine()));

                    }

                    if (option == 6)
                    {
                        Console.WriteLine("What is the location of the store you are looking for? ");
                        m.DisplayOrderByLocation(Console.ReadLine());
                    }

                    if (option == 7)
                    {
                        Console.WriteLine("Enter a email from user you want to view the order history from: ");
                        m.DisplayOrdersByUser(Console.ReadLine());

                    }

                    if (option == 8)
                    {
                        m.SortByAll();
                    }

                    if (option == 9)
                    {
                        m.serializeToXML();

                    }

                    if (option == 10)
                    {

                        m.DezerializedAsync();
                    }

                }
                catch (FormatException ex)
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.ErrorException("Format Error: ", ex);
                    Console.WriteLine(ex);

                }
            }



        }

    }


}

