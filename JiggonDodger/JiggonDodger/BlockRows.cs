using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JiggonDodger
{
    public class BlockRows
    {

        #region public
        public static Texture2D texture;
        public static Texture2D arrowindication;
        public static float speed = 16f;

        public Vector2 position { get; set; }
        public int IndexToEmptyPath { get; set; }

        private Rectangle[] animationHolder;
        #endregion

        private float deltaTime;
        private int timer;
        private int counter = 20;
        private int frame;


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

            IndexToEmptyPath = JiggonDodger.random.Next(Map.numberOfBoxesPerRow);
            
        }

        public void Update(GameTime gameTime)
        {
            timer++;
            if (timer >= counter)
            {
                timer = 0;
                frame++;
                if (frame >= animationHolder.Length)
                {
                    frame = 0;
                }
            }
            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            position += Vector2.UnitY * speed * deltaTime;     
        }

        public void Draw()
        {
            for (int i = 0; i < Map.numberOfBoxesPerRow ; i++)
            {
                if (i != IndexToEmptyPath)
                {
                    JiggonDodger.spriteBatch.Draw(texture, 
                                                  position + i * Vector2.UnitX * texture.Width, 
                                                  BlockColor.CurrentColor.getColor(Map.numberOfBoxesPerRow - i));
                }
                else {
                    JiggonDodger.spriteBatch.Draw(arrowindication, 
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
            Vector2 originalPos = JiggonDodger.linkToPlayer.playerPosition;
           
            for (int i = 0; i < Map.numberOfBoxesPerRow; i++)
            {
                testRectangle.X = i * texture.Width;
                if (i != IndexToEmptyPath && testRectangle.Intersects(rect))
                {
                    return true;
                }

            }
            return false;
        }
    }
}