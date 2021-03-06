using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{
    class Game
    {
        private static Vector2f windowSize;
        private RenderWindow window;

        

        public Game()
        {
            VideoMode videoMode = new VideoMode();
            videoMode.Width = 720;
            videoMode.Height = 900;


            window = new RenderWindow(videoMode, "My Game");
            window.Closed += CloseWindow;
            window.SetFramerateLimit(FrameRate.FRAMERATE_LIMIT);

            Gameplay.GetInstance();
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

        public void UpdateGame()
        {
            Gameplay.GetInstance().Update();
            windowSize = window.GetView().Size;
        }
        public void DrawGame()
        {
            Gameplay.GetInstance().Draw(window);
            window.Display();
        }

        public void CheckGB()
        {
            Gameplay.GetInstance().CheckGB();
        }

        public static Vector2f GetWindowSize()
        {
            return windowSize;
        }
    }
}
