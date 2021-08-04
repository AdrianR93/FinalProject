using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{
    class InvisibleBorder : IColisionable
    {
        private Vector2f position;
        private Vector2f size;

        public InvisibleBorder(Vector2f position, Vector2f size)
        {
            this.position = position;
            this.size = size;
            CollisionManager.GetInstance().AddToCollisionManager(this);
        }

        public FloatRect GetBounds()
        {
            return new FloatRect(position, size); 
        }

        public void OnCollisionEnter(IColisionable other)
        {
            if (other is Player)
            {
                Console.WriteLine("Out of Border!");
            }
        }

        public void OnCollisionExit(IColisionable other)
        {
        }

        public void OnCollisionStay(IColisionable other)
        {
        }
    }
}
