using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    class Points
    {

        #region properties
        public static SpriteFont pointsFont { get; set; }
        public static Texture2D pointsTexture { get; set; }
        public static Vector2 pointsPosition { get; set; }
        public static Vector2 pointTextPosition { get; set; }
        public bool isGameOver = false;
        #endregion

        private Timer timer = new Timer(60);

        public Points()
        {
            CurrentPoints = this;
        }

        public void Update()
        {
                timer.Ticker();
        }

        public void resetPoints() {
            timer.SetTime(0);
        }

        public static Points CurrentPoints { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(pointsFont, timer.GetTime().ToString(), pointTextPosition, Color.White);
            spriteBatch.Draw(pointsTexture, pointsPosition, Color.Yellow);
        }
    }
}
