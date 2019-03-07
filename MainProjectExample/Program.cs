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
            //For Insert one row
            DTO_Product insertRow = new DTO_Product()
            {
                IsNew = true,
                ProductId = -1,
                RegionId = 1,
                ProductCode = "ProdCode",
                ProductLabel = "My Product Label",
                IsActive = true,
                ProductDateCreation = DateTime.Now
            };

            DataBaseWithDapper<DTO_Product> dbwdInsert = new DataBaseWithDapper<DTO_Product>();
            int id = dbwdInsert.Repository_Bdd.Insert(insertRow);


            //For Update one row
            DTO_Product updateRow = new DTO_Product()
            {
                IsNew = false,
                ProductId = 1,
                RegionId = 1,
                ProductCode = "NewProdCode",
                ProductLabel = "New Product Label",
                IsActive = true,
                ProductDateCreation = DateTime.Now
            };

            DataBaseWithDapper<DTO_Product> dbwdUpdate = new DataBaseWithDapper<DTO_Product>();
            bool updateOK = dbwdInsert.Repository_Bdd.Update(updateRow);


            //For Delete one row
            DTO_Product deleteRow = new DTO_Product()
            {
                IsNew = false,
                ProductId = 1,
            };
            DataBaseWithDapper<DTO_Product> dbwdDelete = new DataBaseWithDapper<DTO_Product>();
            bool DeleteOK = dbwdInsert.Repository_Bdd.Delete(deleteRow);


            //For Extract all rows
            List<DTO_Product> lstAllRows = null;
            DataBaseWithDapper<DTO_Product> dbwdAllSelect = new DataBaseWithDapper<DTO_Product>();            
            lstAllRows = dbwdAllSelect.Repository_Bdd.GetAll().ToList();


            //For Extract all rows WithCriteria
            List<DTO_Product> lstAllRowsWithCriteria = null;
            DataBaseWithDapper<DTO_Product> dbwdAllSelectWithCriteria = new DataBaseWithDapper<DTO_Product>();
            lstAllRowsWithCriteria = dbwdAllSelectWithCriteria.Repository_Bdd.GetAllWithCriteria("ProductLabel like '%label%'").ToList();


            //For Extract one row
            DTO_Product selectOneRow = new DTO_Product()
            {
                IsNew = false,
                ProductId = 1,
            };
            DataBaseWithDapper<DTO_Product> dbwdOneSelect = new DataBaseWithDapper<DTO_Product>();
            selectOneRow = dbwdAllSelect.Repository_Bdd.GetSingleByPK(selectOneRow);

        }

    }
}
