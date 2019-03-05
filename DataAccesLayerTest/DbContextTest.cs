using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using DatasAccesLayer;

namespace DataAccesLayerTest
{
    [TestClass]
    public class DbConnectionTest
    {
        [TestMethod]
        public void ConnectionString_CustomerConnectionStringValue_GetConnectionStringValue()
        {
            string connectionString = "Server=xxx.xxx.xxx.xxx;Database=MyDatabase;User ID=MyUserId;Password=MyPassword";
            string connectionStringExpected = "";

            using (DbConnection connex = new DbConnection(connectionString))
            {
                connectionStringExpected = connex.ConnectionString;
            }

            Assert.IsTrue(connectionStringExpected == connectionString);
        }

        [TestMethod]
        public void ConnectionFactory_GetObjectForConnection_ObjectForConnectionIsNotNull()
        {
            bool objectForConnectionIsNotNull = false;

            using (DbConnection connex = new DbConnection())
            {
                objectForConnectionIsNotNull = (connex.ConnectionFactory != null);
            }

            Assert.IsTrue(objectForConnectionIsNotNull);
        }
    }
}
