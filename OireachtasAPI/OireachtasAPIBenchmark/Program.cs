using BenchmarkDotNet.Running;

namespace OireachtasAPIBenchmark
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<FilterBillsSponsoredByBenchmark>();
        }
    }
}