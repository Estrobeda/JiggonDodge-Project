using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    class Points : Timer
    {
        #region properties
        public static SpriteFont pointsFont { get; set; }
        public static Texture2D pointsTexture { get; set; }
        public static Vector2 pointsPosition { get; set; }
        public static Vector2 pointTextPosition { get; set; }
        #endregion

        public Points()
        {
            CurrentPoints = this;
        }

        public static Points CurrentPoints { get; set; }

        public void Draw()
        {
            JiggonDodger.spriteBatch.DrawString(pointsFont, CurrentTimer.Time.ToString(), pointTextPosition, Color.White);
            JiggonDodger.spriteBatch.Draw(pointsTexture, pointsPosition, Color.Yellow);
        }
    }
}
