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
        public SpriteFont pointsFont { get; set; }
        public Texture2D pointsTexture { get; set; }
        public Vector2 pointsPosition { get; set; }
        public Vector2 pointTextPosition { get; set; }

        public Points()
        {
            CurrentPoints = this;
        }

        public static Points CurrentPoints { get; set; }

        public void Draw()
        {
            JiggonDodger.SpriteBatch.DrawString(pointsFont, CurrentTimer.Time.ToString(), pointTextPosition, Color.White);
            JiggonDodger.SpriteBatch.Draw(pointsTexture, pointsPosition, Color.Yellow);
        }
    }
}
