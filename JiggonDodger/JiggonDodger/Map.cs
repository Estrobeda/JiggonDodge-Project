using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    public class Map : JiggonDodger
    {

        
        public Map()
        {
            CreatePlayer();
            CreateBlockLines();
        }

        private void CreatePlayer()
        {
            Console.WriteLine("Create Player");
            Vector2 centeredCloseToBottom = new Vector2(ScreenBoundary.Width / 2, ScreenBoundary.Height * .6f);
            linkToPlayer = new Player() { PlayerTexture = playerTexture, PlayerPosition = centeredCloseToBottom, _speed = playerSpeed };
        }

        //Fix Collision on top of ScreenBoundary!!!
        private void CreateBlockLines()
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                var newBlockLine =
                    new BlockRows(-(Vector2.UnitY * ScreenBoundary.Height + Vector2.UnitY*blockTexture.Height) / numberOfRows * i, blockTexture);
                BlockRows.Add(newBlockLine);
            }
        }

        public void MoveBlockLinesAndPushPlayerIfNeeded(GameTime gameTime)
        {
            foreach (var line in BlockRows)
            {
                line.Update(gameTime);
                Collision.PushPlayerOnCollision(line);
                MoveLineAboveScreenIfItIsBelowScreen(line);
            }
        }


        public void MoveLineAboveScreenIfItIsBelowScreen(BlockRows line)
        {
            if (line.BlockPosition.Y > ScreenBoundary.Height)
            {
                line.GenerateRandom();
            }
        }
    }
}
