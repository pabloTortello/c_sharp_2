using System;
using System.Windows.Forms;
using System.Drawing;

namespace StarWars
{
    /// <summary>Класс программы</summary>
    internal static class Program
    {
        /// <summary>Точка входа в программу</summary>
        [STAThread]
        private static void Main()
        {
            #region Активация стилей оформления пользовательского интерфейса для приложения Win-Forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 
            #endregion

            // Создаём главную форму
            Form game_form = new Form
            {
                Width = 800,
                Height = 600,
                FormBorderStyle = FormBorderStyle.FixedSingle // Запрещаем ей менять свои размеры
            };

            //Button new_game_btn = new Button
            //{
            //    Location = new Point(340, 190),
            //    Text = "Новая игра",
            //    Size = new Size(120,50)
            //};

            //Button highscores_btn = new Button
            //{
            //    Location = new Point(350, 250),
            //    Text = "Рекорды",
            //    Size = new Size(100, 40)
            //};

            //Button exit_btn = new Button
            //{
            //    Location = new Point(350, 300),
            //    Text = "Выход",
            //    Size = new Size(100, 40)
            //};
            //game_form.Controls.Add(new_game_btn);
            //game_form.Controls.Add(highscores_btn);
            //game_form.Controls.Add(exit_btn);

            Game.Load(game_form);           // Загрузка данных игровой логики
            Game.Init(game_form);  // Инициализация игровой логики

            game_form.Show();      // Показываем форму на экране

            //Game.Draw();         // Отрисовываем кадр

            Application.Run(game_form); // Запускаем процесс обработки очереди сообщений Windows
        }
    }
}
