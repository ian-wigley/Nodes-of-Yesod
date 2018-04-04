using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA_Nodes_of_Yesod
{
    class Earth
    {
        private int mEarthX = 600;
        private int mEarthY = 20;
        private int mEarthFrame = 0;
        private int mSpriteWidth = 64;
        private int mSpriteHeight = 69;

        private Texture2D mSprites;

        protected double animTimer = 0;
        protected const double elapsedSecs = 0.1f;

        public Earth(Texture2D sprite)
        {
            mSprites = sprite;
        }

        public Rectangle earthRect
        {
            get
            {
                return new Rectangle((int)mEarthFrame * mSpriteWidth, (int)10 * mSpriteHeight, mSpriteWidth, mSpriteHeight);
            }
        }

        public void Update(GameTime gameTime)
        {
            animTimer += elapsedSecs;
            if (animTimer > 1.2)
            {
                animTimer = 0;
                mEarthFrame++;
            }
            if (mEarthFrame >= 15)
            {
                mEarthFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSprites, new Vector2(mEarthX, mEarthY), earthRect, Color.White);
        }
    }
}
