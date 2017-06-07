using System.Windows;
using System.Windows.Controls;
using chessengine.game;

namespace chessui {
    /// <summary>
    /// Логика взаимодействия для ChessBoard.xaml
    /// </summary>
    public partial class ChessBoard : UserControl {

        #region Dependecy Properties
        public Game Game {
            get { return (Game)GetValue(ChessGameProperty); }
            set { SetValue(ChessGameProperty, value); }
        }

        public static readonly DependencyProperty ChessGameProperty =
            DependencyProperty.Register("Game", typeof(Game), typeof(ChessBoard), new FrameworkPropertyMetadata(default(Game)));


        public double TileSize {
            get { return (double)GetValue(TileSizeProperty); }
            set { SetValue(TileSizeProperty, value); }
        }

        public static readonly DependencyProperty TileSizeProperty =
            DependencyProperty.Register("TileSize", typeof(double), typeof(ChessBoard), new FrameworkPropertyMetadata(100.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty TilesNumberProperty =
            DependencyProperty.Register("NumTiles", typeof(int), typeof(ChessBoard), new PropertyMetadata(64));

        public static readonly DependencyProperty TilesPerRowProperty =
            DependencyProperty.Register("NumTilesPerRow", typeof(int), typeof(ChessBoard), new PropertyMetadata(8));

        #endregion

        public ChessBoard() {
            InitializeComponent();
        }

        private void ChessBoardCanvas_Loaded(object sender, RoutedEventArgs e) {
            Game = (Game)DataContext;
        }
    }
}
