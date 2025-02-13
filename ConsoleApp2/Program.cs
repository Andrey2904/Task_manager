using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp2
{
    class Program
    {
        static void Main()
        {
            try
            {
                Process currentProcess = Process.GetCurrentProcess();
                Process[] allProcesses = Process.GetProcesses();

                Console.WriteLine("Список запущенных процессов:");
                foreach (Process process in allProcesses)
                {
                    string marker = process.Id == currentProcess.Id ? "<-- текущее приложение" : "";
                    Console.WriteLine($"{process.ProcessName} (ID: {process.Id}) {marker}");
                }

                Console.Write("Введите имя процесса для поиска его ID: ");
                string processName = Console.ReadLine();
                var matchingProcesses = allProcesses.Where(p => p.ProcessName.IndexOf(processName, StringComparison.OrdinalIgnoreCase)>=0).ToList();
                if (matchingProcesses.Any())
                {
                    Console.WriteLine("Найденные процессы:");
                    foreach (var proc in matchingProcesses)
                    {
                        Console.WriteLine($"{proc.ProcessName} (ID: {proc.Id})");
                    }
                }
                else
                {
                    Console.WriteLine("Процессы с таким именем не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

    }
}
