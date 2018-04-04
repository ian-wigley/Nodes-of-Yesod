//////////////////////////////
//  Nodes of Yesod Remake   //
//                          //
//       XNA version        // 
//  written by Ian Wigley   //
//                          //
//////////////////////////////

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public class Yesod : Microsoft.Xna.Framework.Game
    {

        private List<Enemy> enemies = new List<Enemy>();
        // Floor textures
        private List<Rectangle> rects = new List<Rectangle>();
        // Platforms & Ledges
        private List<Rectangle> platform = new List<Rectangle>();
        // Standard Walls
        private List<Rectangle> walls = new List<Rectangle>();
        // Mole edible
        private List<Rectangle> edibleWalls = new List<Rectangle>();
        // Alcheims
        private List<Rectangle> alchiems = new List<Rectangle>();
        // Roof rocks
        private List<Rectangle> roof = new List<Rectangle>();

        private List<Rectangle> testList = new List<Rectangle>();

        private Random rand = new Random();
        private GraphicsManager graphicsMan;

        private Charlie man;
        private Rocket rocket;
        private Earth earth;
        private Mole mole;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState oldState;

        private Texture2D gameSprites;
        private Texture2D panel;
        private Texture2D MoonRocks;
        private Texture2D unGroundLedges;
        private Texture2D frontScreen;
        private Texture2D collisionTile;
        private Texture2D colRect;
        private SpriteFont hudFont;

        private Rectangle gameSpritesRect;
        private Rectangle mainSpritesRect;
        private Rectangle heartBeatRect;
        private Rectangle moonRocksRect;
        private Rectangle lowMoonRocksRect;
        private Rectangle moundRect;
        private Rectangle holeRect0;
        private Rectangle holeRect1;
        private Rectangle wallRect;
        private Rectangle groundRect;
        private Rectangle collisionRects;

        private Rectangle test;

        private double animTimer = 0;
        private double heartBeatTimer = 0;

        private const double elapsedClockSecs = 1.0f;
        private const double elapsedSecs = 0.1f;

        private int tempY;
        private int platforms;

        private int spriteWidth = 64;
        private int spriteHeight = 69;
        private int currentFrame = 0;
        private int heartBeatFrame = 8;

        private int rockWidth = 100;
        private int rockHeight = 117;
        private int lowerRockWidth = 100;
        private int lowerRockHeight = 100;
        private int unGroTileHeight = 48;
        private int unGroTileWidth = 62;

        private int seconds = 0;
        private int minutes = 0;

        private float hole0X = 300.0f;
        private float hole1X = 500.0f;
        private float holesY = 400.0f;

        private bool moleManAlive = false;
        private bool gameOn = false;
        private bool jumpRight = false;

        public static bool trip = false;
        public static bool belowMoon = false;
        public static int alchiem = 0;
        public static int screenCounter = 0;
        public static int belowScreenCounter = 0;

        public Yesod()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
            oldState = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameSprites = Content.Load<Texture2D>("sprites");
            graphicsMan = new GraphicsManager(gameSprites, enemies, walls, platform);

            //Debugging Image
            colRect = Content.Load<Texture2D>("charlieBlock");

            man = new Charlie(150, 350, 3, gameSprites, colRect, walls, rects, platform, edibleWalls, alchiems, roof, graphicsMan.ToTheUnderGround);
            rocket = new Rocket(gameSprites);
            earth = new Earth(gameSprites);
            mole = new Mole(gameSprites, walls, edibleWalls, graphicsMan.ToTheUnderGround);

            panel = Content.Load<Texture2D>("panel");
            MoonRocks = Content.Load<Texture2D>("aboverground_tiles");
            unGroundLedges = Content.Load<Texture2D>("underground_tiles_small");
            collisionTile = Content.Load<Texture2D>("collision_tile");
            frontScreen = Content.Load<Texture2D>("nodes_front_Screen");
            hudFont = Content.Load<SpriteFont>("Hud");

            graphicsMan.LoadLevels();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            updateInput();

            if (man.XPosition < 50)
            {
                if (screenCounter > 0)
                {
                    screenCounter -= 1;
                    belowScreenCounter -= 1;
                    if (edibleWalls.Count > 0)
                    {
                        man.XPosition = 740;
                    }
                    else
                    {
                        man.XPosition = 680;
                    }
                    clearAll();
                    if (belowMoon)
                    {
                        graphicsMan.configureEnemies(belowScreenCounter);
                    }
                }
                else
                {
                    screenCounter = 15;
                    belowScreenCounter += 15;
                    man.XPosition = 740;
                    clearAll();
                    if (belowMoon)
                    {
                        graphicsMan.configureEnemies(belowScreenCounter);
                    }
                }
            }

            if (man.XPosition > 750)
            {
                if (screenCounter < 15)
                {
                    screenCounter += 1;
                    belowScreenCounter += 1;
                    man.XPosition = 55;
                    clearAll();
                    graphicsMan.configureEnemies(belowScreenCounter);
                }
                else
                {
                    screenCounter = 0;
                    belowScreenCounter -= 15;
                    man.XPosition = 55;
                    clearAll();
                    graphicsMan.configureEnemies(belowScreenCounter);
                }
            }

            gameSpritesRect = new Rectangle((int)currentFrame * spriteWidth, (int)0, spriteWidth, spriteHeight);
            mainSpritesRect = new Rectangle((int)man.XPosition, (int)man.YPosition, spriteWidth, spriteHeight);
            heartBeatRect = new Rectangle((int)heartBeatFrame * spriteWidth, (int)9 * spriteHeight, spriteWidth, spriteHeight);
            groundRect = new Rectangle((int)10 * unGroTileWidth, (int)0, unGroTileWidth, unGroTileHeight);

            test = new Rectangle((int)0, (int)0, (int)40, (int)30);

            heartBeatTimer += elapsedSecs;
            if (heartBeatTimer > 0.4)
            {
                heartBeatTimer = 0;
                heartBeatFrame++;
            }
            if (heartBeatFrame >= 6)
            {
                heartBeatFrame = 0;
            }

            if (belowMoon && man.Falling)
            {
                if (man.YPosition < 12)
                {
                    graphicsMan.configureEnemies(belowScreenCounter);
                }
                man.YPosition += 2;
                if (man.YPosition >= 425)
                {
                    man.YPosition = 20;
                    man.WalkingOnFloor = false;
                    belowScreenCounter += 16;
                    clearAll();
                    graphicsMan.configureEnemies(belowScreenCounter);
                }

                if (man.YPosition <= 15 && belowScreenCounter > 15)
                {
                    man.YPosition = 400;
                    belowScreenCounter -= 16;
                    man.Jump = false;
                    clearAll();
                }
            }

            if (belowMoon && !man.Falling)
            {
                // Allow us to jump out from under the moon surface....
                if (man.YPosition <= 30 && belowScreenCounter < 15)
                {
                    man.Falling = false;
                    man.WalkingOnFloor = false;
                    belowMoon = false;
                    man.YPosition = 250;
                    clearAll();
                    belowScreenCounter = 0;
                    graphicsMan.configureEnemies(belowScreenCounter);
                }
                if (man.YPosition <= 15 && belowScreenCounter > 15)
                {
                    man.YPosition = 400;
                    belowScreenCounter -= 16;
                    man.Jump = false;
                    clearAll();
                }
            }


            if (belowMoon)
            {
                foreach (Enemy en in enemies)
                {
                    if (en is Alf)
                    {
                        Alf alf = (Alf)en;
                        alf.Update(gameTime);
                    }
                    if (en is Bird)
                    {
                        Bird bird = (Bird)en;
                        bird.Update(gameTime);
                    }
                    if (en is BlueThingy)
                    {
                        BlueThingy bluething = (BlueThingy)en;
                        bluething.Update(gameTime);
                    }
                    if (en is Caterpillar)
                    {
                        Caterpillar caterpillar = (Caterpillar)en;
                        caterpillar.Update(gameTime);
                    }
                    if (en is ChasingEnemy)
                    {
                        ChasingEnemy enem = (ChasingEnemy)en;
                        enem.Update(gameTime, new Vector2(man.XPosition, man.YPosition));
                    }
                    if (en is SpringBear)
                    {
                        SpringBear springbear = (SpringBear)en;
                        springbear.Update(gameTime);
                    }
                    if (en is Fire)
                    {
                        Fire fire = (Fire)en;
                        fire.Update(gameTime);
                    }
                    if (en is Fish)
                    {
                        Fish fish = (Fish)en;
                        fish.Update(gameTime);
                    }
                    if (en is GreenMeanie)
                    {
                        GreenMeanie greenie = (GreenMeanie)en;
                        greenie.Update(gameTime);
                    }
                    if (en is Plant)
                    {
                        Plant plant = (Plant)en;
                        plant.Update(gameTime);
                    }
                    if (en is RedSpaceman)
                    {
                        RedSpaceman redspace = (RedSpaceman)en;
                        redspace.Update(gameTime);
                    }
                    if (en is WhirlWind)
                    {
                        WhirlWind whirlWind = (WhirlWind)en;
                        whirlWind.Update(gameTime);
                    }
                    if (en is WoodLouse)
                    {
                        WoodLouse woodLouse = (WoodLouse)en;
                        woodLouse.Update(gameTime);
                    }
                }
            }

            if (moleManAlive)
            {
                mole.Update(gameTime, belowScreenCounter);
            }

            man.Update(tempY, jumpRight, belowMoon, trip, belowScreenCounter);
            rocket.Update(gameTime);
            if (!belowMoon)
            {
                earth.Update(gameTime);
            }

            seconds = (int)gameTime.ElapsedGameTime.Seconds;
            minutes = (int)gameTime.ElapsedGameTime.Minutes;

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if (gameOn == false)
            {
                spriteBatch.Draw(frontScreen, new Vector2(0, 0), Color.White);
                Vector2 textLocation = new Vector2(10, 10);
                spriteBatch.DrawString(hudFont, "XNA NODES OF YESOD REMAKE ", textLocation + new Vector2(295.0f, 020.0f), Color.Yellow);
                spriteBatch.DrawString(hudFont, "Start", textLocation + new Vector2(480.0f, 100.0f), Color.Yellow);
                spriteBatch.DrawString(hudFont, "Instructions", textLocation + new Vector2(480.0f, 120.0f), Color.Yellow);
                spriteBatch.DrawString(hudFont, "Define Keys ", textLocation + new Vector2(480.0f, 140.0f), Color.Yellow);
                spriteBatch.DrawString(hudFont, "Exit", textLocation + new Vector2(480.0f, 160.0f), Color.Yellow);
                spriteBatch.DrawString(hudFont, "Hit X to Start ", textLocation + new Vector2(480.0f, 240.0f), Color.Yellow);
            }

            else
            {
                if (!belowMoon)
                {

                    if (rocket.RocketScreen == screenCounter)
                    {
                        rocket.Draw(spriteBatch);
                    }

                    for (int j = 0; j < 8; j++)
                    {
                        moonRocksRect = new Rectangle((int)(graphicsMan.UpperRockArray[screenCounter, j] * rockWidth),
                                    (int)0, rockWidth, rockHeight);
                        spriteBatch.Draw(MoonRocks,
                        new Rectangle((j * rockWidth),
                        170,
                        rockWidth, rockHeight),
                        moonRocksRect,
                        Color.White);
                    }

                    moundRect = new Rectangle((int)(graphicsMan.MoundArray[screenCounter, 0] * lowerRockWidth), (int)300, lowerRockWidth, lowerRockHeight);

                    if (graphicsMan.HoleArray0[screenCounter, 0] == 0)
                    {
                        man.colTileRect0 = new Rectangle((int)340, (int)415, 20, 18);
                        holeRect0 = new Rectangle((int)(graphicsMan.HoleArray0[screenCounter, 0] * lowerRockWidth), (int)300, lowerRockWidth, lowerRockHeight);
                        spriteBatch.Draw(MoonRocks, new Vector2(hole0X, holesY), holeRect0, Color.White);
                        spriteBatch.Draw(collisionTile, new Vector2(hole0X + 20.0f, holesY + 15.0f), man.colTileRect0, Color.White);
                    }

                    if (graphicsMan.HoleArray0[screenCounter, 0] != 0)
                    {
                        man.colTileRect0 = new Rectangle((int)340, (int)0, 20, 18);
                    }

                    if (graphicsMan.HoleArray1[screenCounter, 0] == 0)
                    {
                        man.colTileRect1 = new Rectangle((int)540, (int)415, 20, 18);
                        holeRect1 = new Rectangle((int)(graphicsMan.HoleArray1[screenCounter, 0] * lowerRockWidth), (int)300, lowerRockWidth, lowerRockHeight);
                        spriteBatch.Draw(MoonRocks, new Vector2(hole1X, holesY), holeRect1, Color.White);
                        spriteBatch.Draw(collisionTile, new Vector2(hole1X + 20.0f, holesY + 15.0f), man.colTileRect1, Color.White);
                    }
                    if (graphicsMan.HoleArray1[screenCounter, 0] != 0)
                    {
                        man.colTileRect1 = new Rectangle((int)340, (int)0, 20, 18);
                    }

                    spriteBatch.Draw(MoonRocks, new Vector2(100.0f, 300.0f), moundRect, Color.White);
                    spriteBatch.Draw(MoonRocks, new Vector2(600.0f, 300.0f), moundRect, Color.White);

                    for (int l = 0; l < 8; l++)
                    {
                        lowMoonRocksRect = new Rectangle((int)(graphicsMan.LowerRockArray[screenCounter, l] * lowerRockWidth), (int)170, lowerRockWidth, lowerRockHeight);

                        spriteBatch.Draw(MoonRocks,
                                    new Rectangle((l * lowerRockWidth),
                                    470,
                                    lowerRockWidth, lowerRockHeight),
                                    lowMoonRocksRect,
                                    Color.White);
                    }
                    earth.Draw(spriteBatch);
                }

                else if (belowMoon)
                {
                    rects.Clear();
                    walls.Clear();
                    platform.Clear();
                    edibleWalls.Clear();
                    alchiems.Clear();
                    testList.Clear();
                    if (roof.Count > 0)
                    {
                        roof.Clear();
                    }

                    int textureY = 0;
                    int textureX = 0;

                    for (int ii = 0; ii < 10; ii++)
                    {
                        for (int jj = 0; jj < 13; jj++)
                        {
                            // Iterate through the array & point to the start position of the texture to be grabbed by the wallRect
                            platforms = (graphicsMan.ToTheUnderGround[(belowScreenCounter * 10) + ii, jj]);

                            if (platforms < 19)
                            {
                                textureY = 0;
                                textureX = (0 * unGroTileWidth);
                            }

                            // If the image is greater than 19 then point to the next line in the texture  
                            if (platforms >= 19 && platforms < 38)
                            {
                                textureY = (unGroTileHeight * 1);
                                textureX = (19 * unGroTileWidth);
                            }

                            // If the image is greater than 38 then point to the next line in the texture
                            if (platforms >= 38 && platforms < 57)
                            {
                                textureY = (unGroTileHeight * 2);
                                textureX = (38 * unGroTileWidth);
                            }

                            // If the image is greater than 57 then point to the next line in the texture
                            if (platforms >= 57 && platforms < 76)
                            {
                                textureY = (unGroTileHeight * 3);
                                textureX = (57 * unGroTileWidth);
                            }

                            //Grab the rectangle texture from the underground graphics.png  
                            wallRect = new Rectangle((int)(platforms * unGroTileWidth) - textureX,
                            (int)textureY, unGroTileWidth, unGroTileHeight);

                            spriteBatch.Draw(unGroundLedges, new Rectangle((jj * unGroTileWidth),
                            ii * 48, 62, 48), wallRect, Color.White);

                            collisionRects = new Rectangle((jj * unGroTileWidth), ii * 48, 62, 48);

                            // Store the tiles to collide with in a List of Rectangles
                            // Check if floor tiles exist & only 1 screen high (i!) 

                            // Alchiems
                            if (platforms == 19 || platforms == 20 || platforms == 21 || platforms == 22)
                            {
                                alchiems.Add(collisionRects);
                            }
                            // Mole edible wall textures
                            if (platforms == 15 || platforms == 16 || platforms == 17 || platforms == 18)
                            {
                                edibleWalls.Add(collisionRects);
                            }
                            // Floor textures
                            if (platforms == 5 || platforms == 6 || platforms == 7 || platforms == 8
                                || platforms == 24 || platforms == 25 || platforms == 26
                                || platforms == 43 || platforms == 44)
                            {
                                rects.Add(collisionRects);
                            }
                            // Platforms & Ledges
                            if (platforms == 9 || platforms == 10 || platforms == 11 || platforms == 13
                                || platforms == 14 || platforms == 28 || platforms == 29
                                || platforms == 47 || platforms == 48)
                            {
                                platform.Add(collisionRects);

                                //Copy the x,y co-ord from the platform into the test
                                test.Location = collisionRects.Location;
                                testList.Add(test);
                            }
                            // Wall textures                               
                            if (platforms == 0 || platforms == 1 || platforms == 2 || platforms == 3)
                            {
                                walls.Add(collisionRects);
                            }
                            // Roof textures
                            if (platforms >= 57 && platforms < 76)
                            {
                                roof.Add(collisionRects);
                            }
                        }
                    }

                    foreach (Enemy en in enemies)
                    {
                        if (en is Alf)
                        {
                            Alf alf = (Alf)en;
                            alf.Draw(spriteBatch);
                        }
                        if (en is Bird)
                        {
                            Bird bird = (Bird)en;
                            bird.Draw(spriteBatch);
                        }
                        if (en is BlueThingy)
                        {
                            BlueThingy bluething = (BlueThingy)en;
                            bluething.Draw(spriteBatch);
                        }
                        if (en is Caterpillar)
                        {
                            Caterpillar caterpillar = (Caterpillar)en;
                            caterpillar.Draw(spriteBatch);
                        }
                        if (en is ChasingEnemy)
                        {
                            ChasingEnemy chasingEnemy = (ChasingEnemy)en;
                            chasingEnemy.Draw(spriteBatch);
                        }

                        if (en is SpringBear)
                        {
                            SpringBear springbear = (SpringBear)en;
                            springbear.Draw(spriteBatch);
                        }

                        if (en is Fire)
                        {
                            Fire fire = (Fire)en;
                            fire.Draw(spriteBatch);
                        }
                        if (en is Fish)
                        {
                            Fish fish = (Fish)en;
                            fish.Draw(spriteBatch);
                        }
                        if (en is GreenMeanie)
                        {
                            GreenMeanie greenie = (GreenMeanie)en;
                            greenie.Draw(spriteBatch);
                        }
                        if (en is Plant)
                        {
                            Plant plant = (Plant)en;
                            plant.Draw(spriteBatch);
                        }
                        if (en is RedSpaceman)
                        {
                            RedSpaceman redspace = (RedSpaceman)en;
                            redspace.Draw(spriteBatch);
                        }
                        if (en is WhirlWind)
                        {
                            WhirlWind whirlWind = (WhirlWind)en;
                            whirlWind.Draw(spriteBatch);
                        }
                        if (en is WoodLouse)
                        {
                            WoodLouse woodLouse = (WoodLouse)en;
                            woodLouse.Draw(spriteBatch);
                        }
                    }
                }

                if (moleManAlive)
                {
                    mole.Draw(spriteBatch);
                }

                if (!man.summerSaultJump)
                {
                    man.Draw(spriteBatch, gameSpritesRect);
                }
                else
                {
                    man.Draw(spriteBatch, gameSpritesRect);
                }


                spriteBatch.Draw(panel, new Vector2(30, 550), Color.White);
                spriteBatch.Draw(gameSprites, new Vector2(390.0f, 535.0f), heartBeatRect, Color.White);
                spriteBatch.DrawString(hudFont, "" + alchiem, new Vector2(250.0f, 555.0f), Color.White);
                spriteBatch.DrawString(hudFont, "" + man.Lives, new Vector2(395.0f, 555.0f), Color.White);


                string fps = string.Format("{0}", seconds);

                if (seconds < 10 && minutes == 0)
                {
                    spriteBatch.DrawString(hudFont, "00:0" + seconds, new Vector2(635.0f, 555.0f), Color.White);
                }
                else if (seconds >= 10 && minutes == 0)
                {
                    spriteBatch.DrawString(hudFont, "00:" + seconds, new Vector2(635.0f, 555.0f), Color.White);
                }
                if (seconds < 10 && minutes > 0)
                {
                    spriteBatch.DrawString(hudFont, "0" + minutes + ":0" + seconds, new Vector2(635.0f, 555.0f), Color.White);
                }
                else if (seconds >= 10 && minutes > 0)
                {
                    spriteBatch.DrawString(hudFont, "0" + minutes + ":" + seconds, new Vector2(635.0f, 555.0f), Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void clearAll()
        {
            enemies.Clear();
            rects.Clear();
            walls.Clear();
            platform.Clear();
            edibleWalls.Clear();
            alchiems.Clear();
            testList.Clear();
            if (roof.Count > 0)
            {
                roof.Clear();
            }
        }

        private void updateInput()
        {
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            if (gameOn == false)
            {
                // Allows the game to exit
                if (gamePad.Buttons.Back == ButtonState.Pressed ||
                    keyboard.IsKeyDown(Keys.Escape))
                    this.Exit();

                if (gamePad.Buttons.Back == ButtonState.Pressed ||
                    keyboard.IsKeyDown(Keys.X))
                {
                    gameOn = true;
                }
            }
            else
            {
                if (gamePad.Buttons.Back == ButtonState.Pressed ||
                    keyboard.IsKeyDown(Keys.Escape))
                    this.Exit();

                if (keyboard.IsKeyDown(Keys.M) && (belowMoon))
                {
                    mole.MolePosX = man.XPosition;
                    mole.MolePosY = man.YPosition;
                    moleManAlive = true;
                }

                if (keyboard.IsKeyDown(Keys.N))
                {
                    moleManAlive = false;
                }

                if (gamePad.DPad.Left == ButtonState.Pressed ||
                    keyboard.IsKeyDown(Keys.Left) && (!man.Falling) && (!moleManAlive))
                {
                    man.FacingLeft = true;
                    man.XPosition -= 2;
                    animTimer += elapsedSecs;
                    if (animTimer > 0.6)
                    {
                        animTimer = 0;
                        currentFrame++;
                    }
                    if (currentFrame >= 6)
                    {
                        currentFrame = 0;
                    }
                }

                if (gamePad.DPad.Left == ButtonState.Pressed ||
                    keyboard.IsKeyDown(Keys.Left) && (moleManAlive))
                {
                    mole.Direction = true;
                    mole.MolePosX -= 2;
                }


                if (gamePad.DPad.Right == ButtonState.Pressed ||
                    keyboard.IsKeyDown(Keys.Right) && (!man.Falling) && (!moleManAlive))
                {
                    man.XPosition += 2;
                    man.FacingLeft = false;

                    animTimer += elapsedSecs;
                    if (animTimer > 0.6)
                    {
                        animTimer = 0;
                        currentFrame++;
                    }
                    if (currentFrame >= 6)
                    {
                        currentFrame = 0;
                    }
                }

                if (gamePad.DPad.Left == ButtonState.Pressed ||
                    keyboard.IsKeyDown(Keys.Right) && (moleManAlive))
                {
                    mole.Direction = false;
                    mole.MolePosX += 2;
                }

                if (gamePad.DPad.Left == ButtonState.Pressed ||
                    keyboard.IsKeyDown(Keys.Down) && (moleManAlive))
                {
                    mole.MolePosY += 2;
                }

                if (gamePad.DPad.Left == ButtonState.Pressed ||
                    keyboard.IsKeyDown(Keys.Up) && (moleManAlive))
                {
                    mole.MolePosY -= 2;
                }

                if (keyboard.IsKeyDown(Keys.Right) && keyboard.IsKeyDown(Keys.LeftControl)
                    && oldState.IsKeyUp(Keys.LeftControl) && (!man.Falling))
                {
                    man.WalkingOnFloor = false;
                    man.summerSaultJump = true;
                    jumpRight = true;
                    trip = true;

                }

                if (keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyDown(Keys.LeftControl)
                    && oldState.IsKeyUp(Keys.LeftControl) && (!man.Falling))
                {
                    man.WalkingOnFloor = false;
                    man.summerSaultJump = true;
                    jumpRight = false;
                    trip = true;
                }

                if (keyboard.IsKeyDown(Keys.F1))
                {
                    man.Falling = false;
                    man.WalkingOnFloor = false;
                    belowMoon = false;
                    man.YPosition = 350;
                    belowScreenCounter = screenCounter;
                    clearAll();
                }

                if (keyboard.IsKeyDown(Keys.F2))
                {
                    man.Falling = false;
                }

                if (gamePad.Buttons.A == ButtonState.Pressed || keyboard.IsKeyDown(Keys.LeftControl)
                    && (belowMoon) && (!man.Falling) && (!man.summerSaultJump))
                {
                    tempY = man.YPosition;
                    man.Jump = true;
                }

                oldState = keyboard;
            }
        }
    }
}
