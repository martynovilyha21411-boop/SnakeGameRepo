using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    public class Food
    {
        public Position Position { get; set; }
        public int LifeTime { get; set; }

        public Food(int x, int y)
        {
            Position = new Position(x, y);
            LifeTime = 0;
        }

        public void Update()
        {
            LifeTime++;
        }

        public bool ShouldRemove()
        {
            // Исчезает после 50 кадров
            return LifeTime > 50;
        }
    }
}


