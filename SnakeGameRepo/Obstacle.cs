using System;
using System.Drawing;

namespace snake
{
    public class Obstacle
    {
        public Position Position { get; set; }
        public ObstacleType Type { get; set; }
        public int LifeTime { get; set; } // Время жизни в кадрах
        public bool IsBlinking { get; set; }
        public int BlinkCounter { get; set; }

        public Obstacle(int x, int y, ObstacleType type)
        {
            Position = new Position(x, y);
            Type = type;
            LifeTime = 0;
            IsBlinking = false;
            BlinkCounter = 0;
        }

        public void Update()
        {
            LifeTime++;


            if (LifeTime > 150) 
            {
                IsBlinking = true;
                BlinkCounter++;
            }
        }

        public bool ShouldRemove()
        {
            return LifeTime > 180; // РАНЬШЕ: 250, ТЕПЕРЬ: 180 кадров (исчезает быстрее)
        }

        public bool IsVisible()
        {
            if (!IsBlinking) return true;
            // БЫСТРЕЕ МОРГАНИЕ: каждые 3 кадров вместо 10
            return (BlinkCounter / 3) % 2 == 0; // Мерцание: видим/невидим каждые 3 кадров
        }
    }

    public enum ObstacleType
    {
        Rock,
        Bush,
        Log,
        Bone
    }
}