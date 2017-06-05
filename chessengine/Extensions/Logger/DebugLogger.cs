using System.Diagnostics;

namespace chessengine.Extensions.Logger {
    public class DebugLogger : ILogger {
        public void Log(string data) {
            Debug.WriteLine(data);
        }
    
    }
}