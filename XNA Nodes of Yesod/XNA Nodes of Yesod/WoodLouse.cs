using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public class WoodLouse : Enemy
    {
        //private float mSpeed;
        //private bool facingLeft = false;
        //private SpriteEffects flip = SpriteEffects.None;

        public WoodLouse(float xPos, float yPos, float speedX, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, sprite, walls)
        {
            mPositionX = xPos;
            mPositionY = yPos;
            mSpeed = speedX;
            mSprite = sprite;
            mWalls = walls;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 0;
            mCurrentFrameY = 5;
            mSheetSize = 4;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;
        }

        public Rectangle WoodLouseRect
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
                if (mCurrentFrameX >= mCurrentFrameX + mSheetSize)
                {
                    mCurrentFrameX = 0;
                }
            }

            foreach (Rectangle wallRects in mWalls)
            {
                if (WoodLouseRect.Intersects(wallRects))
                {
                    if (this.mSpeed > 0)
                    {
                        mPositionX -= 5;
                        this.mSpeed *= -1;
                        facingLeft = true;
                    }
                    else
                    {
                        mPositionX += 5;
                        this.mSpeed *= -1;
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
