using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NAudio.Wave;

namespace snake
{
    public static class SoundManager
    {
        private static Dictionary<string, AudioFileReader> soundFiles = new Dictionary<string, AudioFileReader>();
        private static Dictionary<string, WaveOutEvent> soundPlayers = new Dictionary<string, WaveOutEvent>();
        private static bool soundsLoaded = false;

        private static List<string> backgroundMusicTracks = new List<string>();
        private static AudioFileReader currentBackgroundMusic;
        private static WaveOutEvent backgroundMusicPlayer;
        private static Random random = new Random();
        private static int currentTrackIndex = -1;

        public static float MasterVolume { get; set; } = 0.5f;
        public static float MusicVolume { get; set; } = 0.7f;
        public static float EffectsVolume { get; set; } = 0.8f;

        public static bool SoundEnabled { get; set; } = true;

        public static void LoadSounds()
        {
            if (soundsLoaded) return;

            try
            {
                string soundsPath = Path.Combine(Application.StartupPath, "Sounds");

                if (!Directory.Exists(soundsPath))
                {
                    soundsLoaded = true;
                    return;
                }

                // Загружаем звуки эффектов
                LoadSound("eat", Path.Combine(soundsPath, "eat.wav"));
                LoadSound("wall_hit", Path.Combine(soundsPath, "wall_hit.wav"));
                LoadSound("self_hit", Path.Combine(soundsPath, "self_hit.wav"));
                LoadSound("button_click", Path.Combine(soundsPath, "button_click.wav"));
                LoadSound("game_over", Path.Combine(soundsPath, "game_over.wav"));

                // Загружаем фоновую музыку
                LoadBackgroundMusicTracks(soundsPath);

                soundsLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки звуков: {ex.Message}", "Sound Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                soundsLoaded = true;
            }
        }

        private static void LoadBackgroundMusicTracks(string soundsPath)
        {
            try
            {
                // Ищем файлы фоновой музыки (background1.wav, background2.wav, ..., background13.wav)
                for (int i = 1; i <= 13; i++)
                {
                    string trackPath = Path.Combine(soundsPath, $"background{i}.wav");
                    if (File.Exists(trackPath))
                    {
                        backgroundMusicTracks.Add(trackPath);
                        Console.WriteLine($"Found background track: background{i}.wav");
                    }
                }

                // Альтернативно: ищем любые файлы с pattern background*.wav
                if (backgroundMusicTracks.Count == 0)
                {
                    var allTracks = Directory.GetFiles(soundsPath, "background*.wav");
                    backgroundMusicTracks.AddRange(allTracks);
                    Console.WriteLine($"Found {allTracks.Length} background tracks");
                }

                if (backgroundMusicTracks.Count > 0)
                {
                    // Создаем плеер
                    backgroundMusicPlayer = new WaveOutEvent();
                    backgroundMusicPlayer.PlaybackStopped += BackgroundMusicPlayer_PlaybackStopped;

                    // Выбираем случайный трек
                    PlayRandomBackgroundTrack();
                }
                else
                {
                    Console.WriteLine("No background music tracks found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading background music: {ex.Message}");
            }
        }

        private static void BackgroundMusicPlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // Когда трек заканчивается, играем следующий
            if (SoundEnabled && backgroundMusicTracks.Count > 0)
            {
                PlayNextBackgroundTrack();
            }
        }

        private static void PlayRandomBackgroundTrack()
        {
            if (backgroundMusicTracks.Count == 0) return;

            currentTrackIndex = random.Next(backgroundMusicTracks.Count);
            PlayBackgroundTrack(currentTrackIndex);
        }

        private static void PlayNextBackgroundTrack()
        {
            if (backgroundMusicTracks.Count == 0) return;

            currentTrackIndex = (currentTrackIndex + 1) % backgroundMusicTracks.Count;
            PlayBackgroundTrack(currentTrackIndex);
        }

        private static void PlayBackgroundTrack(int trackIndex)
        {
            try
            {
                // Останавливаем текущую музыку
                backgroundMusicPlayer?.Stop();
                currentBackgroundMusic?.Dispose();

                // Загружаем новый трек
                string trackPath = backgroundMusicTracks[trackIndex];
                currentBackgroundMusic = new AudioFileReader(trackPath);
                backgroundMusicPlayer.Init(currentBackgroundMusic);

                // Устанавливаем громкость
                UpdateMusicVolume();

                // Запускаем воспроизведение
                if (SoundEnabled && MusicVolume > 0.01f)
                {
                    backgroundMusicPlayer.Play();
                    Console.WriteLine($"Playing background track: {Path.GetFileName(trackPath)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing background track: {ex.Message}");
                // Пробуем следующий трек при ошибке
                if (backgroundMusicTracks.Count > 1)
                {
                    PlayNextBackgroundTrack();
                }
            }
        }

        private static void LoadSound(string name, string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var audioFile = new AudioFileReader(filePath);
                    var player = new WaveOutEvent();
                    player.Init(audioFile);

                    soundFiles[name] = audioFile;
                    soundPlayers[name] = player;

                    // Устанавливаем начальную громкость для эффектов
                    UpdateEffectsVolume();

                    Console.WriteLine($"Loaded sound: {name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading sound {filePath}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Sound file not found: {filePath}");
            }
        }

        public static void PlaySound(string soundName)
        {
            if (!SoundEnabled || !soundPlayers.ContainsKey(soundName)) return;

            try
            {
                // Сбрасываем позицию воспроизведения для повторного проигрывания
                soundFiles[soundName].Position = 0;

                // Обновляем громкость перед воспроизведением (на случай если изменилась)
                UpdateEffectsVolume();

                soundPlayers[soundName].Play();

                Console.WriteLine($"Playing sound effect: {soundName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound {soundName}: {ex.Message}");
            }
        }

        public static void PlayBackgroundMusic()
        {
            if (!SoundEnabled || backgroundMusicPlayer == null) return;

            try
            {
                if (backgroundMusicPlayer.PlaybackState != PlaybackState.Playing)
                {
                    PlayRandomBackgroundTrack();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing background music: {ex.Message}");
            }
        }

        public static void StopBackgroundMusic()
        {
            backgroundMusicPlayer?.Stop();
        }

        public static void UpdateMusicVolume()
        {
            if (currentBackgroundMusic != null)
            {
                float volume = MasterVolume * MusicVolume;
                currentBackgroundMusic.Volume = volume;

                Console.WriteLine($"Music volume set to: {volume:F2}");

                if (volume > 0.01f && SoundEnabled)
                {
                    if (backgroundMusicPlayer.PlaybackState != PlaybackState.Playing)
                    {
                        backgroundMusicPlayer.Play();
                    }
                }
                else
                {
                    backgroundMusicPlayer.Stop();
                }
            }
        }

        public static void UpdateEffectsVolume()
        {
            foreach (var soundFile in soundFiles.Values)
            {
                soundFile.Volume = MasterVolume * EffectsVolume;
            }
            Console.WriteLine($"Effects volume set to: {MasterVolume * EffectsVolume:F2}");
        }

        // Методы для воспроизведения конкретных звуков с эффектами
        public static void PlayButtonClick()
        {
            PlaySound("button_click");
        }

        public static void PlayEatSound()
        {
            PlaySound("eat");
        }

        public static void PlayWallHitSound()
        {
            PlaySound("wall_hit");
        }

        public static void PlaySelfHitSound()
        {
            PlaySound("self_hit");
        }

        public static void PlayGameOverSound()
        {
            PlaySound("game_over");
        }

        // Метод для принудительной смены трека
        public static void ChangeBackgroundTrack()
        {
            if (backgroundMusicTracks.Count > 1)
            {
                PlayNextBackgroundTrack();
            }
        }

        // Метод для получения информации о текущем треке
        public static string GetCurrentTrackName()
        {
            if (currentTrackIndex >= 0 && currentTrackIndex < backgroundMusicTracks.Count)
            {
                return Path.GetFileNameWithoutExtension(backgroundMusicTracks[currentTrackIndex]);
            }
            return "No track";
        }

        public static int GetTotalTracksCount()
        {
            return backgroundMusicTracks.Count;
        }

        public static int GetCurrentTrackIndex()
        {
            return currentTrackIndex;
        }

        public static void Dispose()
        {
            // Останавливаем и освобождаем эффекты
            foreach (var player in soundPlayers.Values)
            {
                player?.Stop();
                player?.Dispose();
            }
            soundPlayers.Clear();

            foreach (var file in soundFiles.Values)
            {
                file?.Dispose();
            }
            soundFiles.Clear();

            // Останавливаем и освобождаем фоновую музыку
            if (backgroundMusicPlayer != null)
            {
                backgroundMusicPlayer.PlaybackStopped -= BackgroundMusicPlayer_PlaybackStopped;
                backgroundMusicPlayer.Stop();
                backgroundMusicPlayer.Dispose();
            }

            currentBackgroundMusic?.Dispose();

            backgroundMusicPlayer = null;
            currentBackgroundMusic = null;
            backgroundMusicTracks.Clear();

            Console.WriteLine("SoundManager disposed");
        }
    }
}