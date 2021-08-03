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
            player = new Player();
            map = new Map();

        }

        public void Update()
        {
            if (map != null)
            {
                map.Update();
            }
            if (player != null)
            {
                player.Update();
            }
        }

        public void Draw(RenderWindow window)
        {
            if (map != null)
            {
                map.Draw(window);
            }
            if (player != null)
            {
                player.Draw(window);
            }
        }

        //public void CheckGB()
        //{
        //    if (player != null)
        //    {
        //        player.CheckGB();
        //        if (player.toDelete)
        //        {
        //            player = null;
        //        }
        //    }
        //}



    }
}
