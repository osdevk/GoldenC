using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace BitterOS.System
{
    //MusicScript compiles to MusicByte which gets played with the sound player.
   public class MusicScript
    {
        public int[] compileMS(string[] text)
        {
            int[] compiledContent = { };
            for(int i = 0; i<text.Length; i++)
            {
                string currentLine = text[i];
                if(currentLine == "Beep")
                {
                    compiledContent.SetValue(1, i);
                }
                if(currentLine == "Stop")
                {
                    compiledContent.SetValue(0, i);
                }
            }
            return compiledContent;
        }
    }
}
