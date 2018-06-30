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
                    Ingredients ingredients;


                    Console.WriteLine("These are the options for your pizza");
                    m.PrintIngredients();
                    Console.WriteLine();

                    Console.WriteLine("Lets place a order but first. Fill the require information. ");






                }


            }


        }
    }
}
