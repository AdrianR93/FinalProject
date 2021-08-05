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
        private float speed = 200.0f;

        private Direction direction;
        public Bullet(Vector2f startPosition, Direction direction) : base("Sprites" + Path.DirectorySeparatorChar + "tiro.png", startPosition)
        {
            sprite.Scale = new Vector2f(0.25f, 0.25f);
            this.direction = direction;
            CollisionManager.GetInstance().AddToCollisionManager(this);

        }

        public override void Update()
        {
            BulletDirection();
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

        public FloatRect GetBounds()
        {
            return sprite.GetGlobalBounds();
        }

        public void OnCollisionEnter(IColisionable other)
        {


        }
        public void OnCollisionStay(IColisionable other)
        {
           
        }
        public void OnCollisionExit(IColisionable other)
        {
            if (other is NPC)
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
