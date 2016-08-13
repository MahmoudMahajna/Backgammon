

namespace Backgammon
{
   public class Checker
    {
        public readonly int PlayerNumber;
        public  Checker(int playerNumber)
        {
            if(playerNumber!=1 && playerNumber != 2)
            {
                throw new BadPlayerNumberException();
            }
            PlayerNumber = playerNumber;
        }
    }
}
