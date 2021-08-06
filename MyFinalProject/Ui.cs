using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Ui : GameObject
    {
        enum HeartStatus { OneHeart = 1, TwoHeart, ThreeHearts, Error }

        private HeartStatus status;
        private int lifeRemaing;


        private Clock frameTimer;
        private int currentFrame = 0;
        private IntRect frameRect;
        private List<List<Vector2i>> animations;
        private int sheetColumns = 1;
        private int sheetRows = 3;
        private float animTime = 6f;
        public Ui() : base("Sprites" + Path.DirectorySeparatorChar + "heartsUI.png", new Vector2f(40.0f, 40.0f))
        {
            frameRect = new IntRect();
            frameRect.Width = (int)texture.Size.X / sheetColumns;
            frameRect.Height = (int)texture.Size.Y / sheetRows;
            sprite.TextureRect = frameRect;
            frameTimer = new Clock();
            UiLoadSprites();

            sprite.Scale = new Vector2f(1f, 1f);

        }

        private void UiLoadSprites()
        {
            animations = new List<List<Vector2i>>();


            animations.Add(new List<Vector2i>());
            animations.Add(new List<Vector2i>());
            animations.Add(new List<Vector2i>());
            animations.Add(new List<Vector2i>());

            animations[(int)HeartStatus.OneHeart].Add(new Vector2i(0, 0));


            animations[(int)HeartStatus.TwoHeart].Add(new Vector2i(1, 0));


            animations[(int)HeartStatus.ThreeHearts].Add(new Vector2i(1, 0));

        }

        private void UiUpdateAnimation(int lifeRemaining)
        {
            this.lifeRemaing = lifeRemaining;
            status = (HeartStatus)lifeRemaining;

            switch (status)
            {
                case HeartStatus.OneHeart:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)HeartStatus.OneHeart].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)HeartStatus.OneHeart].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)HeartStatus.OneHeart][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)HeartStatus.OneHeart][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case HeartStatus.TwoHeart:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)HeartStatus.TwoHeart].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)HeartStatus.TwoHeart].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)HeartStatus.TwoHeart][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)HeartStatus.TwoHeart][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case HeartStatus.ThreeHearts:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)HeartStatus.ThreeHearts].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)HeartStatus.ThreeHearts].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)HeartStatus.ThreeHearts][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)HeartStatus.ThreeHearts][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case HeartStatus.Error:
                    break;
                default:
                    break;
            }


        }


        public override void Update(int lifeRemaing)
        {
            UiUpdateAnimation(lifeRemaing);
            base.Update();
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }

    }
}
