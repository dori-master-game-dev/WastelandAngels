using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using WLA.System;
using WLA.Events;
using WLA.Events.GameEvents;
using WLA.GameComponents.Entities;

namespace WLA.GameComponents
{
    class House : Level
    {
        private bool debug;

        public House(Vector2 position, int width, int height, Color baseColor, Vector2 initialPlayerPosition) : base("House", position, width, height, baseColor, initialPlayerPosition)
        {
        }

        public override void Initialize()
        {
            LevelCamera = new Camera(Vector2.Zero);

            Player = new Player(1);
            Player.Initialize();

            Player.Center = new Vector2(5.5f, 8.5f) * Constants.TILE_SIZE;

            Player.Facing = Entity.Directions.Back;

            eventManager = new EventManager(this);

            eventManager.AddEvent(new PlayerLevelEvent("3", new Vector2(5f, 10f) * Constants.TILE_SIZE - Vector2.UnitX, 1, new Level1(Vector2.Zero, Width, Height, Color.Black, new Vector2(32.5f, 37.5f) * Constants.TILE_SIZE)));

            debug = true;

            base.Initialize();
        }

        public override void LoadContent()
        {
            Player.LoadContent();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Map.Update(gameTime);

            Player.Update(gameTime, Map.GetNearbyColliders(Player, 2));

            LevelCamera.Follow(Player);

            if (InputManager.NewKeyboardState.IsKeyDown(Keys.F) && InputManager.OldKeyboardState.IsKeyUp(Keys.F))
            {
                debug = !debug;
            }

            eventManager.UpdateEvents(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, LevelCamera.Transform);

            for (int i = 0; i < Map.Layers; ++i)
            {
                for (int j = 0; j < Map.Rows; ++j)
                {
                    Map.Draw(i, j, spriteBatch);

                    if (Player.Layer == i && Player.Position.Y > j * Constants.TILE_SIZE && Player.Position.Y <= (j + 1) * Constants.TILE_SIZE)
                    {
                        Player.Draw(spriteBatch);
                    }
                }
            }

            if (debug)
            {
                foreach (Collider col in Map.GetNearbyColliders(Player, 2))
                {
                    col.DebugDraw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
