using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public class Alf : Enemy
    {
        private bool turning = false;

        public Alf(float xPos, float yPos, float speedX, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, sprite, walls)
        {
            mPositionX = xPos;
            mPositionY = yPos;
            mSpeed = speedX;
            mSprite = sprite;
            mWalls = walls;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 0;
            mCurrentFrameY = 3;
            mSheetSize = 8;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;
        }

        public Rectangle AlfRect
        {
            get
            {
                return new Rectangle((int)mPositionX, (int)mPositionY, 64, 69);
            }
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            mPositionX += mSpeed;

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                ++mCurrentFrameX;
                if (mCurrentFrameX >= mSheetSize && !turning)
                {
                    mCurrentFrameX = 0;
                }
                else if (turning)
                {
                    if (mCurrentFrameX >= mSheetSize + 3)
                    {
                        mCurrentFrameX = 0;
                    }
                }
            }

            foreach (Rectangle wallRects in mWalls)
            {
                if (AlfRect.Intersects(wallRects))
                {
                    if (this.mSpeed > 0)
                    {
                        Turning();
                        turning = false;
                        mPositionX -= 5;
                        this.mSpeed = -1;
                        facingLeft = true;
                    }
                    else
                    {
                        Turning();
                        turning = false;
                        mPositionX += 5;
                        this.mSpeed = 1;
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

        private void Turning()
        {
            turning = true;
            mSpeed = 0;
            mCurrentFrameX = 9;

            while (mCurrentFrameX < mSheetSize + 3)
            {
                mCurrentFrameX++;
            }
        }
    }
}