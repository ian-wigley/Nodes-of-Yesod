using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    class WhirlWind : Enemy
    {
        private int mWhirlWindX = 400;
        private int mWhirlWindY = 0;
        private int mWhirlWindFrame = 17;
        private int mSpriteWidth = 64;
        private int mSpriteHeight = 69;

        public WhirlWind(float xPos, float yPos, float speedX, float speedY, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, speedY, sprite, walls)
        {
            this.mSprite = sprite;
            this.mWhirlWindX = 2;
            this.mWhirlWindY = 9;
            this.SheetSize = 3;
            timeSinceLastFrame = 0;
        }

        public Rectangle WhirlWindRect
        {
            get
            {
                return new Rectangle((int)mWhirlWindFrame * mSpriteWidth, (int)9 * mSpriteHeight, mSpriteWidth, mSpriteHeight);
            }
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > 0.6)
            {
                timeSinceLastFrame = 0;
                mWhirlWindFrame++;
            }
            if (mWhirlWindFrame >= 6)
            {
                mWhirlWindFrame = 0;
            }
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSprite, new Vector2(mWhirlWindX, mWhirlWindY), WhirlWindRect, Color.White);
        }
    }
}
