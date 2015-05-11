using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        #region Variables
        public static SpriteFont creditsFont { get; set; }
        public  bool isPaused { get; set; }
        private Vector2 startGamePos;
        private Vector2 creditsPos;
        private Vector2 creditsTextPos;
        private Vector2 helpPos;
        private Vector2 helpTextPos;
        private Vector2 exitPos;


        private String selectLabel = ">";
        private String startLabel = " Start";
        private String continueLabel = " Continue";

        private String creditsLabel = " Credits";
        private String creditsText = " Created by Rickard Ostlund 2015 [LBS Halmstad] \n Supported and Inspired by xnaFAN.net";

        private String helpLabel = " Help";
        private String helpText = " Controlls: [<] [^] [>] \n \n"
                                         + " You play the game using the arrow keys. \n "
                                         + "You will have to move through the holes \n "
                                         + "in the Rows that are falling downwards. \n "
                                         + "You have 3 extra lives and the Rows will \n"
                                         + " fall faster and faster";

        private String exitLabel = " Exit";

        private bool isPressed = false;
        public  int select = 0;
        #endregion


        public Menu()
        {
          //  select = 0;
            startGamePos = new Vector2(JiggonDodger.UI_OffsetX, 40);
            creditsPos = new Vector2(JiggonDodger.UI_OffsetX, 80);
            creditsTextPos = new Vector2(JiggonDodger.UI_OffsetX + 200, 80);

            helpPos = new Vector2(JiggonDodger.UI_OffsetX , 120);
            helpTextPos = new Vector2(JiggonDodger.UI_OffsetX + 200, 120); 

            exitPos = new Vector2(JiggonDodger.UI_OffsetX, JiggonDodger.screenBoundary.Height / 3.25f);

            isPaused = false;
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
                
                
                //Store this later to create a HighScore!!!! [Progress not started yet]
                if (!isPaused)
                {
                    Health.healthCount = 3;
                    Points.CurrentPoints.Time = 0;
                }
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

            if (!isPaused)
            {
                JiggonDodger.spriteBatch.DrawString(Points.pointsFont, startLabel, startGamePos, Color.White);
            }
            else
            {
                JiggonDodger.spriteBatch.DrawString(Points.pointsFont, continueLabel, startGamePos, Color.White);
            }
            JiggonDodger.spriteBatch.DrawString(Points.pointsFont, creditsLabel, creditsPos, Color.White);

            JiggonDodger.spriteBatch.DrawString(Points.pointsFont, helpLabel, helpPos, Color.White);

            JiggonDodger.spriteBatch.DrawString(Points.pointsFont, exitLabel, exitPos, Color.White);

            switch (select)
            {
                case (int)menuState.MoveDown:
                    select = 0;
                    break;

                case (int)menuState.StartGame:
                    BlockRows.speed = 0.3f;
                        JiggonDodger.spriteBatch.DrawString(Points.pointsFont, selectLabel, startGamePos, Color.White);
                    break;

                case (int)menuState.Credits:
                    JiggonDodger.spriteBatch.DrawString(Points.pointsFont, selectLabel, creditsPos, Color.White);
                    JiggonDodger.spriteBatch.DrawString(creditsFont, creditsText, creditsTextPos, Color.White);
                    break;

                case (int)menuState.HelpMe:
                    JiggonDodger.spriteBatch.DrawString(Points.pointsFont, selectLabel, helpPos, Color.White);
                    JiggonDodger.spriteBatch.DrawString(creditsFont, helpText, helpTextPos, Color.White);
                    break;

                case (int)menuState.ExitGame:
                    JiggonDodger.spriteBatch.DrawString(Points.pointsFont, selectLabel, exitPos, Color.White);
                    break;

                case (int)menuState.MoveUp:
                    select = 3;
                    break;

            }


        }

    }
}
