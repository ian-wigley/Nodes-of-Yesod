using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public class Mole
    {
        private float mMoleX = 200;
        private float mMoleY = 100;
        private int mMoleFrame = 0;
        private int mSpriteWidth = 64;
        private int mSpriteHeight = 69;
        private int[,] mtoTheUndergound;
        private int mBelowScreenCounter;
        private Texture2D mSprites;
        private List<Rectangle> mWalls;
        private List<Rectangle> mEdibleWalls;

        protected double animTimer = 0;
        protected const double elapsedSecs = 0.1f;

        private bool facingLeft = false;
        private SpriteEffects flip = SpriteEffects.None;

        public Mole(Texture2D sprite, List<Rectangle> walls, List<Rectangle> edibleWalls, int[,] toTheUndergound)
        {
            mSprites = sprite;
            mWalls = walls;
            mEdibleWalls = edibleWalls;
            mtoTheUndergound = toTheUndergound;
        }

        public Rectangle MoleCollisionRect
        {
            get
            {
                return new Rectangle((int)mMoleX, (int)mMoleY, mSpriteWidth, mSpriteHeight);
            }
        }

        public bool Direction
        {
            get { return facingLeft; }
            set { facingLeft = value; }
        }

        public float MolePosX
        {
            get { return mMoleX; }
            set { mMoleX = value; }
        }

        public float MolePosY
        {
            get { return mMoleY; }
            set { mMoleY = value; }
        }

        public void Update(GameTime gameTime, int belowScreenCounter)
        {
            mBelowScreenCounter = belowScreenCounter;
            animTimer += elapsedSecs;
            if (animTimer > 1.2)
            {
                animTimer = 0;
                mMoleFrame++;
            }
            if (mMoleFrame >= 8)
            {
                mMoleFrame = 0;
            }

            if (mMoleX < 0)
            {
                mMoleX += 2;
            }

            if (mMoleX > 750)
            {
                mMoleX -= 2;
            }

            if (mMoleY < 0)
            {
                mMoleY += 2;
            }

            if (mMoleY > 400)
            {
                mMoleY -= 2;
            }

            if (facingLeft == true)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else
            {
                flip = SpriteEffects.None;
            }
            if (mEdibleWalls.Count > 0)
            {
                collisions();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSprites, new Vector2(mMoleX, mMoleY),
                       new Rectangle((int)mMoleFrame * mSpriteWidth,
                       (int)6 * mSpriteHeight,
                       mSpriteWidth, mSpriteHeight), Color.White, 0, Vector2.Zero, 1.0f, flip, 0);
        }

        private void collisions()
        {
            foreach (Rectangle ed in mEdibleWalls)
            {
                if (MoleCollisionRect.Intersects(ed) == true)
                {
                    int span = mBelowScreenCounter * 10;
                    for (int i = span; i < span + 13; i++)
                    {
                        if (MolePosX < 100)
                        {
                            if (mtoTheUndergound[i, 1] == 15 || mtoTheUndergound[i, 1] == 17)
                            {
                                // replace the edible walls with replacement
                                mtoTheUndergound[i, 1] = 4;
                                if (Yesod.screenCounter > 0)
                                {
                                    mtoTheUndergound[(i - 10), 12] = 4;
                                }
                                else
                                {
                                    mtoTheUndergound[(i + 150), 12] = 4;
                                }
                            }
                        }
                        else
                        {
                            if (MolePosX > 650)
                            {
                                if (mtoTheUndergound[i, 12] == 16 || mtoTheUndergound[i, 12] == 18)
                                {
                                    mtoTheUndergound[i, 12] = 4;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
