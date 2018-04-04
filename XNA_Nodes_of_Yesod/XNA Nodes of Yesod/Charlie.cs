using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA_Nodes_of_Yesod
{
    public class Charlie
    {
        private int spriteWidth = 64;
        private int spriteHeight = 69;
        private int summerSaultFrame;
        private int mXPosition;
        private int mYPosition;
        private int mLives;
        private int mOrigin;
        private int mBelowScreenCounter;
        private int[,] mtoTheUndergound;
        private double animTimer = 0;
        private const double elapsedSecs = 0.1f;
        private bool mFacingLeft;
        private bool mFalling;
        private bool mJump;
        private bool mSummerSaultJump;
        private bool mWalking;
        private bool mTrip;
        private Texture2D mSprites;
        private Texture2D mCollisionRectangle;
        private List<Rectangle> mWalls;
        private List<Rectangle> mFloor;
        private List<Rectangle> mPlatforms;
        private List<Rectangle> mEdibleWalls;
        private List<Rectangle> mAlchiems;
        private List<Rectangle> mRoof;
        private SpriteEffects flip = SpriteEffects.None;
        private Rectangle summerSaultRect;
        private Rectangle mColTileRect0;
        private Rectangle mColTileRect1;

        public Charlie(int x, int y, int lives, Texture2D gamesprites, Texture2D collisionrect,
            List<Rectangle> walls, List<Rectangle> rects, List<Rectangle> plats,
            List<Rectangle> edible, List<Rectangle> alchiem, List<Rectangle> roof,
            int[,] toTheUndergound)
        {
            summerSaultFrame = 0;
            mXPosition = x;
            mYPosition = y;
            mLives = lives;
            mSprites = gamesprites;

            mCollisionRectangle = collisionrect;

            mWalls = walls;
            mFloor = rects;
            mPlatforms = plats;
            mEdibleWalls = edible;
            mAlchiems = alchiem;
            mRoof = roof;
            mtoTheUndergound = toTheUndergound;
            mFacingLeft = false;
            mJump = false;
            mSummerSaultJump = false;
            mWalking = false;
            mOrigin = charlieRect.Height / 2;
        }

        public Rectangle charlieRect
        {
            get
            {
                return new Rectangle((int)mXPosition + 20, (int)mYPosition, 30, 69);
            }
        }

        public Rectangle colTileRect0
        {
            get { return mColTileRect0; }
            set { mColTileRect0 = value; }
        }

        public Rectangle colTileRect1
        {
            get { return mColTileRect1; }
            set { mColTileRect1 = value; }
        }

        public bool summerSaultJump
        {
            get { return mSummerSaultJump; }
            set { mSummerSaultJump = value; }
        }

        public bool Jump
        {
            get { return mJump; }
            set { mJump = value; }
        }

        public bool Falling
        {
            get { return mFalling; }
            set { mFalling = value; }
        }

        public bool FacingLeft
        {
            get { return mFacingLeft; }
            set { mFacingLeft = value; }
        }

        public bool WalkingOnFloor
        {
            get { return mWalking; }
            set { mWalking = value; }
        }

        public int XPosition
        {
            get { return mXPosition; }
            set { mXPosition = value; }
        }

        public int YPosition
        {
            get { return mYPosition; }
            set { mYPosition = value; }
        }
        public int Lives
        {
            get { return mLives; }
            set { mLives = value; }
        }



        public void Update(int tempY, bool jumpright, bool belowMoon, bool trip, int belowScreenCounter)
        {
            mTrip = trip;
            mBelowScreenCounter = belowScreenCounter;
            if (mSummerSaultJump)
            {
                if (jumpright)
                {
                    XPosition += 2;
                }
                else
                {
                    XPosition -= 2;
                }
                if (summerSaultFrame < 8 && mTrip)
                {
                    YPosition -= 10;
                }
                else if (summerSaultFrame >= 8 && summerSaultFrame < 16 && mTrip)
                {
                    YPosition += 10;
                }


                animTimer += elapsedSecs;
                if (animTimer > 0.2)
                {
                    animTimer = 0;
                    summerSaultFrame++;
                }

                summerSaultRect = new Rectangle((int)summerSaultFrame * spriteWidth, (int)1 * spriteHeight, spriteWidth, spriteHeight);

                if (summerSaultFrame >= 16)
                {
                    summerSaultFrame = 0;
                    mSummerSaultJump = false;
                }
            }

            collisions(belowMoon);

            if (FacingLeft == true)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else
            {
                flip = SpriteEffects.None;
            }

            if (mJump)
            {
                YPosition -= 5;
                if ((tempY - YPosition) >= 70)
                {
                    mJump = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle gameSpritesRect)
        {
            //Debugging Image
            //spriteBatch.Draw(mCollisionRectangle, new Vector2(mXPosition + 20, mYPosition), Color.White);

            if (!mSummerSaultJump)
            {
                spriteBatch.Draw(mSprites, new Vector2(XPosition, YPosition), gameSpritesRect, Color.White, 0, Vector2.Zero, 1.0f, flip, 0);
            }
            else
            {
                spriteBatch.Draw(mSprites, new Vector2(XPosition, YPosition), summerSaultRect, Color.White, 0, Vector2.Zero, 1.0f, flip, 0);
            }
        }

        private void collisions(bool belowMoon)
        {
            if (!belowMoon)
            {
                if (charlieRect.Intersects(mColTileRect0) || charlieRect.Intersects(colTileRect1))
                {
                    Yesod.belowScreenCounter = Yesod.screenCounter;
                    Yesod.belowMoon = true;
                    Falling = true;
                    YPosition = 10;
                }
            }

            if (belowMoon)
            {
                if (Falling && !summerSaultJump)
                {
                    foreach (Rectangle platforms in mPlatforms)
                    {
                        if (charlieRect.Intersects(platforms) == true)
                        {
                            if (charlieRect.Bottom == platforms.Top ||
                                charlieRect.Bottom == platforms.Top + 1 ||
                                charlieRect.Bottom == platforms.Top + 2 ||
                                charlieRect.Bottom == platforms.Top + 3)
                            {
                                Falling = false;
                                break;
                            }
                        }
                    }

                    foreach (Rectangle floor in mFloor)
                    {
                        if (charlieRect.Intersects(floor) == true)
                        {
                            mFalling = false;
                            mWalking = true;
                            mYPosition = 366;
                            break;
                        }
                    }
                }

                // Check to see if Charlie hits a ledge when summersault jumping
                if (summerSaultJump && mTrip && mPlatforms.Count > 0)
                {
                    foreach (Rectangle platforms in mPlatforms)
                    {
                        if (charlieRect.Intersects(platforms) == true)
                        {
                            int charBottom = platforms.Top - charlieRect.Height;
                            if (charlieRect.Top <= platforms.Top)
                            {
                                YPosition = charBottom - 5;
                            }

                            if (charlieRect.Top >= platforms.Bottom - 15 ||
                                charlieRect.Top >= platforms.Bottom - 5)
                            {
                                YPosition += 1;
                                Jump = false;
                                Falling = true;// false;
                                mTrip = false;
                                Yesod.trip = false;
                            }
                            //if (charlieRect.Right >= platforms.Left && charlieRect.Left <= platforms.Right)
                            //{
                            //if (mFacingLeft)
                            //{
                            //XPosition += 5;
                            //}
                            //else
                            //{
                            //XPosition -= 5;
                            //}
                            //}
                        }
                    }
                }

                // Check to see if Charlie is jumping and hits a ledge from 
                // underneath
                if (mJump)
                {
                    if (mPlatforms.Count > 0)
                    {
                        foreach (Rectangle platforms in mPlatforms)
                        {
                            if (charlieRect.Intersects(platforms) == true)
                            {
                                if (charlieRect.Top >= platforms.Bottom - 16 &&
                                    charlieRect.Top <= platforms.Bottom - 10)
                                //if (charlieRect.Top <= platforms.Bottom - 16 )
                                {
                                    mYPosition += 2;
                                    mJump = false;
                                    break;
                                }
                            }
                        }
                    }
                }


                // Check to see if Charlie has walked over a gap in the floor
                if (!Falling && !mWalking && !summerSaultJump)
                {
                    int platformCount = mPlatforms.Count;
                    int checkCounter = 0;
                    foreach (Rectangle platforms in mPlatforms)
                    {
                        if (charlieRect.Intersects(platforms) == false)
                        {
                            checkCounter++;
                        }
                    }
                    if (checkCounter == platformCount)
                    {
                        Falling = true;
                        checkCounter = 0;
                    }
                }

                if (mWalking)
                {
                    int floorCount = mFloor.Count;
                    int checkCounter = 0;
                    foreach (Rectangle floor in mFloor)
                    {
                        if (charlieRect.Intersects(floor) == false)
                        {
                            checkCounter++;
                        }
                    }
                    if (checkCounter == floorCount)
                    {
                        Falling = true;
                        checkCounter = 0;
                    }
                }

                // Check to see if Charlie walks into the walls
                if (mWalls.Count > 0)
                {
                    foreach (Rectangle wallRects in mWalls)
                    {
                        if (charlieRect.Intersects(wallRects))
                        {
                            if (mFacingLeft)
                            {
                                XPosition = wallRects.Right - 19;
                                break;
                            }
                            else
                            {
                                XPosition = 680;
                                break;
                            }
                        }
                    }
                }

                if (mEdibleWalls.Count > 0)
                {
                    foreach (Rectangle edible in mEdibleWalls)
                    {
                        if (edible.IsEmpty && XPosition < 50)
                        {
                            if (Yesod.screenCounter > 0)
                            {
                                Yesod.screenCounter -= 1;
                                Yesod.belowScreenCounter -= 1;
                                XPosition = 740;

                            }
                            else
                            {
                                Yesod.screenCounter = 15;
                                Yesod.belowScreenCounter = 15;
                                XPosition = 740;
                            }
                        }
                        else if (charlieRect.Intersects(edible))
                        {
                            if (mFacingLeft)
                            {
                                XPosition = edible.Right - 19;
                                break;
                            }
                            else
                            {
                                XPosition = 680;
                                break;
                            }
                        }
                    }
                }

                if (mRoof.Count > 0)
                {
                    foreach (Rectangle roof in mRoof)
                    {
                        if (charlieRect.Intersects(roof))
                        {
                            YPosition += 5;
                            Jump = false;
                        }
                    }
                }


                if (mAlchiems.Count > 0)
                {
                    foreach (Rectangle alchiem in mAlchiems)
                    {
                        if (charlieRect.Intersects(alchiem))
                        {
                            int span = mBelowScreenCounter * 10;
                            for (int i = span; i < span + 13; i++)
                            {
                                for (int j = 1; j < 10; j++)
                                {

                                    if (mtoTheUndergound[i, j] == 22)
                                    {
                                        // replace the alchiems with space
                                        mtoTheUndergound[i, j] = 4;
                                        Yesod.alchiem += 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
