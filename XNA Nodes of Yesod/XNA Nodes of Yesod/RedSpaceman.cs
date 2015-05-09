using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    class RedSpaceman : Enemy
    {
        private int mRedSpacemanX = 400;
        private int mRedSpacemanY = 0;
        private int mRedSpacemanFrame = 17;
        private int mSpriteWidth = 64;
        private int mSpriteHeight = 69;
        private Texture2D mSprites;


        public RedSpaceman(float xPos, float yPos, float speedX, float speedY, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, speedY, sprite, walls)
        {
            this.mSprites = sprite;
            this.mRedSpacemanX = 2;
            this.mRedSpacemanY = 9;
            this.SheetSize = 3;
            timeSinceLastFrame = 0;
        }

        public Rectangle RedSpacemanRect
        {
            get
            {
                return new Rectangle((int)mRedSpacemanFrame * mSpriteWidth, (int)9 * mSpriteHeight, mSpriteWidth, mSpriteHeight); 
            }
        }


        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > 0.6)
            {
            timeSinceLastFrame = 0;
            mRedSpacemanFrame++;
            }
            if (mRedSpacemanFrame >= 6)
            {
             mRedSpacemanFrame = 0;
            }
        }

       new public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSprites, new Vector2(mRedSpacemanX, mRedSpacemanY), RedSpacemanRect, Color.White);
        }
    }
}
