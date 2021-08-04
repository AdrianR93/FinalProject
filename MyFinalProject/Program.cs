using System;

namespace MyFinalProject
{
    //this is it, my last attempt of an isometric something with bullets and target practices, that may or may not evolve into enemies
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            FrameRate.InitFrameRateSystem();

            while (game.UpdateWindow())
            {
                game.UpdateGame();
                CollisionManager.GetInstance().CheckCollisions();
                game.CheckGB();
                game.DrawGame();

                FrameRate.OnFrameEnd();
            }


        }
    }
}
