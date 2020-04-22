using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Stub
{
    class Program
    {
        static void Main(string[] args)
        {
            ClickDetector.ListenForMouseEvents();
            //Run the app as a windows forms application
            Application.Run(new ApplicationContext());
        }
    }
}
