using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace JiggonDodger
{
    class Health
    {
        public static Texture2D hearthTexture { get; set; }
        public static Vector2 hearthPosition { get; set; }
        public static int healthCount { get; set; }

        public Health() 
        {
            Currenthealth = this;
        }
       
        public static Health Currenthealth{ get; set;}

        public void Draw()
        {
            for (int i = 0; i < healthCount; i++)
            {
                JiggonDodger.spriteBatch.Draw(hearthTexture, new Vector2(hearthPosition.X + hearthTexture.Width * i, hearthPosition.Y), Color.White);
            }
        }
    }
}
