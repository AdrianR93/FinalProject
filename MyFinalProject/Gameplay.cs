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

            // Borders, I'll make it into an array or a vector, too much repetitive code
            // Don't know how to make it an Array lol, just leave it at that atm
            Nborder = new InvisibleBorder(new Vector2f(0.1f, 0.1f), new Vector2f(1000.0f, 10.0f));
            Sborder = new InvisibleBorder(new Vector2f(0.1f, 920.0f), new Vector2f(1000.0f, 10.0f));
            Wborder = new InvisibleBorder(new Vector2f(0.1f, 0.1f), new Vector2f(10.0f, 1000.0f));
            Eborder = new InvisibleBorder(new Vector2f(720.0f, 0), new Vector2f(10.0f, 1000.0f));


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

            List<int> indexToDelete = new List<int>();
            for (int i = 0; i < npcs.Count; i++)
            {
                npcs[i].CheckGB();
                if (npcs[i].toDelete)
                {
                    indexToDelete.Add(i);
                }
            }
            // for reverso
            // dispose now adentro 
            // revisa esta vaina
            for (int i = indexToDelete.Count - 1; i >= 0; i--)
            {
                npcs[i].DisposeNow();
                npcs.RemoveAt(i);
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
                npcs.Add(new NPC(new Vector2f((i * 80.0f) + 140.0f, 75.9f)));
            }
            spawnLocations = 5;
            for (int i = 0; i < spawnLocations; i++)
            {
                npcs.Add(new NPC(new Vector2f((i * 105.0f) + 40, 190.9f)));
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

        public void ObstacleCourse()
        {
            obstacle = new Obstacle();
        }

    }
}
