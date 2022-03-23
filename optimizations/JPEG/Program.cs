using BenchmarkDotNet.Running;

namespace JPEG
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Compressor>();
            //new Compressor().Run();
        }
    }
}