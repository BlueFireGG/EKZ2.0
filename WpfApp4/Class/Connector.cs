using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Class
{
    public static class Connector
    {
        private static DB.EkzEntities DatabaseConnector;

        public static DB.EkzEntities GetDatabase()
        {
            if (DatabaseConnector == null)
            {
                DatabaseConnector = new DB.EkzEntities();
            }
            return DatabaseConnector;
        }
    }
}
