using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{
    public class Gameplay
    {
        private Player player;

        public Gameplay()
        {
            player = new Player();

        }

        public void Update()
        {
            if (player != null)
            {
                //lacks definition
                player.Update();
            }
        }

        public void Draw(RenderWindow window)
        {
            if (player != null)
            {
                //lacks definition
                player.Draw();
            }
        }

        public void CheckGB()
        {
            if (player != null)
            {
                player.CheckGB();
                if (player.toDelete)
                {
                    player = null;
                }
            }
        }



    }
}
