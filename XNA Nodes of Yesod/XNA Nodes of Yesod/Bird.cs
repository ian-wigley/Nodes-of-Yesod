using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA_Nodes_of_Yesod
{
    public class Bird : Enemy
    {

        public Bird(float xPos, float yPos, float speedX, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, sprite, walls)
        {
            mPositionX = xPos;
            mPositionY = yPos;
            mSpeed = speedX;
            mSprite = sprite;
            mWalls = walls;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 14;
            mCurrentFrameY = 4;
            mSheetSize = 4;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;
        }

        public Rectangle BirdRect
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
                if (mCurrentFrameX >= 14 + mSheetSize)
                {
                    mCurrentFrameX = 14;
                }
            }

            foreach (Rectangle wallRects in mWalls)
            {
                if (BirdRect.Intersects(wallRects))
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

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(mSprite, new Vector2(mPositionX, mPositionY),
        //               new Rectangle(mCurrentFrameX * mFrameSize.X,
        //                   mCurrentFrameY * mFrameSize.Y,
        //                   mFrameSize.X, mFrameSize.Y), Color.White, 0, Vector2.Zero, 1.0f, flip, 0);
        //}
    }
}