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
        #endregion

        public Timer()
        {
            CurrentTimer = this;
        }

        public static Timer CurrentTimer { get; set; }

        public void Update(GameTime gameTime)
        {
            SetTimer(gameTime);
        }

        private void SetTimer(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds == 100)
            {
                Time +=1;
            }
        }
    }
}
