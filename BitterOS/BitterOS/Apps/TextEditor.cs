using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BitterOS.Apps
{
    public class TextEditor
    {
        string textWritten = "";
        public bool isWriting = false;
        public void drawTXEdit(BitterOS.Kernel krnl)
        {
            isWriting = true;
            
            Console.Write(">>");
            string txt = Console.ReadLine();
            if(txt == "ENDTEXTFILE")
            {
                Console.WriteLine("Filename?: ");
                string flname = Console.ReadLine();
                krnl.WriteFile(Path.Combine(Directory.GetCurrentDirectory(), flname), textWritten);
                System.Settings.TextOn = false;
            }
            else
            {
                textWritten += txt+"\n";
            }
        }
    }
}
