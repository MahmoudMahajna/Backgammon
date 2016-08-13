using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
   public class BadPointNumberException:Exception
    {
       public  BadPointNumberException() : base() { }
       public BadPointNumberException(string message) : base(message) { }
    }
}
