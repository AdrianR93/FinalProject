using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyFinalProject
{
    class NPC : GameObject, IColisionable
    {
        enum Status { Down, Up, Error };

        private float speed;
        Random speedVariant;
        Status status;

        private Clock frameTimer;
        private int currentFrame = 0;
        private IntRect frameRect;
        private List<List<Vector2i>> animations;
        private int sheetColumns = 16;
        private int sheetRows = 2;
        private float animTime = 16.5f;

        public NPC(Vector2f startposition) : base("Sprites" + Path.DirectorySeparatorChar + "npc2.png", startposition)
        {
            frameRect = new IntRect();
            frameRect.Width = (int)texture.Size.X / sheetColumns;
            frameRect.Height = (int)texture.Size.Y / sheetRows;
            sprite.TextureRect = frameRect;
            status = Status.Down;

            sprite.Scale = new Vector2f(0.25f, 0.25f);
            speedVariant = new Random();
            speed = (float)speedVariant.Next(100, 250);
            CollisionManager.GetInstance().AddToCollisionManager(this);
            frameTimer = new Clock();
            NPCLoadSprites();
        }

        public override void Update()
        {
            NPCMovement();
            NPCUpdateAnimation();
            base.Update();
        }

        private void NPCLoadSprites()
        {
            animations = new List<List<Vector2i>>();

            animations.Add(new List<Vector2i>());
            animations.Add(new List<Vector2i>());

            for (int i = 0; i < sheetColumns; i++)
            {
                animations[(int)Status.Down].Add(new Vector2i(i, 0));

            }

            for (int i = 0; i < sheetColumns; i++)
            {
                animations[(int)Status.Up].Add(new Vector2i(i, 1));

            }


        }

        private void NPCUpdateAnimation()
        {
            switch (status)
            {
                case Status.Down:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)Status.Down].Count-1)
                    {
                        currentFrame++;
                        if (currentFrame == animations[(int)Status.Down].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)Status.Down][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)Status.Down][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case Status.Up:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)Status.Up].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame == animations[(int)Status.Up].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)Status.Up][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)Status.Up][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case Status.Error:
                    break;
                default:
                    break;
            }
            sprite.TextureRect = frameRect;
        }

        public void NPCMovement()
        {
            switch (status)
            {
                case Status.Down:
                    currentPosition.Y += speed * FrameRate.GetDeltaTime();
                    break;
                case Status.Up:
                    currentPosition.Y -= speed * FrameRate.GetDeltaTime();
                    break;
                case Status.Error:
                    break;
                default:
                    break;
            }

        }

        public override void DisposeNow()
        {
            CollisionManager.GetInstance().RemoveFromCollisionManager(this);
            base.DisposeNow();
        }

        public override void CheckGB()
        {
            base.CheckGB();
        }

        public FloatRect GetBounds()
        {
            return sprite.GetGlobalBounds();
        }

        public void OnCollisionEnter(IColisionable other)
        {
            if (other is NPCBouncer)
            {
                if (status == Status.Down)
                {
                    status = Status.Up;
                }
                else if (status == Status.Up)
                {
                    status = Status.Down;
                }
            }
        }

        public void OnCollisionStay(IColisionable other)
        {

        }

        public void OnCollisionExit(IColisionable other)
        {
        }

    }
}
