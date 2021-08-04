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
        private float speed = 50.0f;
        public enum Direction { North, South, East, West, }
        private Direction direction;
        public Bullet(Vector2f startPosition, Direction direction) : base("Sprites" + Path.DirectorySeparatorChar + "tiro.png", startPosition)
        {
            sprite.Scale = new Vector2f(0.25f, 0.25f);
            this.direction = direction;
            CollisionManager.GetInstance().AddToCollisionManager(this);

        }

        public override void Update()
        {
            switch (direction)
            {
                case Direction.North:
                    currentPosition.Y -= speed * FrameRate.GetDeltaTime();
                    Console.WriteLine("norte");
                    break;
                case Direction.South:
                    currentPosition.Y += speed * FrameRate.GetDeltaTime();
                    Console.WriteLine("sur");
                    break;
                case Direction.East:
                    currentPosition.X += speed * FrameRate.GetDeltaTime();
                    Console.WriteLine("est");
                    break;
                case Direction.West:
                    currentPosition.X -= speed * FrameRate.GetDeltaTime();
                    Console.WriteLine("oest");
                    break;
                default:
                    break;
            }
            base.Update();
            
        }

        public FloatRect GetBounds()
        {
            return sprite.GetGlobalBounds();
        }

        public void OnCollisionEnter(IColisionable other)
        {
            if (other is Obstacle)
            {
                DisposeNow();
            }
        }
        public void OnCollisionStay(IColisionable other)
        {

        }
        public void OnCollisionExit(IColisionable other)
        {
        }

        public override void DisposeNow()
        {
            CollisionManager.GetInstance().RemoveFromCollisionManager(this);
            base.DisposeNow();
        }

    }

}
