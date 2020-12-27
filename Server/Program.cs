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
                Console.WriteLine("3.   \t Настройки");
                Console.WriteLine("4.   \t Показать логи");
                Console.WriteLine("5.   \t Связаться с разработчиком");
                Console.WriteLine("6.   \t Остановить сервер");
                Console.WriteLine("7.   \t Выход");
                Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                Console.WriteLine();
                
                Console.WriteLine("------------------------LOG----------------------");
                Console.WriteLine("           ***(ENTER для обновления)***" + Environment.NewLine);
                LogManager.ShowAllRecords(LogFormat.Short);
                Console.WriteLine("--------------------------------------------------");

                int command = 0;
                 try 
                {
                    command = Convert.ToInt32(Console.ReadLine());
                }
                
                catch 
                {
                    continue;
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
                            SettingsManager.ShowMenu();
                            break;

                        case 4:
                            LogManager.ShowMenu();
                            break;

                        case 5:
                            GoVisitMe();
                            break;

                        case 6:
                            server.Stop();
                            break;

                        case 7:
                            Environment.Exit(0);
                            continue;

                        default:
                            Console.Clear();
                            break;
                    }
                }

                Console.Clear();
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