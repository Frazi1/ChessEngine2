namespace chessengine.Extensions.logger.progressLogger {
    public interface IProgressLogger : ILogger {
        ulong JobCount { get; set; }
        ulong CurrentPosition { get; set; }
    }
}