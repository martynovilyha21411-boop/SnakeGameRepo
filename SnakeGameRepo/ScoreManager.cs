using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace snake
{
    public static class ScoreManager
    {
        private static string GetDatabasePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, "SnakeGame");

            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);

            return Path.Combine(appFolder, "SnakeGameDB.db");
        }

        private static string connectionString => $"Data Source={GetDatabasePath()};Version=3;";

        // Инициализация базы данных
        public static void InitializeDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    CreateTables(connection);
                    InitializeGameModes(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации БД: {ex.Message}");
            }
        }

        private static void CreateTables(SQLiteConnection connection)
        {
            // Создаем таблицы если не существуют
            string[] createTables = {
                @"CREATE TABLE IF NOT EXISTS Player (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE
                )",

                @"CREATE TABLE IF NOT EXISTS GameMode (
                    Id INTEGER NOT NULL PRIMARY KEY,
                    Name TEXT
                )",

                @"CREATE TABLE IF NOT EXISTS HighScores (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    PlayerId INTEGER NOT NULL,
                    Score INTEGER NOT NULL,
                    GameModeId INTEGER NOT NULL,
                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (PlayerId) REFERENCES Player(Id),
                    FOREIGN KEY (GameModeId) REFERENCES GameMode(Id)
                )"
            };

            foreach (string sql in createTables)
            {
                using (var command = new SQLiteCommand(sql, connection))
                    command.ExecuteNonQuery();
            }
        }

        private static void InitializeGameModes(SQLiteConnection connection)
        {
            // Добавляем режимы игры если их нет
            string insertModes = @"
                INSERT OR IGNORE INTO GameMode (Id, Name) VALUES (1, 'Классический-Медленный');
                INSERT OR IGNORE INTO GameMode (Id, Name) VALUES (2, 'Классический-Нормальный');
                INSERT OR IGNORE INTO GameMode (Id, Name) VALUES (3, 'Классический-Быстрый');
                INSERT OR IGNORE INTO GameMode (Id, Name) VALUES (4, 'Бесконечный-Медленный');
                INSERT OR IGNORE INTO GameMode (Id, Name) VALUES (5, 'Бесконечный-Нормальный');
                INSERT OR IGNORE INTO GameMode (Id, Name) VALUES (6, 'Бесконечный-Быстрый');";

            using (var command = new SQLiteCommand(insertModes, connection))
                command.ExecuteNonQuery();
        }

        // Добавление рекорда
        public static void AddScore(string playerName, int score, GameSettings settings)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Получаем или создаем игрока
                    int playerId = GetOrCreatePlayer(connection, playerName);

                    // Получаем ID режима игры на основе настроек
                    int gameModeId = GetCurrentGameModeId(settings);

                    // Добавляем рекорд
                    string insertScore = @"
                        INSERT INTO HighScores (PlayerId, GameModeId, Score) 
                        VALUES (@PlayerId, @GameModeId, @Score)";

                    using (var command = new SQLiteCommand(insertScore, connection))
                    {
                        command.Parameters.AddWithValue("@PlayerId", playerId);
                        command.Parameters.AddWithValue("@GameModeId", gameModeId);
                        command.Parameters.AddWithValue("@Score", score);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения рекорда: {ex.Message}");
            }
        }

        private static int GetOrCreatePlayer(SQLiteConnection connection, string playerName)
        {
            string getPlayer = "SELECT Id FROM Player WHERE Name = @Name";
            using (var command = new SQLiteCommand(getPlayer, connection))
            {
                command.Parameters.AddWithValue("@Name", playerName);
                var result = command.ExecuteScalar();
                if (result != null) return Convert.ToInt32(result);
            }

            string insertPlayer = @"
                INSERT INTO Player (Name) VALUES (@Name);
                SELECT last_insert_rowid()";

            using (var command = new SQLiteCommand(insertPlayer, connection))
            {
                command.Parameters.AddWithValue("@Name", playerName);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        // Получение ID текущего режима игры
        public static int GetCurrentGameModeId(GameSettings settings)
        {
            string baseMode = settings.InfiniteField ? "Бесконечный" : "Классический";

            string speed;
            switch (settings.GameSpeed)
            {
                case 200:
                    speed = "Медленный";
                    break;
                case 100:
                    speed = "Нормальный";
                    break;
                case 50:
                    speed = "Быстрый";
                    break;
                default:
                    speed = "Нормальный";
                    break;
            }

            string gameModeName = $"{baseMode}-{speed}";

            // Добавьте отладочный вывод
            Console.WriteLine($"Определение режима: {gameModeName}");

            return GetGameModeIdByName(gameModeName);
        }

        private static int GetGameModeIdByName(string gameModeName)
        {
            var modes = GetAllGameModes();
            var mode = modes.FirstOrDefault(m => m.Name == gameModeName);
            return mode != null ? mode.Id : 2; // По умолчанию Классический-Нормальный
        }

        public static List<PlayerScoreView> GetTopPlayersByGameMode(int gameModeId, int topCount = 10)
        {
            var players = new List<PlayerScoreView>();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"
                        SELECT 
                            p.Name as PlayerName,
                            hs.Score,
                            gm.Name as GameModeName,
                            hs.CreatedDate
                        FROM HighScores hs
                        INNER JOIN Player p ON hs.PlayerId = p.Id
                        INNER JOIN GameMode gm ON hs.GameModeId = gm.Id
                        WHERE hs.GameModeId = @GameModeId
                        ORDER BY hs.Score DESC, hs.CreatedDate ASC
                        LIMIT @TopCount";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@GameModeId", gameModeId);
                        command.Parameters.AddWithValue("@TopCount", topCount);

                        using (var reader = command.ExecuteReader())
                        {
                            int rank = 1;
                            while (reader.Read())
                            {
                                players.Add(new PlayerScoreView
                                {
                                    Rank = rank++,
                                    PlayerName = reader["PlayerName"]?.ToString() ?? "Unknown",
                                    Score = reader["Score"] != DBNull.Value ? Convert.ToInt32(reader["Score"]) : 0,
                                    GameModeName = reader["GameModeName"]?.ToString() ?? "Unknown",
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.Now
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки рекордов: {ex.Message}");
            }

            return players;
        }

        // Получение всех режимов игры
        public static List<GameMode> GetAllGameModes()
        {
            var modes = new List<GameMode>();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT Id, Name FROM GameMode ORDER BY Id";

                    using (var command = new SQLiteCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            modes.Add(new GameMode
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"]?.ToString() ?? "Unknown"
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки режимов: {ex.Message}");
            }

            return modes;
        }

        public static void ClearAllScores()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Включаем поддержку внешних ключей
                    using (var command = new SQLiteCommand("PRAGMA foreign_keys = ON", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    string deleteSql = "DELETE FROM HighScores";

                    using (var command = new SQLiteCommand(deleteSql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка очистки рекордов: {ex.Message}");
            }
        }
    }
}