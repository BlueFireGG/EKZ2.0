using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Pages;

namespace WpfApp4.Class
{
    public static class Nextpage
    {
        private static Welcome welcome;
        private static Employeer employeer;

        public static Welcome GetWelcome()
        {
            if (welcome == null)
            {
                welcome = new Welcome();
            }
            return welcome;
        }
        public static Employeer GetEmployeer()
        {
            if (employeer == null)
            {
                employeer = new Employeer();
            }
            return employeer;
        }
    }
}
