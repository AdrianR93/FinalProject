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
        private List<NPC> npcs;

        private InvisibleBorder border;

        public Gameplay()
        {
            map = new Map();
            //ObstacleCourse();
            player = new Player();
            npcs = new List<NPC>();
            InitialZombies();

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
            if (npcs != null)
            {
                for (int i = 0; i < npcs.Count; i++)
                {
                npcs[i].Update();

                }
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
            if (npcs != null)
            {
                for (int i = 0; i < npcs.Count; i++)
                {
                npcs[i].Draw(window);

                }
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

        public void InitialZombies()
        {
            int spawnLocations = 7;
            for (int i = 0; i < spawnLocations; i++)
            {
                npcs.Add(new NPC(new Vector2f((i * 90.0f), 0.9f)));
            }
            spawnLocations = 4;
            for (int i = 0; i < spawnLocations; i++)
            {
                npcs.Add(new NPC(new Vector2f((i * 80.0f)+ 140.0f, 75.9f)));
            }
            spawnLocations = 5;
            for (int i = 0; i < spawnLocations; i++)
            {
                npcs.Add(new NPC(new Vector2f((i * 105.0f)+40, 190.9f)));
            }


        }

        public void ObstacleCourse()
        {
            obstacle = new Obstacle();
        }

    }
}
