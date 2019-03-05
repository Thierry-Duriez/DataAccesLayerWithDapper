using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using DatasAccesLayer;

namespace DataAccesLayerTest
{
    [TestClass]
    public class TableSQLAttributeTest
    {
        [TestMethod]
        public void NameSQL_DefaultValue_GetEmptyValue()
        {
            TableSQLAttribute tsa = new TableSQLAttribute();

            Assert.AreEqual(string.Empty, tsa.NameSQL);
        }

        [TestMethod]
        public void NameSQL_CustomerValue_GetCustomerValue()
        {
            string testValue = "test";

            TableSQLAttribute tsa = new TableSQLAttribute();
            tsa.NameSQL = testValue;

            Assert.AreEqual(testValue, tsa.NameSQL);
        }

    }

    //-----------------------------------------------------------------------------------------------------------//

    [TestClass]
    public class FieldSQLAttributeTest
    {
        [TestMethod]
        public void Class_CountProperties_NumberOfAllProperties()
        {
            int NumberOfAllProperties = 12; //in the parent class, there is TypeId property

            Type type = typeof(FieldSQLAttribute);

            PropertyInfo[] propertyInfos = type.GetProperties();

            Assert.AreEqual(NumberOfAllProperties, propertyInfos.Length);
        }

        [TestMethod]
        public void Class_CountProperties_NameOfBoolProperties()
        {
            List<string> listNameOfBoolProperties = new List<string>()
            {
                "ExcluSQL",
                "PK",
                "FK",
                "IX",
                "Identity",
                "Nullable"
            };

            Type type = typeof(FieldSQLAttribute);

            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo pi in propertyInfos)
            {
                if (pi.PropertyType.Name == "Boolean")
                {
                    Assert.IsTrue(listNameOfBoolProperties.Exists(p => p == pi.Name), string.Format("{0} : property not include in test.", pi.Name));
                }
            }
        }

        [TestMethod]
        public void Class_CountProperties_NameOfStringProperties()
        {
            List<string> listNameOfStringProperties = new List<string>()
            {
                "NameSQL",
                "TypeSQL"
            };

            Type type = typeof(FieldSQLAttribute);

            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo pi in propertyInfos)
            {
                if (pi.PropertyType.Name == "String")
                {
                    Assert.IsTrue(listNameOfStringProperties.Exists(p => p == pi.Name), string.Format("{0} : property not include in test.", pi.Name));
                }
            }
        }

        [TestMethod]
        public void Class_CountProperties_NameOfIntProperties()
        {
            List<string> listNameOfIntProperties = new List<string>()
            {
                "LengthSQL",
                "PrecisionSQL",
                "OrdreSQL"
            };

            Type type = typeof(FieldSQLAttribute);

            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo pi in propertyInfos)
            {
                if (pi.PropertyType.Name == "Int32")
                {
                    Assert.IsTrue(listNameOfIntProperties.Exists(p => p == pi.Name), string.Format("{0} : property not include in test.", pi.Name));
                }                
            }
        }

    }
}
