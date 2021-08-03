using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{
    public class Gameplay
    {
        private Player player;
        private Map map;

        public Gameplay()
        {
            //player = new Player();
            map = new Map();

        }

        public void Update()
        {
            if (player != null)
            {
                //lacks definition
                player.Update();
            }
            if (map != null)
            {
                map.Update();
            }
        }

        public void Draw(RenderWindow window)
        {
            if (player != null)
            {
                //lacks definition
                player.Draw();
            }
            if (map != null)
            {
                map.Draw(window);
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
