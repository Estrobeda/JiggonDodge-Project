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
        public Vector2 playerPosition { get; set; }
        public static Texture2D playerTexture { get; set; }

        private static float playerSpeed = 16;

        private Vector2 playerOldPosition;
        private Vector2 playerOriginPosition;
        private float playerRoatationAngle;
        //private float deltaTime;
        private bool moveRightOrLeft = true;
        #endregion


       

        public void Update(GameTime gameTime)
        {
          //  deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            playerOldPosition = playerPosition;
            playerPosition += KeyboardAction() * playerSpeed;

            bool isWithinScreen = JiggonDodger.screenBoundary.Contains(this.GetBounds());
            bool isCollidingWithLines = JiggonDodger._blockRowsList.Any(line => line.Overlaps(GetBounds()));

            if (!isWithinScreen || isCollidingWithLines)
            {
                playerPosition = playerOldPosition;
            }

            IsDead();
        }

        private void IsDead()
        {
            if (!JiggonDodger.screenBoundary.Intersects(GetBounds()))
            {
                Health.healthCount--;
                playerPosition = new Vector2(JiggonDodger.screenBoundary.Width / 2, JiggonDodger.screenBoundary.Height/16);
            }
        }

        private Vector2 KeyboardAction()
        {
            Vector2 movement = Vector2.Zero;
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard != new KeyboardState())
            {
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    movement -= Vector2.UnitY / 3;
                    playerRoatationAngle = (float)Math.PI;
                    moveRightOrLeft = false;
                    playerOriginPosition.X = playerTexture.Width;
                    playerOriginPosition.Y = playerTexture.Height;
                }

                if (keyboard.IsKeyDown(Keys.Right))
                {
                    movement += Vector2.UnitX;
                    playerRoatationAngle = (float)Math.PI / 2;
                    moveRightOrLeft = true;
                    playerOriginPosition.X = 0;
                    playerOriginPosition.Y = playerTexture.Height;
                }

                if (keyboard.IsKeyDown(Keys.Left))
                {
                    movement -= Vector2.UnitX;
                    playerRoatationAngle = -(float)Math.PI / 2;
                    moveRightOrLeft = true;
                    playerOriginPosition.X = playerTexture.Width;
                    playerOriginPosition.Y = 0;
                }
            }
            else
            {
                playerRoatationAngle = (float)Math.PI;
                moveRightOrLeft = false;
                playerOriginPosition.X = playerTexture.Width;
                playerOriginPosition.Y = playerTexture.Height;
            }

            return movement;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerTexture.Width, playerTexture.Height);
        }

        public void Draw()
        {
            if (moveRightOrLeft)
            {
                JiggonDodger.spriteBatch.Draw(playerTexture, playerPosition, null, Color.White, playerRoatationAngle, playerOriginPosition, 1, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                JiggonDodger.spriteBatch.Draw(playerTexture, playerPosition, null, Color.White, playerRoatationAngle, playerOriginPosition, 1, SpriteEffects.FlipVertically, 0f);
            }
        }
    }
}