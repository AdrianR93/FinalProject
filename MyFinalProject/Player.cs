using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Player : GameObject, IColisionable
    {
        enum PlayerStatus { IdleUp, IdleDown, IdleLeft, IdleRight, MovingUp, MovingDown, MovingLeft, MovingRight, ShootingUp, ShootingDown, ShootingLeft, ShootingRight, Error }

        private int maxLifes;
        private int lifes;
        private Clock invulneravility;
        private float speed;
        private bool movementOn;
        private List<Bullet> bullets;
        private Direction currentDirection;
        private float fireDelay;
        private float fireRate;
        private PlayerStatus status;

        private Clock frameTimer;
        private int currentFrame = 0;
        private IntRect frameRect;
        private List<List<Vector2i>> animations;
        private int sheetColumns = 7;
        private int sheetRows = 8;
        private float animTime = 20f;


        public Player() : base("Sprites" + Path.DirectorySeparatorChar + "pj.png", new Vector2f(300.0f, 720.0f))
        {
            frameRect = new IntRect();
            frameRect.Width = (int)texture.Size.X / sheetColumns;
            frameRect.Height = (int)texture.Size.Y / sheetRows;
            sprite.TextureRect = frameRect;
            status = PlayerStatus.IdleUp;
            frameTimer = new Clock();
            PlayerLoadSprites();

            sprite.Scale = new Vector2f(0.25f, 0.25f);
            maxLifes = 3;
            lifes = maxLifes;
            invulneravility = new Clock();
            speed = 150.0f;
            movementOn = false;
            bullets = new List<Bullet>();
            fireDelay = 0.2f;
            fireRate = 0.2f;
            CollisionManager.GetInstance().AddToCollisionManager(this);
        }
        public override void Update()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
            }
            Movement();
            Shoot();
            PlayerAnimation();
            base.Update();
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(window);
            }
        }

        private void PlayerLoadSprites()
        {
            animations = new List<List<Vector2i>>();

            for (int i = 0; i < (int)PlayerStatus.Error; i++)
            {
                animations.Add(new List<Vector2i>());
            }

            #region "Animations Vectors"

            //Idles
            animations[(int)PlayerStatus.IdleUp].Add(new Vector2i(6, 6));
            animations[(int)PlayerStatus.IdleUp].Add(new Vector2i(0, 7));
            animations[(int)PlayerStatus.IdleUp].Add(new Vector2i(6, 6));


            animations[(int)PlayerStatus.IdleDown].Add(new Vector2i(5, 3));
            animations[(int)PlayerStatus.IdleDown].Add(new Vector2i(5, 4));
            animations[(int)PlayerStatus.IdleDown].Add(new Vector2i(5, 3));


            animations[(int)PlayerStatus.IdleLeft].Add(new Vector2i(3, 3));
            animations[(int)PlayerStatus.IdleLeft].Add(new Vector2i(4, 3));
            animations[(int)PlayerStatus.IdleLeft].Add(new Vector2i(3, 3));


            animations[(int)PlayerStatus.IdleRight].Add(new Vector2i(3, 2));
            animations[(int)PlayerStatus.IdleRight].Add(new Vector2i(4, 2));
            animations[(int)PlayerStatus.IdleRight].Add(new Vector2i(3, 2));


            //Moving Up
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(1, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(2, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(3, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(4, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(5, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(6, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(0, 7));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(1, 7));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(2, 7));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(3, 7));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(3, 7));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(2, 7));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(1, 7));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(0, 7));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(6, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(5, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(4, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(3, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(2, 6));
            animations[(int)PlayerStatus.MovingUp].Add(new Vector2i(1, 6));

            //Moving Down
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(5, 4));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(6, 4));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(0, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(1, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(2, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(3, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(4, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(5, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(6, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(0, 6));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(0, 6));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(6, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(5, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(4, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(3, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(2, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(1, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(0, 5));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(6, 4));
            animations[(int)PlayerStatus.MovingDown].Add(new Vector2i(5, 4));

            //Moving Right
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(5, 1));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(6, 1));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(0, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(1, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(2, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(3, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(4, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(5, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(6, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(0, 3));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(0, 3));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(6, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(5, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(4, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(3, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(2, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(1, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(0, 2));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(6, 1));
            animations[(int)PlayerStatus.MovingRight].Add(new Vector2i(5, 1));

            //Moving Left
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(1, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(2, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(3, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(4, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(5, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(6, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(0, 4));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(1, 4));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(2, 4));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(3, 4));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(2, 4));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(1, 4));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(0, 4));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(6, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(5, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(4, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(3, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(2, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(1, 3));
            animations[(int)PlayerStatus.MovingLeft].Add(new Vector2i(4, 4));

            //shooting up
            animations[(int)PlayerStatus.ShootingUp].Add(new Vector2i(0, 0));
            animations[(int)PlayerStatus.ShootingUp].Add(new Vector2i(0, 1));
            animations[(int)PlayerStatus.ShootingUp].Add(new Vector2i(0, 2));

            //shooting down
            animations[(int)PlayerStatus.ShootingDown].Add(new Vector2i(2, 1));
            animations[(int)PlayerStatus.ShootingDown].Add(new Vector2i(3, 1));
            animations[(int)PlayerStatus.ShootingDown].Add(new Vector2i(4, 1));

            //shooting rigth
            animations[(int)PlayerStatus.ShootingRight].Add(new Vector2i(0, 3));
            animations[(int)PlayerStatus.ShootingRight].Add(new Vector2i(0, 4));
            animations[(int)PlayerStatus.ShootingRight].Add(new Vector2i(0, 5));

            //shooting left
            animations[(int)PlayerStatus.ShootingLeft].Add(new Vector2i(0, 6));
            animations[(int)PlayerStatus.ShootingLeft].Add(new Vector2i(1, 0));
            animations[(int)PlayerStatus.ShootingLeft].Add(new Vector2i(1, 1));



            #endregion
        }

        private void PlayerAnimation()
        {
            switch (status)
            {
                case PlayerStatus.IdleUp:
                    animTime = 2.0f;
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.IdleUp].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.IdleUp].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.IdleUp][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.IdleUp][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.IdleDown:
                    animTime = 2.0f;
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.IdleDown].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.IdleDown].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.IdleDown][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.IdleDown][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.IdleLeft:
                    animTime = 2.0f;
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.IdleLeft].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.IdleLeft].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.IdleLeft][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.IdleLeft][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.IdleRight:
                    animTime = 2.0f;
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.IdleRight].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.IdleRight].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.IdleRight][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.IdleRight][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.MovingUp:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.MovingUp].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.MovingUp].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.MovingUp][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.MovingUp][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.MovingDown:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.MovingDown].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.MovingDown].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.MovingDown][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.MovingDown][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.MovingLeft:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.MovingLeft].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.MovingLeft].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.MovingLeft][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.MovingLeft][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.MovingRight:
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.MovingRight].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.MovingRight].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.MovingRight][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.MovingRight][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.ShootingUp:
                    animTime = 10.0f;
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.ShootingUp].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.ShootingUp].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.ShootingUp][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.ShootingUp][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.ShootingDown:
                    animTime = 20.0f;
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.ShootingDown].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.ShootingDown].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.ShootingDown][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.ShootingDown][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.ShootingLeft:
                    animTime = 10.0f;
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.ShootingLeft].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.ShootingLeft].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.ShootingLeft][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.ShootingLeft][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.ShootingRight:
                    animTime = 10.0f;
                    if (frameTimer.ElapsedTime.AsSeconds() > animTime / animations[(int)PlayerStatus.ShootingRight].Count - 1)
                    {
                        currentFrame++;
                        if (currentFrame >= animations[(int)PlayerStatus.ShootingRight].Count - 1)
                        {
                            currentFrame = 0;
                        }
                        frameTimer.Restart();
                        frameRect.Left = animations[(int)PlayerStatus.ShootingRight][currentFrame].X * frameRect.Width;
                        frameRect.Top = animations[(int)PlayerStatus.ShootingRight][currentFrame].Y * frameRect.Height;
                    }
                    break;
                case PlayerStatus.Error:
                    break;
                default:
                    break;
            }
            sprite.TextureRect = frameRect;
        }

        private void Movement()
        {
            movementOn = false;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                currentPosition.X += speed * FrameRate.GetDeltaTime();
                status = PlayerStatus.MovingRight;
                movementOn = true;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                currentPosition.X -= speed * FrameRate.GetDeltaTime();
                status = PlayerStatus.MovingLeft;
                movementOn = true;

            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                currentPosition.Y += speed * FrameRate.GetDeltaTime();
                status = PlayerStatus.MovingDown;
                movementOn = true;

            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                currentPosition.Y -= speed * FrameRate.GetDeltaTime();
                status = PlayerStatus.MovingUp;
                movementOn = true;

            }
            bool isMovingHorizontal = Keyboard.IsKeyPressed(Keyboard.Key.A) && Keyboard.IsKeyPressed(Keyboard.Key.D);
            bool isMovingVertical = Keyboard.IsKeyPressed(Keyboard.Key.W) && Keyboard.IsKeyPressed(Keyboard.Key.S);

            if (movementOn == false)
            {
                if (currentDirection == Direction.North)
                {
                    status = PlayerStatus.IdleUp;
                }
                else if (currentDirection == Direction.South)
                {
                    status = PlayerStatus.IdleDown;
                }
                else if (currentDirection == Direction.East)
                {
                    status = PlayerStatus.IdleRight;
                }
                else if (currentDirection == Direction.West)
                {
                    status = PlayerStatus.IdleLeft;
                }
            }

        }
        private void Shoot()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.W) && fireDelay >= fireRate)
            {
                status = PlayerStatus.ShootingUp;
                ShootDirection(Direction.North);
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.S) && fireDelay >= fireRate))
            {
                status = PlayerStatus.ShootingDown;
                ShootDirection(Direction.South);
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.A) && fireDelay >= fireRate))
            {
                status = PlayerStatus.ShootingLeft;
                ShootDirection(Direction.West);
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space) && Keyboard.IsKeyPressed(Keyboard.Key.D) && fireDelay >= fireRate))
            {
                status = PlayerStatus.ShootingRight;
                ShootDirection(Direction.East);
            }
            //Animaciones de disparo fallando :) 
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Space)) && fireDelay >= fireRate)
            {
                //switch (currentDirection)
                //{
                //    case Direction.North:
                //        status = PlayerStatus.ShootingUp;
                //        break;
                //    case Direction.South:
                //        status = PlayerStatus.ShootingDown;
                //        break;
                //    case Direction.East:
                //        status = PlayerStatus.ShootingRight;
                //        break;
                //    case Direction.West:
                //        status = PlayerStatus.ShootingLeft;
                //        break;
                //    default:
                //        break;
                //}
                ShootDirection(currentDirection);
            }
            fireDelay += FrameRate.GetDeltaTime();
        }

        private void ShootDirection(Direction direction)
        {
            Vector2f spawnPosition = currentPosition;
            spawnPosition.X += (texture.Size.X / sheetColumns * sprite.Scale.X) / 2.0f;
            spawnPosition.Y += (texture.Size.Y / sheetRows * sprite.Scale.Y) / 2.0f;
            bullets.Add(new Bullet(spawnPosition, direction));
            fireDelay = 0.0f;
            currentDirection = direction;
        }

        private void BounceOnLimits()
        {
            if ((Keyboard.IsKeyPressed(Keyboard.Key.W)))
            {
                Console.WriteLine("wall W");
                currentPosition.Y += 3 * speed * FrameRate.GetDeltaTime();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.A)))
                {
                    currentPosition.X -= speed * FrameRate.GetDeltaTime();
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.D)))
                {
                    currentPosition.X += speed * FrameRate.GetDeltaTime();
                }
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.A)))
            {
                Console.WriteLine("wall A");
                currentPosition.X += 3 * speed * FrameRate.GetDeltaTime();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.W)))
                {
                    currentPosition.Y -= speed * FrameRate.GetDeltaTime();
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.S)))
                {
                    currentPosition.Y += speed * FrameRate.GetDeltaTime();
                }
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.S)))
            {
                Console.WriteLine("wall S");
                currentPosition.Y -= 3 * speed * FrameRate.GetDeltaTime();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.A)))
                {
                    currentPosition.X -= speed * FrameRate.GetDeltaTime();
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.D)))
                {
                    currentPosition.X += speed * FrameRate.GetDeltaTime();
                }
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.D)))
            {
                Console.WriteLine("wall D");
                currentPosition.X -= 3 * speed * FrameRate.GetDeltaTime();
                if ((Keyboard.IsKeyPressed(Keyboard.Key.W)))
                {
                    currentPosition.Y -= speed * FrameRate.GetDeltaTime();
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.S)))
                {
                    currentPosition.Y += speed * FrameRate.GetDeltaTime();
                }
            }
        }

        public int GetLifes()
        {
            return lifes;
        }

        public int GetMaxLifes()
        {
            return maxLifes;
        }

        public FloatRect GetBounds()
        {
            return sprite.GetGlobalBounds();
        }
        public void OnCollisionEnter(IColisionable other)
        {
            if (other is InvisibleBorder)
            {
                BounceOnLimits();
            }

            if (other is NPC)
            {
                if (invulneravility.ElapsedTime.AsSeconds() >= 3)
                {
                    lifes--;
                }
                invulneravility.Restart();
                {
                    if (lifes <= 0)
                    {
                        Gameplay.GetInstance().GameOver();
                    }
                }
            }
        }


        public void OnCollisionStay(IColisionable other)
        {

        }

        public void OnCollisionExit(IColisionable other)
        {
            if (other is InvisibleBorder)
            {
                Console.WriteLine("wall exit");
            }
        }

        public override void DisposeNow()
        {
            CollisionManager.GetInstance().RemoveFromCollisionManager(this);
            base.DisposeNow();
        }

        public override void CheckGB()
        {
            List<Bullet> indexToDelete = new List<Bullet>();
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].CheckGB();
                if (bullets[i].toDelete)
                {
                    indexToDelete.Add(bullets[i]);
                }
            }
            for (int i = 0; i < indexToDelete.Count; i++)
            {
                indexToDelete[i].DisposeNow();
                bullets.Remove(indexToDelete[i]);
            }
        }
    }
}
