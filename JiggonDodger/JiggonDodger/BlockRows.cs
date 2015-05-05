using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JiggonDodger
{
    public class BlockRows : JiggonDodger
    {

        #region Variables and Properties
        public Vector2 BlockPosition { get; set; }
        public Texture2D BlockTexture { get; set; }
        public int IndexToEmptyPath { get; set; }
       
        #endregion

        public BlockRows(Vector2 _blockPosition, Texture2D _boxTexture)
        {
            BlockPosition = _blockPosition;
            BlockTexture = _boxTexture;
            SetRandomHole();
        }

        //Fix Collision on top of ScreenBoundary!!!
        internal void GenerateRandom()
        {
            BlockPosition = Vector2.Zero - (Vector2.UnitY * blockTexture.Height);
            SetRandomHole();
        }

        public void SetRandomHole()
        {

            IndexToEmptyPath = JiggonDodger.Random.Next(JiggonDodger.NumberOfBoxesPerRow);
            
        }

        public void Update(GameTime _gameTime)
        {
                BoxSpeed += (float)_gameTime.ElapsedGameTime.TotalHours;
                BlockPosition += Vector2.UnitY * (float)_gameTime.ElapsedGameTime.TotalMilliseconds * BoxSpeed;
                BlockColor.CurrentColor.Update();
            
        }

        public void Draw()
        {
            for (int i = 0; i < NumberOfBoxesPerRow; i++)
            {
                if (i != IndexToEmptyPath)
                {
                    SpriteBatch.Draw(BlockTexture, BlockPosition + i * Vector2.UnitX * BlockTexture.Width, BlockColor.CurrentColor.spriteColor);
                }


            }

            //JiggonDodger.SpriteBatch.DrawString(JiggonDodger.scoreFont, Speed.ToString(), new Vector2(JiggonDodger.ScreenBoundary.Width / 4, 0), Color.White);
        }



        public bool Overlaps(Rectangle rect)
        {
            Rectangle testRectangle = new Rectangle((int)BlockPosition.X, (int)BlockPosition.Y, BlockTexture.Width, BlockTexture.Height);
            Vector2 originalPos = JiggonDodger.linkToPlayer.PlayerPosition;
            for (int i = 0; i < JiggonDodger.NumberOfBoxesPerRow; i++)
            {
                testRectangle.X = i * BlockTexture.Width;
                if (i != IndexToEmptyPath && testRectangle.Intersects(rect))
                {
                    return true;
                }
            }
            return false;
        }
    }
}