using System;
using System.Text;
using WangJun.Yun;

namespace WangJun.FileAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            //var mq = RabbitMQ.GetInst("localhost","guest", "guest","FILES");
            //var count = 0;
            //new FILE().Traverse(@"D:\", (sender, e) =>
            //{
            //    var THIS = sender as FILE;
            //    Console.WriteLine($"{THIS.StackDeep}\t{++count}\t{e.DATA}");
            //    mq.Send(Encoding.UTF8.GetBytes(e.DATA.ToString()));
            //}, (sender, e) =>
            //{
            //    var THIS = sender as FILE;
            //    THIS.AddFolder(e.DATA as string);

            //});

            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            //FileService.GetInst(rootPath).CreateFolder($"/DATA/Log1/");
           FileService.GetInst(rootPath).SaveTo("/DATA/tets/","1.TXT", Encoding.UTF8.GetBytes("汪俊"));
            Console.ReadKey();

        }
    }
}
