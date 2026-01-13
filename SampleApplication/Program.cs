using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using ThisOldCurl;
using System.Threading;
using ThisOldCurl.LibCurl;

namespace SampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Example> examples = new List<Example>();
            examples.Add(new SuperEasyCurlExample());
            examples.Add(new CurlWebRequestExample());
            examples.Add(new EasyCurlExample());
            examples.Add(new FormCurlExample());
            examples.Add(new MultiCurlExample());
            examples.Add(new SharedCurlExample());
            examples.Add(new AdvancedEasyCurlExample());
            examples.Add(new AdvancedMultiCurlExample());
            examples.Add(new AdvancedConnectOnlyExample());
            examples.Add(new AdvancedAsyncExample());
            foreach (Example example in examples)
            {
                bool cont = example.Demo();
                if (!cont)
                    return;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("That's all the demos - enjoy ThisOldCurl!");
            Console.WriteLine("Press Enter to exit.");
            Console.ReadKey();
            return;
        }
    }
}
