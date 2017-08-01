using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsService1;
using System.IO;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Diagnostics;
using System.Web;
using System.Net.Http;
using System.Web.Http;


namespace UnitTestModuleCheckSites
{
    [TestClass]
    public class UnitTestClassCheckSites
    {
        [TestMethod]
        public void TestSite_google_true_returned()
        {
            string google1 = "http://google.com/";
            bool expected = true;
            //
            CheckSites check = new CheckSites(google1);
            bool returned = check.testSite();
            //
            Assert.AreEqual(expected, returned);
           
        }
        [TestMethod]
        public void TestSite_google345_false_returned()
        {
            
            string google2 = "http://google345.com/";
            bool expected = false;
            //
            CheckSites check1 = new CheckSites(google2);
            bool returned = check1.testSite();
            //
            Assert.AreEqual(expected, returned);
        }
        [TestMethod]
        public void TestSite_FileInput_true_expected_true()
        {
            
            string output;
            bool message = true;
            Class_Unit_test_FileInput test = new Class_Unit_test_FileInput("http://google345.com/");
            test.FileInput(message);
            using (StreamReader reader = File.OpenText("Test_txt_document.txt"))
            {
                output=reader.ReadLine().ToString();
            }
            Assert.AreEqual("http://google345.com/ : "+message.ToString(), output);
        }
    }
    [TestClass]
    public class TestMethodCheckStatus
    {
        ServiceController controller = new ServiceController("MyService");
        [TestMethod]
        public void Test_Service_Run_True_return()
        {
            bool expected = true;
            controller.Close();
            controller.Start();
            StatusController input = new StatusController();
            Assert.AreEqual(expected, input.CheckStatus());


        }
        [TestMethod]
        public void Test_Service_Close_False_return()
        {
            bool expected = false;
            controller.Close();
            StatusController input = new StatusController();
            Assert.AreEqual(expected, input.CheckStatus());
        }
        
      
    }

}
