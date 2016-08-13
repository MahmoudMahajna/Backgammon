using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
   public interface IBackgammonGame
    {
         Point[] PointsToMoveTo { get; }
         Point PointMoveFrom { get; }
         Player FirstPlayer { get;  }
         Player SecondPlayer { get;  }
         bool IsDiceRolled { get; }
         int Turn { get;  }
         int FirstDice { get;  }
         int SecondDice { get;  }
         Player TheWinner { get;  }
         int GetMiddlePointNumber();
         int GetFirstFinishPointNumber();
         int GetSecondFinishPointNumber();
         bool IsRoundEnded();
         void PointChosen(int pointNum, out bool canMove);
         void FillCheckers();
         Point[] GetPointsToMoveTo(Point point, int firstDice, int secondDice, int checkerType);
         void RollDice();
         void SetTurn(int playerNum);
         bool IsEnded();
    }
}
