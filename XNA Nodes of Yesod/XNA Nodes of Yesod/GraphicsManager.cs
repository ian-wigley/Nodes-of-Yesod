using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace XNA_Nodes_of_Yesod
{
    public class GraphicsManager
    {
        private Alf alf;
        private Bird bird;
        private BlueThingy blueThingy;
        private Caterpillar caterpillar;
        private ChasingEnemy cloud;
        private SpringBear springBear;
        private Fire fire;
        private Fish fish;
        private GreenMeanie greenMeanie;
        private Plant plant;
        private RedSpaceman redSpaceMan;
        private WhirlWind whirlWind;
        private WoodLouse woodLouse;

        private List<Enemy> mEnemies;
        private List<Rectangle> mWalls;
        private List<Rectangle> mPlatforms;
        private Random rand = new Random();
        private Texture2D mSprites;

        private int[,] mUpperRockArray = {{0,1,1,1,1,2,2,2,3,3,3},    //0     
                                        {2,1,3,0,1,3,1,0,2,2,1},    //1
                                        {1,2,3,0,1,3,3,0,3,2,1},    //2
                                        {3,1,2,0,3,2,0,3,2,0,1},    //3
                                        {1,2,3,0,0,3,2,3,2,1,0},    //4
                                        {2,1,3,0,1,3,1,0,2,2,1},    //5
                                        {1,2,3,0,1,3,3,0,3,2,1},    //6
                                        {0,2,3,3,0,1,3,3,2,1,1},    //7
                                        {2,1,3,0,1,3,1,0,2,2,1},    //8
                                        {1,2,3,0,1,3,3,0,3,2,1},    //9
                                        {3,1,2,0,3,2,0,3,2,0,1},    //10
                                        {1,2,3,0,0,3,2,3,2,1,0},    //11
                                        {2,1,3,0,1,3,1,0,2,2,1},    //12
                                        {1,2,3,0,1,3,3,0,3,2,1},    //13
                                        {0,2,3,3,0,1,3,3,2,1,1},    //14
                                        {1,2,3,1,2,3,1,3,2,1,1}};   //15

        private int[,] mMoundArray = { { 1 }, { 1 }, { 1 }, { 2 }, { 1 }, { 2 }, { 2 }, { 2 }, { 1 }, { 1 }, { 2 }, { 1 }, { 2 }, { 2 }, { 2 }, { 2 } };
        private int[,] mHoleArray0 = { { 0 }, { 2 }, { 2 }, { 0 }, { 2 }, { 2 }, { 2 }, { 2 }, { 2 }, { 2 }, { 2 }, { 2 }, { 2 }, { 0 }, { 2 }, { 2 } };


        static int[,] mHoleArray1 = {{0}, //0
                                    {2}, //1
                                    {2}, //2
                                    {2}, //3
                                    {0}, //4
                                    {2}, //5
                                    {2}, //6
                                    {2}, //7
                                    {0}, //8
                                    {2}, //9
                                    {0}, //10
                                    {2}, //11
                                    {2}, //12
                                    {2}, //13
                                    {2}, //14
                                    {2}}; //15


        static int[,] mLowerRockArray = {{0,1,2,3,2,1,3,0,2,1,0},    //0     
                                        {2,1,3,0,1,3,1,0,2,2,1},    //1
                                        {1,2,3,0,1,3,3,0,3,2,1},    //2
                                        {3,1,2,0,3,2,0,3,2,0,1},    //3
                                        {1,2,3,0,0,3,2,3,2,1,0},    //4
                                        {2,1,3,0,1,3,1,0,2,2,1},    //5
                                        {1,2,3,0,1,3,3,0,3,2,1},    //6
                                        {0,2,3,3,0,1,3,3,2,1,1},    //7
                                        {2,1,3,0,1,3,1,0,2,2,1},    //8
                                        {1,2,3,0,1,3,3,0,3,2,1},    //9
                                        {3,1,2,0,3,2,0,3,2,0,1},    //10
                                        {1,2,3,0,0,3,2,3,2,1,0},    //11
                                        {2,1,3,0,1,3,1,0,2,2,1},    //12
                                        {1,2,3,0,1,3,3,0,3,2,1},    //13
                                        {0,2,3,3,0,1,3,3,2,1,1},    //14
                                        {1,2,3,1,2,3,1,3,2,1,1}};   //15


        private int[,] mToTheUnderGround = new int[3181, 13];
        private int[,] mVec2s = new int[1, 20];
        private int[,] mToTheEnemes = new int[256, 3];

        public int[,] UpperRockArray
        {
            get { return mUpperRockArray; }
        }

        public int[,] MoundArray
        {
            get { return mMoundArray; }
        }

        public int[,] HoleArray0
        {
            get { return mHoleArray0; }
        }

        public int[,] HoleArray1
        {
            get { return mHoleArray1; }
        }

        public int[,] LowerRockArray
        {
            get { return mLowerRockArray; }
        }

        public int[,] ToTheUnderGround
        {
            get { return mToTheUnderGround; }
            set { mToTheUnderGround = value; }
        }

        public int[,] Vec2s
        {
            get { return mVec2s; }
            set { mVec2s = value; }
        }

        public int[,] ToTheEnemes
        {
            get { return mToTheEnemes; }
            set { mToTheEnemes = value; }
        }

        public GraphicsManager(Texture2D gamesprites, List<Enemy> enemies, List<Rectangle> walls, List<Rectangle> plats)
        {
            mSprites = gamesprites;
            mEnemies = enemies;
            mWalls = walls;
            mPlatforms = plats;
        }

        public void LoadLevels()
        {

            const int MaxWordCount = 80000;
            int wordListCount = 0;
            string[] wordList = new string[MaxWordCount];

            int enemyCount = 0;
            int count = 0;
            StreamReader wordFile;
            string wordLine;

            try
            {
                wordFile = new StreamReader("levels.txt");
                if (wordFile != null)
                {
                    wordLine = wordFile.ReadLine();
                    while (wordLine != null && wordListCount < MaxWordCount)
                    {
                        int total = wordList.Count();
                        wordList[wordListCount] = wordLine;
                        wordListCount++;
                        string[] mFileContents = wordLine.Split(new Char[] { ',' });

                        for (int a = 0; a < 13; a++)
                        {
                            int converted = int.Parse(mFileContents[a]);

                            ToTheUnderGround[count, a] = converted;

                            if (enemyCount > 2)
                            {
                                enemyCount = 0;
                            }
                        }
                        wordLine = wordFile.ReadLine();
                        count++;
                    }
                    wordFile.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The following error occured while attempting to read the file: " + e.Message);
                wordListCount = 0;
            }
            int test = mToTheUnderGround.Length;
        }

        public void configureEnemies(int belowScreenCounter)
        {
            int index;
            int floatingEnemies;
            for (int i = 0; i < 3; i++)
            {
                index = ToTheEnemes[belowScreenCounter, i];
                switch (index)
                {
                    case 1:  //a
                        mEnemies.Add(alf = (new Alf(300.0f, 366.0f, 1.0f, mSprites, mWalls)));
                        break;
                    case 2:  //b
                        mEnemies.Add(bird = new Bird(300.0f, 366.0f, 1.0f, mSprites, mWalls));
                        break;
                    case 3:  //c
                        mEnemies.Add(caterpillar = new Caterpillar(300.0f, 366.0f, 1.0f, mSprites, mWalls));
                        break;
                    case 4:  //d
                        mEnemies.Add(greenMeanie = (new GreenMeanie((float)rand.Next(100, 400), 366.0f, 1.0f, mSprites, mWalls)));
                        break;
                    case 5:  //e
                        mEnemies.Add(fire = (new Fire(300.0f, 372.0f, 1.0f, mSprites, mWalls)));
                        break;
                    case 6:  //f
                        mEnemies.Add(fish = new Fish(300.0f, 300.0f, 1.0f, 1.0f, mSprites, mWalls));
                        break;
                    case 7:  //g
                        mEnemies.Add(plant = new Plant(300.0f, 300.0f, 1.0f, 1.0f, mSprites, mWalls));
                        break;
                    case 8:  //h
                        mEnemies.Add(whirlWind = new WhirlWind((float)rand.Next(100, 400), (float)rand.Next(100, 400), 1.0f, 1.0f, mSprites, mWalls));
                        break;
                    case 9:  //i
                        mEnemies.Add(woodLouse = new WoodLouse(300.0f, 300.0f, 1.0f, mSprites, mWalls));
                        break;
                    case 10:
                        mEnemies.Add(redSpaceMan = (new RedSpaceman((float)rand.Next(100, 400), (float)rand.Next(100, 400), 1.0f, 1.0f, mSprites, mWalls)));
                        break;
                    case 11:
                        break;
                    case 12:
                        break;
                    case 13:
                        break;
                }
            }
            for (int j = 0; j < 3; j++)
            {
                floatingEnemies = rand.Next(1, 6);
                switch (floatingEnemies)
                {
                    case 1:
                        mEnemies.Add(springBear = (new SpringBear((float)rand.Next(100, 400), (float)rand.Next(100, 400), 1.0f, 1.0f, mSprites, mWalls, mPlatforms)));
                        break;
                    case 2:
                        mEnemies.Add(blueThingy = (new BlueThingy((float)rand.Next(100, 400), (float)rand.Next(100, 400), 1.0f, 1.0f, mSprites, mWalls, mPlatforms)));
                        break;
                    case 3:
                        mEnemies.Add(cloud = new ChasingEnemy(300.0f, 300.0f, 1.0f, 1.0f, mSprites, mWalls));
                        break;
                    case 4:
                        mEnemies.Add(springBear = (new SpringBear((float)rand.Next(100, 400), (float)rand.Next(100, 400), 1.0f, 1.0f, mSprites, mWalls, mPlatforms)));
                        break;
                    case 5:
                        mEnemies.Add(blueThingy = (new BlueThingy((float)rand.Next(100, 400), (float)rand.Next(100, 400), 1.0f, 1.0f, mSprites, mWalls, mPlatforms)));
                        break;
                    case 6:
                        mEnemies.Add(cloud = new ChasingEnemy(300.0f, 300.0f, 1.0f, 1.0f, mSprites, mWalls));
                        break;
                }
            }
        }
    }
}
