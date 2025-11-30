using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameRepo
{
    public class Segment
    {
        public Position Position;
        public SegmentType Type;

        public Segment(Position pos, SegmentType type)
        {
            Position = pos;
            Type = type;
        }
    }
}