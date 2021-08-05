using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Obstacle : GameObject, IColisionable
    {
        public Obstacle() : base("Sprites" + Path.DirectorySeparatorChar + "box.png", new Vector2f(100.0f, 720.0f))
        {
            sprite.Scale = new Vector2f(2, 2);
            CollisionManager.GetInstance().AddToCollisionManager(this);
        }
        public override void Update()
        {
            base.Update();
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }

        public FloatRect GetBounds()
        {
            return sprite.GetGlobalBounds();
        }
        public void OnCollisionEnter(IColisionable other)
        {
            if (other is Player)
            {

            }
        }

        public void OnCollisionStay(IColisionable other)
        {
 
        }

        public void OnCollisionExit(IColisionable other)
        {
            if (other is Player)
            {

            }
        }
        public override void DisposeNow()
        {
            CollisionManager.GetInstance().RemoveFromCollisionManager(this);
            base.DisposeNow();
        }



    }
}
