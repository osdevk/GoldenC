using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace BitterOS.System //Clear
{
   public class SoundPlayer
    {

        int[] soundData;
        public SoundPlayer(int[] sound) { soundData = sound; }
        ~SoundPlayer() { }
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
        public void playSound()
        {
            for(int i=0; i<soundData.Length; i++)
            {
                if(soundData[i] == 1)
                {
                    Console.Beep();
                    DelayInMS(65);
                }
                if(soundData[i] == 0)
                {
                    DelayInMS(500);
                }
            }
        }
    }
}
