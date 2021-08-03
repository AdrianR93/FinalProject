using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{
    public abstract class GameObject
    {
        protected Texture texture;
        protected Sprite sprite;
        protected Vector2f currentPosition;
        //private bool lateDispose;
        //public bool toDelete;

        public GameObject(string texturePath, Vector2f startPosition)
        {
            texture = new Texture(texturePath);
            sprite = new Sprite(texture);
            currentPosition = startPosition;
            sprite.Position = currentPosition;
            //toDelete = false;
            //lateDispose = false;
        }

        public virtual void Update()
        {
            sprite.Position = currentPosition;
        }

        public virtual void Draw(RenderWindow window)
        {
            window.Draw(sprite);
        }


        public Vector2f GetPosition()
        {
            return currentPosition;
        }

        //public virtual void DisposeNow()
        //{
        //    sprite.Dispose();
        //}

        //public void LateDispose()
        //{
        //    LateDispose = true;
        //}

        //public virtual void CheckGB()
        //{
        //    if (lateDispose)
        //    {
        //        DisposeNow();
        //    }
        //}




    }
}
