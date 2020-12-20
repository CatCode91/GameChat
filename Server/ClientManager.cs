using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class ClientManager
    {
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
                        
                        break;
                    case 2:
                     
                        break;
                    case 3:
                      
                        break;
                    case 4:
                      
                        break;
                    case 5:
                        isWorking = false;
                        break;
                    default:
                        break;
                }
            }

        }

    }
}
