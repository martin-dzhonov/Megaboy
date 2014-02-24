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
    //TODO: NPCs
    //TODO: Storyline ?
    //TODO: Different player heroes ?
    //TODO: Add enemies on map
    //TODO: Boss fight / boss added still need to make fight animation
    //TODO: Longer map /done - 200 units

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Map map;
        Camera camera;
        ContinuingBackground background;
        List<Projectile> playerProjectiles = new List<Projectile>();
        List<Projectile> enemyProjectiles = new List<Projectile>();
        List<Enemy> enemies = new List<Enemy>();
        List<Explosion> explosions = new List<Explosion>();

        bool xPressed; //x - shoot
        static readonly int tileSize = 50;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //set screen resolution
            graphics.PreferredBackBufferWidth = (int)WindowSize.Width;
            graphics.PreferredBackBufferHeight = (int)WindowSize.Height;
            this.IsMouseVisible = true;
            
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
                Shoot();
            }
            xPressed = Keyboard.GetState().IsKeyDown(Keys.X);
            UpdateProjectiles();

            player.Update(gameTime);

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, (int)player.Position.X,(int)player.Position.Y);
                if(enemy.GetType().Name == "Ranged")
                {
                    var ranged = (Ranged)enemy;
                    ranged.Shoot(enemyProjectiles, Content); //TODO shoot
                }
            }

            foreach (var tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);

                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Collision(tile.Rectangle, map.Width, map.Height);
                }        

                for (int i = 0; i < playerProjectiles.Count; i++)
                {
                    if (playerProjectiles[i].Rectangle.Intersects(tile.Rectangle))
                    {
                        explosions.Add(new Explosion(Content, playerProjectiles[i].Rectangle.X, playerProjectiles[i].Rectangle.Y));
                        playerProjectiles.RemoveAt(i);
                        i--;
                    }
                }
            }

            for (int i = 0; i < playerProjectiles.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (i >= 0)
                    {
                        if (playerProjectiles[i].Rectangle.Intersects(enemies[j].Rectangle))
                        {
                            explosions.Add(new Explosion(Content, playerProjectiles[i].Rectangle.X, playerProjectiles[i].Rectangle.Y));
                            enemies[j].Health--;
                            if (enemies[j].Health <= 0)
                            {
                                enemies.RemoveAt(j);
                                j--;
                            }
                            playerProjectiles.RemoveAt(i);
                            i--;
                        }
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
            foreach (var projectile in playerProjectiles)
            {
                projectile.Draw(spriteBatch);
            }
            foreach (var projectile in enemyProjectiles)
            {
                projectile.Draw(spriteBatch);
            }
            for (int i = 0; i < explosions.Count; i++)
			{
                explosions[i].Draw(spriteBatch);
                if (explosions[i].Finished)
                {
                    explosions.RemoveAt(i);
                    i--;
                }   
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

            Enemy antiMage = new AntiMage(1500, 700);
            antiMage.Load(Content);  
 
            Enemy rangedEnemy1 = new Ranged(450, 100);
            rangedEnemy1.Load(Content);

            Enemy boss = new Pudge(900, 100);
            boss.Load(Content);

            Enemy ursa = new UrsaWarrior(500, 800);
            ursa.Load(Content);

            Enemy spiritBreaker = new SpiritBreaker(1500, 50);
            spiritBreaker.Load(Content);

            enemies.Add(meleeEnemy1);
            enemies.Add(rangedEnemy1);
            enemies.Add(antiMage);
            enemies.Add(ursa);
            enemies.Add(spiritBreaker);
            enemies.Add(boss);
        }
        public void UpdateProjectiles()
        {
            for (int i = 0; i < playerProjectiles.Count; i++)
            {
                playerProjectiles[i].UpdatePosition();
                if (Vector2.Distance(playerProjectiles[i].Position, player.Position) > 400)
                {
                    playerProjectiles.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < enemyProjectiles.Count; i++)
            {
                enemyProjectiles[i].UpdatePosition();
            }
        }

        public void Shoot()
        {
            Projectile fireball = new Fireball(Content);
            if (player.LookingRight)
            {
                fireball.ShootRight();
            }
            else
            {
                fireball.ShootLeft();
            }
            fireball.Position = new Vector2((int)player.Position.X, (int)player.Position.Y + 6) + fireball.Velocity * 3;
            playerProjectiles.Add(fireball);
        }

    }
}
