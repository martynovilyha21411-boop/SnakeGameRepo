using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace snake
{
    public static class TextureManager
    {
        private static Dictionary<SegmentType, Image> segmentTextures = new Dictionary<SegmentType, Image>();
        private static Image foodTexture;
        private static Image fieldTexture;
        private static Image wallTexture;
        private static Image obstacleTexture;
        private static bool texturesLoaded = false;

        public static void LoadTextures()
        {
            if (texturesLoaded) return;

            try
            {
                string texturePath = Path.Combine(Application.StartupPath, "Textures");

                // Проверяем существует ли папка
                if (!Directory.Exists(texturePath))
                {
                    // Если папки нет, создаем простые текстуры программно
                    CreateSimpleTextures();
                    texturesLoaded = true;
                    return;
                }

                bool anyTextureLoaded = false;

                // Загружаем текстуру поля
                fieldTexture = LoadImage(Path.Combine(texturePath, "Grid.png"));
                if (fieldTexture != null) anyTextureLoaded = true;

                // Загружаем текстуру еды
                foodTexture = LoadImage(Path.Combine(texturePath, "apple.png"));
                if (foodTexture == null)
                {
                    foodTexture = LoadImage(Path.Combine(texturePath, "food.png"));
                }
                if (foodTexture != null) anyTextureLoaded = true;


                obstacleTexture = LoadImage(Path.Combine(texturePath, "obstacle.png"));

                // Загружаем ВСЕ текстуры змейки с правильными именами файлов
                LoadAllHeadTextures(texturePath);
                LoadBodyTextures(texturePath);
                LoadTailTextures(texturePath);
                LoadSpecialTextures(texturePath);

                // Проверяем загрузилось ли хоть что-то
                anyTextureLoaded = segmentTextures.Count > 0 || foodTexture != null || fieldTexture != null;

                // Если ни одна текстура не загрузилась, создаем простые
                if (!anyTextureLoaded)
                {
                    CreateSimpleTextures();
                }
                else
                {
                    // Создаем недостающие текстуры
                    if (fieldTexture == null) fieldTexture = CreateFieldTexture(400, 400);
                    if (obstacleTexture == null) obstacleTexture = CreateObstacleTexture(20);
                }

                texturesLoaded = true;
            }
            catch (Exception ex)
            {
                // Если ошибка, создаем простые текстуры
                CreateSimpleTextures();
                texturesLoaded = true;
            }
        }

        private static void LoadAllHeadTextures(string texturePath)
        {
            // Основные головы
            segmentTextures[SegmentType.HeadUp] = LoadImage(Path.Combine(texturePath, "HeadUp.png"));
            segmentTextures[SegmentType.HeadDown] = LoadImage(Path.Combine(texturePath, "HeadDown.png"));
            segmentTextures[SegmentType.HeadLeft] = LoadImage(Path.Combine(texturePath, "HeadLeft.png"));
            segmentTextures[SegmentType.HeadRight] = LoadImage(Path.Combine(texturePath, "HeadRight.png"));

            // Открытый рот
            segmentTextures[SegmentType.HeadUpOpen] = LoadImage(Path.Combine(texturePath, "HeadUpOpen.png"));
            segmentTextures[SegmentType.HeadDownOpen] = LoadImage(Path.Combine(texturePath, "HeadDownOpen.png"));
            segmentTextures[SegmentType.HeadLeftOpen] = LoadImage(Path.Combine(texturePath, "HeadLeftOpen.png"));
            segmentTextures[SegmentType.HeadRightOpen] = LoadImage(Path.Combine(texturePath, "HeadRightOpen.png"));

            // Мертвые головы
            segmentTextures[SegmentType.HeadUpDead] = LoadImage(Path.Combine(texturePath, "HeadUpDead.png"));
            segmentTextures[SegmentType.HeadDownDead] = LoadImage(Path.Combine(texturePath, "HeadDownDead.png"));
            segmentTextures[SegmentType.HeadLeftDead] = LoadImage(Path.Combine(texturePath, "HeadLeftDead.png"));
            segmentTextures[SegmentType.HeadRightDead] = LoadImage(Path.Combine(texturePath, "HeadRightDead.png"));
        }

        private static void LoadBodyTextures(string texturePath)
        {
            // Тело
            segmentTextures[SegmentType.BodyVertical] = LoadImage(Path.Combine(texturePath, "BodyVertical.png"));
            segmentTextures[SegmentType.BodyHorizontal] = LoadImage(Path.Combine(texturePath, "BodyHorizontal.png"));
            segmentTextures[SegmentType.BodyTopRight] = LoadImage(Path.Combine(texturePath, "BodyTopRight.png"));
            segmentTextures[SegmentType.BodyTopLeft] = LoadImage(Path.Combine(texturePath, "BodyTopLeft.png"));
            segmentTextures[SegmentType.BodyBottomRight] = LoadImage(Path.Combine(texturePath, "BodyBottomRight.png"));
            segmentTextures[SegmentType.BodyBottomLeft] = LoadImage(Path.Combine(texturePath, "BodyBottomLeft.png"));
        }

        private static void LoadTailTextures(string texturePath)
        {
            // Хвост
            segmentTextures[SegmentType.TailUp] = LoadImage(Path.Combine(texturePath, "TailUp.png"));
            segmentTextures[SegmentType.TailDown] = LoadImage(Path.Combine(texturePath, "TailDown.png"));
            segmentTextures[SegmentType.TailLeft] = LoadImage(Path.Combine(texturePath, "TailLeft.png"));
            segmentTextures[SegmentType.TailRight] = LoadImage(Path.Combine(texturePath, "TailRight.png"));
        }

        private static void LoadSpecialTextures(string texturePath)
        {
            // Губы
            segmentTextures[SegmentType.LipsUp] = LoadImage(Path.Combine(texturePath, "LipsUp.png"));
            segmentTextures[SegmentType.LipsDown] = LoadImage(Path.Combine(texturePath, "LipsDown.png"));
            segmentTextures[SegmentType.LipsLeft] = LoadImage(Path.Combine(texturePath, "LipsLeft.png"));
            segmentTextures[SegmentType.LipsRight] = LoadImage(Path.Combine(texturePath, "LipsRight.png"));

            // Язык
            segmentTextures[SegmentType.TongueUp] = LoadImage(Path.Combine(texturePath, "TongueUp.png"));
            segmentTextures[SegmentType.TongueDown] = LoadImage(Path.Combine(texturePath, "TongueDown.png"));
            segmentTextures[SegmentType.TongueLeft] = LoadImage(Path.Combine(texturePath, "TongueLeft.png"));
            segmentTextures[SegmentType.TongueRight] = LoadImage(Path.Combine(texturePath, "TongueRight.png"));
        }

        private static Image LoadImage(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return Image.FromFile(filePath);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private static void CreateSimpleTextures()
        {
            int size = 40; // Размер текстур змейки и объектов

            // Создаем текстуру поля
            fieldTexture = CreateFieldTexture(800, 800);

            // Создаем простую текстуру еды (красный круг)
            foodTexture = CreateColoredCircle(size, Color.Red);


            obstacleTexture = CreateObstacleTexture(size);

            // Основные головы
            segmentTextures[SegmentType.HeadUp] = CreateColoredRectangle(size, Color.DarkGreen);
            segmentTextures[SegmentType.HeadDown] = CreateColoredRectangle(size, Color.DarkGreen);
            segmentTextures[SegmentType.HeadLeft] = CreateColoredRectangle(size, Color.DarkGreen);
            segmentTextures[SegmentType.HeadRight] = CreateColoredRectangle(size, Color.DarkGreen);

            // Открытый рот (более светлый)
            segmentTextures[SegmentType.HeadUpOpen] = CreateColoredRectangle(size, Color.LightGreen);
            segmentTextures[SegmentType.HeadDownOpen] = CreateColoredRectangle(size, Color.LightGreen);
            segmentTextures[SegmentType.HeadLeftOpen] = CreateColoredRectangle(size, Color.LightGreen);
            segmentTextures[SegmentType.HeadRightOpen] = CreateColoredRectangle(size, Color.LightGreen);

            // Мертвые головы (серые)
            segmentTextures[SegmentType.HeadUpDead] = CreateColoredRectangle(size, Color.Gray);
            segmentTextures[SegmentType.HeadDownDead] = CreateColoredRectangle(size, Color.Gray);
            segmentTextures[SegmentType.HeadLeftDead] = CreateColoredRectangle(size, Color.Gray);
            segmentTextures[SegmentType.HeadRightDead] = CreateColoredRectangle(size, Color.Gray);

            // Тело
            segmentTextures[SegmentType.BodyVertical] = CreateColoredRectangle(size, Color.Green);
            segmentTextures[SegmentType.BodyHorizontal] = CreateColoredRectangle(size, Color.Green);
            segmentTextures[SegmentType.BodyTopRight] = CreateColoredRectangle(size, Color.Green);
            segmentTextures[SegmentType.BodyTopLeft] = CreateColoredRectangle(size, Color.Green);
            segmentTextures[SegmentType.BodyBottomRight] = CreateColoredRectangle(size, Color.Green);
            segmentTextures[SegmentType.BodyBottomLeft] = CreateColoredRectangle(size, Color.Green);

            // Хвост
            segmentTextures[SegmentType.TailUp] = CreateColoredRectangle(size, Color.LightGreen);
            segmentTextures[SegmentType.TailDown] = CreateColoredRectangle(size, Color.LightGreen);
            segmentTextures[SegmentType.TailLeft] = CreateColoredRectangle(size, Color.LightGreen);
            segmentTextures[SegmentType.TailRight] = CreateColoredRectangle(size, Color.LightGreen);

            // Губы (розовые)
            segmentTextures[SegmentType.LipsUp] = CreateColoredRectangle(size, Color.Pink);
            segmentTextures[SegmentType.LipsDown] = CreateColoredRectangle(size, Color.Pink);
            segmentTextures[SegmentType.LipsLeft] = CreateColoredRectangle(size, Color.Pink);
            segmentTextures[SegmentType.LipsRight] = CreateColoredRectangle(size, Color.Pink);

            // Язык (красные)
            segmentTextures[SegmentType.TongueUp] = CreateColoredRectangle(size, Color.Red);
            segmentTextures[SegmentType.TongueDown] = CreateColoredRectangle(size, Color.Red);
            segmentTextures[SegmentType.TongueLeft] = CreateColoredRectangle(size, Color.Red);
            segmentTextures[SegmentType.TongueRight] = CreateColoredRectangle(size, Color.Red);
        }

        private static Image CreateFieldTexture(int width, int height)
        {
            var bitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bitmap))
            {
                // Заливаем основным цветом - темно-зеленый для травы
                g.Clear(Color.DarkGreen);

                // Рисуем сетку
                using (var gridPen = new Pen(Color.FromArgb(80, Color.Black), 1))
                {
                    int tileSize = 40; // 20px на клетку для поля 20x20

                    // Вертикальные линии
                    for (int x = 0; x <= width; x += tileSize)
                    {
                        g.DrawLine(gridPen, x, 0, x, height);
                    }

                    // Горизонтальные линии
                    for (int y = 0; y <= height; y += tileSize)
                    {
                        g.DrawLine(gridPen, 0, y, width, y);
                    }
                }

                // Добавляем текстуру травы - случайные светлые пиксели
                using (var grassBrush = new SolidBrush(Color.FromArgb(40, Color.LightGreen)))
                {
                    var random = new Random(123); // Фиксированный seed для одинаковой текстуры
                    for (int x = 2; x < width - 2; x += 3)
                    {
                        for (int y = 2; y < height - 2; y += 3)
                        {
                            if (random.Next(0, 4) == 0) // 25% chance
                            {
                                g.FillRectangle(grassBrush, x, y, 2, 2);
                            }
                        }
                    }
                }
            }
            return bitmap;
        }


        private static Image CreateObstacleTexture(int size)
        {
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.SaddleBrown);

                // Контур
                using (var pen = new Pen(Color.Brown, 1))
                {
                    g.DrawRectangle(pen, 1, 1, size - 2, size - 2);
                }

                // Текстура дерева/камня
                using (var brush = new SolidBrush(Color.FromArgb(100, Color.Black)))
                {
                    // Вертикальные "волокна" дерева
                    for (int x = 3; x < size - 3; x += 3)
                    {
                        g.FillRectangle(brush, x, 3, 1, size - 6);
                    }

                    // Горизонтальные "сучья"
                    g.FillRectangle(brush, 4, 6, size - 8, 1);
                    g.FillRectangle(brush, 4, 12, size - 8, 1);
                }
            }
            return bitmap;
        }

        private static Image CreateColoredCircle(int size, Color color)
        {
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                using (var brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, 2, 2, size - 4, size - 4);
                }
            }
            return bitmap;
        }

        private static Image CreateColoredRectangle(int size, Color color)
        {
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                using (var brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, 2, 2, size - 4, size - 4);
                }
            }
            return bitmap;
        }

        public static Image GetSegmentTexture(SegmentType type)
        {
            if (segmentTextures.ContainsKey(type))
            {
                return segmentTextures[type];
            }
            return null;
        }

        public static Image GetFoodTexture()
        {
            return foodTexture;
        }

        public static Image GetFieldTexture()
        {
            return fieldTexture;
        }



        public static Image GetObstacleTexture()
        {
            return obstacleTexture;
        }

        public static void ReloadTextures()
        {
            texturesLoaded = false;
            segmentTextures.Clear();
            foodTexture = null;
            fieldTexture = null;
            obstacleTexture = null;
            LoadTextures();
        }
    }
}