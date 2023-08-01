namespace MapingFolders
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Folders folder = new Folders(@"C:\Playlist\pgm");
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(86400000, stoppingToken);
            }
        }
    }
}