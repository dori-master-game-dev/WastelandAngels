using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.System;
using WLA.GameComponents.Sprites;

namespace WLA.GameComponents
{
    public abstract class GameObject
    {
        private Vector2 position;
        public Vector2 Position { get => position; set { position = value; Hitbox.Position = Position; } }

        public Vector2 Center { get => Position + new Vector2(Width, Height) / 2f; set => Position = value - new Vector2(Width, Height) / 2f; }

        public Collider Hitbox { get; protected set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Color Tint { get; set; }

        protected SpriteManager spriteManager;

        public GameObject()
        {
            Hitbox = new RectangleCollider(Vector2.Zero, Vector2.Zero, Width, Height, true);

            Position = Vector2.Zero;

            Tint = Color.White;
        }

        public GameObject(Vector2 position, Color tint)
        {
            Hitbox = new RectangleCollider(Vector2.Zero, Width, Height, true);

            Position = position;

            Tint = tint;

        }

        public GameObject(Vector2 position, Collider hitbox, Color tint)
        {
            Hitbox = hitbox;

            Position = position;

            Tint = tint;
        }

        public abstract void Initialize();

        public abstract void LoadContent();

        public abstract void UnloadContent();

        public virtual void Update(GameTime gameTime)
        {
            spriteManager.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteManager.Draw(spriteBatch, Position, Tint);
        }

        public Vector2 GetCenter()
        {
            return Position + new Vector2(Width, Height) / 2f;
        }
    }
}
