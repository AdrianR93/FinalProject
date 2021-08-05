using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class NPC : GameObject, IColisionable
    {
        private float speed;
        Random speedVariant;

        public NPC(Vector2f startposition) : base("Sprites" + Path.DirectorySeparatorChar + "npc.png", startposition)
        {
            sprite.Scale = new Vector2f(0.25f, 0.25f);
            speedVariant = new Random();
            speed = (float)speedVariant.Next(100, 250);
            CollisionManager.GetInstance().AddToCollisionManager(this);
        }

        public override void Update()
        {
            NPCMovement();
            base.Update();
        }

        public void NPCMovement()
        {
            currentPosition.Y += speed * FrameRate.GetDeltaTime();
        }



        public override void DisposeNow()
        {
            CollisionManager.GetInstance().RemoveFromCollisionManager(this);
            base.DisposeNow();
        }

        public override void CheckGB()
        {
            base.CheckGB();
        }

        public FloatRect GetBounds()
        {
            return sprite.GetGlobalBounds();
        }

        public void OnCollisionEnter(IColisionable other)
        {
            if (other is Bullet)
            {
                Gameplay.GetInstance().RevivingZombies();
            }
        }

        public void OnCollisionStay(IColisionable other)
        {
            if (other is Bullet)
            {
                //LateDispose();
            }
        }

        public void OnCollisionExit(IColisionable other)
        {
        }

    }
}
