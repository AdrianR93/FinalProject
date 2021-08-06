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
            sprite.Scale = new Vector2f(0.8f, 0.8f);
            speed = 150.0f;
            bullets = new List<Bullet>();
            fireDelay = 0.3f;
            fireRate = 0.3f;
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
                ShootDirection(Direction.North);
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.S) && fireDelay >= fireRate))
            {
                ShootDirection(Direction.South);
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.A) && fireDelay >= fireRate))
            {
                ShootDirection(Direction.West);
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.D) && fireDelay >= fireRate))
            {
                ShootDirection(Direction.East);
            }
            //Que pasaria si el player no se esta moviendo? hacia adonde dispara?
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space)) && fireDelay >= fireRate)
            {
                ShootDirection(currentDirection);
            }
            fireDelay += FrameRate.GetDeltaTime();
        }

        private void ShootDirection(Direction direction)
        {
            Vector2f spawnPosition = currentPosition;
            spawnPosition.X += (texture.Size.X * sprite.Scale.X) / 2.0f;
            spawnPosition.Y += (texture.Size.Y * sprite.Scale.Y) / 2.0f;
            bullets.Add(new Bullet(spawnPosition, direction));
            fireDelay = 0.0f;
            currentDirection = direction;
        }

        private void BounceOnLimits()
        {
            if ((Keyboard.IsKeyPressed(Keyboard.Key.W)))
            {
                Console.WriteLine("wall W");
                currentPosition.Y += 3 * speed * FrameRate.GetDeltaTime();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.A)))
                {
                    currentPosition.X -= speed * FrameRate.GetDeltaTime();
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.D)))
                {
                    currentPosition.X += speed * FrameRate.GetDeltaTime();
                }
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.A)))
            {
                Console.WriteLine("wall A");
                currentPosition.X += 3 * speed * FrameRate.GetDeltaTime();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.W)))
                {
                    currentPosition.Y -= speed * FrameRate.GetDeltaTime();
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.S)))
                {
                    currentPosition.Y += speed * FrameRate.GetDeltaTime();
                }
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.S)))
            {
                Console.WriteLine("wall S");
                currentPosition.Y -= 3 * speed * FrameRate.GetDeltaTime();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.A)))
                {
                    currentPosition.X -= speed * FrameRate.GetDeltaTime();
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.D)))
                {
                    currentPosition.X += speed * FrameRate.GetDeltaTime();
                }
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.D)))
            {
                Console.WriteLine("wall D");
                currentPosition.X -= 3 * speed * FrameRate.GetDeltaTime();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.W)))
                {
                    currentPosition.Y -= speed * FrameRate.GetDeltaTime();
                }
                 if ((Keyboard.IsKeyPressed(Keyboard.Key.S)))
                {
                    currentPosition.Y += speed * FrameRate.GetDeltaTime();
                }
            }
        }

        public FloatRect GetBounds()
        {
            return sprite.GetGlobalBounds();
        }
        public void OnCollisionEnter(IColisionable other)
        {
            if (other is InvisibleBorder)
            {
                BounceOnLimits();
            }
        }


        public void OnCollisionStay(IColisionable other)
        {
 
        }

        public void OnCollisionExit(IColisionable other)
        {
            if (other is InvisibleBorder)
            {
                Console.WriteLine("wall exit");
            }
        }

        public override void DisposeNow()
        {
            CollisionManager.GetInstance().RemoveFromCollisionManager(this);
            base.DisposeNow();
        }

        public override void CheckGB()
        {
            List<Bullet> indexToDelete = new List<Bullet>();
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].CheckGB();
                if (bullets[i].toDelete)
                {
                    indexToDelete.Add(bullets[i]);
                }
            }
            for (int i = 0; i < indexToDelete.Count; i++)
            {
                indexToDelete[i].DisposeNow();
                bullets.Remove(indexToDelete[i]);
            }
        }
    }
}
