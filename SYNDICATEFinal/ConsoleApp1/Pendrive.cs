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
    public class PendriveRead
    {
        int price = 0;
        int qty = 0;
        int localPrice = 0;
        public void pendriveDisplay(dynamic select3)                                                 //fn to display laptop variants
        {
            Console.WriteLine("Shop-3: Pendrive Store");
            Console.WriteLine();
            Console.WriteLine("-----------------------Available Pendrive Variants----------------------");
            dynamic select = JsonConvert.DeserializeObject(select3);
            foreach (var i in select)
            {
                String id = i.Id;
                String brandname = i.Brand;
                String price_detail = i.Price;
                String model_detail = i.Model;
                String Stock = i.Stock;

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

            pendriveSelection(select);
        }
        public void pendriveSelection(dynamic variant3)                                            //fn to display laptop variant selected by the user
        {
           
            var user_id = Console.ReadLine();
            dynamic var3 = variant3;
            var flag = 0;
            int stock = 0;
            Program pur = new Program();
            PendriveRead pendrive = new PendriveRead();
            foreach (var i in variant3)
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

                    pendrive.price = Convert.ToInt32(price_detail);
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
                calculation(var3);
            }
            else
            {
                Console.WriteLine("Please Enter Correct Item Id:");
                pendriveSelection(var3);
            }

            String user_choice;
            void calculation(dynamic var33)
            {
                do
                {
                    Console.WriteLine();
                    Console.Write("Enter the Quantity Required:");
                    pendrive.qty = Convert.ToInt32(Console.ReadLine());
                    pendrive.localPrice = pendrive.qty * pendrive.price;
                    Console.WriteLine("Total Price: Rs. {0}", pendrive.localPrice);

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
                        stock = stock - pendrive.qty;
                        //Console.WriteLine(stock);
                        foreach (var i in var33)
                        {
                            if (user_id == i.Id.ToString())
                            {
                                i.Stock = stock;
                            }
                        }
                        string output = JsonConvert.SerializeObject(var33, Formatting.Indented);
                        File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Pendrive.json", output);
                        selection();
                    }
                } while (user_choice == "y");
            }
            void selection()
            {
                {
                    Program.quantity.Add(pendrive.qty);
                    Program.totalPrice.Add(pendrive.localPrice);
                    Program.purchasePrice += pendrive.localPrice;
                    Program.pendriveShopPrice += pendrive.localPrice;
                    Console.WriteLine("Do you want to purchase another item...(Y/N)");
                    string shopname = "Pendrive Shop";
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
                string json = File.ReadAllText(@"C:\Users\adharsh.s\Desktop\Publish\Pendrive.json");
                dynamic jObject = JsonConvert.DeserializeObject(json);
                Console.WriteLine(json);
                Console.WriteLine("1.Press 1 to Update-PRICE");
                Console.WriteLine("2.Press 2 to Update-STOCK");
                choiceupd = Convert.ToInt32(Console.ReadLine());
                if (choiceupd == 1)
                {
                    Console.Write("Enter  ID of the ITEM to Update Price : ");
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
                        File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Pendrive.json", output);
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
                        File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Pendrive.json", output);
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
            var json = File.ReadAllText(@"C:\Users\adharsh.s\Desktop\Publish\Pendrive.json");
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
                        File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Pendrive.json", output);
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
            Console.WriteLine("Enter the new Pendrive details to the existing Catalogue");
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
                var json = File.ReadAllText(@"C:\Users\adharsh.s\Desktop\Publish\Pendrive.json");
                dynamic jObject = JsonConvert.DeserializeObject(json);

                dynamic newitem = JsonConvert.DeserializeObject(newdevice);

                jObject.Add((newitem));
                dynamic newJsonResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);

                
                File.WriteAllText(@"C:\Users\adharsh.s\Desktop\Publish\Pendrive.json", newJsonResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Add Error : " + ex.Message.ToString());
            }
        }
    }
}