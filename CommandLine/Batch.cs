using System;
using System.Collections.Generic;
using System.Text;
using BitterOS;
using System.IO;
using System.Threading;
using Cosmos;
using Sys = Cosmos.System;

namespace BitterOS.CommandLine
{
    class Batch
    {
        private bool permissionEdit = false;
        public bool passwordPrompt()
        {
            bool isCorrect = false;
            Console.Write("!This Program Can Damage Your Computer! Enter Password: ");
            string pass = Console.ReadLine();
            if (pass == File.ReadAllText("0:\\sysfolder\\password.sec"))
            {
                isCorrect = true;
            }
            else
            {
                Console.WriteLine("Incorrect Password. Hint(first letter): " + File.ReadAllText("0:\\sysfolder\\password.sec")[0]);
                return isCorrect;
            }
            return isCorrect;
        }
        public void DelayInMS(int ms) // Stops the code for milliseconds and then resumes it (Basically It's delay)
        {
            for (int i = 0; i < ms * 100000; i++)
            {
                ;
                ;
                ;
                ;
                ;
            }
        }
        public void RunScript(string[] codeLine, int startIndex = 0)
        {
            int index = startIndex;
            try
            {
                while (index < codeLine.Length)
                {
                    string line = codeLine[index];
                    index++;
                    string[] split = line.Split("?");
                    switch (split[0])
                    {
                        case "askPassword":
                            bool correct = passwordPrompt();
                            if (correct)
                            {
                                permissionEdit = true;
                            }
                            break;
                        case "say":
                            Console.WriteLine(split[1]);
                            break;
                        case "read":
                            if (File.Exists(@"0:\sysfolder\" + split[1]))
                            {
                                if (permissionEdit == true)
                                {
                                    Console.WriteLine(File.ReadAllText(split[1]));
                                }
                                else
                                {
                                    Console.WriteLine("Access Denied.");
                                }
                            }
                            if (File.Exists(split[1]))
                            {
                                Console.WriteLine(File.ReadAllText(split[1]));
                            }
                            else
                            {
                                Console.WriteLine("File Doesn't Exist");
                            }
                            break;
                        case "input":
                            string input = Console.ReadLine();
                            if (input == split[1])
                            {
                                try
                                {
                                    this.RunScript(codeLine, index+1);
                                }
                                catch (Exception e)
                                {
                                    throw e;
                                }
                            }
                            break;
                        case "run":
                            try
                            {
                                this.RunScript(codeLine, index + 1);
                            }
                            catch(Exception e)
                            {
                                throw e;
                            }
                            break;
                        case "key":
                            if(Console.ReadKey().Key.ToString() == split[1])
                            {
                                try
                                {
                                    this.RunScript(codeLine, index + 1);
                                }
                                catch (Exception e)
                                {
                                    throw e;
                                }
                            }
                            break;
                        case "fileNew":
                            if (permissionEdit == true)
                            {
                                File.Create(split[1]);
                            }
                            break;
                        case "fileDelete":
                            if (permissionEdit == true)
                            {
                                if (File.Exists(split[1]))
                                {
                                    File.Delete(split[1]);
                                }
                                else
                                {
                                    Console.WriteLine("File does not exist");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Access Denied.");
                            }
                            break;
                        case "newDir":
                            if (permissionEdit == true)
                            {
                                Directory.CreateDirectory(split[1]);
                            }
                            else
                            {
                                Console.WriteLine("Access Denied.");
                            }
                            break;
                        case "deleteDir":
                            if (permissionEdit == true)
                            {
                                if (Directory.Exists(split[1]))
                                {
                                    Directory.Delete(split[1]);
                                }
                                else
                                {
                                    Console.WriteLine("Directory does not exist");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Access Denied.");
                            }
                            break;
                        case "beep":
                            Console.Beep();
                            break;
                        case "wait":
                            this.DelayInMS(int.Parse(split[1]));
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }      
    }
}
