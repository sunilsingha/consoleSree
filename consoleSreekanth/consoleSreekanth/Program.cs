using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace consoleSreekanth
{
    class Program
    {
        /// <summary>
        /// C# code to run exe
        /// Exe will wait for user response after sometime (for example: Yes/No)
        /// Provide the response via code and proceed
        /// Wait till the exe is complete
        /// Find out exe is completed successfully or not.
        /// If not, then capture the error message.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Console.Title = AppName;

                UpdateMessage("Enter 'Y' to proceed else 'N' to stop process...");

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key.ToString().ToUpper() == "N")
                {
                    UpdateMessage("\nprocess stopped by user!");
                    Console.ReadKey();
                    return;
                }
                else if (key.Key.ToString().ToUpper() != "Y")
                {
                    throw new Exception("\nUser entered incorrect key...");
                }

                // Run external app
                Task.Factory.StartNew(
                      RunProcess
                ).Wait();

                // wait for sometime and validate if text file is opened in notepad
                if (!ValidateIfProcessCompleted())
                {
                    throw new Exception("process did not complete...");
                }
                else
                {
                    UpdateMessage("Process Completed!");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                UpdateErrorMessage(ex);
            }

        }

        const string AppName = "Demo App v0.0";

        static void UpdateMessage(string msg)
        {
            Console.WriteLine(msg + Environment.NewLine);
        }

        static void UpdateErrorMessage(Exception ex)
        {

            Console.WriteLine("\n########################################################");
            Console.WriteLine($"Error Occured: {ex.Message} {Environment.NewLine}");
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine("########################################################");
            Console.Read();
        }

        /// <summary>
        /// Its a textfile saved in bin directory
        /// we just open it used system.diagnostic process
        /// </summary>
        static void RunProcess()
        {
            try
            {
                UpdateMessage("\nRunning external process: Opening TextFile1.txt");
                Process.Start("notepad.exe", "TextFile1.txt");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to open sample text file. {Environment.NewLine}{ex.Message}");
            }

        }

        static bool ValidateIfProcessCompleted()
        {
            System.Threading.Thread.Sleep(3000);
            Process[] processes = Process.GetProcessesByName("notepad");
            for (int i = 0; i < processes.Length; i++)
            {                
                if (processes[i].MainWindowTitle.Equals("TextFile1.txt - Notepad"))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
