using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.System;

namespace WLA.GameComponents
{
    public abstract class Collider
    {
        public enum Shape { Rectangle, Circle };

        public Shape ColliderShape { get; }

        private Vector2 position;
        public Vector2 Position { get => position + Offset; set => position = value; }
        public Vector2 Offset { get; set; }

        public bool Collision { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        protected Texture2D debugTexture;

        public Collider(Shape shape, Vector2 position, int width, int height, bool collision) : this(shape, position, Vector2.Zero, width, height, collision) { }

        public Collider(Shape shape, Vector2 position, Vector2 offset, int width, int height, bool collision)
        {
            ColliderShape = shape;

            Position = position;
            Offset = offset;

            Width = width;
            Height = height;

            Collision = collision;
        }

        public abstract void CreateTexture(Color color);

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            if (debugTexture != null)
            {
                spriteBatch.Draw(debugTexture, Position, Color.White);
            }
        }

        public abstract bool Intersects(Collider col);

        public static bool Intersects(Collider col1, Collider col2)
        {
            return col1.Intersects(col2);
        }
    }
}
