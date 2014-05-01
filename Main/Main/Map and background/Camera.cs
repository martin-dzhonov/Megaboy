using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Main
{
    class Camera
    {
        private Matrix transform;
        private Viewport viewport;
        private Vector2 centre;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public Vector2 Centre
        {
            get
            {
                return this.centre;
            }
            set
            {
                this.centre = value;
            }
        }

        public Matrix Transform
        {
            get
            {
                return this.transform;
            }
        }

        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            if (position.X < viewport.Width / 2)
            {
                centre.X = viewport.Width / 2;
            }

            else if (position.X > xOffset - (viewport.Width / 2))
            {
                centre.X = xOffset - (viewport.Width / 2);
            }
            else
            {
                centre.X = position.X;
            }

            if (position.Y < viewport.Height / 2)
            {
                centre.Y = viewport.Height / 2;
            }
            else if (position.Y > yOffset - (viewport.Height / 2))
            {
                centre.Y = yOffset - (viewport.Height / 2);
            }
            else
            {
                centre.Y = position.Y;
            }

            transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2), -centre.Y + (viewport.Height / 2), 0));

        }
    }
}
