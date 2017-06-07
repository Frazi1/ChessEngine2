using chessengine.Extensions.logger.progressLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace chessui.View
{
    /// <summary>
    /// Логика взаимодействия для ProgressLoggerWindow.xaml
    /// </summary>
    public partial class ProgressLoggerWindow : Window, IProgressLogger
    {
        private ulong _jobCount;
        private ulong _currentPosition;

        public ProgressLoggerWindow() {
            InitializeComponent();
        }

        public ulong JobCount {
            get {
                return _jobCount;
            }

            set {
                _jobCount = value;
                UpdateProgressBar();
            }
        }

        public ulong CurrentPosition {
            get {
                return _currentPosition;
            }

            set {
                _currentPosition = value;
                UpdateProgressBar();
            }
        }

        public void Log(string data) {
            DispatcherWork(() => {
                LoggerTextBox.AppendText(Environment.NewLine);
                LoggerTextBox.AppendText(data);
            });
        }

        private void UpdateProgressBar() {
            DispatcherWork(() => {
                LoggerProgressBar.Maximum = JobCount;
                LoggerProgressBar.Value = _currentPosition;
            });
        }

        private void DispatcherWork(Action a) {
            Dispatcher.BeginInvoke(a);
        }
    }
}
