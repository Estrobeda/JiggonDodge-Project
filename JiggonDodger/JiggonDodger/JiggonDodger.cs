using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace JiggonDodger
{

    public class JiggonDodger : Game
    {

        //used to easily change the whole game instead of going through all classes, this file is main!.
        #region Variables and Properties
        #region JiggonDodger Variables
        private GraphicsDeviceManager jiggonGraphics;
        protected static Texture2D blockTexture, playerTexture;
        public static List<BlockRows> BlockRows = new List<BlockRows>();
        public static Rectangle ScreenBoundary { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }
        public static Random Random = new Random();
        private Menu menu;
        #endregion

        #region Player Variables
        public static Player linkToPlayer;
        protected float playerSpeed = 16f; //Has to be a multiple of 32 due to collision detection
        #endregion

        #region BlockRows Variables
        public static float BoxSpeed { get; set; }

        protected float numberOfRows = 2;
        public static int NumberOfBoxesPerRow = 16;
        
        #endregion

        #region Map Variables
        private Map map;
        public BlockColor changeColorOnRowsAndMap;
        #endregion

        #region Score Variables
        Points linkToPoints;
        public static SpriteFont scoreFont { get; set; }
        private Texture2D pointTexture;
        private Vector2 pointPosition;
        private Vector2 pointFontPosition;
        #endregion

        #region
        Health linkToHealth;
        private Texture2D healthTexture;
        private Vector2 healthPosition;
        public static int healthCount = 3;
        #endregion

        public static SpriteFont creditsFont { get; set; }
        public static int UI_OffsetY = 10;
        public static int UI_OffsetX = 10;

        public static bool isGameOver{get;set;}
        public static bool exitGame { get; set; }

        //public static bool mainMenu{get;set;}
  


        #endregion

        public JiggonDodger()
        {
            jiggonGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            jiggonGraphics.PreferredBackBufferWidth = 1024;
            jiggonGraphics.PreferredBackBufferHeight = 768;
        }


        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            isGameOver = true;
            #region Content Loads
            blockTexture = Content.Load<Texture2D>(@"Tiles\MiniBlock");
            playerTexture = Content.Load<Texture2D>(@"Tiles\Player");
            pointTexture = Content.Load<Texture2D>(@"Tiles\Points");
            healthTexture = Content.Load<Texture2D>(@"Tiles\Low_Quality_Pixel_hearth");
           
            scoreFont = Content.Load<SpriteFont>(@"Fonts\ScoreFont");

            creditsFont = Content.Load<SpriteFont>(@"Fonts\CreditsFont");
            #endregion
            #region Map
            BoxSpeed = 0.3f;
            ScreenBoundary = GraphicsDevice.PresentationParameters.Bounds;
            changeColorOnRowsAndMap = new BlockColor();
            #endregion
            #region Points
            pointPosition = new Vector2(UI_OffsetX, UI_OffsetY);
            pointFontPosition = new Vector2(UI_OffsetX + pointTexture.Width * 1.25f, pointPosition.Y);

            linkToPoints = new Points { pointsTexture = pointTexture, pointsPosition = pointPosition, pointTextPosition = pointFontPosition, pointsFont = scoreFont };
            #endregion
            #region Health
            healthPosition = new Vector2(UI_OffsetX, healthTexture.Height * 1.5f + UI_OffsetY);
            linkToHealth = new Health { hearthTexture = healthTexture, hearthPosition = healthPosition, healthCount = healthCount };
            Health.Currenthealth.healthCount = healthCount;


            #endregion
            menu = new Menu();
            StartNewGame();
        }

        void StartNewGame()
        {
            map = new Map();
        }

        protected override void Update(GameTime gameTime)
        {
            if (exitGame)
            {
                Exit();
            }
            menu.Select(gameTime);
            Console.WriteLine(isGameOver.ToString());
            if (!isGameOver)
            {
                linkToPlayer.Update(gameTime);
                map.MoveBlockLinesAndPushPlayerIfNeeded(gameTime);
                linkToPoints.Update(gameTime);
            }


            
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(new Vector3(0.5f, 0.70f, 0.85f)));
            SpriteBatch.Begin();

            if (!isGameOver)
            {
                
                    BlockRows.ForEach(line => line.Draw());
                    linkToPlayer.Draw();

                    Points.CurrentPoints.Draw();
                    Health.Currenthealth.Draw();
                    if (Health.Currenthealth.healthCount <= 0)
                    {
                        isGameOver = true;
                        menu.select = 2;
                    }
            }
                else
                {
                    menu.Draw();
                }
            


            SpriteBatch.End();

        }
    }
}