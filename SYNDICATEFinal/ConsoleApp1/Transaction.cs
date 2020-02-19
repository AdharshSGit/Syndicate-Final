using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace ConsoleApp1
{
    public class Transaction
    {
        private readonly Timer timer;
        public Transaction()
        {
            timer = new Timer(5000) { AutoReset = true };
            timer.Elapsed += Timer_Elapsed;
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string[] lines = new string[] { DateTime.Now.ToString() };
            File.AppendAllLines(@"C:\Users\adharsh.s\Desktop\Final\Final\ASMX-SC\ASMX\ConsoleApp1\bin\Debug\transaction.txt", lines);
        }
        public void demo(string t4, string t1, string t2, string t3)
        {

            string[] a = new string[] { t4, "LAPTOP product:", t1, "MOUSE product:", t2, "PENDRIVE product:", t3 };

            File.AppendAllLines(@"C:\Users\adharsh.s\Desktop\Final\Final\ASMX-SC\ASMX\ConsoleApp1\bin\Debug\transaction.txt", a);

        }
        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    
    }
}

