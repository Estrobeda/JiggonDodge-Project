using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiggonDodger
{
    public class ParticleEngine
    {
        public Vector2 EmitterLocation { get; set; }

        private Random random;
        private List<Particle> particles;
        private List<Texture2D> textures;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        private Particle GenerateNewParticle01()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                    1f * (float)(random.NextDouble() * 2 - 1),
                    1f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(
                    0,
                    0,
                    (float)random.NextDouble(),
                    (float)random.NextDouble());
            float size = (float)random.NextDouble();
            int ttl = 10 + random.Next(10);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }
     
        private Particle GenerateNewParticle02(GameTime gameTime)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = new Vector2(32+random.Next(16)*64, 768);
            Vector2 velocity = Vector2.UnitY*1f;
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color =  BlockColor.CurrentColor.colorList[random.Next(16)];
            color.A = 5;
            float size =  2f;
            
            int ttl = 30 + random.Next(30);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }


        public void Update()
        {
            int total = 1;

            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle01());
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Update02(GameTime gameTime)
        {
            int total = 1;

            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle02(gameTime));
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update2();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
          
        }
    }
}
