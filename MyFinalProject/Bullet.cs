using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Bullet : GameObject, IColisionable
    {
        private float speed = 1500.0f;
        private Clock bulletTimeOfLife;
        private float secondsToLive;


        private Direction direction;
        public Bullet(Vector2f startPosition, Direction direction) : base("Sprites" + Path.DirectorySeparatorChar + "bullet.png", startPosition)
        {
            sprite.Scale = new Vector2f(1.0f, 1.0f);
            this.direction = direction;
            CollisionManager.GetInstance().AddToCollisionManager(this);
            bulletTimeOfLife = new Clock();
            secondsToLive = 5;
        }

        public override void Update()
        {
            BulletDirection();
            BulletLife();
            base.Update();

        }

        private void BulletDirection()
        {
            switch (direction)
            {
                case Direction.North:
                    currentPosition.Y -= speed * FrameRate.GetDeltaTime();
                    break;
                case Direction.South:
                    currentPosition.Y += speed * FrameRate.GetDeltaTime();
                    break;
                case Direction.East:
                    currentPosition.X += speed * FrameRate.GetDeltaTime();
                    break;
                case Direction.West:
                    currentPosition.X -= speed * FrameRate.GetDeltaTime();
                    break;
                default:
                    break;
            }
        }

        private void BulletLife()
        {
            if (bulletTimeOfLife.ElapsedTime.AsSeconds() >= secondsToLive )
            {
                Console.WriteLine("Cold Bullet...");
                LateDispose();
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
                LateDispose();
            }

        }
        public void OnCollisionStay(IColisionable other)
        {
            if (other is InvisibleBorder)
            {
                LateDispose();
            }
            if (other is NPC)
            {
                LateDispose();
            }
        }
        public void OnCollisionExit(IColisionable other)
        {
            if (other is InvisibleBorder)
            {
                LateDispose();
            }
        }

        public override void DisposeNow()
        {
            CollisionManager.GetInstance().RemoveFromCollisionManager(this);
            base.DisposeNow();
        }

    }

}
