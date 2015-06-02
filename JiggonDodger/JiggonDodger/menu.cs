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
        #region Properties and Variables
        public static SpriteFont creditsFont { get; set; }
        public  bool isPaused { get; set; }
        private Vector2 startGamePos;
        private Vector2 menuPos;
        private Vector2 creditsPos;
        private Vector2 creditsTextPos;
        private Vector2 helpPos;
        private Vector2 helpTextPos;
        private Vector2 exitPos;


        private String selectLabel = ">";
        private String startLabel = " Start";
        private String continueLabel = " Continue";
        private String menuLable = " [<]Menu";
        private String creditsLabel = " Credits";
        private String creditsText = " Created by Rickard Ostlund 2015 [LBS Halmstad] \n Supported and Inspired by xnaFAN.net";

        private String helpLabel = " Help";
        private String helpText = " Controlls: [<] [^] [>] \n \n"
                                         + " You play the game using the arrow keys. \n "
                                         + "You will have to move through the holes \n "
                                         + "in the Rows that are falling downwards. \n "
                                         + "You have 3 extra lives and the Rows will \n"
                                         + " fall faster and faster"
                                         + "\n"
                                         + "Mute/Unmute on M";

        private String exitLabel = " Exit";

        private bool isPressed = false;
        private bool requestMenu = true;
        private bool isEnter = false;
        private int counter = 0;
        public  int select = 3;
        private int waitForInput = 0;
        #endregion


        public Menu(int UI_OffsetX, int UI_OffsetY)
        {
            select = 3;
            startGamePos = new Vector2(UI_OffsetX, 40);
            creditsPos = new Vector2(UI_OffsetX, 80);
            creditsTextPos = new Vector2(UI_OffsetX + 200, 80);

            helpPos = new Vector2(UI_OffsetX , 120);
            helpTextPos = new Vector2(UI_OffsetX + 200, 120); 

            exitPos = new Vector2(UI_OffsetX, JiggonDodger.screenBoundary.Height / 4.8f);

            isPaused = false;
        }

        public void Select(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState == new KeyboardState())
            {
                isPressed = false;
            }

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
            if (keyState.IsKeyDown(Keys.Enter))
            {
                isEnter = true;
            }
            if (keyState.IsKeyUp(Keys.Enter))
            {
                isEnter = false;
            }

            if (!isPaused && JiggonDodger.isGameOver)
            {
                if(select == (int)menuState.StartGame && isEnter)
                {

                    if (waitForInput <= 0)
                    {
                        Health.healthCount = 3;
                        Points.CurrentPoints.resetPoints();
                        JiggonDodger.isGameOver = false;
                    }
                    
                }
                if (select == (int)menuState.ExitGame && isEnter)
                {
                    JiggonDodger.exitGame = true;
                }
            }
            else 
            {
                if(select == (int)menuState.StartGame && isEnter)
                {
                    JiggonDodger.isGameOver = false;
                    isPaused = false;
                }
                if (select == (int)menuState.HelpMe && isEnter) 
                {
                    isPaused = false;
                    requestMenu = true;
                    isEnter = false;
                    select = 2;
                    JiggonDodger.isGameOver = true;
                }
            }


        }


        public static Menu CurrentMenu { get; set; }

        public void Draw()
        {

            if (!isPaused)
            {
                Console.WriteLine("StartMenu");
                JiggonDodger.spriteBatch.DrawString(Points.pointsFont, startLabel, startGamePos, Color.White);
                JiggonDodger.spriteBatch.DrawString(Points.pointsFont, creditsLabel, creditsPos, Color.White);
                JiggonDodger.spriteBatch.DrawString(Points.pointsFont, helpLabel, helpPos, Color.White);
                JiggonDodger.spriteBatch.DrawString(Points.pointsFont, exitLabel, exitPos, Color.White);
            }
            else
            {
                Console.WriteLine("Pause Menu");
                menuPos = helpPos;
               
                JiggonDodger.spriteBatch.DrawString(Points.pointsFont, continueLabel, startGamePos, Color.White);
                JiggonDodger.spriteBatch.DrawString(Points.pointsFont, menuLable, menuPos, Color.White);
                JiggonDodger.spriteBatch.DrawString(Points.pointsFont, helpLabel, creditsPos, Color.White);
                
            }
                
            switch (select)
            {
                case (int)menuState.MoveDown:
                    if (!isPaused)
                    {
                        select = 0;
                    }
                    else
                    {
                        select = 3;
                    }
                    break;

                case (int)menuState.StartGame:
                    BlockRows.Speed = 0.3f;
                        JiggonDodger.spriteBatch.DrawString(Points.pointsFont, selectLabel, startGamePos, Color.White);
                    break;

                case (int)menuState.Credits:

                        JiggonDodger.spriteBatch.DrawString(Points.pointsFont, selectLabel, creditsPos, Color.White);
                    if (!isPaused)
                    {
                        JiggonDodger.spriteBatch.DrawString(creditsFont, creditsText, creditsTextPos, Color.White);
                    }
                    
                    break;

                case (int)menuState.HelpMe:
                    JiggonDodger.spriteBatch.DrawString(Points.pointsFont, selectLabel, helpPos, Color.White);
                    JiggonDodger.spriteBatch.DrawString(creditsFont, helpText, helpTextPos, Color.White);
                    break;

                case (int)menuState.ExitGame:
                    if (!isPaused)
                    {
                        JiggonDodger.spriteBatch.DrawString(Points.pointsFont, selectLabel, exitPos, Color.White);
                    }
                    else
                    {
                        select = 3;
                    }
                    break;

                case (int)menuState.MoveUp:
                    select = 3;
                    break;
                    
            }


        }

    }
}
