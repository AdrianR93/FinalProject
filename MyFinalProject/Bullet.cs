using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Bullet : GameObject
    {
        private float speed = 50.0f;
        public enum Direction { North, South, East, West, NE}
        private Direction bulletDirection;
        public Bullet(Vector2f startPosition, Direction direction) : base("Sprites" + Path.DirectorySeparatorChar + "tiro.png", startPosition)
        {
            sprite.Scale = new Vector2f(0.25f, 0.25f);
            bulletDirection = direction;
        }


        public override void Update()
        {
            switch (bulletDirection)
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
                case Direction.NE:
                    currentPosition.X += speed * FrameRate.GetDeltaTime();
                    currentPosition.Y += speed * FrameRate.GetDeltaTime();
                    break;
                default:
                    break;
            }

            base.Update();
            
        }

    }

}
