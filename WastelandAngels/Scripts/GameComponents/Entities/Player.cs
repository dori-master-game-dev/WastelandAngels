using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WLA.System;
using WLA.GameComponents.Sprites;

namespace WLA.GameComponents.Entities
{
    public class Player : Entity
    {
        public enum State { Idle, Running, Attacking };
        public State PlayerState { get; set; }

        private Directions[] circleDirectionsIndex;

        public CircleCollider SelectHitbox { get; private set; }
        public CircleCollider AttackHitbox { get; private set; }

        public bool Locked { get; set; }

        private float speed;

        public Player(int layer) : base(layer) { }

        public Player(Vector2 position, int layer) : base(position, layer) { }

        public Player(Vector2 position, int layer, Color tint) : base(position, layer, tint) { }

        public override void Initialize()
        {
            PlayerState = State.Idle;
            Facing = Directions.Front;

            circleDirectionsIndex = new Directions[]
            {
                Directions.Right,
                Directions.Back,
                Directions.Left,
                Directions.Front
            };

            Width = 16;
            Height = 16;

            Center = Position;

            Hitbox = new RectangleCollider(Position, new Vector2(0f, 0f), 16, 16, true);
            Hitbox.CreateTexture(Color.Blue);

            SelectHitbox = new CircleCollider(Hitbox.Position, new Vector2(0f, 12f), 12f, true);
            SelectHitbox.CreateTexture(Color.DarkGreen);

            AttackHitbox = new CircleCollider(Hitbox.Position + new Vector2(Hitbox.Width, Hitbox.Height) / 2f, 30f, true);
            AttackHitbox.CreateTexture(Color.DarkRed);
            
            Direction = Vector2.Zero;

            speed = 128f;

            base.Initialize();
        }

        public override void LoadContent()
        {
            spriteManager = ViewManager.content.Load<SpriteManager>("Entities/PlayerMetadata/SpriteManagerMetadata");

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        private float timeLeft;

        public override void Update(GameTime gameTime, List<Collider> tileMapColliders = null, List<Entity> entities = null)
        {
            if (!Locked)
            {
                ReadInput();
            }

            switch (PlayerState)
            {
                case State.Idle:
                    {
                        CheckDirection();
                        CheckState();

                        spriteManager.Update(gameTime);

                        Translate(Direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds, tileMapColliders, entities);

                        SelectHitbox.Position = Hitbox.Position;
                        AttackHitbox.Center = Hitbox.Position + new Vector2(12.5f, 15f);

                        break;
                    }

                case State.Running:
                    {
                        CheckDirection();
                        CheckState();

                        spriteManager.Update(gameTime);

                        Translate(Direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds, tileMapColliders, entities);

                        SelectHitbox.Position = Hitbox.Position;
                        AttackHitbox.Center = Hitbox.Position + new Vector2(12.5f, 15f);

                        break;
                    }

                case State.Attacking:
                    {
                        spriteManager.Update(gameTime);

                        timeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (timeLeft <= 0)
                        {
                            if (Direction != Vector2.Zero)
                            {
                                PlayerState = State.Running;
                            }
                            else
                            {
                                PlayerState = State.Idle;
                            }
                        }

                        if (AttackHitbox.Intersects(entities[0].Hitbox))
                        {
                            entities[0].Tint = Color.Red;
                        }

                        break;
                    }
            }
        }

        private bool debug = true;
        private int mode = 0;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (debug)
            {
                Hitbox.DebugDraw(spriteBatch);
                SelectHitbox.DebugDraw(spriteBatch);
                AttackHitbox.DebugDraw(spriteBatch);
            }
        }

        public void ReadInput()
        {
            if (InputManager.NewMouseState.LeftButton == ButtonState.Pressed && InputManager.OldMouseState.LeftButton == ButtonState.Released)
            {
                timeLeft = 0.5f;
                PlayerState = State.Attacking;

                Facing = circleDirectionsIndex[MouseCircleOctant()];

                for (int i = 0; i < 3; ++i)
                {
                    bool done = false;
                    for (int j = 0; j < 3; ++j)
                    {
                        if (i == 1 && j == 1)
                        {
                            continue;
                        }

                        if (directionsIndex[i, j] == Facing)
                        {
                            Direction = new Vector2(j - 1, i - 1);
                            SelectHitbox.Offset = new Vector2(6f, 9f) * Direction + Vector2.UnitY * 3f;
                            AttackHitbox.Offset = new Vector2(16f, 16f) * Direction;

                            done = true;

                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }
                }

                int state = (int)Facing;
                if (spriteManager.CurrentState != state)
                {
                    spriteManager.SelectState(state);
                }
            }

            if (InputManager.NewMouseState.MiddleButton == ButtonState.Pressed && InputManager.OldMouseState.MiddleButton == ButtonState.Released)
            {
                ++mode;
                mode %= 2;
            }

            if (InputManager.NewKeyboardState.IsKeyDown(Keys.OemPlus) && InputManager.OldKeyboardState.IsKeyUp(Keys.OemPlus))
            {
                ++Layer;
            }

            if (InputManager.NewKeyboardState.IsKeyDown(Keys.OemMinus) && InputManager.OldKeyboardState.IsKeyUp(Keys.OemMinus))
            {
                --Layer;
            }

            if (InputManager.NewKeyboardState.IsKeyDown(Keys.F) && InputManager.OldKeyboardState.IsKeyUp(Keys.F))
            {
                debug = !debug;
            }

            if (InputManager.NewMouseState.RightButton == ButtonState.Pressed)
            {
                speed = 256f;
                spriteManager.PlaybackSpeed = 1.5f;
            }
            else
            {
                speed = 128f;
                spriteManager.PlaybackSpeed = 1f;
            }

            if (InputManager.NewKeyboardState.IsKeyDown(Keys.W) && InputManager.OldKeyboardState.IsKeyUp(Keys.W))
            {
                Direction = new Vector2(Direction.X, -1f);
            }
            else if (!InputManager.NewKeyboardState.IsKeyDown(Keys.W))
            {
                Direction = new Vector2(Direction.X, InputManager.NewKeyboardState.IsKeyDown(Keys.S) ? 1f : 0f);
            }

            if (InputManager.NewKeyboardState.IsKeyDown(Keys.A) && InputManager.OldKeyboardState.IsKeyUp(Keys.A))
            {
                Direction = new Vector2(-1f, Direction.Y);
            }
            else if (!InputManager.NewKeyboardState.IsKeyDown(Keys.A))
            {
                Direction = new Vector2(InputManager.NewKeyboardState.IsKeyDown(Keys.D) ? 1f : 0f, Direction.Y);
            }

            if (InputManager.NewKeyboardState.IsKeyDown(Keys.S) && InputManager.OldKeyboardState.IsKeyUp(Keys.S))
            {
                Direction = new Vector2(Direction.X, 1f);
            }
            else if (!InputManager.NewKeyboardState.IsKeyDown(Keys.S))
            {
                Direction = new Vector2(Direction.X, InputManager.NewKeyboardState.IsKeyDown(Keys.W) ? -1f : 0f);
            }

            if (InputManager.NewKeyboardState.IsKeyDown(Keys.D) && InputManager.OldKeyboardState.IsKeyUp(Keys.D))
            {
                Direction = new Vector2(1f, Direction.Y);
            }
            else if (!InputManager.NewKeyboardState.IsKeyDown(Keys.D))
            {
                Direction = new Vector2(InputManager.NewKeyboardState.IsKeyDown(Keys.A) ? -1f : 0f, Direction.Y);
            }
        }

        private void CheckDirection()
        {
            if (Direction != Vector2.Zero)
            {
                SelectHitbox.Offset = new Vector2(6f, 9f) * Direction + Vector2.UnitY * 3f;
                AttackHitbox.Offset = new Vector2(16f, 16f) * Direction;
            }

            switch (mode)
            {
                case 0:
                    {
                        if (Direction != Vector2.Zero)
                        {
                            Facing = directionsIndex[(int)(Direction.Y + 1), (int)(Direction.X + 1)];
                        }

                        break;
                    }

                case 1:
                    {
                        Facing = circleDirectionsIndex[MouseCircleOctant() / 2];

                        break;
                    }
            }
        }

        private void CheckState()
        {
            PlayerState = (State)(Direction != Vector2.Zero ? 1 : 0);

            int state = (int)Facing + (int)PlayerState * 8;
            if (spriteManager.CurrentState != state)
            {
                spriteManager.SelectState(state);
            }
        }

        private int MouseCircleOctant()
        {
            float pi = (float)Math.PI;
            float quarter = pi / 4f;

            Vector2 delta = new Vector2(640f, 360f) - InputManager.NewMouseState.Position.ToVector2();
            float angle = ((float)Math.Atan2(delta.Y, -delta.X));

            angle = ((angle < 0 ? 2f * pi + angle : angle) + pi / 8f) % (pi * 2f);

            return (int)(angle / quarter);
        }
    }
}
