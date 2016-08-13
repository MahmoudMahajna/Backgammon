using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Backgammon
{
   public class BackgammonGame:IEnumerable<Point>,IBackgammonGame
    {
        private const int CheckersNumber = 15;
        private readonly Point[] _pToMoveTo = { null, null };
        public Point[] PointsToMoveTo
        {
            get {
                return _pToMoveTo;
            }
           private set
            {
                _pToMoveTo[0] = value[0];
                _pToMoveTo[1] = value[1];
            }
        }
        public Point PointMoveFrom{ get; set; }
        private readonly Board _board;
        private int _numOfRemainMovesPerRoll;
        public Player FirstPlayer { get;  }
        public Player SecondPlayer { get;  }
        public bool IsDiceRolled { get; private set; }
        public int Turn { get;  private set; }
        public int FirstDice { get; private set; }
        public int SecondDice { get; private set; }
        private int _thirdDice;
        
        public Player TheWinner { get; private set; }


        public BackgammonGame(string firstPlayerName, string secondPlayerName)
        {
            PointMoveFrom = null;
            _board = new Board();
            FirstPlayer = new Player(firstPlayerName, 1);
            SecondPlayer = new Player(secondPlayerName, 2);
            IsDiceRolled = false;
        }


        public int GetMiddlePointNumber()
        {
            return _board.MiddlePointNumber;
        }

        public int GetFirstFinishPointNumber()
        {
            return _board.FirstFinishPointNumber;

        }
        public int GetSecondFinishPointNumber()
        {
            return _board.SecondFinishPointNumber;
        }

        public bool IsRoundEnded()
        {
            return _numOfRemainMovesPerRoll == 0 && _thirdDice==0;
        }
       
        private bool IsLegalPointNumber(int num)
        {
            return (num >= 1 && num <= 27);
        }
        public void PointChosen(int pointNum,out bool canMove)
        {
            if (!IsLegalPointNumber(pointNum))
            {
                throw new BadPointNumberException();
            }
            canMove = true;
            var point = this.ElementAt(pointNum - 1);
            
            if (!IsLegalPointChoose(pointNum) || !IsDiceRolled)
            {
                //_IsDiceRolled = false;
                return;
            }
            if (pointNum == GetMiddlePointNumber() && DoesPlayerHasCheckerInMiddle())
            {
                MiddlePointChosenToMoveFrom(out canMove); 
                return;
            }
                if (!IsMoveTo(point) && IsAllToMoveToNull() && point.GetCheckerType() == Turn)
                {
                  PointChosenToMoveFrom(point,out canMove);
                }
                else
                {
                    if (IsMoveTo(point))
                    {
                        PointChosenToMoveTo(pointNum); 
                    }
            }
            if (IsEnded())
            {
                TheWinner = _board.FirstFinishPoint.Count() == CheckersNumber ? FirstPlayer:SecondPlayer;
            }
        }

        private bool IsLegalPointChoose(int pointNum)
        {
            return !(pointNum != GetMiddlePointNumber() && DoesPlayerHasCheckerInMiddle() && PointMoveFrom != _board.MiddlePoint);
        }

       private void MiddlePointChosenToMoveFrom(out bool canMove)
        {
            canMove = true;
            PointsToMoveTo = _board.GetPointsToMoveToFromMiddle(FirstDice, SecondDice, Turn);
            if (IsAllToMoveToNull())
            {
                canMove = false;
                ChangeTurn();
                return;
            }
           PointMoveFrom = _board.MiddlePoint;
        }

        private void PointChosenToMoveFrom(Point point,out bool canMove)
        {
            if (point == null)
            {
                throw new ArgumentNullException();
            }
            canMove = true;
            var points = _board.GetPointsToMoveTo(point, FirstDice, SecondDice, Turn);
            PointsToMoveTo = points;
            if (!CanPlayerMove())
            {
                canMove = false;
                ChangeTurn();
                return;
            }
            PointMoveFrom = point;
        }

        private void PointChosenToMoveTo(int pointNum)
        {
            MoveChecker(PointMoveFrom.Number, pointNum);
            _numOfRemainMovesPerRoll--;
            SetDices(PointMoveFrom.Number, pointNum);
            SetPointsToMoveToNull();
            if (IsRoundEnded())
            {
                ChangeTurn();
                PointMoveFrom = null;
            }
        }

        public void FillCheckers()
        {
            _board.FillCheckers();
        }
        public Point[] GetPointsToMoveTo(Point point,int firstDice,int secondDice,int checkerType)
        {
            if (point == null)
            {
                throw new ArgumentNullException();
            }
            return _board.GetPointsToMoveTo(point, firstDice, secondDice, checkerType);
        }

        private bool CanPlayerMove()
        {
            foreach(var point in this)
            {
                if (point.All(checker => checker.PlayerNumber != Turn)) continue;
                var points = point.Number == GetMiddlePointNumber()
                    ? _board.GetPointsToMoveToFromMiddle(FirstDice, SecondDice, Turn) 
                    : _board.GetPointsToMoveTo(point, FirstDice, SecondDice, Turn);
                if (points[0] != null || points[1] != null)
                {
                    return true;
                }
            }
            return false;
        }

        private void ChangeTurn()
        {
            Turn = Turn == 1 ? 2 : 1;
            IsDiceRolled = false;
        }
        private bool DoesPlayerHasCheckerInMiddle()
        {
           return _board.MiddlePoint.Any(x => x.PlayerNumber == Turn);
        }

        private void MoveChecker(int from, int to)
        {
            if(!IsLegalPointNumber(from) || !IsLegalPointNumber(to))
            {
                throw new BadPointNumberException();
            }
            var pointTo = this.ElementAt(to - 1);
            var pointFrom = this.ElementAt(from - 1);
            if (pointTo.GetCheckerType() != pointFrom.GetCheckerType() && pointTo.Count() == 1)
            {
                _board.MoveChecker(to, _board.MiddlePoint.Number, Turn);
            }
            _board.MoveChecker(from, to,Turn);
        }

        private void SetDices(int fromPointNumber, int toPointNumber)
        {
            if (Turn==1)
            {
                SetDicesForFirstPlayer(fromPointNumber,toPointNumber);
                
            }else
            {
                SetDicesForSecondPlayer(fromPointNumber, toPointNumber);
            }
            if (IsPlayerHasTwoExtraMoves())
            {
                FirstDice = _thirdDice;
                SecondDice = _thirdDice;
                _thirdDice = 0;
                _numOfRemainMovesPerRoll = 2;
            }
        }

        private bool IsPlayerHasTwoExtraMoves()
        {
            return FirstDice == 0 && SecondDice == 0 && (_thirdDice != 0 && _thirdDice != 0);
        }

        private void SetDicesForSecondPlayer(int fromPointNumber, int toPointNumber)
        {

            if (toPointNumber == GetSecondFinishPointNumber())
            {
                SetDicesAfterMoveToSecondFinishPoint();
            }
            if (fromPointNumber - toPointNumber ==FirstDice)
            {
                FirstDice = 0;
            }
            else
            {
                SecondDice = fromPointNumber - toPointNumber ==SecondDice ? 0 : SecondDice;
            }
        }

        private void SetDicesForFirstPlayer(int fromPointNumber,int toPointNumber)
        {
            if (fromPointNumber == GetMiddlePointNumber())
            {
                if (FirstDice == toPointNumber)
                {
                    FirstDice = 0;
                }
                else
                {
                    SecondDice = 0;
                }
            }
            if (toPointNumber == GetFirstFinishPointNumber())
            {
                SetDicesAfterMoveToFirstFinishPoint();
                return;
            }
            if (toPointNumber - fromPointNumber == FirstDice)
            {
                FirstDice = 0;
            }
            else
            {
                SecondDice = toPointNumber - fromPointNumber == SecondDice ? 0 : SecondDice;
            }
        }

        private void SetDicesAfterMoveToFirstFinishPoint()
        {
            if (FirstDice >= SecondDice)
            {
                FirstDice = 0;
            }
            else
            {
                SecondDice = 0;
            }
        }

        private void SetDicesAfterMoveToSecondFinishPoint()
        {
            if (FirstDice > SecondDice)
            {
               FirstDice = 0;
            }
            else
            {
                SecondDice = 0;
            }
        }

        private void SetPointsToMoveToNull()
        {
            PointsToMoveTo[0]= null;
            PointsToMoveTo[1]= null;
        }

        private bool IsAllToMoveToNull()
        {
            foreach(var point in PointsToMoveTo)
            {
                if (point != null)
                {
                    return false;
                }
            }
            return true;
        }
        public void RollDice()
        {
            _thirdDice = 0;
            if (IsDiceRolled)
            {
                return;
            }
            var rand = new Random();
            FirstDice= rand.Next(1, 7);
            SecondDice= rand.Next(1, 7);
            if (FirstDice == SecondDice)
            {
                _thirdDice = FirstDice;
            }
            _numOfRemainMovesPerRoll = 2;
            IsDiceRolled = true;
        }

        public void SetTurn(int playerNum)
        {
            if(playerNum!=1 && playerNum != 2)
            {
                throw new BadPlayerNumberException();
            }
            Turn = playerNum;
        }

        public bool IsEnded()
        {
            return _board.FirstFinishPoint.Count() == CheckersNumber ||
                    _board.SecondFinishPoint.Count()==CheckersNumber;
        }

        private bool IsMoveTo(Point point)
        {
            return PointsToMoveTo.Contains(point);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            foreach(var point in _board)
            {
                yield return point;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
