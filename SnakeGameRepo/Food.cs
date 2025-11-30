using SnakeGameRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameRepo
{
    public class Food
    {
        public Position Position;

        public Food(Position pos)
        {
            Position = pos;
        }
    }
}
