using System;
using WangJun.Yun;

namespace WangJun.FileAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 0;
            new FILE().Traverse(@"D:\", (sender, e) =>
            {
                var THIS = sender as FILE;
                Console.WriteLine($"{THIS.StackDeep}\t{++count}\t{e.DATA}");
            }, (sender, e) =>
            {
                var THIS = sender as FILE;
                THIS.AddFolder(e.DATA as string);

            });

            Console.ReadKey();

        }
    }
}
