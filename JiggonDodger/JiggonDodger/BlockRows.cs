using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JiggonDodger
{
    public class BlockRows
    {
        //Check OOP
        #region Variables
        private Rectangle[] animationHolder;
        private float deltaTime;
        private int frame;
        private static float speed = 16f;
        Timer timer = new Timer(20);
        #endregion

        #region Properties
        public static Texture2D texture { get; set; }
        public static Texture2D arrowindication { get; set; }
        public static float Speed { get { return speed; } set { speed = value; } }
        public Vector2 position { get; set; }
        public int IndexToEmptyPath { get; set; }
        #endregion


        public BlockRows(Vector2 _blockPosition, Texture2D _boxTexture)
        {
            BlockColor.CurrentColor.setColor(16);
            animationHolder = new Rectangle[] { new Rectangle(0, 0, 16, 42), 
                                                new Rectangle(16, 0, 16, 42), 
                                                new Rectangle(32, 0, 16, 42), 
                                                new Rectangle(48, 0, 16, 42)};
            position = _blockPosition;
            SetRandomHole();
        }

        internal void GenerateRandom()
        {
            position = Vector2.Zero - (Vector2.UnitY * texture.Height);
            SetRandomHole();
        }

        public void SetRandomHole()
        {

            IndexToEmptyPath = JiggonDodger.random.Next(Map.getNumberOfBoxesPerRow);
            
        }

        public void Update(GameTime gameTime)
        {
            if (!JiggonDodger.isGameOver)
            {
                timer.Ticker();
                if (timer.IsOneTick())
                {
                    frame++;
                    if (frame >= animationHolder.Length)
                    {
                        frame = 0;
                    }
                }


                deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                position += Vector2.UnitY * speed * deltaTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Map.getNumberOfBoxesPerRow ; i++)
            {
                if (i != IndexToEmptyPath)
                {
                    spriteBatch.Draw(texture, 
                                                  position + i * Vector2.UnitX * texture.Width, 
                                                  BlockColor.CurrentColor.getColor(Map.getNumberOfBoxesPerRow - i));  //Change CurrentColor.getColor(Map.numberOfBoxesPerRow-i) to Color.White if the color changes are to extreame
                }
                else 
                {
                    spriteBatch.Draw(arrowindication, 
                                                  new Rectangle((int)(position.X + i * texture.Width) + texture.Width/4, //Center on X axis
                                                        (int)position.Y + texture.Height/4, //Center on Y axis
                                                        32, //tile width
                                                        32), //tile height
                                                        animationHolder[frame], //Current frame texture
                                                      Color.White); 
                }
            }
        }

        public bool Overlaps(Rectangle rect)
        {
            Rectangle testRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            Vector2 originalPos = JiggonDodger.linkToPlayer.position;
           
            for (int i = 0; i < Map.getNumberOfBoxesPerRow; i++)
            {
                testRectangle.X = i * texture.Width;
                if (i != IndexToEmptyPath && testRectangle.Intersects(rect))
                {
                    return true;
                }

            }
            return false;
        }

        public Rectangle Bounds()
        {

            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
}