using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace JiggonDodger
{

    public class JiggonDodger : Game
    {
        public static Rectangle screenBoundary { get; private set; }
        public static SpriteBatch spriteBatch { get; private set; }
     //   public static JiggonDodger CurrentGame { get; set; }
        public static Random random = new Random();
        private static GameTime _gameTime;
        private GraphicsDeviceManager jiggonGraphics;


        public static Player linkToPlayer;

        public static List<BlockRows> _blockRowsList = new List<BlockRows>();

      //  public BlockColor changeColorOnRowsAndMap;

        private Map map;

        private Menu menu;

        private Points linkToPoints;

        //public static int healthCount = 3;
        private Health linkToHealth;

        //public static SpriteFont creditsFont { get; set; }
        public static int UI_OffsetY = 10;
        public static int UI_OffsetX = 10;

        public static bool isGameOver{ get;set; }
        public static bool exitGame { get; set; }

        public JiggonDodger()
        {
            jiggonGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            jiggonGraphics.PreferredBackBufferWidth = 1024;
            jiggonGraphics.PreferredBackBufferHeight = 768;

            jiggonGraphics.SynchronizeWithVerticalRetrace = true;
            this.IsFixedTimeStep = true;



         //   CurrentGame = this;
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            isGameOver = true;
            #region Content Loads
            BlockRows.texture = Content.Load<Texture2D>(@"Tiles\MiniBlock");
            Player.playerTexture = Content.Load<Texture2D>(@"Tiles\Player");
            Points.pointsTexture = Content.Load<Texture2D>(@"Tiles\Points");
            Points.pointsFont = Content.Load<SpriteFont>(@"Fonts\ScoreFont");
            Health.hearthTexture = Content.Load<Texture2D>(@"Tiles\Low_Quality_Pixel_hearth");
            Menu.creditsFont = Content.Load<SpriteFont>(@"Fonts\CreditsFont");
            #endregion

            
            screenBoundary = GraphicsDevice.PresentationParameters.Bounds;
          //changeColorOnRowsAndMap = new BlockColor();

            Points.pointsPosition = new Vector2(UI_OffsetX, UI_OffsetY);
            Points.pointTextPosition = new Vector2(UI_OffsetX + Points.pointsTexture.Width * 1.25f, Points.pointsPosition.Y);

            linkToPoints = new Points();// { pointsTexture = pointTexture, pointsPosition = pointPosition, pointTextPosition = pointFontPosition, pointsFont = scoreFont };
           
            Health.hearthPosition = new Vector2(UI_OffsetX, Health.hearthTexture.Height * 1.5f + UI_OffsetY);
            linkToHealth = new Health();
 
            menu = new Menu();


            map = new Map();
        }


        protected override void Update(GameTime gameTime)
        {
            _gameTime = gameTime;   
            menu.Select(gameTime);
           // Console.WriteLine(isGameOver.ToString());
            if (!isGameOver)
            {
                linkToPlayer.Update(gameTime);
                map.MoveBlockLinesAndPushPlayerIfNeeded(gameTime);
                linkToPoints.Update(gameTime);
                CheckForKeys();
            }
            if (exitGame && isGameOver)
            {
                Exit();
            }
        }

        private void CheckForKeys()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                menu.isPaused = true;
                isGameOver = true;
            }

        }


        public static void PushPlayerOnCollision(BlockRows line)
        {
            Rectangle playerBounds = linkToPlayer.GetBounds();
            while (line.Overlaps(playerBounds))
            {
                linkToPlayer.playerPosition += Vector2.UnitY / 2 * (float)_gameTime.ElapsedGameTime.TotalSeconds;
                playerBounds = linkToPlayer.GetBounds();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(new Vector3(0.5f, 0.70f, 0.85f)));
            spriteBatch.Begin();

            if (!isGameOver)
            {
                    _blockRowsList.ForEach(line => line.Draw());
                    linkToPlayer.Draw();

                    Points.CurrentPoints.Draw();
                    Health.Currenthealth.Draw();
                    if (Health.healthCount <= 0)
                    {
                        isGameOver = true;
                        menu.select = 3;
                    }
            }
            else
                {
                    if (menu.select > 4 || menu.select < -1) menu.select = 3;
                    menu.Draw();
                }
            


            spriteBatch.End();

        }
    }
}