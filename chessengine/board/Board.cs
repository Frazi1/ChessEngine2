using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using chessengine.board.moves;
using chessengine.board.tiles;
using chessengine.pieces;
using chessengine.player;

namespace chessengine.board {
    public class Board {
        private readonly IList<Tile> _gameBoard;

        public ICollection<Piece> WhitePieces { get; }
        public ICollection<Piece> BlackPieces { get; }

        public Player WhitePlayer { get; }
        public Player BlackPlayer { get; }
        public Player CurrentPlayer { get; }

        public Board(Builder builder) {
            _gameBoard = CreateTilesList(builder);
            WhitePieces = CalculateActivePieces(_gameBoard, Alliance.AllianceEnum.White);
            BlackPieces = CalculateActivePieces(_gameBoard, Alliance.AllianceEnum.Black);

            ICollection<Move> whiteStandardLegalMoves = CalculateLegalMoves(WhitePieces);
            ICollection<Move> blackStandardLegalMoves = CalculateLegalMoves(BlackPieces);

            WhitePlayer = new WhitePlayer(this, whiteStandardLegalMoves, blackStandardLegalMoves);
            BlackPlayer = new BlackPlayer(this, whiteStandardLegalMoves, blackStandardLegalMoves);
            CurrentPlayer = builder.NextMoveMaker == Alliance.AllianceEnum.Black ? BlackPlayer : WhitePlayer;
        }

        public IEnumerable<Move> GetAllLegalMoves() {
            return ImmutableList.CreateRange(WhitePlayer.LegalMoves.Concat(BlackPlayer.LegalMoves));
        }

        private ICollection<Move> CalculateLegalMoves(ICollection<Piece> pieces) {
            if (pieces == null) throw new ArgumentNullException(nameof(pieces));
            if (pieces.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(pieces));
            return pieces.SelectMany(p => p.CalculateLegalMoves(this))
                .ToImmutableList();
        }

        private ICollection<Piece> CalculateActivePieces(IList<Tile> tiles, Alliance.AllianceEnum alliance) {
            if (tiles == null) throw new ArgumentNullException(nameof(tiles));
            if (tiles.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(tiles));
            return tiles
                .Where(t => t.Piece != null)
                .Select(t => t.Piece)
                .Where(p => p.PieceAlliance == alliance)
                .ToImmutableList();
        }

        private static IList<Tile> CreateTilesList(Builder builder) {
            Tile[] tiles = new Tile[BoardUtils.NumTiles];
            for (int i = 0; i < BoardUtils.NumTiles; i++) {
                Piece piece = builder.BoardConfig.ContainsKey(i) ? builder.BoardConfig[i] : null;
                tiles[i] = Tile.CreateTile(i, piece);
            }
            return ImmutableList.CreateRange(tiles);
        }

        public static Board CreateStandardBoard() {
            Builder builder = new Builder();

            //Black
            builder.SetPiece(new Rook(0, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Knight(1, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Bishop(2, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Queen(3, Alliance.AllianceEnum.Black));
            builder.SetPiece(new King(4, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Bishop(5, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Knight(6, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Rook(7, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(8, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(9, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(10, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(11, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(12, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(13, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(14, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(15, Alliance.AllianceEnum.Black));

            //White
            builder.SetPiece(new Pawn(48, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(49, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(50, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(51, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(52, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(53, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(54, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(55, Alliance.AllianceEnum.White));
            builder.SetPiece(new Rook(56, Alliance.AllianceEnum.White));
            builder.SetPiece(new Knight(57, Alliance.AllianceEnum.White));
            builder.SetPiece(new Bishop(58, Alliance.AllianceEnum.White));
            builder.SetPiece(new Queen(59, Alliance.AllianceEnum.White));
            builder.SetPiece(new King(60, Alliance.AllianceEnum.White));
            builder.SetPiece(new Bishop(61, Alliance.AllianceEnum.White));
            builder.SetPiece(new Knight(62, Alliance.AllianceEnum.White));
            builder.SetPiece(new Rook(63, Alliance.AllianceEnum.White));


            builder.SetMoveMaker(Alliance.AllianceEnum.White);
            return builder.Build();
        }

        public Tile GetTile(int tileCoordinate) {
            return _gameBoard[tileCoordinate];
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < BoardUtils.NumTiles; i++) {
                string tileText = _gameBoard[i].ToString();
                builder.Append(tileText)/*.Append(" ")*/;
                if ((i + 1) % BoardUtils.NumTilesPerRow == 0)
                    builder.Append(Environment.NewLine);
            }
            return builder.ToString();

        }
    }
}