using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessInformation
{
    class Program
    {
        private static void ShowMenu()
        {
            Console.WriteLine(new string('-', 30));
            Console.WriteLine("1. Show all processes.");
            Console.WriteLine("2. Select process by Id.");
            Console.WriteLine("3. Start process by name.");
            Console.WriteLine("4. Kill process by name.");
            Console.WriteLine("5. Show threads in process.");
            Console.WriteLine("6. Show modules in process.");
            Console.WriteLine("0. Close solution");
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();
        }

        private static int GetId()
        {
            try
            {
                Console.Write("Enter: ");
                int select = Int32.Parse(Console.ReadLine());
                return select;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        private static string GetName()
        {
            try
            {
                Console.Write("Enter ProcessName: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                    return name;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        private static void Start()
        {
            ProcInfo instance = new ProcInfo();
            bool worker = true;
            while (worker)
            {
                Console.Clear();
                ShowMenu();
                int select = GetId();
                Console.WriteLine();

                switch (select)
                {
                    case 1:
                        {
                            instance.ShowProcesses();
                            break;
                        }
                    case 2:
                        {
                            instance.GetProcessById(GetId());
                            break;
                        }
                    case 3:
                        {
                            instance.StartProc(GetName());
                            instance.UpdateProc();
                            break;
                        }
                    case 4:
                        {
                            instance.KillProc(GetName());
                            instance.UpdateProc();
                            break;
                        }
                    case 5:
                        {
                            instance.ShowThreadsInProcess(GetName());
                            break;
                        }
                    case 6:
                        {
                            instance.ShowModulesInProcess(GetName());
                            break;
                        }
                    case 0:
                        {
                            worker = false;
                            break;
                        }
                    default:
                        break;
                }
                Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {
            Start();
        }
    }
}

//Написать консольное .NET приложение, позволяющее пользователю получить:
//1. Список всех процессов
//2. Выбрать процесс по PID
//3. Запустить процесс
//4. Оставить процесс
//5. Показать информацию о потоках
//6. Показать информацию о модулях
//Предоставить пользователю меню выбора (по пунктам).