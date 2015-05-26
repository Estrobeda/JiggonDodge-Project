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
        public static List<BlockRows> _blockRowsList = new List<BlockRows>();
        public static Random random = new Random();
        public static Player linkToPlayer;
        private GraphicsDeviceManager jiggonGraphics;
        private Texture2D background1;
        private Texture2D background2;
        private List<Texture2D> _tailParticles = new List<Texture2D>();
        private List<Texture2D> _worldEnd = new List<Texture2D>();
        private SoundEffect music;
        private SoundEffectInstance musicState;
        private SoundEffectInstance hitEffectState;
        private Timer timer = new Timer(5);
        private BlockColor color;
        private Map map;
        private Menu menu;
        private Points points;
        private Health health;
        private ParticleEngine particles;
       
        private int UI_OffsetY = 10;
        private static int UI_OffsetX = 10;

        public static bool isGameOver{ get;set; }
        public static bool exitGame { get; set; }
        private bool isMute;

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

            background1 = Content.Load<Texture2D>(@"Backgounds\Menu");
            background2 = Content.Load<Texture2D>(@"Backgounds\BG2");
            music = Content.Load<SoundEffect>(@"Audio\Music\Ouroboros");
            musicState = music.CreateInstance();
            BlockRows.texture = Content.Load<Texture2D>(@"Tiles\MiniBlock");
            BlockRows.arrowindication = Content.Load<Texture2D>(@"Tiles\Arrows-Up-Animation");
            _worldEnd.Add(Content.Load<Texture2D>(@"Particles\LowOpacityRectangle"));

            Player.texture = Content.Load<Texture2D>(@"Tiles\Player");
            _tailParticles.Add(Content.Load<Texture2D>(@"Particles\Rectangle"));
            Player.hitEffect = Content.Load<SoundEffect>(@"Audio\Effects\Bounce");

            Points.pointsTexture = Content.Load<Texture2D>(@"Tiles\Points");
            Points.pointsFont = Content.Load<SpriteFont>(@"Fonts\ScoreFont");
            
            Health.hearthTexture = Content.Load<Texture2D>(@"Tiles\Low_Quality_Pixel_hearth");
            Menu.creditsFont = Content.Load<SpriteFont>(@"Fonts\CreditsFont");
            #endregion

            screenBoundary = GraphicsDevice.PresentationParameters.Bounds;
      
            Player.tailEngine = new ParticleEngine(_tailParticles, new Vector2(0,0));
            Points.pointsPosition = new Vector2(UI_OffsetX, UI_OffsetY);
            Points.pointTextPosition = new Vector2(UI_OffsetX + Points.pointsTexture.Width * 1.25f, Points.pointsPosition.Y);

            points = new Points();

            Health.hearthPosition = new Vector2(UI_OffsetX, Health.hearthTexture.Height * 1.5f + UI_OffsetY);
            health = new Health();
 
            menu = new Menu(UI_OffsetX, UI_OffsetY);
            color = new BlockColor();

            map = new Map();
            particles = new ParticleEngine(_worldEnd, new Vector2(0, 0));
            particles.EmitterLocation = Vector2.UnitY*(screenBoundary.Height-64);

            
        }


        protected override void Update(GameTime gameTime)
        {
            
            menu.Select(gameTime);
            particles.UpdateTail(gameTime);
            timer.Ticker();

            if (!isGameOver)
            {
                musicState.Play();
                if (!isMute)
                {
                    musicState.Volume = 1; 
                }
                else
                {
                    musicState.Volume = 0;
                }
                if (musicState.IsLooped)
                {
                    musicState.Play();
                }
                color.Update();
                linkToPlayer.Update(gameTime);
                map.MoveBlockLinesAndPushPlayerIfNeeded(gameTime);
                points.Update();
                CheckForKeys();
            }
            else
            {
                musicState.Pause();
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

            if (timer.IsOneTick())
            {
                if (keyboard.IsKeyDown(Keys.M))
                {
                    if (isMute)
                    {
                        isMute = false;
                        linkToPlayer.isMuted = false;
                    }
                    else
                    {
                        isMute = true;
                        linkToPlayer.isMuted = true;
                    }
                }
                if(keyboard.IsKeyDown(Keys.D1)){
                    if (isMute)
                    {
                        isMute = false;
                    }
                    else
                    {
                        isMute = true;
                    }
                }

                if (keyboard.IsKeyDown(Keys.D2))
                {
                    if (linkToPlayer.isMuted)
                    {
                        linkToPlayer.isMuted = false;
                    }
                    else
                    {
                        linkToPlayer.isMuted = true;
                    }
                }

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
            spriteBatch.Draw(background2, new Vector2(0, 0), Color.White);
            _blockRowsList.ForEach(row => row.Draw(spriteBatch));
            linkToPlayer.Draw(spriteBatch);
            Points.CurrentPoints.Draw(spriteBatch);
            Health.Currenthealth.Draw(spriteBatch);
            particles.Draw(spriteBatch);
            if (!isGameOver)
            {       
                    if (Health.healthCount <= 0)
                    {
                        isGameOver = true;
                        Health.healthCount = 3;
                        menu.select = 3;
                    }
            } else
                {
                    if (menu.select > 4 || menu.select < -1) menu.select = 3;
                    spriteBatch.Draw(background1, new Vector2(0, 0), Color.White);
                    menu.Draw();
                }

            spriteBatch.End();

        }
    }
}