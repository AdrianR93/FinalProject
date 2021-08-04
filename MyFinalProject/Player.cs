using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Player : GameObject
    {
        float speed;
        private List<Bullet> bullets;
        private float fireDelay;
        private float fireRate;

        //private bool toDelete;

        public Player() : base("Sprites" + Path.DirectorySeparatorChar + "pj.png", new Vector2f(300.0f, 720.0f))
        {
            sprite.Scale = new Vector2f(1.0f, 1.0f);
            speed = 150.0f;
            bullets = new List<Bullet>();
            fireDelay = 0.5f;
            fireRate = 0.5f;
        }
        public override void Update()
        {
            //for (int i = 0; i < bullets.Count; i++)
            //{
            //    bullets[i].Update();
            //}
            Movement();
            Shoot();
            base.Update();
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(window);
            }
        }

        public void Dispose()
        {
            sprite.Dispose();
            texture.Dispose();
        }


        private void Movement()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                currentPosition.X += speed / (int)2.25 * FrameRate.GetDeltaTime();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                currentPosition.X -= speed / (int)2.25 * FrameRate.GetDeltaTime();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                currentPosition.Y += speed * FrameRate.GetDeltaTime();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                currentPosition.Y -= speed * FrameRate.GetDeltaTime();
            }
        }
        private void Shoot()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.W) && fireDelay >= fireRate)
            {
                Console.WriteLine("up");
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, Bullet.Direction.North));
                fireDelay = 0.0f;

            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.S) && fireDelay >= fireRate))
            {
                Console.WriteLine("down");
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, Bullet.Direction.South));
                fireDelay = 0.0f;
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.A) && fireDelay >= fireRate))
            {
                Console.WriteLine("left");
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, Bullet.Direction.West));
                fireDelay = 0.0f;
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.D) && fireDelay >= fireRate))
            {
                Console.WriteLine("right");
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, Bullet.Direction.East));
                fireDelay = 0.0f;
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.W) && Keyboard.IsKeyPressed(Keyboard.Key.D) && fireDelay >= fireRate))
            {
                Console.WriteLine("NE");
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, Bullet.Direction.East));
                fireDelay = 0.0f;
            }
            //Que pasaria si el player no se esta moviendo? hacia adonde dispara? tengo que diseñar que se mueva hacia arriba esa bala! 


            fireDelay += FrameRate.GetDeltaTime();
        }

    }
}
