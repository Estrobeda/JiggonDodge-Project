using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JiggonDodger
{
    public class BlockRows
    {

        #region public
        public static Texture2D texture;
        public static float speed = 16f;

        public Vector2 position { get; set; }
        public int IndexToEmptyPath { get; set; }

        #endregion

        private float deltaTime;

        public BlockRows(Vector2 _blockPosition, Texture2D _boxTexture)
        {
            position = _blockPosition;
            SetRandomHole();
        }

        //Fix Collision on top of ScreenBoundary!!!
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
            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
             //   speed += (float)gameTime.ElapsedGameTime.TotalHours;
            position += Vector2.UnitY * speed * deltaTime;
               // BlockColor.CurrentColor.Update();
        }

        public void Draw()
        {
            for (int i = 0; i < Map.numberOfBoxesPerRow; i++)
            {
                if (i != IndexToEmptyPath)
                {
                    JiggonDodger.spriteBatch.Draw(texture, position + i * Vector2.UnitX * texture.Width, Color.White);
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