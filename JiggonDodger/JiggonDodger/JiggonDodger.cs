using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        public static Random random = new Random();
        public static Player linkToPlayer;
        private GraphicsDeviceManager jiggonGraphics;

        public static List<BlockRows> _blockRowsList = new List<BlockRows>();

        private BlockColor color;
        private Map map;
        private Menu menu;
        private Points points;
        private Health health;
        private int UI_OffsetY = 10;
        private static int UI_OffsetX = 10;
        private List<Texture2D> tailParticles = new List<Texture2D>();
        private List<Texture2D> worldEnd = new List<Texture2D>();
        private ParticleEngine particles;

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

        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            isGameOver = true;
            #region Content Loads
          
            BlockRows.texture = Content.Load<Texture2D>(@"Tiles\MiniBlock");
            BlockRows.arrowindication = Content.Load<Texture2D>(@"Tiles\Arrows-Up-Animation");
            worldEnd.Add(Content.Load<Texture2D>(@"Particles\LowOpacityRectangle"));

            Player.texture = Content.Load<Texture2D>(@"Tiles\Player");
            tailParticles.Add(Content.Load<Texture2D>(@"Particles\Rectangle"));
            Player.hitEffect = Content.Load<SoundEffect>(@"Audio\Effects\Bounce");

            Points.pointsTexture = Content.Load<Texture2D>(@"Tiles\Points");
            Points.pointsFont = Content.Load<SpriteFont>(@"Fonts\ScoreFont");
            
            Health.hearthTexture = Content.Load<Texture2D>(@"Tiles\Low_Quality_Pixel_hearth");
            Menu.creditsFont = Content.Load<SpriteFont>(@"Fonts\CreditsFont");
            #endregion

            screenBoundary = GraphicsDevice.PresentationParameters.Bounds;
      
            Player.tailEngine = new ParticleEngine(tailParticles, new Vector2(0,0));

            Points.pointsPosition = new Vector2(UI_OffsetX, UI_OffsetY);
            Points.pointTextPosition = new Vector2(UI_OffsetX + Points.pointsTexture.Width * 1.25f, Points.pointsPosition.Y);

            points = new Points();

            Health.hearthPosition = new Vector2(UI_OffsetX, Health.hearthTexture.Height * 1.5f + UI_OffsetY);
            health = new Health();
 
            menu = new Menu(UI_OffsetX, UI_OffsetY);
            color = new BlockColor();

            map = new Map();
            particles = new ParticleEngine(worldEnd, new Vector2(0, 0));
            particles.EmitterLocation = Vector2.UnitY*(screenBoundary.Height-64);
        }


        protected override void Update(GameTime gameTime)
        {
            menu.Select(gameTime);
                particles.Update02(gameTime);
         

            if (!isGameOver)
            {
                color.Update();
                linkToPlayer.Update(gameTime);
                map.MoveBlockLinesAndPushPlayerIfNeeded(gameTime);
                points.Update();
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
              
                linkToPlayer.position += Vector2.UnitY / 2;
                playerBounds = linkToPlayer.GetBounds();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
           // GraphicsDevice.Clear(new Color(new Vector3(0.5f, 0.70f, 0.85f)));
            GraphicsDevice.Clear(new Color(0.1f, 0.1f, 0.1f));
            spriteBatch.Begin();
           
            if (!isGameOver)
            {
                    _blockRowsList.ForEach(row => row.Draw(spriteBatch));
                    linkToPlayer.Draw(spriteBatch);

                    particles.Draw(spriteBatch);
                    Points.CurrentPoints.Draw(spriteBatch);
                    Health.Currenthealth.Draw(spriteBatch);
                    if (Health.healthCount <= 0)
                    {
                        isGameOver = true;
                        Health.healthCount = 3;
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