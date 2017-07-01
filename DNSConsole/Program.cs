using System;
using System.Configuration;
using System.Net;
using System.Threading;

namespace DNSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化日志文件
            LoggingExtensions.Logging.Log.InitializeWith<LoggingExtensions.log4net.Log4NetLog>();
            //写日志
            "Main".Log().Info(() => "The Console is start");
            //获取Configuration对象
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //根据Key读取<add>元素的Value
            string url = config.AppSettings.Settings["DNSUrl"].Value;
            int interval = Convert.ToInt32(config.AppSettings.Settings["Interval"].Value);
            while (true)
            {
                try
                {
                    IPHostEntry host = Dns.GetHostEntry(url);
                    IPAddress ip = host.AddressList[0];
                    "Main".Log().Info(() => "Url:" + url + "  IP:" + ip.ToString());
                }
                catch (Exception ex)
                {
                    "Error".Log().Info(() => ex.Message);
                }
               
                Thread.Sleep(1000 * interval);
            }
        }
    }
}
