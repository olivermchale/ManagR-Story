using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoryService.Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoryService.Services
{
    public class BackgroundChartService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        public IServiceScopeFactory _scopeFactory { get; }

        public BackgroundChartService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Store the task we're executing
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // If the task is completed then return it,
            // this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }


        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This will cause the loop to stop if the service is stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                // Due to this being IHostedService, it has no scope meaning we must create our own
                // Use the injected scope factory to create our scope
                using (var scope = _scopeFactory.CreateScope())
                {
                    // consume our products service through the provided scope
                    var chartsService = scope.ServiceProvider.GetRequiredService<IChartsRepository>();
                    await chartsService.SaveDailyChartData();
                }

                // Wait 5 minutes before running again.
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken));
            }
        }

        public void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
