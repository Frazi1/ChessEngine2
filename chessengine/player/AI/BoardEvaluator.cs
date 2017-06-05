using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using chessengine.board;
using chessengine.Extensions.EnumExtensions;
using chessengine.pieces;

namespace chessengine.player.AI {
    public class BoardEvaluator : IBoardEvaluator {

        private const int CheckBonus = 50;
        private const int CheckMateBonus = 30000;
        private const int CastledBonus = 100;

        private static readonly short[] PawnTable = new short[] {
            0, 0, 0, 0, 0, 0, 0, 0,
            50, 50, 50, 50, 50, 50, 50, 50,
            10, 10, 20, 30, 30, 20, 10, 10,
            5, 5, 10, 27, 27, 10, 5, 5,
            0, 0, 0, 25, 25, 0, 0, 0,
            5, -5, -10, 0, 0, -10, -5, 5,
            5, 10, 10, -25, -25, 10, 10, 5,
            0, 0, 0, 0, 0, 0, 0, 0
        };

        private static readonly short[] KnightTable = new short[] {
            -50, -40, -30, -30, -30, -30, -40, -50,
            -40, -20, 0, 0, 0, 0, -20, -40,
            -30, 0, 10, 15, 15, 10, 0, -30,
            -30, 5, 15, 20, 20, 15, 5, -30,
            -30, 0, 15, 20, 20, 15, 0, -30,
            -30, 5, 10, 15, 15, 10, 5, -30,
            -40, -20, 0, 5, 5, 0, -20, -40,
            -50, -40, -20, -30, -30, -20, -40, -50,
        };

        private static readonly short[] BishopTable = new short[] {
            -20, -10, -10, -10, -10, -10, -10, -20,
            -10, 0, 0, 0, 0, 0, 0, -10,
            -10, 0, 5, 10, 10, 5, 0, -10,
            -10, 5, 5, 10, 10, 5, 5, -10,
            -10, 0, 10, 10, 10, 10, 0, -10,
            -10, 10, 10, 10, 10, 10, 10, -10,
            -10, 5, 0, 0, 0, 0, 5, -10,
            -20, -10, -40, -10, -10, -40, -10, -20,
        };

        private static readonly short[] KingTable = new short[] {
            -30, -40, -40, -50, -50, -40, -40, -30,
            -30, -40, -40, -50, -50, -40, -40, -30,
            -30, -40, -40, -50, -50, -40, -40, -30,
            -30, -40, -40, -50, -50, -40, -40, -30,
            -20, -30, -30, -40, -40, -30, -30, -20,
            -10, -20, -20, -20, -20, -20, -20, -10,
            20, 20, 0, 0, 0, 0, 20, 20,
            20, 30, 10, 0, 0, 10, 30, 20
        };

        //NotUsed
        private static readonly short[] KingTableEndGame = new short[] {
            -50, -40, -30, -20, -20, -30, -40, -50,
            -30, -20, -10, 0, 0, -10, -20, -30,
            -30, -10, 20, 30, 30, 20, -10, -30,
            -30, -10, 30, 40, 40, 30, -10, -30,
            -30, -10, 30, 40, 40, 30, -10, -30,
            -30, -10, 20, 30, 30, 20, -10, -30,
            -30, -30, 0, 0, 0, 0, -30, -30,
            -50, -30, -30, -30, -30, -30, -30, -50
        };

        public static Dictionary<PieceType, short[]> Tables { get; private set; }

        public BoardEvaluator() {
            Tables = new Dictionary<PieceType, short[]>();
            Tables.Add(PieceType.Pawn, PawnTable);
            Tables.Add(PieceType.Knight, KnightTable);
            Tables.Add(PieceType.Bishop, BishopTable);
            Tables.Add(PieceType.King, KingTable);
        }


        private static int ConvertIndex(int piecePosition, Alliance.AllianceEnum alliance) {
            //TODO: fix ? test
            return alliance == Alliance.AllianceEnum.White
                ? piecePosition
                : BoardUtils.NumTiles - piecePosition;
            //return alliance == Alliance.AllianceEnum.White
            //    ? piecePosition
            //    : (byte)(((byte)(piecePosition + 56)) - (byte)((byte)(piecePosition / 8) * 16));
        }

        public int Evaluate(Board board, Alliance.AllianceEnum alliance) {
            Player player = alliance == Alliance.AllianceEnum.Black ? board.BlackPlayer : board.WhitePlayer;
            int score1 = EvaluatePlayer(player);
            int score2 = EvaluatePlayer(player.Opponent);

            return score1 - score2;
        }

        private static int EvaluatePlayer(Player player) {
            return PieceValue(player)
                   + Mobility(player)
                   + Check(player)
                   + CheckMate(player)
                //+ Castled(player)
                ;
        }

        private static int Castled(Player player) {
            return player.IsCastled() ? CastledBonus : 0;
        }

        private static int CheckMate(Player player) {
            return player.IsInCheckMate() ? CheckMateBonus : 0;
        }

        private static int Check(Player player) {
            return player.IsInCheck ? CheckBonus : 0;
        }

        private static int Mobility(Player player) {
            return player.LegalMoves.Count;
        }

        private static int PieceValue(Player player) {
            int score = 0;
            foreach (Piece piece in player.ActivePieces) {
                score += piece.PieceType.GetValue();
                if (Tables.ContainsKey(piece.PieceType)) {
                    score += Tables[piece.PieceType][ConvertIndex(piece.PiecePosition, piece.PieceAlliance)];
                }
            }
            return score;
        }
    }
}