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
using System.Windows.Navigation;
using System.Windows.Shapes;
using chessengine;
using chessengine.game;
using chessui.Controller;
using chessui.View;

namespace chessui {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public Game Game { get; set; }
        public ChessController ChessController { get; set; }
        public ProgressLoggerWindow ProgressLogger { get; set; }

        public MainWindow() {
            InitializeComponent();
            ProgressLogger = new ProgressLoggerWindow();
            Game = new Game(2,ProgressLogger);
            Helper.CreateInstance(Game.NumTiles, Game.NumTilesPerRow, ChessBoard.TileSize);
            ChessController = new ChessController(Game, ChessBoard);
            ChessBoard.DataContext = Game;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            ProgressLogger.Show();
        }
    }
}
