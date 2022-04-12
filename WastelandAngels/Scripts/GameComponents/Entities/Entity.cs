using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WLA.GameComponents.Entities
{
    public abstract class Entity : GameObject
    {
        public enum Directions { Front, Back, Left, Right, FrontLeft, FrontRight, BackLeft, BackRight, None };
        public Directions Facing { get; set; }

        protected Directions[,] directionsIndex;

        public Vector2 Direction { get; protected set; }

        public int Layer { get; set; }

        public Entity(int layer)
        {
            Layer = layer;
        }

        public Entity(Vector2 position, int layer) : base(position, Color.White)
        {
            Layer = layer;
        }

        public Entity(Vector2 position, int layer, Color tint) : base(position, tint)
        {
            Layer = layer;
        }

        public Entity(Vector2 position, Collider hitbox, int layer, Color tint) : base(position, hitbox, tint)
        {
            Layer = layer;
        }

        public override void Initialize()
        {
            directionsIndex = new Directions[,]
            {
                { Directions.BackLeft, Directions.Back, Directions.BackRight },
                { Directions.Left, Directions.None, Directions.Right },
                { Directions.FrontLeft, Directions.Front, Directions.FrontRight },
            };

            Facing = Directions.Front;
        }

        public override void LoadContent()
        {

        }

        public override void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime, List<Collider> tileMapColliders = null, List<Entity> entities = null)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected void Translate(Vector2 movement, List<Collider> tileMapColliders = null, List<Entity> entities = null)
        {
            Vector2 steps = new Vector2((int)Math.Ceiling(Math.Abs(movement.X)), (int)Math.Ceiling(Math.Abs(movement.Y)));

            bool done = false;
            for (int i = 0; i < steps.X; ++i)
            {
                float amount = Math.Sign(movement.X) * (Math.Abs(movement.X) - i < 1 ? Math.Abs(movement.X) - i : 1f);

                Position += Vector2.UnitX * amount;
                if (tileMapColliders != null)
                {
                    foreach (Collider col in tileMapColliders)
                    {
                        if (Hitbox.Intersects(col))
                        {
                            Position -= Vector2.UnitX * amount;

                            Position = new Vector2((int)Position.X + (movement.X > 0f ? 1f : 0f), Position.Y);
                            if (Hitbox.Intersects(col))
                            {
                                Position -= Math.Sign(movement.X) * Vector2.UnitX;
                            }

                            done = true;

                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }
                }

                if (entities != null)
                {
                    foreach (Entity entity in entities)
                    {
                        if (Hitbox.Intersects(entity.Hitbox))
                        {
                            Position -= Vector2.UnitX * amount;

                            Position = new Vector2((int)Position.X + (movement.X > 0f ? 1f : 0f), Position.Y);
                            if (Hitbox.Intersects(entity.Hitbox))
                            {
                                Position -= Math.Sign(movement.X) * Vector2.UnitX;
                            }

                            done = true;

                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }
                }
            }

            done = false;
            for (int i = 0; i < steps.Y; ++i)
            {
                float amount = Math.Sign(movement.Y) * (Math.Abs(movement.Y) - i < 1 ? Math.Abs(movement.Y) - i : 1f);

                Position += Vector2.UnitY * amount;
                if (tileMapColliders != null)
                {
                    foreach (Collider col in tileMapColliders)
                    {
                        if (Hitbox.Intersects(col))
                        {
                            Position -= Vector2.UnitY * amount;

                            Position = new Vector2(Position.X, (int)Position.Y + (movement.Y > 0f ? 1f : 0f));
                            if (Hitbox.Intersects(col))
                            {
                                Position -= Math.Sign(movement.Y) * Vector2.UnitY;
                            }

                            done = true;

                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }
                }

                if (entities != null)
                {
                    foreach (Entity entity in entities)
                    {
                        if (Hitbox.Intersects(entity.Hitbox))
                        {
                            Position -= Vector2.UnitY * amount;

                            Position = new Vector2(Position.X, (int)Position.Y + (movement.Y > 0f ? 1f : 0f));
                            if (Hitbox.Intersects(entity.Hitbox))
                            {
                                Position -= Math.Sign(movement.Y) * Vector2.UnitY;
                            }

                            done = true;

                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }
                }
            }
        }
    }
}
