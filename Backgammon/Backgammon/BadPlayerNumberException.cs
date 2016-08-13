using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class BadPlayerNumberException:Exception
    {
       public BadPlayerNumberException() : base() { }
       public BadPlayerNumberException(string message) : base(message) { }
    }
}
