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
    public class EnemyTest : Entity
    {
        public enum State
        {
            IdleFront, IdleBack, IdleLeft, IdleRight, IdleFrontLeft, IdleFrontRight, IdleBackLeft, IdleBackRight,
            RunningFront, RunningBack, RunningLeft, RunningRight, RunningFrontLeft, RunningFrontRight, RunningBackLeft, RunningBackRight
        };

        public State EnemyState { get; private set; }

        private float speed;

        private Tuple<int, int, float>[] script;

        public EnemyTest(int layer) : base(Vector2.Zero, layer)
        {

        }

        public EnemyTest(Vector2 position, int layer) : base(position, layer)
        {

        }

        public EnemyTest(Vector2 position, int layer, Color tint) : base(position, layer, tint)
        {

        }

        public override void Initialize()
        {
            EnemyState = State.IdleFront;

            Width = 41;
            Height = 59;

            Hitbox = new RectangleCollider(Position, new Vector2(8f, 26f), 25, 30, true);
            Hitbox.CreateTexture(Color.Blue);

            speed = 128f;

            //script = new Tuple<int, int, float>[]
            //{
            //    new Tuple<int, int, float>(0, 1, 0.1f),
            //    new Tuple<int, int, float>(-1, 1, 0.1f),
            //    new Tuple<int, int, float>(-1, 0, 0.1f),
            //    new Tuple<int, int, float>(-1, -1, 0.1f),
            //    new Tuple<int, int, float>(0, -1, 0.1f),
            //    new Tuple<int, int, float>(1, -1, 0.1f),
            //    new Tuple<int, int, float>(1, 0, 0.1f),
            //    new Tuple<int, int, float>(1, 1, 0.1f)
            //};

            script = new Tuple<int, int, float>[]
            {
                new Tuple<int, int, float>(0, 0, 1f)
            };

            Direction = new Vector2(script[0].Item1, script[0].Item2);

            timeLeft = script[0].Item3;
            current = 0;

            base.Initialize();
        }

        private float timeLeft;
        private int current;

        private void ExecuteScript(GameTime gameTime)
        {
            timeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeLeft <= 0)
            {
                ++current;
                current %= script.Length;

                Direction = new Vector2(script[current].Item1, script[current].Item2);

                timeLeft = script[current].Item3;
            }
        }

        public override void LoadContent()
        {
            spriteManager = ViewManager.content.Load<SpriteManager>("Entities/EnemyMetadata/SpriteManagerMetadata");

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, List<Collider> tileMapColliders = null, List<Entity> entities = null)
        {
            ExecuteScript(gameTime);

            CheckDirection();
            CheckState();

            if (InputManager.NewKeyboardState.IsKeyDown(Keys.F) && InputManager.OldKeyboardState.IsKeyUp(Keys.F))
            {
                debug = !debug;
            }

            Translate(Direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds, tileMapColliders, entities);

            base.Update(gameTime);
        }

        private bool debug = true;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (debug)
                Hitbox.DebugDraw(spriteBatch);
        }

        private void CheckDirection()
        {

            if (Direction == Vector2.Zero)
            {
                return;
            }

            if (Direction.Y == 1f)
            {
                if (Direction.X == 1f)
                {
                    Facing = Directions.FrontRight;
                }
                else if (Direction.X == -1f)
                {
                    Facing = Directions.FrontLeft;
                }
                else
                {
                    Facing = Directions.Front;
                }
            }
            else if (Direction.Y == -1f)
            {
                if (Direction.X == 1f)
                {
                    Facing = Directions.BackRight;
                }
                else if (Direction.X == -1f)
                {
                    Facing = Directions.BackLeft;
                }
                else
                {
                    Facing = Directions.Back;
                }
            }
            else if (Direction.X == 1f)
            {
                Facing = Directions.Right;
            }
            else if (Direction.X == -1f)
            {
                Facing = Directions.Left;
            }
        }

        private void CheckState()
        {
            EnemyState = (State)(Direction != Vector2.Zero ? 1 : 0);

            int state = (int)Facing + (int)EnemyState * 8;
            if (spriteManager.CurrentState != state)
            {
                spriteManager.SelectState(state);
            }
        }
    }
}
