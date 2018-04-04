using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA_Nodes_of_Yesod
{
    public class Plant : Enemy
    {
        private int mPlantX = 400;
        private int mPlantY = 0;
        private int mPlantFrame = 17;
        private int mSpriteWidth = 64;
        private int mSpriteHeight = 69;

        public Plant(float xPos, float yPos, float speedX, float speedY, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, speedY, sprite, walls)
        {
            this.mSprite = sprite;
            this.mPlantX = 2;
            this.mPlantY = 9;
            this.SheetSize = 3;
            timeSinceLastFrame = 0;
        }

        public Rectangle PlantRect
        {
            get
            {
                return new Rectangle((int)mPlantFrame * mSpriteWidth, (int)9 * mSpriteHeight, mSpriteWidth, mSpriteHeight);
            }
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > 0.6)
            {
                timeSinceLastFrame = 0;
                mPlantFrame++;
            }
            if (mPlantFrame >= 6)
            {
                mPlantFrame = 0;
            }
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSprite, new Vector2(mPlantX, mPlantY), PlantRect, Color.White);
        }
    }
}
