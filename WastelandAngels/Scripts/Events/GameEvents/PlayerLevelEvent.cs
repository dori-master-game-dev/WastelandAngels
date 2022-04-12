using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WLA.System;
using WLA.GameComponents;

namespace WLA.Events.GameEvents
{
    class PlayerLevelEvent : GameEvent
    {
        public Vector2 Position { get; private set; }
        public Level TargetLevel { get; private set; }

        private readonly Collider col;

        public PlayerLevelEvent(string id, Vector2 position, int layer, Level targetLevel) : base(layer)
        {
            Position = position;

            TargetLevel = targetLevel;

            col = new RectangleCollider(position, Constants.TILE_SIZE, Constants.TILE_SIZE, true);
        }

        public override void Update(GameTime gameTime, Level level)
        {
            base.Update(gameTime, level);
        }

        public override void CheckEventTrigger(GameTime gameTime, Level level)
        {
            if (InputManager.NewKeyboardState.IsKeyDown(Keys.Space) && InputManager.OldKeyboardState.IsKeyUp(Keys.Space))
            {
                if (level.Player.SelectHitbox.Intersects(col) && level.Player.Layer == Layer)
                {
                    state = EventState.ExecuteEvent;
                }
            }

            base.CheckEventTrigger(gameTime, level);
        }

        public override void ExecuteEvent(GameTime gameTime, Level level)
        {
            level.EditViews(EventCommands.RemoveAddView, TargetLevel);

            state = EventState.CheckTrigger;

            base.ExecuteEvent(gameTime, level);
        }
    }
}
