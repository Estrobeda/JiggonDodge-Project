using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    class Menu
    {
        private enum menuState
        {
            MoveUp = -1,
            ExitGame = 0,
            HelpMe = 1,
            Credits = 2,
            StartGame = 3,
            MoveDown = 4
        }

        private  static Vector2 startGamePos;
        private static Vector2 creditsPos;
        private static Vector2 creditsTextPos;
        private static Vector2 helpPos;
        private static Vector2 helpTextPos;
        private static Vector2 exitPos;

        private static String selectLabel = ">";
        private static String startLabel = " Start";

        private static String creditsLabel = " Credits";
        private static String creditsText = " Created by Rickard Ostlund 2015 [LBS Halmstad] \n Supported and Inspired by xnaFAN.net";

        private static String helpLabel = " Help";
        private static String helpText = " Controlls: [<] [^] [>] \n \n"
                                         + " You play the game using the arrow keys. \n "
                                         + "You will have to move through the holes \n "
                                         + "in the Rows that are falling downwards. \n "
                                         + "You have 3 extra lives and the Rows will \n"
                                         + " fall faster and faster";

        private static String exitLabel = " Exit";

        private bool isPressed = false;
        public  int select = 0;


        public Menu()
        {
          //  select = 0;
            startGamePos = new Vector2(JiggonDodger.UI_OffsetX, 40);
            creditsPos = new Vector2(JiggonDodger.UI_OffsetX, 80);
            creditsTextPos = new Vector2(JiggonDodger.UI_OffsetX + 200, 80);

            helpPos = new Vector2(JiggonDodger.UI_OffsetX , 120);
            helpTextPos = new Vector2(JiggonDodger.UI_OffsetX + 200, 120); 

            exitPos = new Vector2(JiggonDodger.UI_OffsetX, JiggonDodger.ScreenBoundary.Height / 3.25f);
        }

        public void Select(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
                if (keyState.IsKeyDown(Keys.Up) && !isPressed)
                {
                    select++;
                    isPressed = true;
                }
                if (keyState.IsKeyDown(Keys.Down) && !isPressed)
                {
                    select--;
                    isPressed = true;
                }
                if(keyState == new KeyboardState()){
                    isPressed = false;
                }

            if (select == 3 && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                
                Health.Currenthealth.healthCount = 3;
                //Store this later to create a HighScore!!!! [Progress not started yet]
                Points.CurrentPoints.Time = 0;
                JiggonDodger.isGameOver = false;
            }

            if (select == 0 && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                JiggonDodger.exitGame = true;
            }
            
        }


        public static Menu CurrentMenu { get; set; }

        public void Draw()
        {

            JiggonDodger.SpriteBatch.DrawString(JiggonDodger.scoreFont, startLabel, startGamePos, Color.White);
            
            JiggonDodger.SpriteBatch.DrawString(JiggonDodger.scoreFont, creditsLabel, creditsPos, Color.White);

            JiggonDodger.SpriteBatch.DrawString(JiggonDodger.scoreFont, helpLabel, helpPos, Color.White);

            JiggonDodger.SpriteBatch.DrawString(JiggonDodger.scoreFont, exitLabel, exitPos, Color.White);

            switch (select)
            {
                case (int)menuState.MoveDown:
                    select = 0;
                    break;

                case (int)menuState.StartGame:
                    JiggonDodger.BoxSpeed = 0.3f;
                    JiggonDodger.SpriteBatch.DrawString(JiggonDodger.scoreFont, selectLabel, startGamePos, Color.White);
                    break;

                case (int)menuState.Credits:
                    JiggonDodger.SpriteBatch.DrawString(JiggonDodger.scoreFont, selectLabel, creditsPos, Color.White);
                    JiggonDodger.SpriteBatch.DrawString(JiggonDodger.creditsFont, creditsText, creditsTextPos, Color.White);
                    break;

                case (int)menuState.HelpMe:
                    JiggonDodger.SpriteBatch.DrawString(JiggonDodger.scoreFont, selectLabel, helpPos, Color.White);
                    JiggonDodger.SpriteBatch.DrawString(JiggonDodger.creditsFont, helpText, helpTextPos, Color.White);
                    break;

                case (int)menuState.ExitGame:
                    JiggonDodger.SpriteBatch.DrawString(JiggonDodger.scoreFont, selectLabel, exitPos, Color.White);
                    break;

                case (int)menuState.MoveUp:
                    select = 3;
                    break;

            }


        }

    }
}
