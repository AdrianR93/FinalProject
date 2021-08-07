using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Obstacle : GameObject, IColisionable
    {
        public enum Trees { Tree1, Tree2, Tree3, Error }

        Trees treeType;

        private Clock frameTimer;
        private int currentFrame = 0;
        private IntRect frameRect;
        private List<List<Vector2i>> animations;
        private int sheetColumns = 4;
        private int sheetRows = 3;
        private float animTime = 2f;

        public Obstacle(Trees treeType, Vector2f startposition) : base("Sprites" + Path.DirectorySeparatorChar + "obstacles.png", startposition)
        {
            this.treeType = treeType;

            frameRect = new IntRect();
            frameRect.Width = (int)texture.Size.X / sheetColumns;
            frameRect.Height = (int)texture.Size.Y / sheetRows;
            sprite.TextureRect = frameRect;
            frameTimer = new Clock();
            ObstacleLoadSprite();

            sprite.Scale = new Vector2f(1.80f, 1.80f);
            CollisionManager.GetInstance().AddToCollisionManager(this);
        }


        private void ObstacleLoadSprite()
        {

            animations = new List<List<Vector2i>>
            {
                new List<Vector2i>(),
                new List<Vector2i>(),
                new List<Vector2i>(),
            };

            for (int i = 0; i < sheetColumns; i++)
            {
                animations[(int)Trees.Tree1].Add(new Vector2i(i, 0));

            }

            for (int i = 0; i < sheetColumns; i++)
            {
                animations[(int)Trees.Tree2].Add(new Vector2i(i, 0));
            
            }
            
            for (int i = 0; i < sheetColumns; i++)
            {
                animations[(int)Trees.Tree3].Add(new Vector2i(i, 0));
            }

        }

        private void ObstacleUpdateAnimation()
        {
            switch (treeType)
            {
                case Trees.Tree1:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)Trees.Tree1].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)Trees.Tree1].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)Trees.Tree1][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)Trees.Tree1][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case Trees.Tree2:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)Trees.Tree2].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)Trees.Tree2].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)Trees.Tree2][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)Trees.Tree2][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case Trees.Tree3:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)Trees.Tree3].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)Trees.Tree3].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)Trees.Tree3][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)Trees.Tree3][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case Trees.Error:
                    break;
                default:
                    break;
            }
            sprite.TextureRect = frameRect;
        }


        public override void Update()
        {
            ObstacleUpdateAnimation();
            base.Update();
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }

        public FloatRect GetBounds()
        {
            return sprite.GetGlobalBounds();
        }
        public void OnCollisionEnter(IColisionable other)
        {

        }

        public void OnCollisionStay(IColisionable other)
        {

        }

        public void OnCollisionExit(IColisionable other)
        {

        }
        public override void DisposeNow()
        {
            CollisionManager.GetInstance().RemoveFromCollisionManager(this);
            base.DisposeNow();
        }



    }
}
