using System.ServiceProcess;

using System.Web.Http;
using System.Timers;
using Quartz;
using Quartz.Impl;
using System;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        ISchedulerFactory schedFact = new StdSchedulerFactory();
        Timer google;
        Timer apple;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            Console.WriteLine("Working...");

            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            IJobDetail job = JobBuilder.Create<Myjob>()
      .WithIdentity("job1", "group1")
      .Build();
            DateTime myTimeToStartFiring = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 15, 0);
            ITrigger trigger = TriggerBuilder.Create()
        .WithIdentity("trigger1", "group1")
        .StartAt(myTimeToStartFiring) // if a start time is not given (if this line were omitted), "now" is implied
        .WithSimpleSchedule(x => x
        .WithIntervalInHours(48)
        .RepeatForever())
        .ForJob(job) // identify job with handle to its JobDetail itself                   
        .Build();
            sched.ScheduleJob(job, trigger);
            CheckSites chechGoogle = new CheckSites("https://www.google.com");
            CheckSites chechApple = new CheckSites("https://www.apple.com");
            google = new Timer();
            google.Elapsed += chechGoogle.InpBool;
            google.Interval = 120000;
            apple = new Timer();
            apple.Elapsed += chechApple.InpBool;
            apple.Interval = 300000;
            apple.Start();
            google.Start();
        }


        protected override void OnStop()
        {
            schedFact.AllSchedulers.Clear();
            apple.Close();
            google.Close();
        }
    }
    public class Myjob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            CheckSites checkMicrosoft = new CheckSites("http");
            
            checkMicrosoft.FileInput(checkMicrosoft.testSite());

        }
    }
    public class StatusController:ApiController
    {
        ServiceController controller = new ServiceController("MyService");
        public bool CheckStatus()
        {
            if (controller.Status == ServiceControllerStatus.Running)
                return true;
            
            else
                return false;

        }
    }
   
}
