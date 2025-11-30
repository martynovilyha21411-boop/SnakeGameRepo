using System;
using System.Collections.Generic;
using System.Linq;

namespace snake
{
    public class Snake
    {
        public List<Segment> Body { get; set; } = new List<Segment>();
        public Direction CurrentDirection { get; set; } = Direction.Right;
        public Direction NextDirection { get; set; } = Direction.Right;

        // Свойства для анимации
        public bool IsMouthOpen { get; set; } = false;
        public bool IsTongueOut { get; set; } = false;
        public bool IsDead { get; set; } = false;
        public int AnimationCounter { get; set; } = 0;
        public Position FoodNearby { get; set; } = null;
        public int MouthOpenTimer { get; set; } = 0;

        public Snake(int startX, int startY, int initialLength)
        {
            for (int i = 0; i < initialLength; i++)
            {
                var segmentType = i == 0 ? SegmentType.HeadRight :
                                i == initialLength - 1 ? SegmentType.TailRight :
                                SegmentType.BodyHorizontal;

                Body.Add(new Segment(new Position(startX - i, startY), segmentType));
            }
        }

        public void Move()
        {
            CurrentDirection = NextDirection;

            // Обновляем анимацию
            UpdateAnimation();

            // Обновляем типы всех сегментов
            UpdateSegmentTypes();

            // Двигаем змею
            var head = Body[0];
            var newHead = new Position(head.Position.X, head.Position.Y);

            switch (CurrentDirection)
            {
                case Direction.Up: newHead.Y--; break;
                case Direction.Down: newHead.Y++; break;
                case Direction.Left: newHead.X--; break;
                case Direction.Right: newHead.X++; break;
            }

            Body.Insert(0, new Segment(newHead, SegmentType.HeadRight));
            Body.RemoveAt(Body.Count - 1);

            // Снова обновляем типы после движения
            UpdateSegmentTypes();
        }

        private void UpdateSegmentTypes()
        {
            if (Body.Count < 2) return;

            // Голова
            Body[0].Type = GetHeadType();

            // Хвост
            var tail = Body[Body.Count - 1];
            var tailPrev = Body[Body.Count - 2];
            Body[Body.Count - 1].Type = GetTailType(tail.Position, tailPrev.Position);

            // Тело
            for (int i = 1; i < Body.Count - 1; i++)
            {
                var prev = Body[i - 1].Position;
                var current = Body[i].Position;
                var next = Body[i + 1].Position;

                Body[i].Type = GetBodyType(prev, current, next);
            }
        }

        private SegmentType GetHeadType()
        {
            if (IsDead)
            {
                // Мертвая голова
                switch (CurrentDirection)
                {
                    case Direction.Up: return SegmentType.HeadUpDead;
                    case Direction.Down: return SegmentType.HeadDownDead;
                    case Direction.Left: return SegmentType.HeadLeftDead;
                    case Direction.Right: return SegmentType.HeadRightDead;
                    default: return SegmentType.HeadRightDead;
                }
            }
            else if (IsMouthOpen)
            {
                // Голова с открытым ртом (при съедении еды)
                switch (CurrentDirection)
                {
                    case Direction.Up: return SegmentType.HeadUpOpen;
                    case Direction.Down: return SegmentType.HeadDownOpen;
                    case Direction.Left: return SegmentType.HeadLeftOpen;
                    case Direction.Right: return SegmentType.HeadRightOpen;
                    default: return SegmentType.HeadRightOpen;
                }
            }
            else
            {
                // Обычная голова
                switch (CurrentDirection)
                {
                    case Direction.Up: return SegmentType.HeadUp;
                    case Direction.Down: return SegmentType.HeadDown;
                    case Direction.Left: return SegmentType.HeadLeft;
                    case Direction.Right: return SegmentType.HeadRight;
                    default: return SegmentType.HeadRight;
                }
            }
        }

        public void CheckFoodProximity(List<Food> foods)
        {
            var head = Body[0].Position;
            FoodNearby = null;

            foreach (var food in foods)
            {
                int distanceX = Math.Abs(head.X - food.Position.X);
                int distanceY = Math.Abs(head.Y - food.Position.Y);
                int totalDistance = distanceX + distanceY;

                // Если еда в радиусе 2 клеток
                if (totalDistance <= 2)
                {
                    FoodNearby = food.Position;
                    return;
                }
            }
        }

        public void UpdateAnimation()
        {
            AnimationCounter++;

            if (IsDead) return;

            // Таймер для открытия рта после съедения еды
            if (MouthOpenTimer > 0)
            {
                MouthOpenTimer--;
                IsMouthOpen = true;
                IsTongueOut = false;
                return;
            }

            // Сбрасываем состояние анимации
            IsMouthOpen = false;
            IsTongueOut = false;

            // Если еда рядом - случайным образом показываем язык
            if (FoodNearby != null)
            {
                // Случайная анимация языка (примерно каждые 20 кадров)
                IsTongueOut = (AnimationCounter % 20) < 5;
            }
        }

        public void StartMouthAnimation()
        {
            // Устанавливаем таймер для открытия рта на 10 кадров
            MouthOpenTimer = 10;
        }

        private SegmentType GetTailType(Position tail, Position beforeTail)
        {
            if (tail.X == beforeTail.X)
                return tail.Y < beforeTail.Y ? SegmentType.TailUp : SegmentType.TailDown;
            else
                return tail.X < beforeTail.X ? SegmentType.TailLeft : SegmentType.TailRight;
        }

        private SegmentType GetBodyType(Position prev, Position current, Position next)
        {
            bool vertical = prev.X == current.X && current.X == next.X;
            bool horizontal = prev.Y == current.Y && current.Y == next.Y;

            if (vertical) return SegmentType.BodyVertical;
            if (horizontal) return SegmentType.BodyHorizontal;

            // Угловые сегменты
            if (prev.X == current.X && current.Y == next.Y)
            {
                if (prev.Y < current.Y) // Движение вниз
                    return current.X < next.X ? SegmentType.BodyTopLeft : SegmentType.BodyTopRight;
                else // Движение вверх
                    return current.X < next.X ? SegmentType.BodyBottomLeft : SegmentType.BodyBottomRight;
            }
            else if (prev.Y == current.Y && current.X == next.X)
            {
                if (prev.X < current.X) // Движение вправо
                    return current.Y < next.Y ? SegmentType.BodyTopLeft : SegmentType.BodyBottomLeft;
                else // Движение влево
                    return current.Y < next.Y ? SegmentType.BodyTopRight : SegmentType.BodyBottomRight;
            }

            return SegmentType.BodyHorizontal;
        }

        public void Grow()
        {
            var tail = Body[Body.Count - 1];
            Body.Add(new Segment(new Position(tail.Position.X, tail.Position.Y), SegmentType.TailRight));
            UpdateSegmentTypes();
        }

        public bool CheckSelfCollision()
        {
            var head = Body[0].Position;
            return Body.Skip(1).Any(segment => segment.Position.Equals(head));
        }
    }
}