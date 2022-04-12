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
using WLA.GameComponents.Entities;
using WLA.GameComponents.Sprites;
using WLA.Tiled;
using WLA.Events;
using WLA.Events.GameEvents;

namespace WLA.GameComponents
{
    public class Level1 : Level
    {
        private bool debug = true;

        private CircleCollider circleCollider;

        public Level1(Vector2 position, int width, int height, Vector2 initialPlayerPosition) : base("Level16x16", position, width, height, initialPlayerPosition)
        {

        }

        public Level1(Vector2 position, int width, int height, Color baseColor, Vector2 initialPlayerPosition) : base("Level16x16", position, width, height, baseColor, initialPlayerPosition)
        {

        }

        public override void Initialize()
        {
            LevelCamera = new Camera(Vector2.Zero);

            Player = new Player(initialPlayerPosition, 1);
            Player.Initialize();

            Debug.Print("{0}, {1}", initialPlayerPosition, Player.Center);

            enemy = new EnemyTest(1) { Center = new Vector2(28.5f, 40.5f) * Constants.TILE_SIZE };
            enemy.Initialize();

            circleCollider = new CircleCollider(new Vector2(45.5f, 35.5f) * Constants.TILE_SIZE, 12f, true);
            circleCollider.CreateTexture(Color.DarkGreen);

            eventManager = new EventManager(this);

            eventManager.AddEvent(new EnityLayerEvent("0", new Vector2(928f, 960f), 4, 1));
            eventManager.AddEvent(new EnityLayerEvent("1", new Vector2(928f, 896f), 1, 4));
            eventManager.AddEvent(new PlayerLevelEvent("2", new Vector2(32f, 36f) * Constants.TILE_SIZE, 1, new House(Vector2.Zero, Width, Height, Color.Black, new Vector2(5.5f, 8.5f) * Constants.TILE_SIZE)));

            base.Initialize();
        }

        public override void LoadContent()
        {
            Player.LoadContent();
            enemy.LoadContent();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Map.Update(gameTime);

            Player.Update(gameTime, Map.GetNearbyColliders(Player, 2), new List<Entity> { enemy });

            enemy.Update(gameTime, Map.GetNearbyColliders(enemy, 2), new List<Entity> { Player });

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

                    if (enemy.Layer == i && enemy.Position.Y > j * Constants.TILE_SIZE && enemy.Position.Y <= (j + 1) * Constants.TILE_SIZE)
                    {
                        enemy.Draw(spriteBatch);
                    }
                }
            }

            if (debug)
            {
                foreach (Collider col in Map.GetNearbyColliders(Player, 2))
                {
                    col.DebugDraw(spriteBatch);
                }

                circleCollider.DebugDraw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
