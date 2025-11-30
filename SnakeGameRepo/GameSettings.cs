using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    public class GameSettings
    {
        public int Width { get; set; } = 20;
        public int Height { get; set; } = 20;
        public bool UsePixelArt { get; set; } = true;
        public int TileSize { get; set; } = 40;
        public int GameSpeed { get; set; } = 100;
        public int InitialFoodCount { get; set; } = 1;
        public bool InfiniteField { get; set; } = false;
        public string GameMode { get; set; } = "Classic";
        public bool SoundEnabled { get; set; } = true; // Новая настройка
        public bool MusicEnabled { get; set; } = true; // Новая настройка

        // Для будущих модификаций
        public Dictionary<string, object> CustomSettings { get; set; } = new Dictionary<string, object>();
    }
}
