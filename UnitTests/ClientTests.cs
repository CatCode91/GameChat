using ClientLibrary;
using ClientLibrary.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void CreateGoodConnectionObject()
        {
            Connection conn = new Connection("127.0.0.1", "8050");
            Assert.IsNotNull(conn);
        }


        [TestMethod]
        public void CheckConnection()
        {
            ChatClient client = new ChatClient();
            Connection conn = new Connection("127.0.0.1", "8050");
            client.ConnectToServer(conn);
        }
        
    }

}
