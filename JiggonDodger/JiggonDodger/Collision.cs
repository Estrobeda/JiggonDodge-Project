using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    class Collision: Map
    {

        public static void PushPlayerOnCollision(BlockRows line)
        {
            Rectangle playerBounds = linkToPlayer.GetBounds();
            while (line.Overlaps(playerBounds))
            {
                linkToPlayer.PlayerPosition += Vector2.UnitY / 2;
                playerBounds = linkToPlayer.GetBounds();
            }
        }
    }
}
