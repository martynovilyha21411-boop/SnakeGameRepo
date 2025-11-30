using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameRepo
{
    public class Snake
    {
        public List<Segment> Segments;
        private Direction _direction;

        public Snake(Position startPos)
        {
            Segments = new List<Segment>();
            Segments.Add(new Segment(startPos, SegmentType.Head));
            Segments.Add(new Segment(new Position(startPos.X - 1, startPos.Y), SegmentType.Body));
            Segments.Add(new Segment(new Position(startPos.X - 2, startPos.Y), SegmentType.Tail));

            _direction = Direction.Right;
        }

        public void SetDirection(Direction dir)
        {
            // Простейшая защита от разворота назад
            if ((_direction == Direction.Up && dir == Direction.Down) ||
                (_direction == Direction.Down && dir == Direction.Up) ||
                (_direction == Direction.Left && dir == Direction.Right) ||
                (_direction == Direction.Right && dir == Direction.Left))
            {
                return;
            }

            _direction = dir;
        }

        public Direction GetDirection()
        {
            return _direction;
        }

        public Position GetNextHeadPosition()
        {
            Position head = Segments[0].Position;

            switch (_direction)
            {
                case Direction.Up: return new Position(head.X, head.Y - 1);
                case Direction.Down: return new Position(head.X, head.Y + 1);
                case Direction.Left: return new Position(head.X - 1, head.Y);
                default: return new Position(head.X + 1, head.Y);
            }
        }

        public void Move(bool grow)
        {
            Position newHead = GetNextHeadPosition();
            Segments.Insert(0, new Segment(newHead, SegmentType.Head));

            // менять старую голову на body
            Segments[1].Type = SegmentType.Body;

            if (!grow)
            {
                Segments.RemoveAt(Segments.Count - 1);
            }

            // последний — хвост
            Segments[Segments.Count - 1].Type = SegmentType.Tail;
        }
    }
}