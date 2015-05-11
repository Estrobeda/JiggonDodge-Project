using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    public class BlockColor
    {
        private  Color returnColor;
        private  Random randomColor =  new Random();
        private List<Color> colorList = new List<Color>();
        private List<Color> oldColorList = new List<Color>();

        private int timer;
        private int counter = 50;

        public BlockColor() {
            CurrentColor = this;
        }

        public static BlockColor CurrentColor { get; set; }

        public void setColor(int size)
        {

            for (int i = 0; i < size; i++) {
                switch (i) { 
                    case 0:
                        colorList.Add(Color.MidnightBlue);
                        break;
                    case 1:
                        colorList.Add(Color.Purple);
                        break;
                    case 2:
                        colorList.Add(Color.MediumPurple);
                        break;
                    case 3:
                        colorList.Add(Color.Magenta);
                        break;
                    case 4:
                        colorList.Add(Color.Red);
                        break;
                    case 5:
                        colorList.Add(Color.Orange);
                        break;
                    case 6:
                        colorList.Add(Color.Yellow);
                        break;
                    case 7:
                        colorList.Add(Color.YellowGreen);
                        break;
                    case 8:
                        colorList.Add(Color.Lime);
                        break;
                    case 9:
                        colorList.Add(Color.MediumSeaGreen);
                        break;
                    case 10:
                        colorList.Add(Color.Aqua);
                        break;
                    case 12:
                        colorList.Add(Color.RoyalBlue);
                        break;
                    case 13:
                        colorList.Add(Color.Blue);
                        break;
                    case 14:
                        colorList.Add(Color.BlueViolet);
                        break;
                    case 15:
                        colorList.Add(Color.DarkBlue);
                        break;
                    case 16:
                        colorList.Add(Color.Violet);
                        break;
                }
            }
        }

        public void Update() {
            timer++;

            if (timer >= counter)
            {
                oldColorList = colorList;
               
                for (int i = 0; i < colorList.Count - 1; i++)
                {
                    colorList.RemoveAt((colorList.Count - 1) - i);
                    colorList.Insert((colorList.Count - 1) - i, oldColorList[i]);   
                }
               
                timer = 0;
            }
        }

        public Color getColor(int i) {
            
            returnColor = colorList[i];           
            return returnColor;
        }
    }
}
