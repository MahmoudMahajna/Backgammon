using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Point : IPoint, IEnumerable<Checker>
    {
        private Stack<Checker> _checkers;
        public int Number { get; }

        public Point(int number)
        {
            if (!(number >= 1 && number <= 27))
            {
                throw new BadPointNumberException();
            }
            _checkers = new Stack<Checker>();
            Number = number;
        }

        public bool IsVacant(int checkerType)
        {
            if (checkerType != 1 && checkerType != 2)
            {
                throw new BadPlayerNumberException();
            }
            return GetCheckerType() == 0 || (GetCheckerType() != checkerType && _checkers.Count == 1) || GetCheckerType() == checkerType;
        }

        public int GetCheckerType()
        {
            return !IsEmpty() ? _checkers.First().PlayerNumber : 0;
        }

        public void AddChecker(Checker checker)
        {
            if (checker == null)
            {
                throw new ArgumentNullException();
            }
            _checkers.Push(checker);
        }

        public void Clear()
        {
            _checkers.Clear();
        }

        public IEnumerator<Checker> GetEnumerator()
        {
            foreach (Checker checker in _checkers)
            {
                yield return checker;
            }
        }

        public bool IsEmpty()
        {
            return _checkers.Count == 0;
        }

        public bool TryPopChecker(out Checker checker)
        {
            checker = null;

            if (_checkers.Any())
            {
                checker = _checkers.Pop();
                return true;
            }

            return false;
        }

        public Checker PopChecker()
        {
            try
            {
                return _checkers.Pop();
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
