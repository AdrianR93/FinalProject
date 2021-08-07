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
        private Ui ui;

        private Obstacle tree1;
        private Obstacle tree2;
        private Obstacle tree3;
        private Obstacle tree4;
        private Obstacle tree5;
        private Obstacle tree6;
        private Player player;
        private List<NPC> npcs;
        private GameOverScreen gameOver;

        private InvisibleBorder Nborder;
        private InvisibleBorder Sborder;
        private InvisibleBorder Wborder;
        private InvisibleBorder Eborder;

        private Gameplay()
        {
            map = new Map();
            TreeCreation();

            ui = new Ui();
            CreateBorders();
            player = new Player();
            npcs = new List<NPC>();
            InitialZombies();


        }

        private void TreeCreation()
        {
            tree1 = new Obstacle(Obstacle.Trees.Tree1, new Vector2f(650.0f, 400.0f));
            tree2 = new Obstacle(Obstacle.Trees.Tree2, new Vector2f(80.0f, 180.0f));
            tree3 = new Obstacle(Obstacle.Trees.Tree3, new Vector2f(450.0f, 600.0f));
            tree4 = new Obstacle(Obstacle.Trees.Tree2, new Vector2f(300.0f, 380.0f));
            tree5 = new Obstacle(Obstacle.Trees.Tree3, new Vector2f(150.0f, 90.0f));
            tree6 = new Obstacle(Obstacle.Trees.Tree3, new Vector2f(500.0f, 900.0f));
        }

        public void Update()
        {
 
            ui.Update(player.GetLifes());

            if (player != null)
            {
                player.Update();
            }

            if (npcs != null)
            {
                for (int i = 0; i < npcs.Count; i++)
                {
                    npcs[i].Update();

                }
            }
            if (gameOver != null)
            {
                gameOver.Update();
            }

        }

        public void Draw(RenderWindow window)
        {
            map.Draw(window);
            if (player != null)
            {
                player.Draw(window);
            }


            if (npcs != null)
            {
                for (int i = 0; i < npcs.Count; i++)
                {
                    npcs[i].Draw(window);

                }
            }

            ui.Draw(window);

            if (tree1 != null && tree2 != null && tree3 != null && tree4 != null && tree5 != null)
            {
                tree1.Draw(window);
                tree2.Draw(window);
                tree3.Draw(window);
                tree4.Draw(window);
                tree5.Draw(window);
            }
            if (gameOver != null)
            {
                gameOver.Draw(window);
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
            int spawnLocations = 6;
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
            int lIndex;
            Vector2f nextLocation;
            Vector2f[] newLocations =
            {
            new Vector2f(100.0f, 40.0f),
            new Vector2f(250.0f, 40.0f),
            new Vector2f(300.0f, 40.0f),
            new Vector2f(350.0f, 90.0f),
            new Vector2f(500.0f, 10.0f),
            new Vector2f(400.0f, 40.0f),
            new Vector2f(600.0f, 10.0f),
            };

            Random selectLocation = new Random();

            lIndex = selectLocation.Next(newLocations.Length);
            nextLocation = newLocations[lIndex];
            npcs.Add(new NPC(newLocations[lIndex]));

        }

        private void CreateBorders()
        {
            Nborder = new InvisibleBorder(new Vector2f(1f, 1f), new Vector2f(720.0f, 0.5f));
            Sborder = new InvisibleBorder(new Vector2f(1f, 900.0f), new Vector2f(720f, 0.5f));
            Wborder = new InvisibleBorder(new Vector2f(1f, 1f), new Vector2f(0.5f, 900.0f));
            Eborder = new InvisibleBorder(new Vector2f(720.0f, 0), new Vector2f(0.5f, 900.0f));
        }



        public void GameOver()
        {
            gameOver = new GameOverScreen(new Vector2f((float)-60.0, 0));
        }

    }
}

