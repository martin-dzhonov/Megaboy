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

using Main.Interfaces;


namespace Main
{    
    class Map : IMap
    {
        private List<CollisionTile> collisionTiles = new List<CollisionTile>();

        public List<CollisionTile> CollisionTiles
        {
            get { return this.collisionTiles; }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public void Generate(int[,] map, int size)
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    int number = map[j, i];

                    if (number > 0)
                    {
                        collisionTiles.Add(new CollisionTile(number, new Rectangle(i * size, j * size, size, size)));
                    }

                    this.Width = (i + 1) * size;
                    this.Height = (j + 1) * size;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in collisionTiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
