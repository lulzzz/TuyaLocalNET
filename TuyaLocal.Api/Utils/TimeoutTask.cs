namespace TuyaLocal.Api.Utils
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class TimeoutTask
    {
        public static async Task<TResult> TimeoutAfter<TResult>(
            this Task<TResult> task,
            TimeSpan timeout)
        {
            using (var timeoutCancellationTokenSource =
                new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(
                    task,
                    Task.Delay(timeout, timeoutCancellationTokenSource.Token));

                if (completedTask != task)
                {
                    throw new TimeoutException("The operation has timed out.");
                }

                timeoutCancellationTokenSource.Cancel();

                return await task;
            }
        }
    }
}
