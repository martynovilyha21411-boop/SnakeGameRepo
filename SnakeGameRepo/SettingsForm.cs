using System;
using System.Windows.Forms;

namespace snake
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            trackBarMasterVolume.Value = (int)(SoundManager.MasterVolume * 100);
            trackBarMusicVolume.Value = (int)(SoundManager.MusicVolume * 100);
            trackBarEffectsVolume.Value = (int)(SoundManager.EffectsVolume * 100);
            UpdateLabels();
        }

        private void trackBarMasterVolume_ValueChanged(object sender, EventArgs e)
        {
            labelMasterValue.Text = $"{trackBarMasterVolume.Value}";
        }

        private void trackBarMusicVolume_ValueChanged(object sender, EventArgs e)
        {
            labelMusicValue.Text = $"{trackBarMusicVolume.Value}";
        }

        private void trackBarEffectsVolume_ValueChanged(object sender, EventArgs e)
        {
            labelEffectsValue.Text = $"{trackBarEffectsVolume.Value}";
        }

        private void UpdateLabels()
        {
            labelMasterValue.Text = $"{trackBarMasterVolume.Value}";
            labelMusicValue.Text = $"{trackBarMusicVolume.Value}";
            labelEffectsValue.Text = $"{trackBarEffectsVolume.Value}";
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            SoundManager.MasterVolume = trackBarMasterVolume.Value / 100f;
            SoundManager.MusicVolume = trackBarMusicVolume.Value / 100f;
            SoundManager.EffectsVolume = trackBarEffectsVolume.Value / 100f;
            SoundManager.UpdateMusicVolume();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}