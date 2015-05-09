using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public abstract class Enemy
    {
        protected float mSpeedX;
        protected float mSpeedY;
        protected Point mFrameSize;
        protected int mCurrentFrameX;
        protected int mCurrentFrameY;
        protected int mSheetSize;
        protected List<Rectangle> mWalls;
        protected List<Rectangle> mPlatforms;
        protected Texture2D mSprite;
        protected int timeSinceLastFrame;
        protected int millisecondsPerFrame;
        protected float mPositionX;
        protected float mPositionY;
        protected float mSpeed;
        protected bool facingLeft = false;
        protected SpriteEffects flip = SpriteEffects.None;

        public Enemy()
        {
        }

        public Enemy(float posX, float posY, float speedX, float speedY, Texture2D sprite,
            List<Rectangle> walls, List<Rectangle> platforms)
        {
            mPositionX = posX;
            mPositionY = posY;
            mSpeedX = speedX;
            mSpeedY = speedY;
            mSprite = sprite;
            mWalls = walls;
            mPlatforms = platforms;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 7;
            mCurrentFrameY = 5;
            mSheetSize = 4;
        }

        public Enemy(float posX, float posY, float speedX, float speedY, Texture2D sprite,
            List<Rectangle> walls)
        {
            mPositionX = posX;
            mPositionY = posY;
            mSpeedX = speedX;
            mSpeedY = speedY;
            mSprite = sprite;
            mWalls = walls;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 7;
            mCurrentFrameY = 5;
            mSheetSize = 4;
        }

        public Enemy(float posX, float posY, float speed, Texture2D sprite, List<Rectangle> walls)
        {
            mPositionX = posX;
            mPositionY = posY;
            mSpeedX = speed;
            mSprite = sprite;
            mWalls = walls;
            mFrameSize = new Point(64, 69);
            mCurrentFrameX = 4;
            mCurrentFrameY = 4;
            mSheetSize = 4;
        }

        public float PositionX
        {
            get { return mPositionX; }
            set { mPositionX = value; }
        }

        public float PositionY
        {
            get { return mPositionY; }
            set { mPositionY = value; }
        }

        public float SpeedX
        {
            get { return mSpeedX; }
            set { mSpeedX = value; }
        }

        public float SpeedY
        {
            get { return mSpeedY; }
            set { mSpeedY = value; }
        }

        public Texture2D Sprite
        {
            get { return mSprite; }
            set { mSprite = value; }
        }

        public Point FrameSize
        {
            get { return mFrameSize; }
        }

        public int CurrentFrameX
        {
            get { return mCurrentFrameX; }
            set { mCurrentFrameX = value; }
        }

        public int CurrentFrameY
        {
            get { return mCurrentFrameY; }
            set { mCurrentFrameY = value; }
        }

        public int SheetSize
        {
            get { return mSheetSize; }
            set { mSheetSize = value; }
        }

        public void Collision()
        {
            mSpeedX *= -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSprite, new Vector2(mPositionX, mPositionY),
                       new Rectangle(mCurrentFrameX * mFrameSize.X,
                           mCurrentFrameY * mFrameSize.Y,
                           mFrameSize.X, mFrameSize.Y), Color.White, 0, Vector2.Zero, 1.0f, flip, 0);
        }
    }
}
