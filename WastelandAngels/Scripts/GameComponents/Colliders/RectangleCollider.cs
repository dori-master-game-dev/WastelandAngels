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
    public class RectangleCollider : Collider
    {
        public RectangleCollider(Vector2 position, int width, int height, bool collision) : base(Shape.Rectangle, position, width, height, collision) { }

        public RectangleCollider(Vector2 position, Vector2 offset, int width, int height, bool collision) : base(Shape.Rectangle, position, offset, width, height, collision) { }

        public override void CreateTexture(Color color)
        {
            debugTexture = new Texture2D(ViewManager.graphics.GraphicsDevice, Width, Height);
            Color[] data = new Color[Width * Height];

            for (int i = 0; i < Width * Height; ++i)
            {
                data[i] = new Color(color, 63f / 255f);
            }

            debugTexture.SetData(data);
        }

        public override bool Intersects(Collider col)
        {
            switch (col.ColliderShape)
            {
                case Shape.Rectangle:
                    {
                        return Collision && col.Collision && Position.X > col.Position.X - Width && Position.X < col.Position.X + col.Width && Position.Y > col.Position.Y - Height && Position.Y < col.Position.Y + col.Height;
                    }

                case Shape.Circle:
                    {
                        float x = col.Position.X + (col.Width / 2f);
                        float y = col.Position.Y + (col.Height / 2f);

                        if (Position.X > x)
                        {
                            x = Position.X;
                        }
                        else if (Position.X + Width < x)
                        {
                            x = Position.X + Width;
                        }

                        if (Position.Y > y)
                        {
                            y = Position.Y;
                        }
                        else if (Position.Y + Height < y)
                        {
                            y = Position.Y + Height;
                        }

                        float dx = col.Position.X + (col.Width / 2f) - x;
                        float dy = col.Position.Y + (col.Height / 2f) - y;
                        float dist = (float)Math.Sqrt((dx * dx) + (dy * dy));

                        return Collision && col.Collision && dist <= col.Width / 2f;
                    }

                default:
                    return false;
            }
        }
    }
}
