using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{
    public class Gameplay
    {
        private Map map;
        private Obstacle obstacle;
        private Player player;
        private InvisibleBorder border;

        public Gameplay()
        {
            map = new Map();
            obstacle = new Obstacle();
            player = new Player();

            border = new InvisibleBorder(new Vector2f(0, 0), new Vector2f(200.0f, 200.0f));
        }

        public void Update()
        {
            map.Update();

            if (player != null)
            {
                player.Update();
            }
            if (obstacle != null)
            {
                obstacle.Update();
            }
        }

        public void Draw(RenderWindow window)
        {
            map.Draw(window);

            if (player != null)
            {
                player.Draw(window);
            }

            if (obstacle != null)
            {
                obstacle.Draw(window);
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
