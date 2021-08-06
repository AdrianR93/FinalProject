using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class GameOverScreen : GameObject
    {

        public GameOverScreen(Vector2f startposition) : base("Sprites" + Path.DirectorySeparatorChar + "gameOver.png", startposition)
        {

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
