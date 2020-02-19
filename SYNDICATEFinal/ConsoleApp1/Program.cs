using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using WebApplication1;
using MultiClient;

namespace ConsoleApp1
{
    public class Program
    {
        public static int purchasePrice = 0;
        public static int laptopShopPrice = 0;
        public static int pendriveShopPrice = 0;
        public static int mouseShopPrice = 0;
        public static ArrayList brandCart = new ArrayList();
        public static ArrayList priceCart = new ArrayList();
        public static ArrayList modelCart = new ArrayList();
        public static ArrayList quantity = new ArrayList();
        public static ArrayList totalPrice = new ArrayList();

        
        public void purchase(string shopname)
        {
            Program pur = new Program();
            Console.WriteLine(shopname);
            Console.WriteLine("Laptop Shop Total Price: Rs. {0}",laptopShopPrice);
            Console.WriteLine("Mouse Shop Total Price: Rs. {0}", mouseShopPrice);
            Console.WriteLine("Pendrive Shop Total Price: Rs. {0}", pendriveShopPrice);
            Console.ReadLine();
            Console.WriteLine("Cart Total Price: Rs. {0}", purchasePrice);
            //Console.ReadLine();
            
      
            MultiClient.Program display = new MultiClient.Program();
            display.transactDisplay(shopname);
        }

    public void display(string shop)                              //Function to display cart items
        {
            Console.Clear();
            Console.WriteLine(shop);
            Console.WriteLine("Your Cart Items:");
            Console.WriteLine();
            cart();
            purchase(shop);
        }

        public void purchase1()                            //Function to display shops for selection
        {
            localhost.WebService1 obj = new localhost.WebService1();
            LaptopRead lap = new LaptopRead();
            MouseRead mouse = new MouseRead();
            PendriveRead pendrive = new PendriveRead();

            int choice;

            Console.Clear();
            Console.WriteLine("***************************** Welcome ******************************");
            Console.WriteLine();
            Console.WriteLine("------------Select the Shop 1/2/3 to display the Items--------------");
            Console.WriteLine();
            Console.WriteLine("-------> Shop-1: Laptop  Shop-2: Mouse  Shop-3: Pendrives <-------");

            Console.WriteLine();
            Console.Write("Choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    dynamic x = obj.Getlaptop();
                    lap.lapDisplay(x);
                    Console.ReadKey();
                    break; 
                case 2:
                    dynamic z = obj.Getmouse();
                    mouse.mouseDisplay(z);
                    Console.ReadKey();
                    break;
                case 3:
                    dynamic y = obj.Getpendrive();
                    pendrive.pendriveDisplay(y);
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Invalid choice - Please enter 1/2/3");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }
        public void cart()                         //fn to add variant selected by the user into cart
        {
            Console.WriteLine("Brand:  ");
            foreach (var i in brandCart)
            {
                Console.WriteLine(i + "   ");
            }
            Console.WriteLine();
            Console.WriteLine("Model:  ");
            foreach (var i in modelCart)
            {
                Console.WriteLine(i + "   ");
            }
            Console.WriteLine();
            Console.WriteLine("Price:  ");
            foreach (var i in priceCart)
            {
                Console.WriteLine("Rs." + i + "   ");
            }
            Console.WriteLine();
            Console.WriteLine("Quantity:  ");
            foreach (var i in quantity)
            {
                Console.WriteLine(i + "   ");
            }
            Console.WriteLine();
            Console.WriteLine("Total Price:  ");
            foreach (var i in totalPrice)
            {
                Console.WriteLine("Rs." + i + "   ");
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
