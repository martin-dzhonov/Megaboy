using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Main.Enum;

namespace Main
{
    //TODO: Start and end screen
    //TODO: Music and sound effects
    //TODO: Ranged enemy
    //TODO: Different types of melee and range enemies
    //TODO: Health
    //TODO: Enemies health
    //TODO: NPCs
    //TODO: Storyline ?
    //TODO: Different player heroes ?
    //TODO: Longer map
    //TODO: Boss fight
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Map map;
        Camera camera;
        ContinuingBackground background;
        List<Projectile> projectiles = new List<Projectile>();
        
        List<Enemy> enemies = new List<Enemy>();
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

        protected override void Initialize()
        {
            map = new Map();
            player = new Player();
            background = new ContinuingBackground();
            camera = new Camera(GraphicsDevice.Viewport);      
;           base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Enemies
            Enemy meleeEnemy1 = new Melee(500, 100); 
            meleeEnemy1.Load(Content);
            Enemy rangedEnemy1 = new Ranged(1000, 100);
            rangedEnemy1.Load(Content);
            enemies.Add(meleeEnemy1);
            enemies.Add(rangedEnemy1);
            Tiles.Content = Content; 
            map.Generate(ReadMapFromFIle(), tileSize);
            background.Load(Content, 2);
            player.Load(Content);
            
        }

        protected override void UnloadContent()
        {
        }

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

            foreach (var enm in enemies)
            {
                enm.Update(gameTime, player.X, player.Y);
            }

            foreach (var tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
                foreach (var enemy in enemies)
                {
                    enemy.Collision(tile.Rectangle, map.Width, map.Height);
                }          
                for (int i = 0; i < projectiles.Count; i++)
                {
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        if (projectiles[i].Rectangle.Intersects(enemies[j].Rectangle))
                        {
                            enemies.RemoveAt(j);
                            j--;
                        }
                    }

                    if (projectiles[i].Rectangle.Intersects(tile.Rectangle))
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
                
            }

            camera.Update(player.Position, map.Width, map.Height);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            background.Draw(spriteBatch);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);

            foreach (var enm in enemies)
            {
                enm.Draw(spriteBatch);
            }
            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void UpdateProjectiles()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].UpdatePosition();
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

        static int[,] ReadMapFromFIle()
        {
            StreamReader mapFile = new StreamReader(@"..\..\..\..\MainContent\MapMatrix.txt");
            int[,] mapRead;
            using (mapFile)
            {
                mapRead = new int[11, 40];
                string line = string.Empty;
                int row = 0;
                while ((line = mapFile.ReadLine()) != null)
                {
                    int[] lineArr = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                    for (int col = 0; col < mapRead.GetLength(1); col++)
                    {
                        mapRead[row, col] = lineArr[col];
                    }
                    row++;
                }
            }
            return mapRead;
        }

    }
}
