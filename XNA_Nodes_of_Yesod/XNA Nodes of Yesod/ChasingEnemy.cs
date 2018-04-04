using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XNA_Nodes_of_Yesod
{
    public class ChasingEnemy : Enemy
    {

        public ChasingEnemy(float xPos, float yPos, float speedX, float speedY, Texture2D sprite, List<Rectangle> walls)
            : base(xPos, yPos, speedX, speedY, sprite, walls)
        {
            this.CurrentFrameX = 4;
            this.CurrentFrameY = 5;
            this.SheetSize = 3;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;
        }

        public void Update(GameTime gameTime, Vector2 charliePos)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (charliePos.X < this.PositionX)
            {
                this.PositionX -= this.SpeedX;
            }
            else if (charliePos.X > this.PositionX)
            {
                this.PositionX += this.SpeedX;
            }

            if (charliePos.Y < this.PositionY)
            {
                this.PositionY -= this.SpeedY;
            }
            else if (charliePos.Y > this.PositionY)
            {
                this.PositionY += this.SpeedY;
            }

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                ++this.CurrentFrameX;
                if (this.CurrentFrameX >= 4 + this.SheetSize)
                {
                    this.CurrentFrameX = 4;
                }
            }
        }
    }
}
