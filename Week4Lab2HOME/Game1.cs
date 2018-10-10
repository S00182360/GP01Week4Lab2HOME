using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprites;

namespace Week4Lab2HOME.Desktop
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //2 Viewports, one for map, other origonal
        Viewport mapViewport;
        Viewport originalViewPort;
        //Sprites: Character and a dot
        private Texture2D _txCharacter;
        private SimpleSprite CharacterSprite;
        private Texture2D _txLips;
        private SimpleSprite LipsSprite;
        private Texture2D _txBackGround;
        private SimpleSprite BackGroundSprite;
        Vector2 CharacterPosition = new Vector2(10, 10);
        private Texture2D _txDot; //dot sprite for map
        private SimpleSprite DotSprite; //dot sprite
        private SpriteFont GameFont;
        float scale = 0.1f;
        Viewport mapViewport2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Sample the original Viewport
            originalViewPort = GraphicsDevice.Viewport;
            GraphicsDevice.Viewport = originalViewPort;
            // Create the map viewport
            mapViewport.Bounds = new Rectangle(0, 0,
                (int)(originalViewPort.Bounds.Width * scale), //scale is 0.1 above to scale up/down
                (int)(originalViewPort.Bounds.Height * scale)); //
            mapViewport2.Bounds = new Rectangle(0, 0,
                                                (int)(originalViewPort.Bounds.Width * scale), //scale is 0.1 above to scale up/down
                                                (int)(originalViewPort.Bounds.Height *scale)); //
            mapViewport.X = 0;
            mapViewport.Y = 0;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _txBackGround = Content.Load<Texture2D>(@"Textures\backgroundImage");
            BackGroundSprite = new SimpleSprite(_txBackGround, Vector2.Zero);
            _txCharacter = Content.Load<Texture2D>(@"Textures\body2");
            CharacterSprite = new SimpleSprite(_txCharacter, originalViewPort.Bounds.Center.ToVector2()); //CharacterPosition is in originalViewport
            _txDot = Content.Load<Texture2D>(@"Textures\body");
            DotSprite = new SimpleSprite(_txDot, mapViewport.Bounds.Center.ToVector2()); //dot is in mapViewport bounds
            _txLips = Content.Load<Texture2D>(@"Textures\lips");
            LipsSprite = new SimpleSprite(_txLips, originalViewPort.Bounds.Center.ToVector2());


            GameFont = Content.Load<SpriteFont>(@"GameFont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float speed = 5.0f;
            //float scale = 0.01f;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Vector2 perviousPos = CharacterSprite.Position;
            Vector2 PreviousLips = LipsSprite.Position;
            // TODO: Add your update logic here
            //***************************************************************************
            //WASD keyboard controls
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                CharacterSprite.Move(new Vector2(-1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                CharacterSprite.Move(new Vector2(1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                CharacterSprite.Move(new Vector2(0, -1) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                CharacterSprite.Move(new Vector2(0, 1) * speed);
            //keeps within bounds of viewport
            if (!GraphicsDevice.Viewport.Bounds
                .Contains(CharacterSprite.BoundingRect))
                CharacterSprite.Move(perviousPos - CharacterSprite.Position);
            DotSprite.Position = CharacterPosition;
            DotSprite.text = DotSprite.Position.ToString();
            //***************************************************************************

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                LipsSprite.Move(new Vector2(-1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                LipsSprite.Move(new Vector2(1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                LipsSprite.Move(new Vector2(0, -1) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                LipsSprite.Move(new Vector2(0, 1) * speed);

            if (!GraphicsDevice.Viewport.Bounds.Contains(LipsSprite.BoundingRect))
                LipsSprite.Move(PreviousLips - LipsSprite.Position);

            MouseState ms = Mouse.GetState();
            if (originalViewPort.Bounds.Contains(ms.Position))
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    //mapViewport.X = ms.X;
                    //mapViewport.Y = ms.Y;
                    mapViewport.Bounds = new Rectangle(ms.X, ms.Y, (int)(mapViewport.Bounds.Width),
                        (int)(mapViewport.Bounds.Height));
                }
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            CharacterPosition = CharacterSprite.Position;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            //these are the draw overloads from three separate draw calls
            BackGroundSprite.draw(spriteBatch, originalViewPort.Bounds);
            CharacterSprite.text = CharacterSprite.Position.ToString();
            CharacterSprite.draw(spriteBatch, GameFont);
            LipsSprite.text = LipsSprite.Position.ToString();
            LipsSprite.draw(spriteBatch, GameFont);
            //CharacterSprite.draw(spriteBatch);
            //spriteBatch.Draw(_txBackGround, originalViewPort.Bounds, Color.White);

            //spriteBatch.Draw(_txCharacter, CharacterPosition, Color.White);
            spriteBatch.End();

            //minimap viewport overrides device viewport
            GraphicsDevice.Viewport = mapViewport;
            spriteBatch.Begin();
            //CharacterSprite.draw(spriteBatch, 0.1f); //scaled draw
            BackGroundSprite.draw(spriteBatch, mapViewport.Bounds);
            DotSprite.text = DotSprite.Position.ToString();
            CharacterSprite.draw(spriteBatch, scale);
            LipsSprite.draw(spriteBatch, scale);
            //spriteBatch.Draw(_txDot, CharacterPosition * 0.1f,
            //                null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
            //spriteBatch.Draw(_txBackGround, mapViewport.Bounds, Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here
            GraphicsDevice.Viewport = originalViewPort;
            base.Draw(gameTime);
        }
    }
}