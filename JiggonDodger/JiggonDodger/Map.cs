using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    public class Map
    {

        #region Public
        public static float numberOfRows = 2;
        public static int numberOfBoxesPerRow = 16;
        #endregion

        public Map()
        {
            CreatePlayer();
            CreateBlockLines();
        }

        private void CreatePlayer()
        {
          //  Console.WriteLine("Create Player");
            Vector2 centeredCloseToBottom = new Vector2(JiggonDodger.screenBoundary.Width / 2, 
                                                        JiggonDodger.screenBoundary.Height * .6f);
            JiggonDodger.linkToPlayer = new Player() { playerPosition = centeredCloseToBottom };
        }

        //Fix Collision on top of ScreenBoundary!!!
        private void CreateBlockLines()
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                var newBlockLine = new BlockRows(-(Vector2.UnitY * JiggonDodger.screenBoundary.Height + 
                                                    Vector2.UnitY*BlockRows.texture.Height) / numberOfRows * i, 
                                                   BlockRows.texture);
                JiggonDodger._blockRowsList.Add(newBlockLine);
            }
        }

        public void MoveBlockLinesAndPushPlayerIfNeeded(GameTime gameTime)
        {
            foreach (var line in JiggonDodger._blockRowsList)
            {
                line.Update(gameTime);
                JiggonDodger.PushPlayerOnCollision(line);
                MoveLineAboveScreenIfItIsBelowScreen(line);
            }
        }

        public void MoveLineAboveScreenIfItIsBelowScreen(BlockRows line)
        {
            if (line.position.Y > JiggonDodger.screenBoundary.Height)
            {
                line.GenerateRandom();
            }
        }
    }
}