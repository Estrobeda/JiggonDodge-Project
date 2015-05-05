using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    public class BlockColor
    {

        private Color color = new Color();
        private bool greenLimit = false;
        private bool blueLimit = false;
        public Color spriteColor { get; set; }

        private int timer = 0;
        private int limit = 10;


        public BlockColor()
        {
            CurrentColor = this;
        }

        public static BlockColor CurrentColor { get; set; }

        public void Update()
        {
            timer++;
            GenerateColor();
        }

        private void GenerateColor()
        {
            //   timer++;
            if (timer >= limit)
            {
                timer = 0;

                for (byte i = 0; i < 255; i++)
                {
                    color = new Color(new Vector3(0, i + 1, i));

                }


                    spriteColor = color;

            }

        }
    }
}
