using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using DatasAccesLayer;

namespace DataAccesLayerTest
{
    [TestClass]
    public class RequestFactoryTest
    {
        DTO_Product dtoProduct = null;

        public RequestFactoryTest()
        {
            dtoProduct = new DTO_Product()
            {
                ProductId = 1,
                RegionId = 2,
                ProductCode = "PRD001",
                ProductLabel = "Produit001",
                IsActive = true,
                ProductDateCreation = DateTime.Parse("01/01/2019 12:01:02")
            };
        }

        [TestMethod]
        public void GetFieldSQLAttribute_GetRequestForType_Object()
        {
            RequestFactory<DTO_Product> rf = new RequestFactory<DTO_Product>();

            Type type = typeof(DTO_Product);

            PropertyInfo[] propertyInfos = type.GetProperties();

            FieldSQLAttribute fsa = rf.GetFieldSQLAttribute(propertyInfos[0]);

            Assert.IsNotNull(fsa);
            Assert.IsTrue(fsa.NameSQL == "ProductId");
        }

        [TestMethod]
        public void GetRequestSelect_GetRequestForType_RequestSelect()
        {
            string requestExpected = "SELECT [ProductId],[RegionId],[ProductCode],[ProductLabel],[IsActive],[ProductDateCreation] FROM [dbo].[DTO_Product] ";

            RequestFactory<DTO_Product> rf = new RequestFactory<DTO_Product>();

            string requestGenerated = rf.GetRequestSelect();

            Assert.AreEqual(requestGenerated, requestExpected);
        }

        [TestMethod]
        public void GetRequestSelectWithCriteriaOnPK_GetRequestForType_RequestSelectWithCriteriaOnPK()
        {
            string requestExpected = "SELECT [ProductId],[RegionId],[ProductCode],[ProductLabel],[IsActive],[ProductDateCreation] FROM [dbo].[DTO_Product]  WHERE [ProductId] = @productid ";

            RequestFactory<DTO_Product> rf = new RequestFactory<DTO_Product>();

            string requestGenerated = rf.GetRequestSelectWithCriteriaOnPK();

            Assert.AreEqual(requestGenerated, requestExpected);
        }

        [TestMethod]
        public void GetRequestInsert_GetRequestForType_RequestInsert()
        {
            string requestExpected = "INSERT INTO [dbo].[DTO_Product] ([RegionId],[ProductCode],[ProductLabel],[IsActive],[ProductDateCreation]) VALUES (@regionid, @productcode, @productlabel, @isactive, @productdatecreation) ";

            RequestFactory<DTO_Product> rf = new RequestFactory<DTO_Product>();

            string requestGenerated = rf.GetRequestInsert();

            Assert.AreEqual(requestGenerated, requestExpected);
        }

        [TestMethod]
        public void GetRequestUpdate_GetRequestForType_RequestUpdate()
        {
            string requestExpected = "UPDATE [dbo].[DTO_Product] SET [RegionId] = @regionid, [ProductCode] = @productcode, [ProductLabel] = @productlabel, [IsActive] = @isactive, [ProductDateCreation] = @productdatecreation WHERE [ProductId] = @productid ";

            RequestFactory<DTO_Product> rf = new RequestFactory<DTO_Product>();

            string requestGenerated = rf.GetRequestUpdate();

            Assert.AreEqual(requestGenerated, requestExpected);
        }

        [TestMethod]
        public void GetRequestDelete_GetRequestForType_RequestDelete()
        {
            string requestExpected = "DELETE FROM [dbo].[DTO_Product] WHERE [ProductId] = @productid ";

            RequestFactory<DTO_Product> rf = new RequestFactory<DTO_Product>();

            string requestGenerated = rf.GetRequestDelete();

            Assert.AreEqual(requestGenerated, requestExpected);
        }
    }
}
