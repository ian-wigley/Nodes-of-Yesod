using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA_Nodes_of_Yesod
{
    public class Fire : Enemy
    {

        public Fire(float xPos, float yPos, float speedX, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, sprite, walls)
        {
            mPositionX = xPos;
            mPositionY = yPos;
            mSpeed = speedX;
            mSprite = sprite;
            mWalls = walls;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 10;
            mCurrentFrameY = 5;
            mSheetSize = 4;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;
        }

        public Rectangle FireRect
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
                if (mCurrentFrameX >= 10 + mSheetSize)
                {
                    mCurrentFrameX = 10;
                }
            }

            foreach (Rectangle wallRects in mWalls)
            {
                if (FireRect.Intersects(wallRects))
                {
                    if (this.mSpeed > 0)
                    {
                        mPositionX -= 5;
                        this.mSpeed *= -1;
                    }
                    else
                    {
                        mPositionX += 5;
                        this.mSpeed *= -1;
                    }
                }
            }
        }
    }
}
