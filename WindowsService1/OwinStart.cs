using System;
using Microsoft.Owin;
using Owin;
using System.ServiceProcess;
using System.Web;   
using System.Web.Http;


[assembly: OwinStartup(typeof(WindowsService1.Startup))]

namespace WindowsService1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            // Дополнительные сведения о настройке приложения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
