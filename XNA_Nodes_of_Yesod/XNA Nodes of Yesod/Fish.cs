using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public class Fish : Enemy
    {

        public Fish(float xPos, float yPos, float speedX, float speedY, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, speedY, sprite, walls)
        {
            mPositionX = xPos;
            mPositionY = yPos;
            mSpeedX = speedX;
            mSpeedY = speedY;
            mSprite = sprite;
            mWalls = walls;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 7;
            mCurrentFrameY = 5;
            mSheetSize = 6;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;
        }

        public Rectangle FishRect
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

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                ++mCurrentFrameX;
                if (mCurrentFrameX == 9 && mCurrentFrameY == 5)
                {
                    mCurrentFrameX = 8;
                    mCurrentFrameY = 4;
                }
                else if (mCurrentFrameX >= 8 + mSheetSize)
                {
                    mCurrentFrameX = 8;
                }
            }

            if (mPositionY > 400 ||
                    mPositionY < 0)
            {
                mSpeedY *= -1;
            }

            foreach (Rectangle wallRects in mWalls)
            {
                if (FishRect.Intersects(wallRects))
                {

                    if (this.mSpeedX > 0)
                    {
                        mPositionX -= 5;
                        this.mSpeedX *= -1;
                        facingLeft = true;
                    }
                    else
                    {
                        mPositionX += 5;
                        this.mSpeedX *= -1;
                        facingLeft = false;
                    }
                }
            }

            if (facingLeft)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else
            {
                flip = SpriteEffects.None;
            }
        }
    }
}
