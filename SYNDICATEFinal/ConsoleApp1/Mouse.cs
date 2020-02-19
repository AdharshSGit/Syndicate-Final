using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using WebApplication1;


namespace ConsoleApp1
{
    public class MouseRead
    {
        int price = 0; 
        int qty = 0;
        int localPrice = 0;
        public void mouseDisplay(dynamic select2)                   //fn to display laptop variants
        {
            Console.WriteLine("Shop-2: Mouse Store");
            Console.WriteLine();
            Console.WriteLine("-----------------------Available Mouse Variants----------------------");
            dynamic select = JsonConvert.DeserializeObject(select2);
            foreach (var i in select)
            {
                String id = i.Id;
                String brandname = i.Brand;
                String price_detail = i.Price;
                String model_detail = i.Model;
                String Stock = i.Stock;
                //String stock = lap.Element("stock").Value;

                Console.WriteLine("Id: {0}", id);
                Console.WriteLine("Brand: {0}", brandname);
                Console.WriteLine("Model: {0}", model_detail);
                Console.WriteLine("Price: Rs. {0}", price_detail);
                Console.WriteLine("Stock:{0}", Stock);
                //Console.WriteLine("Stock: {0}", stock);
                Console.WriteLine("-----------------------------------------------------------------------");
            }
            Console.WriteLine();
            Console.Write("Please Enter the Item Id you wish to buy -");

            mouseSelection(select);
        }
        public void mouseSelection(dynamic variant2 )                                            //fn to display laptop variant selected by the user
        {
            
            var user_id = Console.ReadLine();
            Program pur = new Program();
            MouseRead mouse = new MouseRead();
            dynamic var2 = variant2; int stock = 0;
            var flag = 0;
            foreach (var i in var2)
            {
                if (user_id == i.Id.ToString())
                {
                    Console.WriteLine();
                    Console.WriteLine("---------------------------Your Selection-----------------------------");
                    Console.WriteLine();
                    String id = i.Id;
                    String brandname = i.Brand;
                    String price_detail = i.Price;
                    String model_detail = i.Model;
                    String Stock = i.Stock;

                    mouse.price = Convert.ToInt32(price_detail);
                    stock = Convert.ToInt32(Stock);
                    Program.brandCart.Add(brandname);
                    Program.priceCart.Add(price_detail);
                    Program.modelCart.Add(model_detail);

                    Console.WriteLine("Id: {0}", id);
                    Console.WriteLine("Brand: {0}", brandname);
                    Console.WriteLine("Model: {0}", model_detail);
                    Console.WriteLine("Price: Rs. {0}", price_detail);
                    Console.WriteLine("----------------------------------------------------------------------");

                    flag = 1;
                }
            }
            if (flag == 1)
            {
                calculation(var2);
            }
            else
            {
                Console.WriteLine("Please Enter Correct Item Id:");
                mouseSelection(var2);
            }
            
            String user_choice;

            void calculation(dynamic var22)
            {
                do
                {
                    Console.WriteLine();
                    Console.Write("Enter the Quantity Required:");
                    mouse.qty = Convert.ToInt32(Console.ReadLine());
                    mouse.localPrice = mouse.qty * mouse.price;
                    Console.WriteLine("Total Price: Rs. {0}", mouse.localPrice);

                    Console.WriteLine();
                    Console.WriteLine("Do you want to Change quantity? (y/n)");
                    user_choice = Console.ReadLine();
                    Console.WriteLine();

                    while ((user_choice != "y") && (user_choice != "n"))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid Option. Please Choose 'y' or 'n'");
                        user_choice = Console.ReadLine();
                    }

                    if (user_choice == "n")
                    {
                        stock = stock - mouse.qty;
                        //Console.WriteLine(stock);
                        foreach (var i in var22)
                        {
                            if (user_id == i.Id.ToString())
                            {
                                i.Stock = stock;
                            }
                        }
                        string output = JsonConvert.SerializeObject(var22, Formatting.Indented);
                        File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Mouse.json", output);
                        selection();
                    }
                } while (user_choice == "y");
            }

            void selection()
            {   
               {
                    string shopname = "Pendrive Shop";
                    Program.quantity.Add(mouse.qty);
                    Program.totalPrice.Add(mouse.localPrice);
                    Program.purchasePrice += mouse.localPrice;
                    Program.mouseShopPrice += mouse.localPrice;
                    Console.WriteLine("Do you want to purchase another item...(Y/N)");
                    string choice = Console.ReadLine();
                    if (choice == "y" || choice == "Y")
                    {
                        pur.purchase1();
                    }
                    else if (choice == "N" || choice == "n")
                    {
                        pur.display(shopname);
                    }
                    else
                    {
                        Console.WriteLine("Please Enter Y or N...");
                        selection();
                    }
               }
            }
        }
        public void updateDetail()
        {
            try
            {
                string output;
                var flag = 0;
                var choiceupd = 0;
                string json = File.ReadAllText(@"C:\Users\adharsh.s\Desktop\Publish\Mouse.json");
                dynamic jObject = JsonConvert.DeserializeObject(json);
                Console.WriteLine(json);
                Console.WriteLine("1.Press 1 to Update-PRICE");
                Console.WriteLine("2.Press 2 to Update-STOCK");
                choiceupd = Convert.ToInt32(Console.ReadLine());
                if (choiceupd == 1)
                {
                    Console.Write("Enter  ID of the ITEM to Update Price :");
                    string Id = Console.ReadLine();

                    foreach (var i in jObject)
                    {
                        if (Id == i.Id.ToString())
                        {
                            Console.Write("Enter new price : ");
                            dynamic price = Convert.ToInt32(Console.ReadLine());
                            i.Price = price;
                            flag = 1;
                        }
                    }
                    if (flag == 1)
                    {
                        output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                        File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Mouse.json", output);
                        Console.WriteLine("Price Updated");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("InValid Id");
                        Console.ReadLine();
                        updateDetail();
                    }
                }
                else if (choiceupd == 2)
                {
                    Console.WriteLine("Enter the ID of the ITEM to Update Stock");
                    string Id = Console.ReadLine();
                    foreach (var i in jObject)
                    {
                        if (Id == i.Id.ToString())
                        {
                            Console.WriteLine("Enter the updated Stock level");
                            dynamic stocknew = Convert.ToInt32(Console.ReadLine());
                            i.Stock = stocknew;
                            flag = 1;
                        }
                    }
                    if (flag == 1)
                    {
                        output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                        File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Mouse.json", output);
                        Console.WriteLine("Stock Updated");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("InValid Id");
                        Console.ReadLine();
                        updateDetail();
                    }

                }
                else
                {
                    Console.WriteLine("Enter the valid choice from list !");
                    updateDetail();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Update Error : " + ex.Message.ToString());

            }
        }

        public void deleteDetail()
        {
            var json = File.ReadAllText(@"C:\Users\adharsh.s\Desktop\Publish\Mouse.json");
            Console.WriteLine(json);
            try
            {
                var flag = 0;
                dynamic jObject = JsonConvert.DeserializeObject(json);
                Console.Write("Enter ID from the ITEM to Delete from Catalogue : ");
                string Id = Console.ReadLine();

                foreach (var i in jObject)
                {
                    if (Id == i.Id.ToString())
                    {
                        i.Remove();
                        string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                        File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Mouse.json", output);
                        Console.WriteLine("Deletion Done");
                        flag = 1;
                    }
                }

                if (flag == 0)
                {
                    Console.WriteLine("Invalid ID");
                    deleteDetail();

                }
            }
            catch (Exception)
            {
                Console.WriteLine();

            }
        }
        public void addDetail()
        {
            Console.Write("Instructions :");
            Console.Write("'Brand','Price' - Add the Details in single Quotes('_')");
            Console.WriteLine();
            Console.WriteLine("Enter the new mouse details to the existing Catalogue");
            Console.Write("ID :");
            var Idadd = (Console.ReadLine());
            Console.Write("Brand :");
            string Brandadd = (Console.ReadLine());
            Console.Write("Model :");
            string Modeladd = (Console.ReadLine());
            Console.Write("Price :");
            var PriceAdd = Convert.ToInt32(Console.ReadLine());
            Console.Write("Stock :");
            var stockAdd = Convert.ToInt32(Console.ReadLine());
            var newdevice = "{'Id':" + Idadd.ToString() + ",'Brand':" + Brandadd.ToString() + ",'Model':"
                            + Modeladd.ToString() + ",'Price':" + PriceAdd + ",'Stock':" + stockAdd + "}";

            try
            {
                var json = File.ReadAllText(@"C:\Users\adharsh.s\Desktop\Publish\Mouse.json");
                dynamic jObject = JsonConvert.DeserializeObject(json);
                dynamic newitem = JsonConvert.DeserializeObject(newdevice);
                jObject.Add((newitem));
                dynamic newJsonResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Mouse.json", newJsonResult);
 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Add Error : " + ex.Message.ToString());
            }
        }
    }
}