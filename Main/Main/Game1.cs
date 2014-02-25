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
using Main.StartMenu;

namespace Main
{
    //TODO: Start and end screen
    //TODO: Music and sound effects
    //TODO: Different types of melee and range enemies
    //TODO: NPCs
    //TODO: Storyline ?
    //TODO: Add enemies on map
    //TODO: Boss fight / boss added still need to make fight animation
    //TODO: Longer map /done - 200 units

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GameState currentGameState;
        Player player;
        Map map;
        Camera camera;
        ContinuingBackground background;
        List<Projectile> playerProjectiles = new List<Projectile>();
        List<Projectile> enemyProjectiles = new List<Projectile>();
        List<Enemy> enemies = new List<Enemy>();
        List<Explosion> explosions = new List<Explosion>();
        NPC maleNpc;
        NPC femaleNpc;
        NPC femaleNpc2;
        Button startButton;
        Button exitButton;
        Button playAgainButton;
        bool xPressed; //x - shoot
        static readonly int tileSize = 50;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Rectangle endingRect = new Rectangle(10000, 0, 200, 10000000);

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
            currentGameState = GameState.StartMenu;;
            IsMouseVisible = true;
            map = new Map();
            player = new Player();
            background = new ContinuingBackground();
            camera = new Camera(GraphicsDevice.Viewport);
            
;           base.Initialize();
        }

        protected override void LoadContent()
        {
            startButton = new Button(Content, "startbutton", 348, 103);
            startButton.SetPosition(375, 100);
            exitButton = new Button(Content, "startexitbutton", 348, 103);
            exitButton.SetPosition(375, 250);
            playAgainButton = new Button(Content, "playAgainButton", 300, 89);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tiles.Content = Content; 
            map.Generate(ReadMapFromFIle(), tileSize);
            background.Load(Content, 10);
            player.Load(Content);
            LoadEnemies();
            LoadNpcs();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            
            if (player.Rectangle.Intersects(endingRect))
            {
                currentGameState = GameState.End;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || player.Health <= 0)
            {
                this.Exit();
            }
            switch(currentGameState)
            {
                case (GameState.StartMenu):
                    if (startButton.isClicked)
                    {
                        currentGameState = GameState.Playing;
                    }
                    else if(exitButton.isClicked)
                    {
                        this.Exit();
                    }
                    exitButton.Update(mouse);
                    startButton.Update(mouse);
                    break;
                case (GameState.Playing) :

                    if(playAgainButton.isClicked)
                    {
                        // NEED FIX
                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.X) && xPressed == true)
                    {
                        Shoot();
                    }
                    xPressed = Keyboard.GetState().IsKeyDown(Keys.X);

                    UpdateProjectiles();

                    player.Update(gameTime);
                    maleNpc.Update(gameTime, player);
                    femaleNpc2.Update(gameTime, player);
                    femaleNpc.Update(gameTime, player);

                    foreach (var enemy in enemies)
                    {
                        enemy.Update(gameTime, (int)player.Position.X, (int)player.Position.Y);
                        if (enemy is Ranged)
                        {
                            var ranged = (Ranged)enemy;
                            ranged.Shoot(enemyProjectiles, Content, gameTime);
                        }
                    }

                    foreach (var tile in map.CollisionTiles)
                    {
                        player.Collision(tile.Rectangle, map.Width, map.Height);                
                        EnemiesCollision(tile);
                        ProjectilesCollison(tile);
                    }
                    HitEnemies();
                    HitPlayer();
            
                    camera.Update(player.Position, map.Width, map.Height);

                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            switch(currentGameState)
            {
                case GameState.StartMenu:
                    spriteBatch.Begin();
                   
                    spriteBatch.Draw(Content.Load<Texture2D>("Forest2"), new Rectangle(0,0, (int)WindowSize.Width, (int)WindowSize.Height), Color.White);
                    startButton.Draw(spriteBatch);
                    exitButton.Draw(spriteBatch);

                    spriteBatch.End();
                    break;
                case GameState.Playing :
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

                    background.Draw(spriteBatch);
                    map.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    maleNpc.Draw(spriteBatch);
                    femaleNpc.Draw(spriteBatch);
                    femaleNpc2.Draw(spriteBatch);
                    maleNpc.toolTip.Draw(spriteBatch);
                    femaleNpc.toolTip.Draw(spriteBatch);
                    femaleNpc2.toolTip.Draw(spriteBatch);
                    

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
                    break;

                case GameState.End :
                    spriteBatch.Begin();

                    spriteBatch.Draw(Content.Load<Texture2D>("Forest2"), new Rectangle(0, 0, (int)WindowSize.Width, (int)WindowSize.Height), Color.White);

                   // spriteBatch.Draw(Content.Load<Texture2D>("YouWon"), new Rectangle(0, 100, (int)WindowSize.Width, 200), Color.White);

                    spriteBatch.Draw(Content.Load<Texture2D>("YouWon"), new Rectangle(225, 15, 672, 400), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("playAgainButton"), new Rectangle(350, 330, 190, 55), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("startexitbutton"), new Rectangle(560, 330, 190, 55), Color.White);

                    spriteBatch.End();
                    break;
            }
           
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
        
        public void LoadNpcs()
        {
            maleNpc = new MaleNpc("maleNpc",530 , 250, new ToolTip(Content, "quest1",400,30,250));
            maleNpc.Load(Content);

            femaleNpc = new FemaleNpc("femaleNpc", 2750, 100, new ToolTip(Content, "quest2", 2590, 7,200));
            femaleNpc.Load(Content);

            femaleNpc2 = new FemaleNpc("femaleNpc2", 6000, 360, new ToolTip(Content, "quest2", 6000, 360, 200));
            femaleNpc2.Load(Content);


        }
        public void LoadEnemies()
        {
            Enemy meleeEnemy1 = new Knight(500, 50, Content);
            meleeEnemy1.Load(Content);


            Enemy rangedEnemy1 = new Archer(450, 50, Content);
            rangedEnemy1.Load(Content);

            Enemy drowRanger = new Archer(1000, 50, Content);
            drowRanger.Load(Content);

            enemies.Add(meleeEnemy1);
           enemies.Add(rangedEnemy1);
            enemies.Add(drowRanger);

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
        public void HitEnemies()
        {
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
        }
        public void HitPlayer()
        {
            for (int i = 0; i < enemyProjectiles.Count; i++)
            {
                
                if(enemyProjectiles[i].Rectangle.Intersects(player.Rectangle))
                {
                    player.Health--;
                    enemyProjectiles.RemoveAt(i);
                    i--;
                }
            }
            foreach (var enemy in enemies)
            {
                if (enemy is Melee)
                {
                    var melee = (Melee)enemy;
                    if(melee.Atacking == true)
                    {
                        if(melee.Rectangle.Intersects(player.Rectangle))
                        {
                            player.Health = player.Health - 1;
                        }
                    }
                }

            }
        }
        public void ProjectilesCollison(CollisionTile tile)
        {
            for (int i = 0; i < playerProjectiles.Count; i++)
            {
                if (playerProjectiles[i].Rectangle.Intersects(tile.Rectangle))
                {
                    explosions.Add(new Explosion(Content, playerProjectiles[i].Rectangle.X, playerProjectiles[i].Rectangle.Y));
                    playerProjectiles.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < enemyProjectiles.Count; i++)
            {
                if (enemyProjectiles[i].Rectangle.Intersects(tile.Rectangle))
                {
                    enemyProjectiles.RemoveAt(i);
                    i--;
                }
            }
        }
        public void EnemiesCollision(CollisionTile tile)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Collision(tile.Rectangle, map.Width, map.Height);
                enemies[i].PlayerCollision(player.Rectangle, map.Width, map.Height);
            }
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
                if (Vector2.Distance(enemyProjectiles[i].Position, player.Position) > 500)
                {
                    enemyProjectiles.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
