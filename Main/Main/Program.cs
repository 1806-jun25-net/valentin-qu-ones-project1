using Pizza.Library.Pizza;
using System;
using System.Collections.Generic;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {

            int option = 1;
            Methods m = new Methods();
            //string[] toppingsOfPizza = new string[3] { "Bacon", "Peperonie", "Sausagge" };


            while (option != 0)
            {

                m.PrintAll();
                option = int.Parse(Console.ReadLine());

                if (option == 1)
                {
                    m.PopulateInventory();

                }
                if (option == 2)
                {



                    Console.WriteLine("These are the options for your pizza. Choose one: ");
                    m.PrintIngredients();
                    Console.WriteLine();


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

                                    Console.WriteLine("Enter your ID: ");
                                    string ID = Console.ReadLine();

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
                                    Console.WriteLine();
                                    #endregion

                                    Console.WriteLine("How many " + m.ingredients[pizzaOption].topping.ToString() + " Pizza do you want? ");
                                    pizzaQty = int.Parse(Console.ReadLine());

                                    if (int.TryParse(pizzaQty.ToString(), out i))
                                    {
                                        if (pizzaQty <= m.ingredients[pizzaOption].qty)
                                        {
                                            m.OrderPizza(int.Parse(ID), fName, lName, adLine, adCity, adState, pizzaOption, pizzaQty);

                                        }
                                        else
                                        {
                                            Console.WriteLine("We are low on stock at the moment. Please try again later. Thanks.");
                                        }


                                    }
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine($"Unexpected error: {ex.Message}");

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Unexpected error: {ex.Message}");

                                }

                            }

                        }


                    }








                }


            }


        }
    }
}
