/* This file was created with the help and inspiration of xnafan.net
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JiggonDodger
{
    public struct PathFinder
    {
       //copied from xnafan.net with changed names
       public Vector2 TryMove { get; private set; }
       public Vector2 FurthestDistance { get; set; }
       public int MoveSteps { get; private set; }
       public Vector2 Steps { get; private set; }
       public bool IsDiagonalMove { get; private set; }
       public Rectangle BoundingBox { get; set; }

        public PathFinder(Vector2 originalPos, Vector2 destination, Rectangle boundingBox):this()
        {
            TryMove = destination - originalPos;
            FurthestDistance = originalPos;
            MoveSteps = (int)(TryMove.Length() * 2) + 1;
            IsDiagonalMove = TryMove.X != 0 && TryMove.Y != 0;
            Steps = TryMove / MoveSteps;
            BoundingBox = boundingBox;
        }
    }
}
