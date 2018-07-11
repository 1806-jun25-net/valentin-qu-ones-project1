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
                        m.LoadFromDB();

                    }

                    //This if will contain the order info to pass it to parameters to another class so it can be processed.
                    if (option == 2)
                    {



                        Console.WriteLine("These are the options for your pizza. Choose one: ");
                        m.PrintPizza();
                        Console.WriteLine();

                        try
                        {
                            int pizzaOption = int.Parse(Console.ReadLine());
                            int pizzaQty = 0;
                            int i;
                            int count = 0;

                            if (int.TryParse(pizzaOption.ToString(), out i))
                            {
                                count++;
                                if (count == 1)
                                {
                                    if (pizzaOption >= 0)
                                    {
                                        try
                                        {

                                            #region UserInfo
                                            Console.WriteLine("Lets place a order but first. Fill the require  information about you. ");
                                            Console.WriteLine("Enter the First Name: ");
                                            string fName = Console.ReadLine();
                                            Console.WriteLine("Enter the Last Name: ");
                                            string lName = Console.ReadLine();
                                            m.SearchUser(fName,lName);

                                            Console.WriteLine("Enter the user ID you want to use the order with: ");
                                            int userID = int.Parse(Console.ReadLine());

                                            m.PrintLocations();
                                            Console.WriteLine("What is the location ID of the store: ");
                                            int locationID = int.Parse(Console.ReadLine());

                                            
                                            #endregion

                                            Console.WriteLine("How many " + m.piz[pizzaOption].NamePizza.ToString() + " Pizza do you want? ");
                                            pizzaQty = int.Parse(Console.ReadLine());
                                            
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
                       // m.getCountOfList();

                    }


                    if (option == 4)
                    {
                        Console.WriteLine("Enter a Name to display its data: ");
                        var name = Console.ReadLine();
                        Console.WriteLine("Enter Last Name: ");
                        var lName = Console.ReadLine();
                        m.SearchUser(name, lName);

                    }


                    if (option == 5)
                    {
                        Console.WriteLine("Enter the order ID you want to display: ");
                        m.DisplayOrderByID(int.Parse(Console.ReadLine()));

                    }

                    if (option == 6)
                    {
                        Console.WriteLine("What is the location of the store you are looking for? ");
                        m.DisplayOrderByLocation(int.Parse(Console.ReadLine()));
                    }

                    if (option == 7)
                    {
                        Console.WriteLine("Enter a ID from user you want to view the order history from: ");
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

