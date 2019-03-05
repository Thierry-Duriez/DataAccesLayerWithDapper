using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DatasObjects;
using DatasAccesLayer;

namespace MainProjectExample
{
    class Program
    {
        static string cnxbdd = ConfigurationManager.AppSettings["connectionString"];

        static void Main(string[] args)
        {
            List<DTO_Product> lstAllRows = null;
            DataBaseWithDapper<DTO_Product> dbwd = new DataBaseWithDapper<DTO_Product>();            
            lstAllRows = dbwd.Repository_Bdd.GetAll().ToList();
        
        }

    }
}
