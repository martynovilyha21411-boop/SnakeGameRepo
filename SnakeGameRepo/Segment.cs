using System.Drawing;

namespace snake
{
    public class Segment
    {
        public Position Position { get; set; }
        public SegmentType Type { get; set; }

        public Segment(Position position, SegmentType type)
        {
            Position = position;
            Type = type;
        }
    }
}