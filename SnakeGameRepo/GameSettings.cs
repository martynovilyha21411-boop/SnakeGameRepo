using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameRepo
{
    public class GameSettings
    {
        public GameSettings() : this(40, 30) { }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool InfiniteField { get; set; } = false;

        public GameSettings(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
