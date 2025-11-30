using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameRepo
{
    public partial class Form1 : Form
    {
        private GameState _game;
        private Timer _timer;

        public Form1()
        {
            InitializeComponent();

            // Создание базового состояния игры
            _game = new GameState(new GameSettings());

            // Минимальный таймер
            _timer = new Timer();
            _timer.Interval = 100; // Можно регулировать скоростью
            _timer.Tick += GameLoop;
            _timer.Start();

            // Настройка формы
            this.DoubleBuffered = true;
            this.Width = 820;
            this.Height = 620;
            this.BackColor = Color.Black;

            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) _game.Snake.SetDirection(Direction.Up);
            if (e.KeyCode == Keys.Down) _game.Snake.SetDirection(Direction.Down);
            if (e.KeyCode == Keys.Left) _game.Snake.SetDirection(Direction.Left);
            if (e.KeyCode == Keys.Right) _game.Snake.SetDirection(Direction.Right);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!_game.GameOver)
                _game.Update();

            this.Invalidate(); // Перерисовка
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawGame(e.Graphics);
        }

        private void DrawGame(Graphics g)
        {
            int cell = 20;

            // Рисуем змейку
            foreach (var segment in _game.Snake.Segments)
            {
                g.FillRectangle(
                    Brushes.Lime,
                    segment.Position.X * cell,
                    segment.Position.Y * cell,
                    cell,
                    cell
                );
            }

            // Рисуем еду
            foreach (var food in _game.Foods)
            {
                g.FillRectangle(
                    Brushes.Red,
                    food.Position.X * cell,
                    food.Position.Y * cell,
                    cell,
                    cell
                );
            }

            // Рисуем препятствия
            foreach (var obs in _game.Obstacles)
            {
                g.FillRectangle(
                    Brushes.Gray,
                    obs.Position.X * cell,
                    obs.Position.Y * cell,
                    cell,
                    cell
                );
            }

            // Если игра окончена — выводим текст
            if (_game.GameOver)
            {
                g.DrawString(
                    "GAME OVER",
                    new Font("Arial", 32),
                    Brushes.White,
                    200, 200
                );
            }
        }
    }
}