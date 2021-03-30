using System;
using System.Collections.Generic;
using System.Text;
using BitterOS;
using System.IO;
using System.Threading;

namespace BitterOS.CommandLine
{
    class CommandParser
    {
        System.ExecutableHandler exeHandler = new System.ExecutableHandler();
        System.MusicScript ms = new System.MusicScript();
        public void Run(string userInput, bool restricted)
        {
            if (userInput != null || userInput != "")
            {
                string[] splitInput = userInput.Split("?");
                if (!restricted)
                {
                    switch (splitInput[0])
                    {
                        case "say":
                            Console.WriteLine(splitInput[1]);
                            break;
                        case "newFile":
                            File.Create(splitInput[1]);
                            break;
                        case "delFile":
                            Console.Write("Enter Password: ");
                            string pass = Console.ReadLine();
                            if (pass == File.ReadAllText("0:\\" + "sysfolder" + "\\password.sec"))
                            {
                                File.Delete(splitInput[1]);
                            }
                            else
                            {
                                Console.WriteLine("Incorrect Password. Cancelling Request...");
                            }
                            break;
                        case "write":
                            if (File.Exists(splitInput[1]))
                            {
                                File.WriteAllText(splitInput[1], splitInput[2]);
                            }
                            else
                            {
                                Console.WriteLine("File doesn't exist");
                            }
                            break;
                        case "dir":
                            Console.WriteLine("Files: ");
                            int sizeBytes = 0;
                            foreach (var f in Directory.GetFiles(Directory.GetCurrentDirectory()))
                            {
                                Console.WriteLine(f + ": " + new FileInfo(f).Length + "B");
                                sizeBytes += (int)new FileInfo(f).Length;
                            }

                            Console.WriteLine("Directories: ");
                            foreach (var d in Directory.GetDirectories(Directory.GetCurrentDirectory()))
                            {
                                if (d != "sysfolder")
                                {
                                    Console.WriteLine(d);
                                }
                            }
                            Console.WriteLine("Directory Size: " + sizeBytes + "B");
                            break;
                        case "newDir":
                            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), splitInput[1]));
                            break;
                        case "cd":
                            if (Directory.Exists(splitInput[1]) && splitInput[1] != "sysfolder")
                            {
                                Directory.SetCurrentDirectory(splitInput[1]);
                            }
                            break;
                        case "rmDir":
                            Console.Write("Enter Password: ");
                            string passs = Console.ReadLine();
                            if (passs == File.ReadAllText("0:\\" + "sysfolder" + "\\password.sec"))
                            {
                                if (Directory.Exists(splitInput[1]) && splitInput[1] != "sysfolder" && splitInput[1] != "0:/sysfolder")
                                {
                                    Directory.Delete(splitInput[1], true);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Incorrect Password. Cancelling Request...");
                            }
                            break;
                        case "help":
                            Console.WriteLine("BitterOS Help");
                            Console.WriteLine("You seperate command arguments using the ? character.");
                            Console.WriteLine("say?<what to say> - Says the argument.");
                            Console.WriteLine("newFile?<fileName> - Creates a new file in the current directory.");
                            Console.WriteLine("delFile?<fileName> - Deletes a file in the current directory.");
                            Console.WriteLine("write?<fileName>?<content> - Writes to an existing file.");
                            Console.WriteLine("dir - Gives information about the current directory.");
                            Console.WriteLine("newDir<directoryName> - Makes a new directory in the current directory.");
                            Console.WriteLine("cd<directoryName> - Sets the current directory to that directory.");
                            Console.WriteLine("rmDir<directoryName> - Removes the set directory.");
                            Console.WriteLine("help - Sends this help sequence :D");
                            Console.WriteLine("Power: off, restart");
                            break;
                        case "setup":
                            if (!File.Exists("0:\\sysfolder\\setup.sec"))
                            {
                                Console.Write("Choose Password: ");
                                string pase = Console.ReadLine();
                                File.WriteAllText("0:\\sysfolder\\password.sec", pase);
                                File.WriteAllText("0:\\sysfolder\\setup.sec", "Setup Ran.");
                                Console.WriteLine("Successfully set up the computer, restarting in 5 seconds...");
                                Cosmos.System.Power.Reboot();
                            }
                            break;
                        case "off":
                            Cosmos.System.Power.Shutdown();
                            break;
                        case "restart":
                            Cosmos.System.Power.Reboot();
                            break;
                        case "run":
                            Console.WriteLine(splitInput[1]);
                            new Batch().RunScript(File.ReadAllLines(splitInput[1]),0);
                            break;
                        /*case "textedit":
                            Console.WriteLine("BitterTextEditor: Write ENDTEXTFILE to stop writing.");
                            new Apps.TextEditor().drawTXEdit(System.Settings.kernel);
                            System.Settings.TextOn = true;
                            break;*/
                        case "readFile":
                            if (File.Exists(splitInput[1]))
                            {
                                Console.WriteLine(File.ReadAllText(splitInput[1]));
                            }
                            break;
                        case "time":
                            Console.WriteLine("Current Time: " + Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute);
                            break;
                        case "testSound":
                            int[] music = {1,1,1,0,0,1,1,1,0,0,0,1,1,1,0,0,0,1,0,1,0,1};
                            new System.SoundPlayer(music).playSound();
                            break;
                        /*case "CompileMS":
                            int[] data = ms.compileMS(File.ReadAllLines(splitInput[1]));
                            exeHandler.toMSX(splitInput[1].Split(".")[0], data);
                            break;
                        case "RunMS":
                            new System.SoundPlayer(exeHandler.decodeCSV(File.ReadAllText(splitInput[1]))).playSound();
                            break;*/
                    }
                }
                if(splitInput[0] == "setup")
                {
                    if (!File.Exists("0:\\sysfolder\\setup.sec"))
                    {
                        Console.Write("Choose Password: ");
                        string pase = Console.ReadLine();
                        File.WriteAllText("0:\\sysfolder\\password.sec", pase);
                        File.WriteAllText("0:\\sysfolder\\setup.sec", "Setup Ran.");
                        Console.WriteLine("Successfully set up the computer, restarting in 5 seconds...");
                        Cosmos.System.Power.Reboot();
                    }
                }
                if(splitInput[0] == "off")
                {
                    Cosmos.System.Power.Shutdown();
                }
                if (splitInput[0] == "restart")
                {
                    Cosmos.System.Power.Reboot();
                }
            }
        }
    }
}
