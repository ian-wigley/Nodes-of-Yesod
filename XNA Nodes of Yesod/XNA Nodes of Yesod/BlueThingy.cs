using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public class BlueThingy : Enemy
    {

        public BlueThingy(float xPos, float yPos, float speedX, float speedY, Texture2D sprite,
            List<Rectangle> walls, List<Rectangle> platforms)
            : base(xPos, yPos, speedX, speedY, sprite, walls)
        {
            mPositionX = xPos;
            mPositionY = yPos;
            mSpeedX = speedX;
            mSpeedY = speedY;
            mSprite = sprite;
            mWalls = walls;
            mPlatforms = platforms;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 7;
            mCurrentFrameY = 5;
            mSheetSize = 7;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;
        }

        public Rectangle BlueThingyRect
        {
            get
            {
                return new Rectangle((int)mPositionX + 20, (int)mPositionY + 40, 20, 30);
            }
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            mPositionX += mSpeedX;
            mPositionY += mSpeedY;

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                ++mCurrentFrameX;
                if (mCurrentFrameX == 9 && mCurrentFrameY == 5)
                {
                    mCurrentFrameX = 12;
                    mCurrentFrameY = 3;
                }
                else if (mCurrentFrameX >= mSheetSize + 12)
                {
                    mCurrentFrameX = 12;
                }
            }

            if (mPositionY > 366 ||
                    mPositionY < 0)
            {
                mSpeedY *= -1;
            }

            if (mPositionX > 750 || mPositionX < 0)
            {
                mSpeedX *= -1;
            }

            if (mWalls.Count > 0)
            {
                foreach (Rectangle wallRects in mWalls)
                {
                    if (wallRects.X == 62)
                    {
                        if (mPositionX <= 100)
                        {
                            this.mSpeedX *= -1;
                            mPositionX += 5;
                        }
                    }

                    if (wallRects.X == 744)
                    {
                        if (mPositionX >= 700)
                        {
                            this.mSpeedX *= -1;
                            mPositionX -= 5;
                        }
                    }
                }
            }

            foreach (Rectangle platform in mPlatforms)
            {
                if (BlueThingyRect.Intersects(platform))
                {
                    if (BlueThingyRect.Top >= platform.Bottom + 5 ||
                        BlueThingyRect.Top <= platform.Bottom + 5)
                    {
                        mPositionY += 5;
                        this.mSpeedY *= -1;
                    }
                    if (BlueThingyRect.Right >= platform.Left + 3 ||
                        BlueThingyRect.Right <= platform.Left + 3)
                    {
                        mPositionX -= 5;
                        this.mSpeedX *= -1;
                    }

                    if (BlueThingyRect.Left >= platform.Right + 3 ||
                        BlueThingyRect.Left <= platform.Right + 3)
                    {
                        this.mSpeedX *= -1;
                    }

                }
            }
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(mSprite, new Vector2(mPositionX, mPositionY),
        //               new Rectangle(mCurrentFrameX * mFrameSize.X,
        //                   mCurrentFrameY * mFrameSize.Y,
        //                   mFrameSize.X, mFrameSize.Y), Color.White);
        //}
    }
}
