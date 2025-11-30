using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace snake
{
    public partial class HighScoresForm : Form
    {
        private List<GameMode> gameModes;
        private int currentGameModeId;
        private GameSettings currentSettings;

        public HighScoresForm(GameSettings settings)
        {
            InitializeComponent();
            currentSettings = settings;
            InitializeForm();
            LoadGameModes();
        }

        private void InitializeForm()
        {
            // Настройка формы
            this.Text = "Таблица рекордов";
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void LoadGameModes()
        {
            gameModes = ScoreManager.GetAllGameModes();
            CreateModeButtons();

            // Устанавливаем текущий режим игры
            currentGameModeId = ScoreManager.GetCurrentGameModeId(currentSettings);
            LoadScoresForCurrentMode();
        }

        private void CreateModeButtons()
        {
            panelModeButtons.Controls.Clear();

            foreach (var mode in gameModes)
            {
                var button = new Button();
                button.Text = mode.Name;
                button.Tag = mode.Id;
                button.Size = new Size(170, 35);
                button.BackColor = Color.FromArgb(75, 75, 80);
                button.ForeColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.Gray;
                button.FlatAppearance.BorderSize = 1;
                button.Font = new Font("Arial", 8f);
                button.Margin = new Padding(5);

                button.Click += ModeButton_Click;

                panelModeButtons.Controls.Add(button);
            }

            // Подсвечиваем кнопку текущего режима
            HighlightCurrentModeButton();
        }

        private void HighlightCurrentModeButton()
        {
            foreach (Button button in panelModeButtons.Controls)
            {
                if ((int)button.Tag == currentGameModeId)
                {
                    button.BackColor = Color.FromArgb(100, 100, 180);
                    button.FlatAppearance.BorderColor = Color.Cyan;
                    break;
                }
            }
        }

        private void ModeButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            currentGameModeId = (int)button.Tag;
            LoadScoresForCurrentMode();

            // Подсвечиваем активную кнопку
            foreach (Button btn in panelModeButtons.Controls)
            {
                btn.BackColor = Color.FromArgb(75, 75, 80);
                btn.FlatAppearance.BorderColor = Color.Gray;
            }
            button.BackColor = Color.FromArgb(100, 100, 180);
            button.FlatAppearance.BorderColor = Color.Cyan;
        }

        private void LoadScoresForCurrentMode()
        {
            // Сохраняем ссылку на статический заголовок
            Control staticHeader = null;
            foreach (Control control in panelScores.Controls)
            {
                if (control.Name == "headerPanel") // Замените на реальное имя вашего заголовка
                {
                    staticHeader = control;
                    break;
                }
            }

            // Очищаем всю панель
            panelScores.Controls.Clear();

            // Восстанавливаем статический заголовок
            if (staticHeader != null)
            {
                panelScores.Controls.Add(staticHeader);
            }

            var scores = ScoreManager.GetTopPlayersByGameMode(currentGameModeId, 10);

            if (scores.Count == 0)
            {
                var noDataLabel = new Label();
                noDataLabel.Text = "Нет рекордов для этого режима";
                noDataLabel.ForeColor = Color.DeepPink;
                noDataLabel.Font = new Font("8BIT WONDER(RUS BY LYAJKA)", 12f, FontStyle.Italic);

                // Размещаем под заголовком
                noDataLabel.Location = new Point(
                    (panelScores.Width - noDataLabel.Width) / 2,
                    staticHeader != null ? staticHeader.Bottom + 20 : 20
                );
                noDataLabel.AutoSize = true;
                panelScores.Controls.Add(noDataLabel);
                return;
            }

            // Данные рекордов (начинаем под заголовком)
            for (int i = 0; i < scores.Count; i++)
            {
                CreateScoreRow(scores[i], i, staticHeader);
            }
        }


        private void CreateScoreRow(PlayerScoreView score, int index, Control staticHeader)
        {
            var rowPanel = new Panel();
            rowPanel.Size = new Size(panelScores.Width - 30, 30);

            int startY = staticHeader != null ? staticHeader.Bottom + 10 : 10;
            rowPanel.Location = new Point(15, startY + (index * 35));

            rowPanel.BackColor = index % 2 == 0 ?
                Color.FromArgb(50, 50, 55) :
                Color.FromArgb(45, 45, 50);

            var rankLabel = new Label();
            rankLabel.Text = $"{score.Rank}.";
            rankLabel.Location = new Point(10, 5);
            rankLabel.Size = new Size(50, 20);
            rankLabel.ForeColor = GetRankColor(score.Rank);
            rankLabel.Font = new Font("Arial", 9f, FontStyle.Bold);
            rowPanel.Controls.Add(rankLabel);

            var playerLabel = new Label();
            playerLabel.Text = score.PlayerName;
            playerLabel.Location = new Point(70, 5);
            playerLabel.Size = new Size(200, 20);
            playerLabel.ForeColor = Color.White;
            playerLabel.Font = new Font("Arial", 9f);
            rowPanel.Controls.Add(playerLabel);

            var scoreLabel = new Label();
            scoreLabel.Text = score.Score.ToString();
            scoreLabel.Location = new Point(280, 5);
            scoreLabel.Size = new Size(100, 20);
            scoreLabel.ForeColor = Color.LightGreen;
            scoreLabel.Font = new Font("Arial", 9f, FontStyle.Bold);
            rowPanel.Controls.Add(scoreLabel);

            var dateLabel = new Label();
            dateLabel.Text = score.CreatedDate.ToString("dd.MM.yyyy HH:mm");
            dateLabel.Location = new Point(390, 5);
            dateLabel.Size = new Size(150, 20);
            dateLabel.ForeColor = Color.LightGray;
            dateLabel.Font = new Font("Arial", 8f);
            rowPanel.Controls.Add(dateLabel);

            panelScores.Controls.Add(rowPanel);
        }

        private Color GetRankColor(int rank)
        {
            switch (rank)
            {
                case 1:
                    return Color.Gold;
                case 2:
                    return Color.Silver;
                case 3:
                    return Color.Orange;
                default:
                    return Color.White;
            }
        }

        private void ButtonClearScores_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите очистить ВСЕ рекорды? Это действие нельзя отменить.",
                "Очистка рекордов",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                ScoreManager.ClearAllScores();
                LoadScoresForCurrentMode(); // Обновляем отображение
                MessageBox.Show("Все рекорды очищены!", "Готово",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Обработчик изменения размера формы
        private void HighScoresForm_Resize(object sender, EventArgs e)
        {
            // Пересчитываем позиции при изменении размера
            if (panelScores != null)
            {
                LoadScoresForCurrentMode();
            }
        }
    }
}