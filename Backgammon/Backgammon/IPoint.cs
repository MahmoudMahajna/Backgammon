using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public interface IPoint
    {
        void AddChecker(Checker checker);
        Checker PopChecker();
        void Clear();
        bool IsEmpty();
         int Number { get; }
         bool IsVacant(int checkerType);
         int GetCheckerType();
    }
}
