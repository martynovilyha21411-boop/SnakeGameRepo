using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Text;


namespace snake
{
    public partial class Form1 : Form
    {
        private bool alreadyGameOverHandled = false;
        private GameState gameState;
        private GameSettings settings;
        private Timer gameTimer;
        private Bitmap canvas;
        private Graphics graphics;

        // Кисти для рисования
        private Brush snakeBrush = Brushes.Green;
        private Brush foodBrush = Brushes.Red;
        private Brush backgroundBrush = Brushes.Black;

        // Режимы скорости
        private readonly Dictionary<int, int> speedModes = new Dictionary<int, int>
        {
            { 0, 200 }, // Медленная скорость
            { 1, 100 }, // Нормальная скорость
            { 2, 50 }   // Быстрая скорость
        };

        public Form1()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;
            TextureManager.LoadTextures();
            SoundManager.LoadSounds(); // Добавляем эту строку
            Namelabel.Text = Program.PlayerNickname;
            settings = new GameSettings
            {
                Width = 20,
                Height = 20,
                TileSize = 40,
                InfiniteField = false,
                GameMode = "Classic",
                SoundEnabled = true,
                MusicEnabled = true,
                GameSpeed = 100
            };
            InitializeGame();
            SetupEventHandlers();
            UpdateGameTypeLabel();

            // Запускаем фоновую музыку если звук включен
            if (settings.SoundEnabled && settings.MusicEnabled)
            {
                SoundManager.PlayBackgroundMusic();
            }

        }


        private void SetSettingsEnabled(bool enabled)
        {
            SettingBox.Enabled = enabled;
        }

        private bool IsGamePaused()
        {
            // Проверяем, находится ли игра на паузе
            return buttonPause != null && buttonPause.Text == "Resume";
        }


        private bool CanChangeNickname()
        {
            // Нельзя менять ник во время игры (включая паузу)
            if (gameTimer != null && (gameTimer.Enabled || IsGamePaused()))
            {
                return false;
            }

            return true;
        }

    



        private void Namelabel_Click(object sender, EventArgs e)
        {
            // Проверяем, можно ли сейчас менять ник
            if (!CanChangeNickname())
            {
                return;
            }

            // Меняем ник с заполнением текущего значения
            using (var nicknameForm = new NicknameForm())
            {
                // Заполняем текущий ник в форме
                nicknameForm.SetCurrentNickname(Program.PlayerNickname);

                if (nicknameForm.ShowDialog() == DialogResult.OK)
                {
                    Program.PlayerNickname = nicknameForm.Nickname;
                    Namelabel.Text = Program.PlayerNickname;
                }
            }
        }
        private void buttonHighScores_Click(object sender, EventArgs e)
        {
            SoundManager.PlayButtonClick();

            // Передаем текущие настройки в форму рекордов
            using (var scoresForm = new HighScoresForm(settings))
            {
                scoresForm.ShowDialog();
            }
        }

        private void SetupEventHandlers()
        {
            checkBoxInfinite.CheckedChanged += CheckBoxInfinite_CheckedChanged;
            trackBarSpeed.ValueChanged += TrackBarSpeed_ValueChanged;
        }

        private void InitializeGame()
        {
            gameState = new GameState(settings);

            // Настраиваем таймер
            gameTimer = new Timer();
            gameTimer.Interval = settings.GameSpeed;
            gameTimer.Tick += GameLoop;

            // Настраиваем trackBar для 3 режимов
            trackBarSpeed.Minimum = 0;
            trackBarSpeed.Maximum = 2;
            trackBarSpeed.Value = 1;
            trackBarSpeed.TickFrequency = 1;
            trackBarSpeed.SmallChange = 1;
            trackBarSpeed.LargeChange = 1;

            // Создаем canvas для отрисовки
            UpdateGameFieldSize();

            // Обновляем интерфейс
            UpdateUI();

            // Фокус на форме для обработки клавиш
            this.Focus();
        }

        private void CreateGameCanvas()
        {
            canvas?.Dispose();
            graphics?.Dispose();

            int canvasWidth = settings.Width * settings.TileSize;
            int canvasHeight = settings.Height * settings.TileSize;

            canvas = new Bitmap(canvasWidth, canvasHeight);
            graphics = Graphics.FromImage(canvas);

            pictureBoxGame.Size = new Size(canvasWidth, canvasHeight);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!gameState.IsGameOver)
            {
                UpdateGame();
                DrawGame();
            }
            else if (!alreadyGameOverHandled)
            {
                GameOver();
            }
        }

        private void UpdateGame()
        {
            // Проверяем близость еды для анимации
            gameState.Snake.CheckFoodProximity(gameState.Foods);

            // Обновляем препятствия
            gameState.UpdateObstacles();

            gameState.UpdateFood();

            // Двигаем змею
            gameState.Snake.Move();
            var newHead = gameState.Snake.Body[0].Position;

            // Обработка бесконечного поля
            if (settings.InfiniteField)
            {
                if (newHead.X < 0) newHead.X = settings.Width - 1;
                if (newHead.X >= settings.Width) newHead.X = 0;
                if (newHead.Y < 0) newHead.Y = settings.Height - 1;
                if (newHead.Y >= settings.Height) newHead.Y = 0;
                gameState.Snake.Body[0].Position = newHead;
            }

            // Проверка столкновений (только с препятствиями и границами)
            if (gameState.CheckCollision(newHead))
            {
                gameState.IsGameOver = true;
                gameState.Snake.IsDead = true;
                SoundManager.PlayWallHitSound();
                return;
            }

            // Проверка столкновения с собой
            if (gameState.Snake.CheckSelfCollision())
            {
                gameState.IsGameOver = true;
                gameState.Snake.IsDead = true;
                SoundManager.PlaySelfHitSound();
                return;
            }

            // Проверка съедания еды
            var foodToEat = gameState.Foods.FirstOrDefault(f => f.Position.Equals(newHead));
            if (foodToEat != null)
            {
                gameState.Snake.Grow();
                gameState.Score += 10;
                gameState.Foods.Remove(foodToEat);
                

                gameState.Snake.StartMouthAnimation();
                SoundManager.PlayEatSound();
                UpdateScore();
            }
            gameState.GenerateFood();
        }

        private void DrawGame()
        {
            // Настройка графики для четкого пиксельного отображения
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.CompositingQuality = CompositingQuality.AssumeLinear;

            // Очищаем canvas
            graphics.Clear(Color.Black);

            // Рисуем текстуру всего поля (масштабированную)
            DrawField();

            // Рисуем препятствия (масштабированные)
            DrawObstacles();

            // Рисуем еду (масштабированную)
            foreach (var food in gameState.Foods)
            {
                var foodTexture = TextureManager.GetFoodTexture();
                if (foodTexture != null)
                {
                    // МАСШТАБИРУЕМ: используем settings.TileSize (40px вместо 20px)
                    graphics.DrawImage(foodTexture,
                        food.Position.X * settings.TileSize,
                        food.Position.Y * settings.TileSize,
                        settings.TileSize, settings.TileSize);
                }
                else
                {
                    graphics.FillRectangle(Brushes.Red,
                        food.Position.X * settings.TileSize,
                        food.Position.Y * settings.TileSize,
                        settings.TileSize, settings.TileSize);
                }
            }

            // Рисуем тело змеи (масштабированное)
            for (int i = 1; i < gameState.Snake.Body.Count; i++)
            {
                var segment = gameState.Snake.Body[i];
                DrawSnakeSegment(segment);
            }

            // Рисуем голову змеи (масштабированную)
            if (gameState.Snake.Body.Count > 0)
            {
                var head = gameState.Snake.Body[0];
                DrawSnakeSegment(head);
            }

            // Рисуем язык если нужно (масштабированный)
            if (gameState.Snake.IsTongueOut && gameState.Snake.FoodNearby != null && !gameState.Snake.IsMouthOpen)
            {
                DrawTongue();
            }

            // Рисуем губы если нужно (масштабированные)
            if (gameState.Snake.IsMouthOpen && gameState.Snake.Body.Count > 0)
            {
                DrawLips();
            }

            // Если игра окончена, рисуем затемнение
            if (gameState.IsGameOver)
            {

                DrawGameOverOverlay();
            }

            pictureBoxGame.Image = canvas;
        }

        private void DrawField()
        {
            var fieldTexture = TextureManager.GetFieldTexture();

            if (fieldTexture != null)
            {
                // Рисуем текстуру всего поля
                graphics.DrawImage(fieldTexture, 0, 0, canvas.Width, canvas.Height);
            }
            else
            {
                // Fallback - зеленый фон с сеткой
                graphics.Clear(Color.DarkGreen);
            }
        }

        private void DrawObstacles()
        {
            var obstacleTexture = TextureManager.GetObstacleTexture();

            foreach (var obstacle in gameState.Obstacles)
            {
                if (obstacle.IsVisible())
                {
                    if (obstacleTexture != null)
                    {
                        int cellSize = settings.TileSize;

                        // Центрируем текстуру в клетке
                        int offsetX = (cellSize - obstacleTexture.Width) / 2;
                        int offsetY = (cellSize - obstacleTexture.Height) / 2;

                        int drawX = obstacle.Position.X * cellSize + offsetX;
                        int drawY = obstacle.Position.Y * cellSize + offsetY;

                        // Масштабируем текстуру под текущий размер клетки
                        graphics.DrawImage(obstacleTexture,
                            obstacle.Position.X * cellSize,
                            obstacle.Position.Y * cellSize,
                            cellSize, cellSize);
                    }
                    else
                    {
                        graphics.FillRectangle(Brushes.SaddleBrown,
                            obstacle.Position.X * settings.TileSize,
                            obstacle.Position.Y * settings.TileSize,
                            settings.TileSize, settings.TileSize);
                    }
                }
            }
        }



        private void DrawSnakeSegment(Segment segment)
        {
            var segmentTexture = TextureManager.GetSegmentTexture(segment.Type);
            if (segmentTexture != null)
            {
                // МАСШТАБИРУЕМ: используем settings.TileSize (40px)
                graphics.DrawImage(segmentTexture,
                    segment.Position.X * settings.TileSize,
                    segment.Position.Y * settings.TileSize,
                    settings.TileSize, settings.TileSize);
            }
            else
            {
                Brush segmentBrush = GetFallbackBrushForSegment(segment.Type);
                graphics.FillRectangle(segmentBrush,
                    segment.Position.X * settings.TileSize,
                    segment.Position.Y * settings.TileSize,
                    settings.TileSize, settings.TileSize);
            }
        }

        private Brush GetFallbackBrushForSegment(SegmentType type)
        {
            // Простая цветовая схема для fallback
            switch (type)
            {
                case SegmentType.HeadUp:
                case SegmentType.HeadDown:
                case SegmentType.HeadLeft:
                case SegmentType.HeadRight:
                    return Brushes.DarkGreen;

                case SegmentType.HeadUpOpen:
                case SegmentType.HeadDownOpen:
                case SegmentType.HeadLeftOpen:
                case SegmentType.HeadRightOpen:
                    return Brushes.LightGreen;

                case SegmentType.HeadUpDead:
                case SegmentType.HeadDownDead:
                case SegmentType.HeadLeftDead:
                case SegmentType.HeadRightDead:
                    return Brushes.Gray;

                case SegmentType.BodyVertical:
                case SegmentType.BodyHorizontal:
                case SegmentType.BodyTopRight:
                case SegmentType.BodyTopLeft:
                case SegmentType.BodyBottomRight:
                case SegmentType.BodyBottomLeft:
                    return Brushes.Green;

                case SegmentType.TailUp:
                case SegmentType.TailDown:
                case SegmentType.TailLeft:
                case SegmentType.TailRight:
                    return Brushes.LightGreen;

                case SegmentType.LipsUp:
                case SegmentType.LipsDown:
                case SegmentType.LipsLeft:
                case SegmentType.LipsRight:
                    return Brushes.Pink;

                case SegmentType.TongueUp:
                case SegmentType.TongueDown:
                case SegmentType.TongueLeft:
                case SegmentType.TongueRight:
                    return Brushes.Red;

                default:
                    return Brushes.Green;
            }
        }

        private void DrawTongue()
        {
            var head = gameState.Snake.Body[0];
            var tongueType = GetTongueType(gameState.Snake.CurrentDirection);
            var tongueTexture = TextureManager.GetSegmentTexture(tongueType);

            if (tongueTexture != null)
            {
                var tonguePos = GetTonguePosition(head.Position, gameState.Snake.CurrentDirection);
                graphics.DrawImage(tongueTexture,
                    tonguePos.X * settings.TileSize,
                    tonguePos.Y * settings.TileSize,
                    settings.TileSize, settings.TileSize);
            }
        }

        private void DrawLips()
        {
            var head = gameState.Snake.Body[0];
            var lipsType = GetLipsType(gameState.Snake.CurrentDirection);
            var lipsTexture = TextureManager.GetSegmentTexture(lipsType);

            if (lipsTexture != null)
            {
                var lipsPos = GetLipsPosition(head.Position, gameState.Snake.CurrentDirection);
                graphics.DrawImage(lipsTexture,
                    lipsPos.X * settings.TileSize,
                    lipsPos.Y * settings.TileSize,
                    settings.TileSize, settings.TileSize);
            }
        }

        private SegmentType GetTongueType(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return SegmentType.TongueUp;
                case Direction.Down: return SegmentType.TongueDown;
                case Direction.Left: return SegmentType.TongueLeft;
                case Direction.Right: return SegmentType.TongueRight;
                default: return SegmentType.TongueRight;
            }
        }

        private SegmentType GetLipsType(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return SegmentType.LipsUp;
                case Direction.Down: return SegmentType.LipsDown;
                case Direction.Left: return SegmentType.LipsLeft;
                case Direction.Right: return SegmentType.LipsRight;
                default: return SegmentType.LipsRight;
            }
        }

        private Position GetTonguePosition(Position headPos, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return new Position(headPos.X, headPos.Y - 1);
                case Direction.Down: return new Position(headPos.X, headPos.Y + 1);
                case Direction.Left: return new Position(headPos.X - 1, headPos.Y);
                case Direction.Right: return new Position(headPos.X + 1, headPos.Y);
                default: return new Position(headPos.X + 1, headPos.Y);
            }
        }

        private Position GetLipsPosition(Position headPos, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return new Position(headPos.X, headPos.Y - 1);
                case Direction.Down: return new Position(headPos.X, headPos.Y + 1);
                case Direction.Left: return new Position(headPos.X - 1, headPos.Y);
                case Direction.Right: return new Position(headPos.X + 1, headPos.Y);
                default: return new Position(headPos.X + 1, headPos.Y);
            }
        }

        private void DrawGameOverOverlay()
        {
            // Полупрозрачное затемнение
            using (var overlayBrush = new SolidBrush(Color.FromArgb(128, Color.Black)))
            {
                graphics.FillRectangle(overlayBrush, 0, 0, canvas.Width, canvas.Height);
            }

            // Текст Game Over
            var font = new Font("Arial", 16, FontStyle.Bold);
            var text = $"GAME OVER!\n {gameState.Score}";
            var textSize = graphics.MeasureString(text, font);

            // Фон для текста
            using (var textBackgroundBrush = new SolidBrush(Color.FromArgb(200, Color.Black)))
            {
                graphics.FillRectangle(textBackgroundBrush,
                    canvas.Width / 2 - textSize.Width / 2 - 10,
                    canvas.Height / 2 - textSize.Height / 2 - 10,
                    textSize.Width + 20,
                    textSize.Height + 20);
            }

            // Сам текст
            graphics.DrawString(text, font, Brushes.White,
                canvas.Width / 2 - textSize.Width / 2,
                canvas.Height / 2 - textSize.Height / 2);

            font.Dispose();
        }

        private void GameOver()
        {
            if (alreadyGameOverHandled) { 
                return;
            }

            alreadyGameOverHandled = true;

            gameTimer.Stop();
            SetSettingsEnabled(true);
            SetNicknameClickable(true);

            // Останавливаем фоновую музыку
            SoundManager.StopBackgroundMusic();

            // Проигрываем звук конца игры
            SoundManager.PlayGameOverSound();

            // Сбрасываем кнопки в правильное состояние
            buttonStart.Enabled = true;
            buttonPause.Text = "Pause";
            buttonPause.Enabled = false;

            // Рисуем сообщение о Game Over
            DrawGameOverOverlay();

            pictureBoxGame.Image = canvas;

            buttonStart.Text = "Restart";
            MessageBox.Show($"Game Over!\nYour score: {gameState.Score}", "Game Over",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (gameState.Score > 0)
            {
                ScoreManager.AddScore(Program.PlayerNickname, gameState.Score, settings);
            }
        }

        private void UpdateScore()
        {
            labelScore.Text = $"{gameState.Score}";
        }

        private void UpdateSpeedLabel()
        {
            string speedName;
            switch (trackBarSpeed.Value)
            {
                case 0:
                    speedName = "Медленная";
                    break;
                case 1:
                    speedName = "Нормальная";
                    break;
                case 2:
                    speedName = "Быстрая";
                    break;
                default:
                    speedName = "Нормальная";
                    break;
            }
        }

        private void UpdateUI()
        {
            labelScore.Text = $"{gameState.Score}";
            UpdateSpeedLabel();
        }

        // Обработчики клавиш
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (gameState.IsGameOver) { 
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (gameTimer.Enabled && !gameState.IsGameOver)
            {
                switch (keyData)
                {
                    case Keys.Up:
                    case Keys.W:
                        if (gameState.Snake.CurrentDirection != Direction.Down)
                            gameState.Snake.NextDirection = Direction.Up;
                        return true;
                    case Keys.Down:
                    case Keys.S:
                        if (gameState.Snake.CurrentDirection != Direction.Up)
                            gameState.Snake.NextDirection = Direction.Down;
                        return true;
                    case Keys.Left:
                    case Keys.A:
                        if (gameState.Snake.CurrentDirection != Direction.Right)
                            gameState.Snake.NextDirection = Direction.Left;
                        return true;
                    case Keys.Right:
                    case Keys.D:
                        if (gameState.Snake.CurrentDirection != Direction.Left)
                            gameState.Snake.NextDirection = Direction.Right;
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Обработчики кнопок
        private void buttonStart_Click(object sender, EventArgs e)
        {
            SoundManager.PlayButtonClick();
            RestartGame();
            if (!gameTimer.Enabled && !gameState.IsGameOver)
            {
                gameTimer.Start();
                buttonStart.Enabled = false;
                buttonPause.Text = "Pause";
                SetNicknameClickable(false);
                SetSettingsEnabled(false);
                this.Focus();
            }
        }

        private void RestartGame()
        {
            // Останавливаем всё
            gameTimer.Stop();
            alreadyGameOverHandled = false;

            // Пересоздаём состояние игры
            gameState = new GameState(settings);

            UpdateGameFieldSize();

            // Перерисовываем игру
            DrawGame();
            UpdateScore();
            UpdateGameTypeLabel();

            // Сбрасываем состояния кнопок
            buttonPause.Text = "Pause";
            buttonStart.Enabled = false;
            buttonPause.Enabled = true;

            SetSettingsEnabled(false);
            SetNicknameClickable(false);

            // ЗАПУСКАЕМ ИГРУ
            gameTimer.Interval = settings.GameSpeed;
            gameTimer.Start();

            this.Focus();
        }
        private void SetNicknameClickable(bool clickable)
        {
            Namelabel.Cursor = clickable ? Cursors.Hand : Cursors.Default;
            Namelabel.ForeColor = Color.DeepPink;

            // Добавляем визуальный индикатор неактивности
            if (!clickable)
            {
                Namelabel.ForeColor = Color.FromArgb(150, Color.DeepPink); // Полупрозрачный
                                                                           // Или можно добадеть эффект "зачеркивания"
                                                                           // Namelabel.Font = new Font(Namelabel.Font, FontStyle.Strikeout);
            }
            else
            {
                Namelabel.ForeColor = Color.DeepPink; // Полностью яркий
                Namelabel.Font = new Font(Namelabel.Font, FontStyle.Regular); // Убираем зачеркивание если было
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            SoundManager.PlayButtonClick();
            if (gameState.IsGameOver)
            {
                return; // Нельзя ставить на паузу после смерти
            }
            if (gameTimer.Enabled)
            {
                // ПАУЗА - останавливаем таймер
                gameTimer.Stop();
                buttonPause.Text = "Resume";
                buttonStart.Enabled = true;

            }
            else
            {
                // ПРОДОЛЖЕНИЕ - запускаем таймер
                gameTimer.Start();
                buttonPause.Text = "Pause";
                buttonStart.Enabled = false;

                this.Focus();
            }
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            SoundManager.PlayButtonClick();
            gameTimer.Stop();

            // Сохраняем текущие настройки перед пересозданием игры
            bool currentInfinite = settings.InfiniteField;
            int currentSpeed = settings.GameSpeed; // Сохраняем текущую скорость

            // Пересоздаем состояние игры
            gameState = new GameState(settings);

            // Восстанавливаем настройки
            settings.InfiniteField = currentInfinite;
            settings.GameSpeed = currentSpeed; // Восстанавливаем скорость
            gameTimer.Interval = currentSpeed;

            // Сбрасываем кнопки и ЗАПУСКАЕМ ИГРУ СРАЗУ
            buttonStart.Enabled = false;
            buttonPause.Text = "Pause";
            buttonPause.Enabled = true;

            // Запускаем игру сразу после рестарта
            gameTimer.Start();
            SetSettingsEnabled(false);
            SetNicknameClickable(false);

            // Перерисовываем
            DrawGame();
            UpdateScore();
            UpdateGameTypeLabel();
            this.Focus();
        }

        // Обработчики настроек
        private void CheckBoxInfinite_CheckedChanged(object sender, EventArgs e)
        {
            settings.InfiniteField = checkBoxInfinite.Checked;
            UpdateGameTypeLabel();
        }

        private void TrackBarSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (gameTimer != null && speedModes.ContainsKey(trackBarSpeed.Value))
            {
                int newSpeed = speedModes[trackBarSpeed.Value];
                gameTimer.Interval = newSpeed;
                settings.GameSpeed = newSpeed;
                UpdateSpeedLabel();
                UpdateGameTypeLabel();
            }
        }

        private void UpdateGameTypeLabel()
        {
            string speedName = "";
            switch (trackBarSpeed.Value)
            {
                case 0: speedName = "Медленный"; break;
                case 1: speedName = "Классический"; break;
                case 2: speedName = "Быстрый"; break;
            }

            string fieldType = checkBoxInfinite.Checked ? "Бесконечный" : "Классический";

            typegame.Text = $"{fieldType}-{speedName}";
        }

        // При активации формы возвращаем фокус для обработки клавиш
        private void Form1_Activated(object sender, EventArgs e)
        {
            this.Focus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            gameTimer?.Stop();
            canvas?.Dispose();
            graphics?.Dispose();
            SoundManager.StopBackgroundMusic();
            SoundManager.Dispose();
            base.OnFormClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SnakeGameDB;Integrated Security=True"))
                {
                    connection.Open();
                    MessageBox.Show("Подключение к БД успешно!");

                    // Проверяем таблицу
                    string checkTable = "SELECT COUNT(*) FROM HighScores";
                    using (var command = new System.Data.SqlClient.SqlCommand(checkTable, connection))
                    {
                        int count = (int)command.ExecuteScalar();
                        MessageBox.Show($"В таблице HighScores записей: {count}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SoundManager.PlayButtonClick();
            using (var settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Настройки звука применены!", "Настройки",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();  // модальное окно
        }

        private void UpdateGameFieldSize()
        {
            if (settings == null || pictureBoxGame == null)
                return;

            int width = settings.Width * settings.TileSize;
            int height = settings.Height * settings.TileSize;
            int topOffset = 100; // фиксированный отступ сверху

            pictureBoxGame.Size = new Size(width, height);

            // Центрируем поле по горизонтали и фиксируем отступ сверху
            pictureBoxGame.Location = new Point(
                (this.ClientSize.Width - SettingBox.Width - width) / 2,
                topOffset
            );

            // Создаём canvas под новый размер
            CreateGameCanvas();
            DrawGame();
        }




        private void Form1_Resize(object sender, EventArgs e)
        {
            if (settings == null || pictureBoxGame == null)
                return;

            int topOffset = 100; // фиксированный отступ сверху
            int availableWidth = this.ClientSize.Width - SettingBox.Width;
            int availableHeight = this.ClientSize.Height - topOffset;

            // Рассчитываем максимальный размер тайла по ширине и высоте
            int maxTileWidth = Math.Max(1, availableWidth / settings.Width);
            int maxTileHeight = Math.Max(1, availableHeight / settings.Height);

            // Выбираем минимальный тайл, чтобы поле помещалось полностью
            settings.TileSize = Math.Min(maxTileWidth, maxTileHeight);

            // Обновляем поле и центрируем его
            UpdateGameFieldSize();
        }

    }
}