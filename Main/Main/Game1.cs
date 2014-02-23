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
    //TODO: Different types of melee and range enemies
    //TODO: Health
    //TODO: Enemies health
    //TODO: NPCs
    //TODO: Storyline ?
    //TODO: Different player heroes ?
    //TODO: Longer map /done - 200 units
    //TODO: Boss fight /done - added boss

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

        bool xPressed; //x - shoot
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
            Tiles.Content = Content; 
            map.Generate(ReadMapFromFIle(), tileSize);
            background.Load(Content, 10);
            player.Load(Content);
            LoadEnemies();
            
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

            if (Keyboard.GetState().IsKeyUp(Keys.X) && xPressed == true)
            {
                ShootProjectile();
            }
            xPressed = Keyboard.GetState().IsKeyDown(Keys.X);
            UpdateProjectiles();

            player.Update(gameTime);

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, (int)player.Position.X,(int)player.Position.Y);
            }

            foreach (var tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Collision(tile.Rectangle, map.Width, map.Height);
                    if(player.Rectangle.Intersects(enemies[i].Rectangle))
                    {
                        enemies.RemoveAt(i);
                        i--;
                    }

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

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        static int[,] ReadMapFromFIle()
        {
            StreamReader mapFile = new StreamReader(@"..\..\..\..\MainContent\MapMatrix.txt");
            int[,] mapRead;
            using (mapFile)
            {
                mapRead = new int[11, 200];
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
        public void LoadEnemies()
        {
            Enemy meleeEnemy1 = new Melee(500, 100);
            meleeEnemy1.Load(Content);
            Enemy rangedEnemy1 = new Ranged(1000, 100);
            rangedEnemy1.Load(Content);
            Enemy boss = new Boss(900/*9500*/, 100, 100, 100);
            boss.Load(Content);
            enemies.Add(meleeEnemy1);
            enemies.Add(rangedEnemy1);
            enemies.Add(boss);
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
            newProjectile.Position = new Vector2((int)player.Position.X, (int)player.Position.Y + 6) + newProjectile.Velocity * 3;
            projectiles.Add(newProjectile);
        }
    }
}
