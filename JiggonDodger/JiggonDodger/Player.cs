using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace JiggonDodger
{
    public class Player
    {

        #region Properties and variables
        public Vector2 PlayerPosition { get; set; }
        public Texture2D PlayerTexture { get; set; }
        public Vector2 playerOldPosition;

        public float playerRoatationAngle;
        public float _speed { get; set; }
        public Vector2 playerOriginPosition;
        public bool moveRightOrLeft = true;
        #endregion

        public void Update(GameTime gameTime)
        {
            
            playerOldPosition = PlayerPosition;
            PlayerPosition += KeyboardAction() * _speed;

            bool isWithinScreen = JiggonDodger.ScreenBoundary.Contains(this.GetBounds());
            bool isCollidingWithLines = JiggonDodger.BlockRows.Any(line => line.Overlaps(GetBounds()));

            if (!isWithinScreen || isCollidingWithLines)
            {
                PlayerPosition = playerOldPosition;
            }

            if (PlayerPosition.Y > JiggonDodger.ScreenBoundary.Height)
            {
                Health.Currenthealth.healthCount--;
                IsDead();
                
            }
        }

        private void IsDead()
        {
            if (!JiggonDodger.ScreenBoundary.Intersects(GetBounds()))
            {
                PlayerPosition = new Vector2(JiggonDodger.ScreenBoundary.Width / 2, JiggonDodger.ScreenBoundary.Height/16);
            }
        }

        private Vector2 KeyboardAction()
        {
            Vector2 movement = Vector2.Zero;
            KeyboardState keyboard = Keyboard.GetState();


            if (keyboard.IsKeyDown(Keys.Up)) { 
                movement -= Vector2.UnitY /3; 
                playerRoatationAngle = (float)Math.PI; 
                moveRightOrLeft = false; 
                playerOriginPosition.X = PlayerTexture.Width; 
                playerOriginPosition.Y = PlayerTexture.Height; 
            }
          
            if (keyboard.IsKeyDown(Keys.Right)) { 
                movement += Vector2.UnitX; 
                playerRoatationAngle = (float)Math.PI / 2; 
                moveRightOrLeft = true;
                playerOriginPosition.X = 0;
                playerOriginPosition.Y = PlayerTexture.Height; 
            }
           
            if (keyboard.IsKeyDown(Keys.Left)) { 
                movement -= Vector2.UnitX; 
                playerRoatationAngle = -(float)Math.PI / 2; 
                moveRightOrLeft = true;
                playerOriginPosition.X = PlayerTexture.Width;
                playerOriginPosition.Y = 0; 
            }
            if (keyboard == new KeyboardState()) {
                playerRoatationAngle = (float)Math.PI;
                moveRightOrLeft = false;
                playerOriginPosition.X = PlayerTexture.Width;
                playerOriginPosition.Y = PlayerTexture.Height; 
            }
            
            return movement;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)PlayerPosition.X, (int)PlayerPosition.Y, PlayerTexture.Width, PlayerTexture.Height);
        }

        public void Draw()
        {

            if (moveRightOrLeft)
            {
                JiggonDodger.SpriteBatch.Draw(PlayerTexture, PlayerPosition, null, Color.White, playerRoatationAngle, playerOriginPosition, 1, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                JiggonDodger.SpriteBatch.Draw(PlayerTexture, PlayerPosition, null, Color.White, playerRoatationAngle, playerOriginPosition, 1, SpriteEffects.FlipVertically, 0f);
            }
        }
    }
}