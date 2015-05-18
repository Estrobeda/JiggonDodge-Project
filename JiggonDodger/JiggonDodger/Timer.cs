/*
 * This Class will be created in order to make it possible to track time in the game
 * but also to create frame limits etc. This class shall be made so that it can be
 * reused in future game projects where any kind of a timer is needed for handling
 * countdown, countup and time tracking
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    public class Timer
    {
        private int timer = 0;
        private int limit;
        private int timeHolder;
        private bool isOneTick;


        public Timer(int limit) 
        {
            this.limit = limit;
        }

        /// <para>
        /// Ticker counts the timer untill the timer is equals or (more) than the Limit.
        /// </para>
        public void Ticker() 
        {
            timer++;
            isOneTick = false;
            if (timer >= limit)
            {
                timer = 0;
                timeHolder++;
                isOneTick = true;
            }
        }

        public bool IsOneTick()
        {
            return isOneTick;    
        }

        public int GetTime() {
            return timeHolder;
        }

        

        
        






















        /* TODO
         * Make the Timer class available for all classes 
         * so that timer and counter can be removed from BlockRows, BlockColor etc
         */
     /*   #region variables
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
        }*/
    }
}
