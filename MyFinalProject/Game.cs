using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{
    public class Game
    {
        private static Vector2f windowSize;
        private RenderWindow window;
        // create gameplay constructor 
        private GamePlay gamePlay;
        private Camera camera;

        public Game()
        {
            VideoMode videoMode = new VideoMode();
            videoMode.Width = 800;
            videoMode.Height = 600;

            window = new RenderWindow(videoMode, "Adrian Rojas");
            //create function close window
            window.Closed += CloseWindow;
            window.SetFramerateLimit(FrameRate.FRAMERATE_LIMIT);
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            window.Close();
        }

        public bool UpdateWindow()
        {
            window.DispatchEvents();
            window.Clear(Color.Black);
            return window.IsOpen;
        }




    }
}
