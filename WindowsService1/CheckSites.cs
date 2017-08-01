using System;
using System.Net;
using System.Timers;
using System.IO;
using System.Threading;
using Quartz;
using Quartz.Impl;

namespace WindowsService1
{
     
    public class CheckSites
    {

        public CheckSites()
        {
            this.url = "";
        }
        protected string url;
        public CheckSites(string url)
        {
            this.url = url;
        }
         public void InpBool(object sourse, System.Timers.ElapsedEventArgs e)
        {
            FileInput(testSite());
        }
        virtual public void FileInput(bool b)
        {   
            using (StreamWriter writer = File.CreateText("siteStatus.txt"))
            {
                writer.WriteLine(this.url + " : " + b);
            }
        }

        public bool testSite()
        {
            Uri uri = new Uri(url);
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
    
    public  class Class_Unit_test_FileInput:CheckSites
    {
        public Class_Unit_test_FileInput()
        {
            url = "";
        }
        public Class_Unit_test_FileInput(string uri): base(uri) { }
        public override void FileInput(bool b)
        {
            using (StreamWriter writer = File.CreateText("Test_txt_document.txt"))
            {
                writer.WriteLine(this.url + " : " + b);
                
            };
        }
    }


}
