using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Backgammon
{
    class Board : IBoard, IEnumerable<Point>
    {
        public readonly int FirstFinishPointNumber = 26;
        public readonly int SecondFinishPointNumber = 27;
        public readonly int MiddlePointNumber = 25;

        private Point[] _points;
        public Point FirstFinishPoint { get; }
        public Point SecondFinishPoint { get; }
        public Point MiddlePoint { get; }


        public Board()
        {
            InitializePoints();
            FirstFinishPoint = new Point(FirstFinishPointNumber);
            SecondFinishPoint = new Point(SecondFinishPointNumber);
            MiddlePoint = new Point(MiddlePointNumber);
        }

        private void InitializePoints()
        {
            _points = new Point[24];
            for (int i = 0; i < 24; i++)
            {
                _points[i] = new Point(i + 1);
            }
        }

        public void Clear()
        {
            MiddlePoint.Clear();
            FirstFinishPoint.Clear();
            SecondFinishPoint.Clear();
            foreach (var point in this)
            {
                point.Clear();
            }
        }

        public void FillCheckers()
        {
            FillPoint(_points[0], 1, 2);
            FillPoint(_points[5], 2, 5);
            FillPoint(_points[7], 2, 3);
            FillPoint(_points[11], 1, 5);
            FillPoint(_points[12], 2, 5);
            FillPoint(_points[16], 1, 3);
            FillPoint(_points[18], 1, 5);
            FillPoint(_points[23], 2, 2);

        }

        private void FillPoint(Point point, int playerNum, int checkersNum)
        {
            if (point == null)
            {
                throw new ArgumentNullException();
            }
            if ((playerNum != 1 && playerNum != 2))
            {
                throw new BadPlayerNumberException();
            }
            if (checkersNum < 0)
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < checkersNum; i++)
            {
                point.AddChecker(new Checker(playerNum));
            }
        }
        private Checker MiddlePointGetAndRemoveChecker(int playerNum)
        {
            if (!IsLegalPlayerNumber(playerNum))
            {
                throw new BadPlayerNumberException();
            }
            var tmpList = MiddlePoint.ToList();
            var checker = tmpList.First(ch => ch.PlayerNumber == playerNum);
            tmpList.Remove(checker);
            MiddlePoint.Clear();
            foreach (var c in tmpList)
            {
                MiddlePoint.AddChecker(c);
            }
            return checker;
        }
        private bool IsLegalPointNumber(int num)
        {
            return (num >= 1 && num <= 27);
        }
        private bool IsLegalPlayerNumber(int num)
        {
            return (num == 1 || num == 2);
        }
        public void MoveChecker(int from, int to, int checkerType)
        {
            if (!IsLegalPointNumber(from) || !IsLegalPointNumber(to))
            {
                throw new BadPointNumberException();
            }
            if (!IsLegalPlayerNumber(checkerType))
            {
                throw new BadPlayerNumberException();
            }
            var checker = from != MiddlePointNumber ? this.ElementAt(from - 1).PopChecker() : MiddlePointGetAndRemoveChecker(checkerType);
            this.ElementAt(to - 1).AddChecker(checker);
        }

        public bool IsAllInLastQuarter(int playerNum)
        {
            if (!IsLegalPlayerNumber(playerNum))
            {
                throw new BadPlayerNumberException();
            }
            int startPoint;
            int endPoint;
            if (playerNum == 1)
            {
                startPoint = 0;
                endPoint = 18;
            }
            else
            {
                startPoint = 6;
                endPoint = 24;
            }
            for (int i = startPoint; i < endPoint; i++)
            {
                if (_points[i].GetCheckerType() == playerNum)
                {
                    return false;
                }
            }

            return true;
        }
        public Point[] GetPointsToMoveToFromMiddle(int firstNum, int secondNum, int checkerType)
        {
            if (!IsLegalPlayerNumber(checkerType))
            {
                throw new BadPlayerNumberException();
            }
            var points = new Point[] { null, null };
            if (checkerType == 1)
            {
                if (firstNum != 0)
                {
                    points[0] = this.ElementAt(firstNum - 1).IsVacant(checkerType) ? this.ElementAt(firstNum - 1) : null;
                }
                if (firstNum != 0)
                {
                    points[1] = this.ElementAt(secondNum - 1).IsVacant(checkerType) ? this.ElementAt(secondNum - 1) : null;
                }
            }
            else
            {
                if (firstNum != 0)
                {
                    points[0] = this.ElementAt(24 - firstNum).IsVacant(checkerType) ? this.ElementAt(24 - firstNum) : null;
                }
                if (firstNum != 0)
                {
                    points[1] = this.ElementAt(24 - secondNum).IsVacant(checkerType) ? this.ElementAt(24 - secondNum) : null;
                }
            }
            return points;
        }



        public Point[] GetPointsToMoveTo(Point currentPoint, int firstNum, int secondNum, int checkerType)
        {
            if (currentPoint == null)
            {
                throw new ArgumentNullException();
            }
            IsAllInLastQuarter(checkerType);
            if (currentPoint.Number == MiddlePointNumber)
            {
                return GetPointsToMoveToFromMiddle(firstNum, secondNum, checkerType);
            }
            var points = checkerType == 1 ? GetPointsToMoveToForFirstPlayer(currentPoint, firstNum, secondNum) : GetPointsToMoveToForSecondPlayer(currentPoint, firstNum, secondNum);
            return points;
        }

        private Point[] GetPointsToMoveToForSecondPlayer(Point currentPoint, int firstNum, int secondNum)
        {
            if (currentPoint == null)
            {
                throw new ArgumentNullException();
            }
            var points = new Point[] { null, null };
            if (IsAllInLastQuarter(2))
            {
                points[0] = IsLegalPointToMoveWithDiceNumSecondPlayer(currentPoint, firstNum)
                            ? this.ElementAt(currentPoint.Number - firstNum - 1) : (firstNum == 0 ? null : SecondFinishPoint);
                points[1] = IsLegalPointToMoveWithDiceNumSecondPlayer(currentPoint, secondNum)
                            ? this.ElementAt(currentPoint.Number - secondNum - 1) : (secondNum == 0 ? null : SecondFinishPoint);
            }
            else
            {
                points[0] = IsLegalPointToMoveWithDiceNumSecondPlayer(currentPoint, firstNum)
                            ? this.ElementAt(currentPoint.Number - firstNum - 1) : null;
                points[1] = IsLegalPointToMoveWithDiceNumSecondPlayer(currentPoint, secondNum)
                            ? this.ElementAt(currentPoint.Number - secondNum - 1) : null;
            }
            return points;
        }
        private bool IsLegalPointToMoveWithDiceNumSecondPlayer(Point currentPoint, int diceNum)
        {
            if (currentPoint == null)
            {
                throw new ArgumentNullException();
            }
            return diceNum != 0 && currentPoint.Number - diceNum > 0 &&
                            this.ElementAt(currentPoint.Number - diceNum - 1).IsVacant(2);
        }
        private bool IsLegalPointToMoveWithDiceNumFirstPlayer(Point currentPoint, int diceNum)
        {
            if (currentPoint == null)
            {
                throw new ArgumentNullException();
            }
            return diceNum != 0 && currentPoint.Number + diceNum <= 24 &&
                            this.ElementAt(currentPoint.Number + diceNum - 1).IsVacant(1);
        }

        private Point[] GetPointsToMoveToForFirstPlayer(Point currentPoint, int firstNum, int secondNum)
        {
            if (currentPoint == null)
            {
                throw new ArgumentNullException();
            }
            var points = new Point[] { null, null };
            if (IsAllInLastQuarter(1))
            {
                points[0] = IsLegalPointToMoveWithDiceNumFirstPlayer(currentPoint, firstNum)
                            ? this.ElementAt(currentPoint.Number + firstNum - 1) : (firstNum == 0 ? null : FirstFinishPoint);
                points[1] = IsLegalPointToMoveWithDiceNumFirstPlayer(currentPoint, secondNum)
                             ? this.ElementAt(currentPoint.Number + secondNum - 1) : (secondNum == 0 ? null : FirstFinishPoint);
            }
            else
            {
                points[0] = IsLegalPointToMoveWithDiceNumFirstPlayer(currentPoint, firstNum)
                            ? this.ElementAt(currentPoint.Number + firstNum - 1) : null;
                points[1] = IsLegalPointToMoveWithDiceNumFirstPlayer(currentPoint, secondNum)
                            ? this.ElementAt(currentPoint.Number + secondNum - 1) : null;
            }
            return points;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            foreach (var point in _points)
            {
                yield return point;
            }
            yield return MiddlePoint;
            yield return FirstFinishPoint;
            yield return SecondFinishPoint;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
