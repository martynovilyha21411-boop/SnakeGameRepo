using System;
using System.Windows.Forms;

namespace snake
{
    internal static class Program
    {
        public static string PlayerNickname { get; set; } = "Player";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ScoreManager.InitializeDatabase();


            // Показываем форму ввода никнейма до тех пор, пока не введут валидный ник
            while (true)
            {
                using (var nicknameForm = new NicknameForm())
                {
                    if (nicknameForm.ShowDialog() == DialogResult.OK)
                    {
                        PlayerNickname = nicknameForm.Nickname;
                        break; // Выходим из цикла если ник введен
                    }
                    // Если форма закрыта без валидного ника - показываем снова
                }
            }

            // Запускаем основную форму
            Application.Run(new Form1());
        }
    }
}