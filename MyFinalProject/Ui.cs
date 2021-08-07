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
        enum HeartStatus { OneHeart, TwoHeart, ThreeHearts, Error }

        private HeartStatus status;
        private int lifeRemaing;


        private int currentFrame = 0;
        private IntRect frameRect;
        private List<List<Vector2i>> animations;
        private int sheetColumns = 1;
        private int sheetRows = 3;
        public Ui() : base("Sprites" + Path.DirectorySeparatorChar + "heartsUI.png", new Vector2f(40.0f, 40.0f))
        {
            frameRect = new IntRect();
            frameRect.Width = (int)texture.Size.X / sheetColumns;
            frameRect.Height = (int)texture.Size.Y / sheetRows;
            sprite.TextureRect = frameRect;
            status = HeartStatus.ThreeHearts;
            UiLoadSprites();

            sprite.Scale = new Vector2f(1f, 1f);

        }

        private void UiLoadSprites()
        {
            animations = new List<List<Vector2i>>
            {
                new List<Vector2i>(),
                new List<Vector2i>(),
                new List<Vector2i>(),

            };

            animations[(int)HeartStatus.OneHeart].Add(new Vector2i(0, 0));

            animations[(int)HeartStatus.TwoHeart].Add(new Vector2i(0, 1));

            animations[(int)HeartStatus.ThreeHearts].Add(new Vector2i(0, 2));



        }
        private void UiUpdateAnimation(int lifeRemaining)
        {
            this.lifeRemaing = lifeRemaining - 1;
            status = (HeartStatus)lifeRemaing;

            switch (status)
            {
                case HeartStatus.OneHeart:
                    currentFrame = 0;
                    frameRect.Left = animations[(int)HeartStatus.OneHeart][currentFrame].X * frameRect.Width;
                    frameRect.Top = animations[(int)HeartStatus.OneHeart][currentFrame].Y * frameRect.Height;
                    break;
                case HeartStatus.TwoHeart:

                    currentFrame = 0;
                    frameRect.Left = animations[(int)HeartStatus.TwoHeart][currentFrame].X * frameRect.Width;
                    frameRect.Top = animations[(int)HeartStatus.TwoHeart][currentFrame].Y * frameRect.Height;
                    break;
                case HeartStatus.ThreeHearts:
                    currentFrame = 0;
                    frameRect.Left = animations[(int)HeartStatus.ThreeHearts][currentFrame].X * frameRect.Width;
                    frameRect.Top = animations[(int)HeartStatus.ThreeHearts][currentFrame].Y * frameRect.Height;
                    break;
                case HeartStatus.Error:
                    break;
                default:
                    break;
            }
            sprite.TextureRect = frameRect;
        }


        public override void Update(int lifeRemaing)
        {
            UiUpdateAnimation(lifeRemaing);
            base.Update();
        }



    }
}
