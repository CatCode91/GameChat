using System;
using System.Diagnostics;

namespace Server
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Server server = new Server();
            MainMenu(server);
        }

        //Основное меню с выбором функций
        private static void MainMenu(Server server)
        {
            bool isWorking = true;
            server.UsefulMessages += Server_UsefulMessages;

            while (isWorking)
            {
                Console.WriteLine();
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "GAME CHAT SERVER"));
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "ver 1.0    "));
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                Console.WriteLine("1.   \t Запустить сервер");
                Console.WriteLine("2.   \t Статус сервера");
                Console.WriteLine("3.   \t Управление клиентами");
                Console.WriteLine("4.   \t Настройки");
                Console.WriteLine("5.   \t Показать логи");
                Console.WriteLine("6.   \t Связаться с разработчиком");
                Console.WriteLine("7.   \t Остановить сервер");
                Console.WriteLine("8.   \t Выход");
                Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                Console.WriteLine();

                Console.WriteLine("------------------------LOG-----------------------");
                LogManager.ShowAllRecords(LogFormat.Short);
                Console.WriteLine("--------------------------------------------------");

                int command = 0;
                 try 
                {
                    command = Convert.ToInt32(Console.ReadLine());
                }
                
                catch 
                {
                    Console.WriteLine("Некорректный ввод!");
                }
                    

                    switch (command)
                    {
                        case 1:                
                            server.Run();
                            break;

                        case 2:
                            server.ShowStatus();
                            break;

                        case 3:
                            ClientManager.ShowMenu();
                            break;

                        case 4:
                            SettingsManager.ShowMenu();
                            break;

                        case 5:
                            LogManager.ShowMenu();
                            break;

                        case 6:
                            GoVisitMe();
                            break;

                        case 7:
                            server.Stop();
                            break;

                        case 8:
                            Environment.Exit(0);
                            continue;

                        default:
                            Console.Clear();
                            break;
                    }
                }

                Console.Clear();
            }

        //Метод, подписаный на событие UsefulMessages на сервере. Если что-то происходит на сервере, метод отображает в консольке.
        private static void Server_UsefulMessages(string text)
        {
            Console.WriteLine(text);
        }

        // Заходите в гости, буду рад :)
        private static void GoVisitMe() 
        {
            var psi = new ProcessStartInfo
            {
                FileName = "cmd",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = $"/c start {"https://t.me/daniel_stupakevich"}"
            };
            Process.Start(psi);
        }
    }
}