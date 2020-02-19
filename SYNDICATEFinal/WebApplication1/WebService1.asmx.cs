using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication1
{
 
    public class WebService1 : System.Web.Services.WebService
    {
       
        [WebMethod]
        public String Getlaptop()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\adharsh.s\Desktop\Publish\Laptop.json"))
            {
                dynamic laptop = r.ReadToEnd();
                return laptop;
            }
        }


        [WebMethod]
        public String Getmouse()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\adharsh.s\Desktop\Publish\Mouse.json"))
            {
                dynamic mouse = r.ReadToEnd();
                return mouse;
            }
        }

        [WebMethod]
        public String Getpendrive()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\adharsh.s\Desktop\Publish\Pendrive.json"))
            {
                dynamic pendrive = r.ReadToEnd();
                return pendrive;
            }
        }



    }
}
