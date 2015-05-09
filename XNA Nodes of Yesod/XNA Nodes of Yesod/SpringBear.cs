using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public class SpringBear : Enemy
    {
        public SpringBear(float posX, float posY, float speedX, float speedY, Texture2D sprite,
            List<Rectangle> walls, List<Rectangle> platforms)
            : base(posX, posY, speedX, speedY, sprite, walls)
        {
            mPositionX = posX;
            mPositionY = posY;
            mSpeedX = speedX;
            mSpeedY = speedY;
            mSprite = sprite;
            mWalls = walls;
            mPlatforms = platforms;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 4;
            mCurrentFrameY = 4;
            mSheetSize = 4;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;
        }

        public Rectangle enemyRect
        {
            get
            {
                return new Rectangle((int)mPositionX, (int)mPositionY, 64, 69);
            }
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            mPositionX += mSpeedX;
            mPositionY += mSpeedY;

            if (mPositionY > 366 ||
                    mPositionY < 0)
            {
                mSpeedY *= -1;
            }

            if (mPositionX > 750 || mPositionX < 0)
            {
                mSpeedX *= -1;
            }

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                ++mCurrentFrameX;
                if (mCurrentFrameX == 9 && mCurrentFrameY == 5)
                {
                    mCurrentFrameX = 6;
                    mCurrentFrameY = 4;
                }
                else if (mCurrentFrameX >= mSheetSize + 4)
                {
                    mCurrentFrameX = 4;
                }
            }

            // Wall Collision check
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

            // Platform Collision check
            foreach (Rectangle platform in mPlatforms)
            {
                if (enemyRect.Intersects(platform))
                {
                    if (this.mSpeedX > 0)
                    {
                        mPositionX -= 5;
                        this.mSpeedX *= -1;
                    }
                    else
                    {
                        mPositionX += 5;
                        this.mSpeedX *= -1;
                    }
                }
            }
        }
    }
}
