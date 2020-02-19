using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using WebApplication1;


namespace ConsoleApp1
{
    public  class LaptopRead
    {
        public static void LapDisplay()                                                 //fn to display laptop variants
        {
            Console.ReadKey();
            Console.WriteLine("Shop-1: Laptop Store");
            Console.WriteLine();
            Console.WriteLine("-----------------------Available Laptop Variants----------------------");
            foreach (var i in variant)
            {
                String id = i.Id;
                String brandname = i.Brand;
                String price_detail = i.Price;
                String model_detail = i.Model;
                //String stock = lap.Element("stock").Value;

                Console.WriteLine("Id: {0}", id);
                Console.WriteLine("Brand: {0}", brandname);
                Console.WriteLine("Model: {0}", model_detail);
                Console.WriteLine("Price: Rs. {0}", price_detail);
                //Console.WriteLine("Stock: {0}", stock);
                Console.WriteLine("-----------------------------------------------------------------------");
            }
            Console.WriteLine();
            Console.Write("Please Enter the Item Id you wish to buy -");

            LapSelection();
        }
        public static void LapSelection()                                            //fn to display laptop variant selected by the user
        {
            int price = 0;
            //int qty = 0;
            //int localprice = 0;
            var user_id = Console.ReadLine();
            //Console.Clear();
            localhost.WebService1 obj1 = new localhost.WebService1();
            var x = obj1.GetLaptopJSON(); ;

            dynamic variant1 = JsonConvert.DeserializeObject(x);

            foreach (var i in variant1)
            {
                if (user_id == i.Id.ToString())
                {
                    String id = i.Id;
                    String brandname = i.Brand;
                    String price_detail = i.Price;
                    String model_detail = i.Model;

                    price = Convert.ToInt32(price_detail);

                    Program.brandcart.Add(brandname);
                    Program.pricecart.Add(price_detail);
                    Program.modelcart.Add(model_detail);

                    Console.WriteLine("Id: {0}", id);
                    Console.WriteLine("Brand: {0}", brandname);
                    Console.WriteLine("Model: {0}", model_detail);
                    Console.WriteLine("Price: Rs. {0}", price_detail);
                }
            }
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("---------------------------Your Selection-----------------------------");
            Console.WriteLine();
        }

    }
}