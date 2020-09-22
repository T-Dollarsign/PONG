using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Game1 : Game
    {
        Random rnd = new Random();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball, blue, red;
        Vector2 ballPos, bluePos, redPos, newBallPos;
        int blueLives, redLives, speedCounter, maxSpeedCounter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            blueLives = 3;
            redLives = 3;
            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 720;
        }

        public void LoseLife(int caseSwitch)
        {

            switch (caseSwitch)
            {
                case 1:
                    blueLives--;
                    break;
                case 2:
                    redLives--;
                    break;
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ball = Content.Load<Texture2D>("bal");
            blue = Content.Load<Texture2D>("blauweSpeler");
            red = Content.Load<Texture2D>("rodeSpeler");

            ballPos = new Vector2((Window.ClientBounds.Width / 2) - (ball.Width / 2), (Window.ClientBounds.Height / 2) - (ball.Height / 2));
            bluePos = new Vector2(0, (Window.ClientBounds.Height / 2) - (blue.Height / 2));
            redPos = new Vector2((Window.ClientBounds.Width - red.Width), (Window.ClientBounds.Height / 2) - (red.Height /2));


            newBallPos = new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10));
            newBallPos.Normalize();
            newBallPos *= 4;

            speedCounter = 0;
        }

        public Rectangle BoundingboxRed
        {
            get
            {
                Rectangle redBounds = red.Bounds;
                redBounds.Offset(redPos);
                return redBounds;
            }
        }

       

        public Rectangle BoundingboxBlue
        {
            get
            {
                Rectangle blueBounds = blue.Bounds;
                blueBounds.Offset(bluePos);
                return blueBounds;
            }
        }

        public Rectangle BoundingboxBall
        {
            get
            {
                Rectangle ballBounds = ball.Bounds;
                ballBounds.Offset(ballPos);
                return ballBounds;
            }
        }

        public void PlayerMovement()
        {
            //moving the player
            int playerSpeed = Window.ClientBounds.Height / 50;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                bluePos.Y -= playerSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                bluePos.Y += playerSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                redPos.Y -= playerSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                redPos.Y += playerSpeed;
            }

            if (bluePos.Y < 0)
                bluePos.Y = 0;

            if (bluePos.Y > Window.ClientBounds.Height - blue.Height)
                bluePos.Y = Window.ClientBounds.Height - blue.Height;

            if (redPos.Y < 0)
                redPos.Y = 0;

            if (redPos.Y > Window.ClientBounds.Height - blue.Height)
                redPos.Y = Window.ClientBounds.Height - blue.Height;
        }

        public void BallMovement()
        {
            //moving the ball
            ballPos += newBallPos;

            maxSpeedCounter = 10;

            //keeping the ball within bounds
            if (ballPos.Y < 0)
            {
                newBallPos.Y = -newBallPos.Y;
            }

            if (ballPos.Y > Window.ClientBounds.Height - ball.Height)
            {
                newBallPos.Y = -newBallPos.Y;
            }

            if (ballPos.X < 0)
            {
                //Add score red
                LoadContent();
            }

            if (ballPos.X > Window.ClientBounds.Width - ball.Width)
            {
                //Add score blue
                LoadContent();
            }

            if (BoundingboxBall.Intersects(BoundingboxBlue) || BoundingboxBall.Intersects(BoundingboxRed))
            {
                newBallPos.X = -newBallPos.X;
                

                if (speedCounter < maxSpeedCounter)
                {
                    speedCounter += 1;
                    newBallPos *= 1.1f;
                }
            }
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            PlayerMovement();
            BallMovement();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(ball, ballPos, Color.White);
            spriteBatch.Draw(blue, bluePos, Color.White);
            spriteBatch.Draw(red, redPos, Color.White);
            spriteBatch.End();
        }

       
    }
}