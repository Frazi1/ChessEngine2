﻿using System;
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

        public ICollection<Piece> WhitePieces { get; private set; }
        public ICollection<Piece> BlackPieces { get; private set; }

        public Player WhitePlayer { get; private set; }
        public Player BlackPlayer { get; private set; }
        public Player CurrentPlayer { get; private set; }

        public Pawn EnPassantPawn { get; private set; }

        public Board(Builder builder) {
            _gameBoard = CreateTilesList(builder);
            WhitePieces = CalculateActivePieces(_gameBoard, Alliance.AllianceEnum.White);
            BlackPieces = CalculateActivePieces(_gameBoard, Alliance.AllianceEnum.Black);
            EnPassantPawn = builder.EnPassantPawn;

            ICollection<Move> whiteStandardLegalMoves = CalculateLegalMoves(WhitePieces);
            ICollection<Move> blackStandardLegalMoves = CalculateLegalMoves(BlackPieces);

            WhitePlayer = new WhitePlayer(this, whiteStandardLegalMoves, blackStandardLegalMoves);
            BlackPlayer = new BlackPlayer(this, whiteStandardLegalMoves, blackStandardLegalMoves);
            CurrentPlayer = builder.NextMoveMaker == Alliance.AllianceEnum.Black
                ? BlackPlayer
                : WhitePlayer;
        }

        public IEnumerable<Move> GetAllLegalMoves() {
            return ImmutableList.CreateRange(WhitePlayer.LegalMoves.Concat(BlackPlayer.LegalMoves));
        }

        private ICollection<Move> CalculateLegalMoves(ICollection<Piece> pieces) {
            List<Move> list = new List<Move>();
            foreach (Piece p in pieces)
                foreach (Move move in p.CalculateLegalMoves(this))
                    list.Add(move);
            return list.ToImmutableList();
        }

        private static ICollection<Piece> CalculateActivePieces(IEnumerable<Tile> tiles, Alliance.AllianceEnum alliance) {
            return tiles
                .Where(t => t.Piece != null)
                .Select(t => t.Piece)
                .Where(p => p.PieceAlliance == alliance)
                .ToImmutableList();
        }

        private static IList<Tile> CreateTilesList(Builder builder) {
            Tile[] tiles = new Tile[BoardUtils.NumTiles];
            for (int i = 0; i < BoardUtils.NumTiles; i++) {
                Piece piece = builder.BoardConfig.ContainsKey(i)
                    ? builder.BoardConfig[i]
                    : null;
                tiles[i] = Tile.CreateTile(i, piece);
            }
            return ImmutableList.CreateRange(tiles);
        }

        public static Board CreateStandardBoard() {
            Builder builder = new Builder();

            //Black
            builder.SetPiece(new Rook(0, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Knight(1, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Bishop(2, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Queen(3, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new King(4, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Bishop(5, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Knight(6, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Rook(7, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(8, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(9, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(10, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(11, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(12, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(13, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(14, true, Alliance.AllianceEnum.Black));
            builder.SetPiece(new Pawn(15, true, Alliance.AllianceEnum.Black));

            //White
            builder.SetPiece(new Pawn(48, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(49, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(50, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(51, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(52, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(53, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(54, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Pawn(55, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Rook(56, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Knight(57, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Bishop(58, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Queen(59, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new King(60, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Bishop(61, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Knight(62, true, Alliance.AllianceEnum.White));
            builder.SetPiece(new Rook(63, true, Alliance.AllianceEnum.White));


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