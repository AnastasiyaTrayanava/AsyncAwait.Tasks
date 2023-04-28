using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens;

internal static class Calculator
{
    // todo: change this method to support cancellation token
    public static async Task<long> Calculate(int n, CancellationToken token)
    {
        long sum = 0;

        await Task.Run(() =>
        {
            for (var i = 0; i < n; i++)
            {
                // i + 1 is to allow 2147483647 (Max(Int32)) 
                sum = sum + (i + 1);
                Thread.Sleep(10);

                if (token.IsCancellationRequested) return;
            }
        }, token);

        if (token.IsCancellationRequested)
        {
            Console.WriteLine($"Sum for {n} cancelled...");
            return 0;
        }

        return sum;
    }
}
