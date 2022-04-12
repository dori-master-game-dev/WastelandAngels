using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.System;

namespace WLA.GameComponents
{
    public class CircleCollider : Collider
    {
        public Vector2 Center { get => Position + Vector2.One * Radius; set => Position = value - Vector2.One * Radius; }
        public float Radius { get => Width / 2; }

        public CircleCollider(Vector2 center, float radius, bool collision) : this(center, Vector2.Zero, radius, collision) { }

        public CircleCollider(Vector2 center, Vector2 offset, float radius, bool collision) : base(Shape.Circle, center - Vector2.One * radius, offset, (int)(radius * 2f), (int)(radius * 2f), collision) { }

        public override void CreateTexture(Color color)
        {
            debugTexture = new Texture2D(ViewManager.graphics.GraphicsDevice, Width, Height);
            Color[] data = new Color[Width * Height];

            float sinus = 0.70710678118f;

            int range = (int)(Radius / (sinus * 2));

            for (int i = (int)Radius; i >= range; --i)
            {
                int j = (int)Math.Sqrt((Radius * Radius) - (i * i));
                for (int k = -j; k <= j; ++k)
                {
                    data[(int)(Radius + i - 1) * Width + (int)(Radius + k)] = new Color(color, 63f / 255f);
                    data[(int)(Radius + k) * Width + (int)(Radius + i - 1)] = new Color(color, 63f / 255f);
                    data[(int)(Radius - i) * Width + (int)(Radius - k)] = new Color(color, 63f / 255f);
                    data[(int)(Radius - k) * Width + (int)(Radius - i)] = new Color(color, 63f / 255f);
                }
            }

            range = (int)(Radius * sinus);
            for (int i = (int)Radius - range + 1; i < (int)Radius + range; i++)
            {
                for (int j = (int)Radius - range + 1; j < (int)Radius + range; j++)
                {
                    data[j * Width + i] = new Color(color, 63f / 255f);
                }
            }

            debugTexture.SetData(data);
        }

        public override bool Intersects(Collider col)
        {
            switch (col.ColliderShape)
            {
                case Shape.Circle:
                    {
                        float dx = Center.X - (col.Position.X + (col.Width / 2f));
                        float dy = Center.Y - (col.Position.Y + (col.Height / 2f));
                        float dist = (float)Math.Sqrt((dx * dx) + (dy * dy));

                        return Collision && col.Collision && dist <= Radius + (col.Width / 2f);
                    }

                case Shape.Rectangle:
                    {
                        float x = Center.X;
                        float y = Center.Y;

                        if (col.Position.X > x)
                        {
                            x = col.Position.X;
                        }
                        else if (col.Position.X + col.Width < x)
                        {
                            x = col.Position.X + col.Width;
                        }

                        if (col.Position.Y > y)
                        {
                            y = col.Position.Y;
                        }
                        else if (col.Position.Y + col.Height < y)
                        {
                            y = col.Position.Y + col.Height;
                        }

                        float dx = Center.X - x;
                        float dy = Center.Y - y;
                        float dist = (float)Math.Sqrt((dx * dx) + (dy * dy));

                        return Collision && col.Collision && dist <= Radius;
                    }

                default:
                    return false;
            }
        }
    }
}
