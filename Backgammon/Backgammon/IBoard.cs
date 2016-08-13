namespace Backgammon
{
   public interface IBoard
    {
        
         Point FirstFinishPoint { get; }
         Point SecondFinishPoint { get; }
         Point MiddlePoint { get; }
         void Clear();
         void FillCheckers();
         void MoveChecker(int from, int to, int checkerType);
         bool IsAllInLastQuarter(int playerNum);
         Point[] GetPointsToMoveToFromMiddle(int firstNum, int secondNum, int checkerType);
         Point[] GetPointsToMoveTo(Point currentPoint, int firstNum, int secondNum, int checkerType);
    }
}
