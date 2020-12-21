using System.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Server
{
    public class SettingsManager
    {
        private static BinaryFormatter _formatter = new BinaryFormatter();
        private static string _filename = "settings.dat";

        //отображает в консоли меню настроек
        public static void ShowMenu()
        {
            bool isWorking = true;

            while (isWorking)
            {
                SettingsModel settings = SettingsManager.LoadSettings();
                //переменная чтоб запоминать измененные значения для вывода в диалогах
                object temp;

                Console.WriteLine(Environment.NewLine + "-------------------------------------------------");
                Console.WriteLine($"1.   \t Изменить IP adress ({settings.IPadress}):");
                Console.WriteLine($"2.   \t Изменить порт ({settings.Port}):");
                Console.WriteLine($"3.   \t Выйти из настроек");
                Console.WriteLine("-------------------------------------------------" + Environment.NewLine);

                int command;
                try
                {
                    command = Convert.ToInt32(Console.ReadLine());
                }

                catch 
                {
                    command = 3;
                }


                switch (command)
                {
                    case 1:
                        Console.WriteLine("Введите новый IPadress:");
                        temp = settings.IPadress;
                        settings.IPadress = Console.ReadLine().ToString();

                        if ((string)temp != settings.IPadress)
                        {
                            LogManager.AddLog($"Изменен IP адрес c {temp} на {settings.IPadress}", MessageStatus.Warning);
                            SettingsManager.SaveSettings(settings);
                        }
                        break;

                    case 2:
                        Console.WriteLine("Введите новый порт:");
                        temp = settings.Port;
                        settings.Port = Convert.ToInt32(Console.ReadLine());

                        if ((int)temp != settings.Port)
                        {
                            LogManager.AddLog($"Изменен IP адрес c {temp} на {settings.Port}", MessageStatus.Warning);
                            SettingsManager.SaveSettings(settings);
                        }

                        break;

                    case 3:
                        isWorking = false;
                        break;

                    default:
                        return;
                }
            }
        }

        //загружает настройки из файла (десериализирует)
        public static SettingsModel LoadSettings()
        {
            SettingsModel settings = new SettingsModel { IPadress = "127.0.0.1", Port = 8050 };

            if (!File.Exists(_filename))
            {
                SaveSettings(settings);
                return settings;
            }

            using (FileStream fs = new FileStream(_filename, FileMode.OpenOrCreate))
            {
                settings = (SettingsModel)_formatter.Deserialize(fs);
            }

            return settings;
        }

        //сохраняет настройки в файл (сериализирует)
        private static void SaveSettings(SettingsModel settings)
        {
            using (Stream fStream = new FileStream(_filename,
               FileMode.Create, FileAccess.Write, FileShare.None))
            {
                _formatter.Serialize(fStream, settings);
            }
        }

    }
}
