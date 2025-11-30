using System;
using System.Collections.Generic;
using System.Linq;

namespace snake
{
    public class GameState
    {
        public Snake Snake { get; set; }
        public List<Food> Foods { get; set; } = new List<Food>();
        public List<Obstacle> Obstacles { get; set; } = new List<Obstacle>();
        public int Score { get; set; }
        public bool IsGameOver { get; set; }
        public GameSettings Settings { get; set; }
        private Random random = new Random();
        private int obstacleSpawnCounter = 0;

        public GameState(GameSettings settings)
        {
            Settings = settings;
            Snake = new Snake(settings.Width / 2, settings.Height / 2, 3);
            GenerateFood();
        }

        public void GenerateFood()
        {
            int maxFood = GetMaxFoodCount();

            // Если еды уже достаточно — ничего не делаем
            if (Foods.Count >= maxFood)
                return;

            int needToSpawn = maxFood - Foods.Count;

            for (int i = 0; i < needToSpawn; i++)
            {
                int attempts = 0;
                while (attempts < 30)
                {
                    int x = random.Next(0, Settings.Width);
                    int y = random.Next(0, Settings.Height);

                    if (IsPositionFree(x, y))
                    {
                        Foods.Add(new Food(x, y));
                        break;
                    }

                    attempts++;
                }
            }
        }

        public void UpdateObstacles()
        {
            // Обновляем существующие препятствия
            for (int i = Obstacles.Count - 1; i >= 0; i--)
            {
                Obstacles[i].Update();
                if (Obstacles[i].ShouldRemove())
                {
                    Obstacles.RemoveAt(i);
                }
            }

            // Генерируем новые препятствия в зависимости от счета
            obstacleSpawnCounter++;

            // Интервал появления: реже с ростом счета чтобы не перегружать поле
            int spawnInterval = Math.Max(80, 150 - (Score / 15));

            // МАКСИМУМ 10 ПРЕПЯТСТВИЙ (вместо 30)
            if (obstacleSpawnCounter >= spawnInterval && Obstacles.Count < GetMaxObstacles())
            {
                TrySpawnObstacle();
                obstacleSpawnCounter = 0;
            }
        }

        private int GetMaxObstacles()
        {
            // МАКСИМУМ 10 ПРЕПЯТСТВИЙ (не зависит от счета)
            return 10;
        }

        public int GetMaxFoodCount()
        {
            int snakeLength = Snake.Body.Count;

            // Каждые 5 сегментов +1 яблоко
            return Math.Max(1, snakeLength / 5 + 1);
        }

        private void TrySpawnObstacle()
        {
            int attempts = 0;
            while (attempts < 30) // УМЕНЬШИЛИ с 50 до 30 попыток
            {
                int x = random.Next(0, Settings.Width);
                int y = random.Next(0, Settings.Height);

                if (IsSafePositionForObstacle(x, y))
                {
                    var obstacleType = (ObstacleType)random.Next(0, 4);
                    Obstacles.Add(new Obstacle(x, y, obstacleType));
                    return;
                }
                attempts++;
            }
        }
        public void UpdateFood()
        {
            for (int i = Foods.Count - 1; i >= 0; i--)
            {
                Foods[i].Update();

                if (Foods[i].ShouldRemove())
                    Foods.RemoveAt(i);
            }
        }

        private bool IsSafePositionForObstacle(int x, int y)
        {
            // Проверяем что позиция свободна
            if (!IsPositionFree(x, y)) return false;

            // Проверяем что не блокирует змею
            var head = Snake.Body[0].Position;

            // Не ставим препятствие прямо перед головой
            switch (Snake.CurrentDirection)
            {
                case Direction.Up:
                    if (x == head.X && y == head.Y - 1) return false;
                    break;
                case Direction.Down:
                    if (x == head.X && y == head.Y + 1) return false;
                    break;
                case Direction.Left:
                    if (x == head.X - 1 && y == head.Y) return false;
                    break;
                case Direction.Right:
                    if (x == head.X + 1 && y == head.Y) return false;
                    break;
            }

            // Проверяем что есть хотя бы 2 свободных пути от головы
            return HasEnoughEscapeRoutes(head.X, head.Y);
        }

        private bool HasEnoughEscapeRoutes(int headX, int headY)
        {
            int freeDirections = 0;

            // Проверяем все 4 направления от головы
            var directions = new (int dx, int dy)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };

            foreach (var (dx, dy) in directions)
            {
                int newX = headX + dx;
                int newY = headY + dy;

                // Проверяем границы
                if (newX >= 0 && newX < Settings.Width && newY >= 0 && newY < Settings.Height)
                {
                    if (IsPositionFree(newX, newY))
                    {
                        freeDirections++;
                    }
                }
                else if (Settings.InfiniteField)
                {
                    // В бесконечном поле выход за границу - это свободный путь
                    freeDirections++;
                }
            }

            return freeDirections >= 2; // Нужно минимум 2 свободных пути
        }

        public bool IsPositionFree(int x, int y)
        {
            // Проверяем змею
            if (Snake.Body.Any(segment => segment.Position.X == x && segment.Position.Y == y))
                return false;

            // Проверяем еду
            if (Foods.Any(food => food.Position.X == x && food.Position.Y == y))
                return false;

            // Проверяем препятствия
            if (Obstacles.Any(obs => obs.Position.X == x && obs.Position.Y == y && obs.IsVisible()))
                return false;

            return true;
        }

        public bool CheckCollision(Position position)
        {
            // Столкновение с препятствиями
            if (Obstacles.Any(obs => obs.Position.Equals(position) && obs.IsVisible()))
                return true;

            // Столкновение со стенами (только если не бесконечное поле)
            if (!Settings.InfiniteField &&
                (position.X < 0 || position.X >= Settings.Width ||
                 position.Y < 0 || position.Y >= Settings.Height))
                return true;

            return false;
        }
    }
}