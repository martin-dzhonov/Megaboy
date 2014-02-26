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
using Main;

namespace Main
{
    //TODO: Add enemies on map
    public class GameMegaBoy : Microsoft.Xna.Framework.Game
    {
        GameState currentGameState;
        Player player;
        Map map;
        Camera camera;
        ContinuingBackground background;
        public static List<Projectile> playerProjectiles = new List<Projectile>();
        public static List<Projectile> enemyProjectiles = new List<Projectile>();
        static List<Enemy> enemies = new List<Enemy>();
        List<Explosion> explosions = new List<Explosion>();
        NPC maleNpc;
        NPC femaleNpc;
        NPC femaleNpc2;
        Button startButton;
        Button exitButton;
        Button playAgainButton;
        Button startOnEndScreen;
        Button exitOnEndScreen; 

        SoundEffect gameMusicLoop;
        SoundEffectInstance instance;
        bool xPressed; //x - shoot
        static readonly int tileSize = 50;
        Boss witch;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameMegaBoy()
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
            currentGameState = GameState.StartMenu;
            
            //Design pattern singleton
            player = Player.GetState();
            IsMouseVisible = true;
            map = new Map();

            background = new ContinuingBackground();

            camera = new Camera(GraphicsDevice.Viewport);
            
            ;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            witch = new Boss(15000, 50, Content);
            gameMusicLoop = Content.Load<SoundEffect>("Sounds//loop");
            instance = gameMusicLoop.CreateInstance();
            instance.IsLooped = true;
            startButton = new Button(Content, "playbuttonNEW", 348, 103);
            startButton.SetPosition(375, 125);
            exitButton = new Button(Content, "exitbuttonNEW", 348, 103);
            exitButton.SetPosition(375, 300);
            startOnEndScreen = new Button(Content, "playAgainButton", 250, 70);
            startOnEndScreen.SetPosition(200, 300);
            exitOnEndScreen = new Button(Content, "startexitbutton", 250, 70);
            exitOnEndScreen.SetPosition(648, 300);

            playAgainButton = new Button(Content, "playAgainButton", 300, 89);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tiles.Content = Content; 
            map.Generate(ReadMapFromFIle(), tileSize);
            background.Load(Content, 15);
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
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
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
                    else if (exitButton.isClicked)
                    {
                        this.Exit();
                    }
                    exitButton.Update(mouse);
                    startButton.Update(mouse);
                    break;
                case (GameState.Playing) :
                   
                    instance.Play();
                    if (witch.Health == 0)
                    {
                        currentGameState = GameState.End;
                    }
                    if (player.Health <= 0)
                    {
                        currentGameState = GameState.Dead;
                    }
                    if (playAgainButton.isClicked)
                    {
                        // NEED FIX
                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.X) && xPressed == true)
                    {
                        Shoot();
                    }
                    xPressed = Keyboard.GetState().IsKeyDown(Keys.X);

                    UpdateProjectiles();

                    player.Update(gameTime, playerProjectiles.Count);

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
                case (GameState.End):
                    if (startOnEndScreen.isClicked)
                    {
                        currentGameState = GameState.Playing;
                    }
                    else if (exitOnEndScreen.isClicked)
                    {
                        this.Exit();
                    }
                    exitOnEndScreen.Update(mouse);
                    startOnEndScreen.Update(mouse);
                    break;
                case (GameState.Dead):
                    if (startOnEndScreen.isClicked)
                    {
                        currentGameState = GameState.Playing;
                    }
                    else if (exitOnEndScreen.isClicked)
                    {
                        this.Exit();
                    }
                    exitOnEndScreen.Update(mouse);
                    startOnEndScreen.Update(mouse);
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

                    spriteBatch.Draw(Content.Load<Texture2D>("megamanwalpaper"), new Rectangle(0, 0, (int)WindowSize.Width, (int)WindowSize.Height), Color.White);
                    startButton.Draw(spriteBatch);
                    exitButton.Draw(spriteBatch);

                    spriteBatch.End();
                    break;
                case GameState.Playing :
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
                    
                    background.Draw(spriteBatch);
                    map.Draw(spriteBatch);
                    player.Draw(spriteBatch, camera);
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

                    spriteBatch.Draw(Content.Load<Texture2D>("endScreen"), new Rectangle(0, 0, (int)WindowSize.Width, (int)WindowSize.Height), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("YouWon"), new Rectangle(225, 15, 672, 400), Color.White);
                    startOnEndScreen.Draw(spriteBatch);
                    exitOnEndScreen.Draw(spriteBatch);

                    spriteBatch.End();
                    break;
                case GameState.Dead :
                    spriteBatch.Begin();

                    spriteBatch.Draw(Content.Load<Texture2D>("endScreen"), new Rectangle(0, 0, (int)WindowSize.Width, (int)WindowSize.Height), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("gameOver"), new Rectangle(225, 15, 672, 400), Color.White);
                    startOnEndScreen.Draw(spriteBatch);
                    exitOnEndScreen.Draw(spriteBatch);

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
                mapRead = new int[11, 317];
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
            maleNpc = new MaleNpc("maleNpc", 530, 250, new ToolTip(Content, "intro", 400, 8, 533, 295));
            maleNpc.Load(Content);

            femaleNpc = new FemaleNpc("femaleNpc", 2750, 100, new ToolTip(Content, "quest1", 2800, 5, 215, 165));
            femaleNpc.Load(Content);

            femaleNpc2 = new FemaleNpc("femaleNpc2", 6000, 360, new ToolTip(Content, "quest2", 6030, 250, 215, 165));
            femaleNpc2.Load(Content);
        }

        public void DrawNpcStuff(SpriteBatch spriteBatch)
        {
        }

        public void LoadEnemies()
        {
           Enemy archer1 = new Archer(1500, 50, Content);
           archer1.Load(Content);
           Enemy archer2 = new Archer(1700, 50, Content);
           archer2.Load(Content);
           Enemy archer3 = new Archer(2000, 50, Content);
           archer3.Load(Content);
           Enemy archer4 = new Archer(3650, 300, Content);
           archer4.Load(Content);
           Enemy archer5 = new Archer(4100, 50, Content);
           archer5.Load(Content);
           Enemy archer6 = new Archer(4500, 50, Content);
           archer6.Load(Content);
           Enemy knight1 = new Knight(5500, 50, Content);
           knight1.Load(Content);
           Enemy knight2 = new Knight(2500, 350, Content);
           knight2.Load(Content);
           Enemy knight3 = new Knight(3700, 50, Content);
           knight3.Load(Content);
           Enemy knight4 = new Knight(4000, 50, Content);
           knight4.Load(Content);
           Enemy knight5 = new Knight(10500, 50, Content);
           knight5.Load(Content);
           Enemy knight6 = new Knight(10900, 50, Content);
           knight6.Load(Content);
           Enemy knight7 = new Knight(9300, 50, Content);
           knight7.Load(Content);
           Enemy knight8 = new Knight(5000, 50, Content);
           knight8.Load(Content);
           Enemy knight9 = new Knight(5000, 50, Content);
           knight9.Load(Content);
           Enemy ogre1 = new Ogre(5100, 50, Content);
           ogre1.Load(Content);
           Enemy ogre2 = new Ogre(5800, 50, Content);
           ogre2.Load(Content);
           Enemy ogre3 = new Ogre(7000, 400, Content);
           ogre3.Load(Content);
           Enemy ogre4 = new Ogre(7000, 50, Content);
           ogre4.Load(Content);
           Enemy ogre5 = new Ogre(6500, 50, Content);
           ogre5.Load(Content);
           Enemy ogre6 = new Ogre(12000, 50, Content);
           ogre5.Load(Content);
           Enemy ogre7 = new Ogre(12700, 50, Content);
           ogre5.Load(Content);
           Enemy ogre8 = new Ogre(13200, 50, Content);
           ogre5.Load(Content);
            Enemy orc1 = new Orc(6100, 350, Content);
            orc1.Load(Content);
            Enemy orc2 = new Orc(8100, 50, Content);
            orc2.Load(Content);
            Enemy orc3 = new Orc(9100, 300, Content);
            orc3.Load(Content);
            Enemy orc4 = new Orc(11000, 50, Content);
            orc4.Load(Content);
            Enemy orc5 = new Orc(11000, 50, Content);
            orc5.Load(Content);
            Enemy orc6 = new Orc(11500, 50, Content);
            orc6.Load(Content);
            Enemy orc7 = new Orc(11900, 50, Content);
            orc7.Load(Content);
            enemies.Add(archer1);
            enemies.Add(archer2);
            enemies.Add(archer3);
            enemies.Add(archer4);
            enemies.Add(archer5);
            enemies.Add(archer6);
            enemies.Add(knight1);
            enemies.Add(knight2);
            enemies.Add(knight3);
            enemies.Add(knight4);
            enemies.Add(knight5);
            enemies.Add(knight6);
            enemies.Add(knight7);
            enemies.Add(ogre1);
            enemies.Add(ogre2);
            enemies.Add(ogre3);
            enemies.Add(ogre4);
            enemies.Add(ogre5);
            enemies.Add(ogre6);
            enemies.Add(ogre7);
            enemies.Add(ogre8);
            enemies.Add(orc1);
            enemies.Add(orc2);
            enemies.Add(orc3);
            enemies.Add(orc4);
            enemies.Add(orc5);
            enemies.Add(orc6);
            enemies.Add(orc7);

            
            enemies.Add(witch);
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
                if (enemyProjectiles[i].Rectangle.Intersects(player.Rectangle))
                {
                    player.Health -= 7;
                    enemyProjectiles.RemoveAt(i);
                    i--;
                }
            }
            foreach (var enemy in enemies)
            {
                if (enemy is Melee)
                {
                    var melee = (Melee)enemy;
                    if (melee.Atacking == true)
                    {
                        if (melee.Rectangle.Intersects(player.Rectangle))
                        {
                            if (melee.CurrentFrame == 3)
                            {
                                player.Health = player.Health - 1;
                            }
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
            if (playerProjectiles.Count > 5)
            {
                playerProjectiles.RemoveAt(playerProjectiles.Count - 1);
            }
        }

        public void EnemiesCollision(CollisionTile tile)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Collision(tile.Rectangle, map.Width, map.Height);
            }
        }

        public void UpdateProjectiles()
        {
            for (int i = 0; i < playerProjectiles.Count; i++)
            {
                playerProjectiles[i].UpdatePosition();
                if (Vector2.Distance(playerProjectiles[i].Position, player.Position) > 800)
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
