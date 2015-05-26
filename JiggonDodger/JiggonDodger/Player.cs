using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JiggonDodger
{
    public class Player
    {

        #region Properties and variables
        public Vector2 position { get; set; }
        public static Texture2D texture { private get; set; }
        public static SoundEffect hitEffect {  get; set; }
        public static ParticleEngine tailEngine;
        public bool isMuted = false;

        private bool isHit;
        private Timer timer = new Timer(20);

        private static float playerSpeed = 16;
        
        private Vector2 playerOldPosition;
        private Vector2 playerOriginPosition;
        
        private float playerRoatationAngle;
        private bool moveRightOrLeft = true;

        #endregion

        public void Update(GameTime gameTime)
        {
            #region Update Components
            if (!JiggonDodger.isGameOver)
            {
                timer.Ticker();
              
                playerOldPosition = position;
                position += KeyboardAction() * playerSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 25;

                tailEngine.Update();
                tailEngine.EmitterLocation = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);

            
             #endregion
                bool isWithinScreen = JiggonDodger.screenBoundary.Contains(this.GetBounds());
                bool isCollidingWithLines = JiggonDodger._blockRowsList.Any(line => line.Overlaps(GetBounds()));

                if (!isWithinScreen || isCollidingWithLines)
                {
                    position = playerOldPosition;
                }

                if (isCollidingWithLines && !isMuted)
                {
                    if (!isHit)
                    {
                        Console.WriteLine("Is playering");
                        hitEffect.Play();
                    }

                    isHit = true;
                }
                else
                {
                    if (timer.IsOneTick())
                    {
                      
                        isHit = false;
                    }
                }

                IsDead();
            }
        }


        private Vector2 KeyboardAction()
        {
            Vector2 movement = Vector2.Zero;
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard != new KeyboardState())
            {
                if (keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Right) && !keyboard.IsKeyDown(Keys.Left))
                {
                    movement -= Vector2.UnitY / 3;
                    playerRoatationAngle = (float)Math.PI;
                    moveRightOrLeft = false;
                    playerOriginPosition.X = texture.Width;
                    playerOriginPosition.Y = texture.Height;
                }

                if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.Right) && keyboard.IsKeyDown(Keys.Up))
                {
                    movement += Vector2.UnitX;
                    playerRoatationAngle = (float)Math.PI / 2;
                    moveRightOrLeft = true;
                    playerOriginPosition.X = 0;
                    playerOriginPosition.Y = texture.Height;
                }

                if (keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyDown(Keys.Up))
                {
                    movement -= Vector2.UnitX;
                    playerRoatationAngle = -(float)Math.PI / 2;
                    moveRightOrLeft = true;
                    playerOriginPosition.X = texture.Width;
                    playerOriginPosition.Y = 0;
                }
            }
            else
            {
                playerRoatationAngle = (float)Math.PI;
                moveRightOrLeft = false;
                playerOriginPosition.X = texture.Width;
                playerOriginPosition.Y = texture.Height;
            }

            return movement;
        }


        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        private void IsDead()
        {
            if (!JiggonDodger.screenBoundary.Intersects(GetBounds()))
            {
                Health.healthCount--;
                position = new Vector2(JiggonDodger.screenBoundary.Width / 2, JiggonDodger.screenBoundary.Height / 16);
            }
        }

  


        public void Draw(SpriteBatch spriteBatch)
        {

     
                tailEngine.Draw(spriteBatch);
            
                if (moveRightOrLeft)
                {
                    spriteBatch.Draw(texture,
                                                  position,
                                                  null,
                                                  Color.White,
                                                  playerRoatationAngle,
                                                  playerOriginPosition,
                                                  1,
                                                  SpriteEffects.FlipHorizontally,
                                                  0f);
                }
                else
                {
                    spriteBatch.Draw(texture,
                                                  position,
                                                  null,
                                                  Color.White,
                                                  playerRoatationAngle,
                                                  playerOriginPosition,
                                                  1,
                                                  SpriteEffects.FlipVertically,
                                                  0f);
                }
        }
    }
}