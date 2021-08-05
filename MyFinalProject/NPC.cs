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
            sprite.Scale = new Vector2f(0.35f, 0.35f);
            speedVariant = new Random();
            speed = (float)speedVariant.Next(100, 250);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }

        public override void DisposeNow()
        {
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
            throw new NotImplementedException();
        }

        public void OnCollisionExit(IColisionable other)
        {
            throw new NotImplementedException();
        }

        public void OnCollisionStay(IColisionable other)
        {
            throw new NotImplementedException();
        }
    }
}
