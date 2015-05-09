using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public class GreenMeanie : Enemy
    {
        public GreenMeanie(float xPos, float yPos, float speedX, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, sprite, walls)
        {
            mPositionX = xPos;
            mPositionY = yPos;
            mSpeedX = speedX;
            mSprite = sprite;
            mWalls = walls;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 0;
            mCurrentFrameY = 4;
            mSheetSize = 4;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;
            facingLeft = false;
        }

        public Rectangle GreenMeanieRect
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

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                ++mCurrentFrameX;
                if (mCurrentFrameX >= mSheetSize)
                {
                    mCurrentFrameX = 0;
                }
            }

            if (mPositionX > 750)
            {
                mSpeedX *= -1;
                facingLeft = true;
            }

            foreach (Rectangle wallRects in mWalls)
            {
                if (GreenMeanieRect.Intersects(wallRects))
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
