namespace chessengine.game.events {
    public class BoardChangedArgs {
        public bool IsGameOver { get; set; }
        public Alliance.AllianceEnum WinnerAlliance { get; set; }

        public BoardChangedArgs(bool isGameOver) {
            IsGameOver = isGameOver;
        }
    }
}