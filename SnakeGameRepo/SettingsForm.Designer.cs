
namespace snake
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.trackBarMasterVolume = new System.Windows.Forms.TrackBar();
            this.trackBarMusicVolume = new System.Windows.Forms.TrackBar();
            this.trackBarEffectsVolume = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.labelMasterValue = new System.Windows.Forms.Label();
            this.labelMusicValue = new System.Windows.Forms.Label();
            this.labelEffectsValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMasterVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMusicVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEffectsVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarMasterVolume
            // 
            this.trackBarMasterVolume.LargeChange = 1;
            this.trackBarMasterVolume.Location = new System.Drawing.Point(60, 63);
            this.trackBarMasterVolume.Maximum = 100;
            this.trackBarMasterVolume.Name = "trackBarMasterVolume";
            this.trackBarMasterVolume.Size = new System.Drawing.Size(277, 45);
            this.trackBarMasterVolume.TabIndex = 0;
            this.trackBarMasterVolume.Value = 50;
            this.trackBarMasterVolume.Scroll += new System.EventHandler(this.trackBarMasterVolume_ValueChanged);
            // 
            // trackBarMusicVolume
            // 
            this.trackBarMusicVolume.LargeChange = 1;
            this.trackBarMusicVolume.Location = new System.Drawing.Point(60, 135);
            this.trackBarMusicVolume.Maximum = 100;
            this.trackBarMusicVolume.Name = "trackBarMusicVolume";
            this.trackBarMusicVolume.Size = new System.Drawing.Size(277, 45);
            this.trackBarMusicVolume.TabIndex = 1;
            this.trackBarMusicVolume.Value = 70;
            this.trackBarMusicVolume.Scroll += new System.EventHandler(this.trackBarMusicVolume_ValueChanged);
            // 
            // trackBarEffectsVolume
            // 
            this.trackBarEffectsVolume.LargeChange = 1;
            this.trackBarEffectsVolume.Location = new System.Drawing.Point(60, 214);
            this.trackBarEffectsVolume.Maximum = 100;
            this.trackBarEffectsVolume.Name = "trackBarEffectsVolume";
            this.trackBarEffectsVolume.Size = new System.Drawing.Size(277, 45);
            this.trackBarEffectsVolume.TabIndex = 2;
            this.trackBarEffectsVolume.Value = 80;
            this.trackBarEffectsVolume.Scroll += new System.EventHandler(this.trackBarEffectsVolume_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(60, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Общая громкость:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(60, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Громкость музыки:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(60, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Громкость эффектов:";
            // 
            // buttonApply
            // 
            this.buttonApply.BackColor = System.Drawing.Color.Black;
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApply.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 10F, System.Drawing.FontStyle.Bold);
            this.buttonApply.ForeColor = System.Drawing.Color.Orange;
            this.buttonApply.Location = new System.Drawing.Point(60, 280);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(338, 29);
            this.buttonApply.TabIndex = 6;
            this.buttonApply.Text = "Применить";
            this.buttonApply.UseVisualStyleBackColor = false;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // labelMasterValue
            // 
            this.labelMasterValue.AutoSize = true;
            this.labelMasterValue.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMasterValue.ForeColor = System.Drawing.Color.White;
            this.labelMasterValue.Location = new System.Drawing.Point(340, 63);
            this.labelMasterValue.Name = "labelMasterValue";
            this.labelMasterValue.Size = new System.Drawing.Size(58, 24);
            this.labelMasterValue.TabIndex = 8;
            this.labelMasterValue.Text = "50";
            // 
            // labelMusicValue
            // 
            this.labelMusicValue.AutoSize = true;
            this.labelMusicValue.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 18F);
            this.labelMusicValue.ForeColor = System.Drawing.Color.White;
            this.labelMusicValue.Location = new System.Drawing.Point(340, 135);
            this.labelMusicValue.Name = "labelMusicValue";
            this.labelMusicValue.Size = new System.Drawing.Size(58, 24);
            this.labelMusicValue.TabIndex = 9;
            this.labelMusicValue.Text = "70";
            // 
            // labelEffectsValue
            // 
            this.labelEffectsValue.AutoSize = true;
            this.labelEffectsValue.Font = new System.Drawing.Font("8BIT WONDER(RUS BY LYAJKA)", 18F);
            this.labelEffectsValue.ForeColor = System.Drawing.Color.White;
            this.labelEffectsValue.Location = new System.Drawing.Point(340, 214);
            this.labelEffectsValue.Name = "labelEffectsValue";
            this.labelEffectsValue.Size = new System.Drawing.Size(58, 24);
            this.labelEffectsValue.TabIndex = 10;
            this.labelEffectsValue.Text = "80";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(21)))), ((int)(((byte)(29)))));
            this.ClientSize = new System.Drawing.Size(455, 346);
            this.Controls.Add(this.labelEffectsValue);
            this.Controls.Add(this.labelMusicValue);
            this.Controls.Add(this.labelMasterValue);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarEffectsVolume);
            this.Controls.Add(this.trackBarMusicVolume);
            this.Controls.Add(this.trackBarMasterVolume);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Настройки";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMasterVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMusicVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEffectsVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarMasterVolume;
        private System.Windows.Forms.TrackBar trackBarMusicVolume;
        private System.Windows.Forms.TrackBar trackBarEffectsVolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Label labelMasterValue;
        private System.Windows.Forms.Label labelMusicValue;
        private System.Windows.Forms.Label labelEffectsValue;
    }
}