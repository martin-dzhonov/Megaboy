using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Main
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Map map;
        Camera camera;
        ContinuingBackground background;
        List<Projectile> projectiles = new List<Projectile>();
        bool spacePressed;
        static readonly int tileSize = 50;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //set screen resolution
            graphics.PreferredBackBufferWidth = (int)WindowSize.Width;
            graphics.PreferredBackBufferHeight = (int)WindowSize.Height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            map = new Map();
            player = new Player();
            background = new ContinuingBackground();
            camera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Tiles.Content = Content;
            map.Generate(new int[,]{
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0},
                {2,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {2,1,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0},
                {2,2,1,1,0,0,0,0,2,2,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,2,2,2,1,1,0,0,0,0,0,0,0,0},
                {2,0,0,0,0,0,1,1,2,2,2,1,0,0,0,0,0,0,0,1,1,1,2,2,1,0,1,2,2,2,2,2,1,0,0,0,0,0,0,0},
                {2,0,0,0,0,1,2,2,2,2,2,2,0,0,0,0,0,0,1,2,2,2,2,2,2,1,2,2,2,2,2,2,2,0,0,0,0,0,0,0},
                {2,1,1,1,1,2,2,2,2,2,2,2,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1},
            }, tileSize);
            background.Load(Content, 2);
            player.Load(Content);
        }

        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space) && spacePressed == true)
            {
                ShootProjectile();
            }
            spacePressed = Keyboard.GetState().IsKeyDown(Keys.Space);
            UpdateProjectiles();

            player.Update(gameTime);

            foreach (var tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
                for (int i = 0; i < projectiles.Count; i++)
                {
                    if (projectiles[i].Collided(tile.Rectangle))
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
            }

            camera.Update(player.Position, map.Width, map.Height);
            base.Update(gameTime);
        }

        public void UpdateProjectiles()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Position += projectiles[i].Velocity;
                if (Vector2.Distance(projectiles[i].Position, player.Position) > 500)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ShootProjectile()
        {
            Projectile newProjectile = new Projectile(Content);
            if (player.LookingRight)
            {
                newProjectile.ShootRight();
            }
            else
            {
                newProjectile.ShootLeft();
            }
            newProjectile.Position = new Vector2(player.X, player.Y + 20) + newProjectile.Velocity * 2;
            projectiles.Add(newProjectile);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            background.Draw(spriteBatch);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);

            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
