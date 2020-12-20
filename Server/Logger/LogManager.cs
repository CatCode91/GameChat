using System;
using System.Collections.Generic;
using System.Linq;

namespace Server
{
    public class LogManager
    {
        //хранит в себе коллекцию лог файлов работы сервера
        private static List<LogModel> _logs = new List<LogModel>();

        //отображает основное меню работы сервера
        public static void ShowMenu()
        {
            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine(Environment.NewLine + "-------------------------------------------------");
                Console.WriteLine("1.   \t Показать полный лог:");
                Console.WriteLine("2.   \t Показать полный лог за дату:");
                Console.WriteLine("3.   \t Показать ошибки:");
                Console.WriteLine("4.   \t Показать предупреждения:");
                Console.WriteLine("5.   \t Выход:");
                Console.WriteLine("-------------------------------------------------" + Environment.NewLine);

                int command = Convert.ToInt32(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        LogManager.ShowAllRecords(LogFormat.Full);
                        break;
                    case 2:
                        Console.WriteLine("Введите дату(ддммгггг):");
                        LogManager.ShowRecordsOnDate(Convert.ToDateTime(Console.ReadLine()), LogFormat.Full);
                        break;
                    case 3:
                        LogManager.ShowByCategory(MessageStatus.Error, LogFormat.Short);
                        break;
                    case 4:
                        LogManager.ShowByCategory(MessageStatus.Warning, LogFormat.Short);
                        break;
                    case 5:
                        isWorking = false;
                        break;
                    default:
                        break;
                }
            }
        }

        //добавляет запись в коллекцию логов
        public static void AddLog(string text, MessageStatus status) 
        {
            LogModel record = new LogModel(text,status);
            if(!string.IsNullOrEmpty(record.Text))
            _logs.Add(record);
        }

        /// <summary>
        /// Отображает все записи из коллекции логов
        /// </summary>
        /// <param name="format">Позволяет задать детальность отображения записи</param>
        public static void ShowAllRecords(LogFormat format) 
        {
            ShowInConsole(_logs, format);
        }

        private static void ShowByCategory(MessageStatus status,LogFormat format)
        {
            ShowInConsole(_logs.Where(x => x.Status == status), format);
        }

        private static void ShowRecordsOnDate(DateTime date,LogFormat format)
        {
            ShowInConsole(_logs.Where(x => x.Date.Date == date.Date), format);
        }

        //позволяет отобразить переданную коллекцию логов в консоли с заданной детализацией
        private static void ShowInConsole(IEnumerable<LogModel> collection, LogFormat format) 
        {
            if (collection?.Count() == 0) 
            {
                Console.WriteLine("В журнале нет записей...");
            }

            foreach (LogModel record in collection.ToArray())
            {
                switch (format)
                {
                    case LogFormat.Full:
                        Console.WriteLine($"{record.Status} - {record.Date.ToString("dd MM yyyy HH:mm")} - {record.WorkStation} - {record.UserName} - {record.Text}");
                        break;
                    case LogFormat.Short:
                        Console.WriteLine($"{record.Status} - {record.Date.ToString("HH:mm:ss")} - {record.Text}");
                        break;
                }
            }
        }
    }
}