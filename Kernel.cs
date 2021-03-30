using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using XSharp;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.HAL;
using Cosmos.HAL.Drivers;
using Cosmos.System.Graphics;

namespace BitterOS
{
    public class Kernel : Sys.Kernel
    {
        public void BSOD(string message, string error, int errorCode)
        {
            System.Settings.currentCanvas.Clear(Color.Blue);
            new System.SoundPlayer(new int[] {1,1,1,0,1,1,0,1,1,1,1,0,0,0,1,0,1,1,0,1,0,0,1,1,1,1,0,0,1,0,1,1}).playSound();
            this.WriteFile(@"0:\log.txt", "BSOD Crash!! Exception: "+message);
            System.Settings.currentCanvas.DrawString("System Crash! " + message + " " + error + ": " + errorCode+" Shutdown in 8 seconds.", Sys.Graphics.Fonts.PCScreenFont.Default, new Pen(Color.White, 4), new Sys.Graphics.Point(System.Settings.currentCanvas.Mode.Rows / 2, System.Settings.currentCanvas.Mode.Columns / 2));
            this.DelayInMS(8000);
            Sys.Power.Shutdown();
        }
        public static Sys.FileSystem.CosmosVFS FS;
        public static bool passEntered = false;
        public static string pass;
        public static Apps.TextEditor txted = new Apps.TextEditor();
        System.Graphics.ExplorerGUI EXGUI = new System.Graphics.ExplorerGUI();
        protected override void BeforeRun()
        {
            
            FS = new Sys.FileSystem.CosmosVFS(); 
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(FS); 
            FS.Initialize();
            if (!Directory.Exists("0:\\sysfolder"))
            {
                Directory.CreateDirectory("0:\\sysfolder");
            }
            Directory.SetCurrentDirectory("0:\\");
            System.Settings.kernel = this;
            /*System.Settings.currentCanvas = Sys.Graphics.FullScreenCanvas.GetFullScreenCanvas();
            System.Graphics.BitterScreen bitScreen = new System.Graphics.BitterScreen();
            bitScreen.initScreen();
            bitScreen.DrawFilledRectangle(150, 150, 15, 15, Color.Blue);
            bitScreen.DrawBuffer();*/
            
            //vcan.Display();
            /*System.Graphics.Window wnd = new System.Graphics.Window(new IntPtr(1), "Long Program Name Lol Cool Right Haha LMAOOOOOOOO OKKKKKK grgrgrgrgrgrgrg", new Sys.Graphics.Point(150, 150), new Sys.Graphics.Point(200, 100));
            wnd.DrawWindow();*/
        }
        public void WriteFile(string path, string contents = "")
        {
            File.WriteAllText(path, contents);
        }
    private void MouseMan()
        {
            //System.Settings.currentCanvas.DrawFilledRectangle(new Sys.Graphics.Pen(Color.Black, 3), new Sys.Graphics.Point((int)Cosmos.System.MouseManager.X, (int)Cosmos.System.MouseManager.Y), 8, 8);
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
        protected override void Run()
        {
            
            /*Cosmos.System.MouseManager.ScreenHeight = (uint)vcan.Mode.Columns;
            Cosmos.System.MouseManager.ScreenWidth = (uint)vcan.Mode.Rows;*/
           
            /*MouseMan();
            System.Settings.currentCanvas.Clear(Color.White);
            DelayInMS(1);*/
            if (!File.Exists("0:\\sysfolder\\setup.sec"))
            {
                Console.WriteLine("You haven't run the setup on your machine yet. Please run the \"setup\" command to prevent the system from crashing.");
                var input = Console.ReadLine();
                CommandLine.CommandParser commandParser = new CommandLine.CommandParser();
                commandParser.Run(input, true);
            }
            else
            {
                if (System.Settings.TextOn == true)
                {
                    txted.drawTXEdit(this);
                }
                else
                {
                    if (passEntered == false)
                    {
                        Console.Write("Enter Password: ");
                        pass = Console.ReadLine();
                    }
                    if (pass == File.ReadAllText("0:\\sysfolder\\password.sec"))
                    {
                        passEntered = true;
                        Console.Write("<" + Directory.GetCurrentDirectory() + ">: ");
                        var input = Console.ReadLine();
                        CommandLine.CommandParser commandParser = new CommandLine.CommandParser();
                        commandParser.Run(input, false);
                        if(input == "textedit")
                        {
                            Console.WriteLine("BitterTextEditor: Write ENDTEXTFILE to stop writing.");
                            txted.drawTXEdit(System.Settings.kernel);
                            System.Settings.TextOn = true;
                        }
                    }
                }
            }

        }
    }
}
