
namespace snake
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBoxGame = new System.Windows.Forms.PictureBox();
            this.labelScore = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxInfinite = new System.Windows.Forms.CheckBox();
            this.SettingBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.typegame = new System.Windows.Forms.Label();
            this.Namelabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonHighScores = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            this.SettingBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxGame
            // 
            this.pictureBoxGame.BackColor = System.Drawing.Color.Black;
            this.pictureBoxGame.Image = global::snake.Properties.Resources.Grid;
            this.pictureBoxGame.Location = new System.Drawing.Point(10, 92);
            this.pictureBoxGame.Name = "pictureBoxGame";
            this.pictureBoxGame.Size = new System.Drawing.Size(800, 800);
            this.pictureBoxGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxGame.TabIndex = 0;
            this.pictureBoxGame.TabStop = false;
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelScore.ForeColor = System.Drawing.Color.DeepPink;
            this.labelScore.Location = new System.Drawing.Point(107, 65);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(104, 19);
            this.labelScore.TabIndex = 1;
            this.labelScore.Text = "Score";
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonStart.BackColor = System.Drawing.Color.Black;
            this.buttonStart.FlatAppearance.BorderColor = System.Drawing.Color.DeepPink;
            this.buttonStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Purple;
            this.buttonStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.ForeColor = System.Drawing.Color.Lime;
            this.buttonStart.Location = new System.Drawing.Point(847, 420);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(184, 30);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonPause.BackColor = System.Drawing.Color.Black;
            this.buttonPause.FlatAppearance.BorderColor = System.Drawing.Color.DeepPink;
            this.buttonPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Purple;
            this.buttonPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPause.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 10F, System.Drawing.FontStyle.Bold);
            this.buttonPause.ForeColor = System.Drawing.Color.Yellow;
            this.buttonPause.Location = new System.Drawing.Point(1066, 420);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(181, 30);
            this.buttonPause.TabIndex = 3;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = false;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz;
            this.trackBarSpeed.Location = new System.Drawing.Point(6, 111);
            this.trackBarSpeed.Maximum = 2;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(337, 45);
            this.trackBarSpeed.TabIndex = 5;
            this.trackBarSpeed.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Скорость игры";
            // 
            // checkBoxInfinite
            // 
            this.checkBoxInfinite.FlatAppearance.CheckedBackColor = System.Drawing.Color.DeepPink;
            this.checkBoxInfinite.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Purple;
            this.checkBoxInfinite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.checkBoxInfinite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxInfinite.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxInfinite.Location = new System.Drawing.Point(9, 39);
            this.checkBoxInfinite.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.checkBoxInfinite.Name = "checkBoxInfinite";
            this.checkBoxInfinite.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxInfinite.Size = new System.Drawing.Size(278, 40);
            this.checkBoxInfinite.TabIndex = 7;
            this.checkBoxInfinite.Text = "Бесконечное поле";
            this.checkBoxInfinite.UseVisualStyleBackColor = false;
            // 
            // SettingBox
            // 
            this.SettingBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SettingBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.SettingBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SettingBox.Controls.Add(this.label4);
            this.SettingBox.Controls.Add(this.trackBarSpeed);
            this.SettingBox.Controls.Add(this.checkBoxInfinite);
            this.SettingBox.Controls.Add(this.label1);
            this.SettingBox.Controls.Add(this.typegame);
            this.SettingBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingBox.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SettingBox.ForeColor = System.Drawing.Color.White;
            this.SettingBox.Location = new System.Drawing.Point(847, 181);
            this.SettingBox.Name = "SettingBox";
            this.SettingBox.Size = new System.Drawing.Size(400, 212);
            this.SettingBox.TabIndex = 8;
            this.SettingBox.TabStop = false;
            this.SettingBox.Text = "Настройки игрового процесса";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(6, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Режим:";
            // 
            // typegame
            // 
            this.typegame.AutoSize = true;
            this.typegame.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.typegame.Location = new System.Drawing.Point(112, 159);
            this.typegame.Name = "typegame";
            this.typegame.Size = new System.Drawing.Size(97, 16);
            this.typegame.TabIndex = 9;
            this.typegame.Text = "Режим";
            // 
            // Namelabel
            // 
            this.Namelabel.AutoSize = true;
            this.Namelabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Namelabel.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Namelabel.ForeColor = System.Drawing.Color.DeepPink;
            this.Namelabel.Location = new System.Drawing.Point(399, 65);
            this.Namelabel.Name = "Namelabel";
            this.Namelabel.Size = new System.Drawing.Size(95, 19);
            this.Namelabel.TabIndex = 10;
            this.Namelabel.Text = "Name";
            this.Namelabel.Click += new System.EventHandler(this.Namelabel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(279, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Игрок:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Счет:";
            // 
            // buttonHighScores
            // 
            this.buttonHighScores.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonHighScores.BackColor = System.Drawing.Color.Black;
            this.buttonHighScores.FlatAppearance.BorderColor = System.Drawing.Color.DeepPink;
            this.buttonHighScores.FlatAppearance.BorderSize = 2;
            this.buttonHighScores.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Purple;
            this.buttonHighScores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonHighScores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHighScores.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonHighScores.ForeColor = System.Drawing.Color.Orange;
            this.buttonHighScores.Location = new System.Drawing.Point(847, 92);
            this.buttonHighScores.Name = "buttonHighScores";
            this.buttonHighScores.Size = new System.Drawing.Size(405, 70);
            this.buttonHighScores.TabIndex = 13;
            this.buttonHighScores.Text = "Таблица рекордов";
            this.buttonHighScores.UseVisualStyleBackColor = false;
            this.buttonHighScores.Click += new System.EventHandler(this.buttonHighScores_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Black;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1268, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "MenuStrip";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.настройкиToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(149, 20);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(21)))), ((int)(((byte)(29)))));
            this.ClientSize = new System.Drawing.Size(1268, 935);
            this.Controls.Add(this.buttonHighScores);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Namelabel);
            this.Controls.Add(this.SettingBox);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.pictureBoxGame);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Snale 8-bit";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            this.SettingBox.ResumeLayout(false);
            this.SettingBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGame;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.TrackBar trackBarSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxInfinite;
        private System.Windows.Forms.GroupBox SettingBox;
        private System.Windows.Forms.Label typegame;
        private System.Windows.Forms.Label Namelabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonHighScores;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label4;
    }
}

