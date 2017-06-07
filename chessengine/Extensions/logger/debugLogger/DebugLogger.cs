using System.Diagnostics;

namespace chessengine.Extensions.logger.debugLogger {
    public class DebugLogger : ILogger {
        public void Log(string data) {
            Debug.WriteLine(data);
        }
    
    }
}