using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Player : GameObject, IColisionable
    {
        private float speed;
        private List<Bullet> bullets;
        private Direction currentDirection;
        private float fireDelay;
        private float fireRate;

        public Player() : base("Sprites" + Path.DirectorySeparatorChar + "pj.png", new Vector2f(300.0f, 720.0f))
        {
            sprite.Scale = new Vector2f(1.0f, 1.0f);
            speed = 100.0f;
            bullets = new List<Bullet>();
            fireDelay = 0.5f;
            fireRate = 0.5f;
            CollisionManager.GetInstance().AddToCollisionManager(this);
        }
        public override void Update()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
            }
            Movement();
            Shoot();
            DeleteOldBullets();
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
                currentPosition.X += speed * FrameRate.GetDeltaTime();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                currentPosition.X -= speed * FrameRate.GetDeltaTime();
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
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, Direction.North));
                fireDelay = 0.0f;
                currentDirection = Direction.North;
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.S) && fireDelay >= fireRate))
            {
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, Direction.South));
                fireDelay = 0.0f;
                currentDirection = Direction.South;

            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.A) && fireDelay >= fireRate))
            {
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, Direction.West));
                fireDelay = 0.0f;
                currentDirection = Direction.West;

            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.D) && fireDelay >= fireRate))
            {
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, Direction.East));
                fireDelay = 0.0f;
                currentDirection = Direction.East;

            }
            //Que pasaria si el player no se esta moviendo? hacia adonde dispara?
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space)) && fireDelay >= fireRate)
            {
                Vector2f spawnPosition = currentPosition;
                spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
                spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
                bullets.Add(new Bullet(spawnPosition, currentDirection));
                fireDelay = 0.0f;
            }

             


            fireDelay += FrameRate.GetDeltaTime();
        }
        private void DeleteOldBullets()
        {
            List<int> indexToDelete = new List<int>();
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
                if (bullets[i].GetPosition().X > Game.GetWindowSize().X)
                {
                    indexToDelete.Add(i);
                }
            }

            for (int i = indexToDelete.Count - 1; i >= 0; i--)
            {
                bullets[i].DisposeNow();
                bullets.RemoveAt(i);
            }
        }

        public FloatRect GetBounds()
        {
            return sprite.GetGlobalBounds();
        }
        public void OnCollisionEnter(IColisionable other)
        {
            if (other is Obstacle)
            {
                Console.WriteLine("Rock enter");
            }
        }

        public void OnCollisionStay(IColisionable other)
        {
            if (other is Obstacle)
            {
                Console.WriteLine("Rock stays");
            }
        }

        public void OnCollisionExit(IColisionable other)
        {
            if (other is Obstacle)
            {
                Console.WriteLine("Rock exit");
            }
        }

        public override void DisposeNow()
        {
            CollisionManager.GetInstance().RemoveFromCollisionManager(this);
            base.DisposeNow();
        }

        public override void CheckGB()
        {
            List<int> indexToDelete = new List<int>();
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].CheckGB();
                if (bullets[i].toDelete)
                {
                    indexToDelete.Add(i);
                }
            }
            for (int i = 0; i < indexToDelete.Count; i++)
            {
                bullets.RemoveAt(i);
            }
        }
    }
}
