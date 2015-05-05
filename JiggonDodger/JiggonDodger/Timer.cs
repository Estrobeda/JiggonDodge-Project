using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    public class Timer
    {
        #region variables
        public int Time { get;  set; }
        private int timerCount = 0;
        private int timerLimit = 60;
        #endregion


        public Timer()
        {
            
            CurrentTimer = this;
        }

        public static Timer CurrentTimer { get; set; }

        public void Update(GameTime gameTime)
        {
            timerCount++;
            SetTimer(gameTime);
        }

        private void SetTimer(GameTime gameTime)
        {
            if (timerCount >= timerLimit)
            {
                
                Time++;
                timerCount = 0;
            }
        }
    }
}
