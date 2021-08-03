using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Map : GameObject
    {
        public Map() : base("Sprites" + Path.DirectorySeparatorChar + "b.png", new Vector2f(0.0f, 0.0f)) 
        {
            sprite.Scale = new Vector2f(1.0f, 1.0f);

        }
        

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }


    }
}
