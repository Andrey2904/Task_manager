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
                Console.Write("Введите ID или имя процесса для завершения: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int processId))
                {
                    var processToKill = allProcesses.FirstOrDefault(p => p.Id == processId);
                    if (processToKill != null)
                    {
                        processToKill.Kill();
                        Console.WriteLine($"Процесс {processToKill.ProcessName} (ID: {processId}) завершен.");
                    }
                    else
                    {
                        Console.WriteLine("Процесс с таким ID не найден.");
                    }
                }
                else
                {
                    var processesToKill = allProcesses.Where(p => p.ProcessName.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    if (processesToKill.Any())
                    {
                        foreach (var proc in processesToKill)
                        {
                            proc.Kill();
                            Console.WriteLine($"Процесс {proc.ProcessName} (ID: {proc.Id}) завершен.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Процессы с таким именем не найдены.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

    }
}
