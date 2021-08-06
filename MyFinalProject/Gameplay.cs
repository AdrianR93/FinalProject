using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{

    public class Gameplay
    {
        private static Gameplay instance;

        public static Gameplay GetInstance()
        {
            if (instance == null)
            {
                instance = new Gameplay();
            }
            return instance;
        }

        private Map map;
        private Obstacle obstacle;
        private Player player;
        private List<NPC> npcs;


        private InvisibleBorder Nborder;
        private InvisibleBorder Sborder;
        private InvisibleBorder Wborder;
        private InvisibleBorder Eborder;

        private Gameplay()
        {
            map = new Map();
            CreateBorders();
            //ObstacleCourse();
            player = new Player();
            npcs = new List<NPC>();
            InitialZombies();


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
            if (npcs != null)
            {
                List<NPC> indexToDelete = new List<NPC>();
                for (int i = 0; i < npcs.Count; i++)
                {
                    npcs[i].CheckGB();
                    if (npcs[i].toDelete)
                    {
                        indexToDelete.Add(npcs[i]);
                    }
                }
                for (int i = 0; i < indexToDelete.Count; i++)
                {
                    indexToDelete[i].DisposeNow();
                    npcs.Remove(indexToDelete[i]);

                }

            }
        }

        public void InitialZombies()
        {
            int spawnLocations = 4;
            for (int i = 0; i < spawnLocations; i++)
            {
                npcs.Add(new NPC(new Vector2f((i * 180.0f), 10.0f)));
            }
            spawnLocations = 4;
            for (int i = 0; i < spawnLocations; i++)
            {
                npcs.Add(new NPC(new Vector2f((i * 80.0f) + 140.0f, 75.9f)));
            }
            spawnLocations = 3;
            for (int i = 0; i < spawnLocations; i++)
            {
                npcs.Add(new NPC(new Vector2f((i * 105.0f) + 50, 190.9f)));
            }


        }

        public void RevivingZombies()
        {
            int spawnLocations = 2;
            for (int i = 0; i < spawnLocations; i++)
            {
                npcs.Add(new NPC(new Vector2f((i * 50.0f), 0.9f)));
            }
        }

        private void ObstacleCourse()
        {
            obstacle = new Obstacle();
        }

        private void CreateBorders()
        {
            Nborder = new InvisibleBorder(new Vector2f(1f, 1f), new Vector2f(720.0f, 0.5f));
            Sborder = new InvisibleBorder(new Vector2f(1f, 900.0f), new Vector2f(720f, 0.5f));
            Wborder = new InvisibleBorder(new Vector2f(1f, 1f), new Vector2f(0.5f, 900.0f));
            Eborder = new InvisibleBorder(new Vector2f(720.0f, 0), new Vector2f(0.5f, 900.0f));
        }



    }
}

