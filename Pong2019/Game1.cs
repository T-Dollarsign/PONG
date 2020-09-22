using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong2019
{
    public class Game1 : Game
    {
        Random rnd = new Random();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D ball, blue, red;
        Vector2 ballPos, bluePos, redPos, newBallPos;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ball = Content.Load<Texture2D>("bal");
            blue = Content.Load<Texture2D>("blauweSpeler");
            red = Content.Load<Texture2D>("rodeSpeler");

            ballPos = new Vector2((Window.ClientBounds.Width / 2) - (ball.Width / 2), (Window.ClientBounds.Height / 2) - (ball.Height / 2));
            bluePos = new Vector2(0, (Window.ClientBounds.Height / 2) - (blue.Height / 2));
            redPos = new Vector2((Window.ClientBounds.Width - red.Width), (Window.ClientBounds.Height / 2) - (red.Height / 2));

            newBallPos = GenerateVector();
            newBallPos.Normalize();
            newBallPos *= 4;
        }

        public Vector2 GenerateVector()
        {
            Vector2 direction = new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10));

            while ((direction.X >= -8) && (direction.X <= 8))
            {
                direction = new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10));
            }

            return direction;
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

            //keeping the player within upper and lower bounds
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
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            PlayerMovement();
            BallMovement();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(ball, ballPos, Color.White);
            _spriteBatch.Draw(blue, bluePos, Color.White);
            _spriteBatch.Draw(red, redPos, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
