using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.System;
using WLA.GameComponents;
using WLA.GameComponents.Entities;

namespace WLA.Events.GameEvents
{
    class EnityLayerEvent : GameEvent
    {
        public Vector2 Position { get; private set; }
        public int TargetLayer { get; private set; }

        private readonly Collider col;
        private Entity target;

        public EnityLayerEvent(string id, Vector2 position, int layer, int targetLayer) : base(layer)
        {
            Position = position;

            TargetLayer = targetLayer;

            col = new RectangleCollider(position, Constants.TILE_SIZE, Constants.TILE_SIZE, true);
        }

        public override void Update(GameTime gameTime, Level level)
        {
            base.Update(gameTime, level);
        }

        public override void CheckEventTrigger(GameTime gameTime, Level level)
        {
            if (level.Player.Hitbox.Intersects(col) && level.Player.Layer == Layer)
            {
                state = EventState.ExecuteEvent;
                target = level.Player;

                return;
            }

            if (level.enemy.Hitbox.Intersects(col) && level.enemy.Layer == Layer)
            {
                state = EventState.ExecuteEvent;
                target = level.enemy;

                return;
            }

            base.CheckEventTrigger(gameTime, level);
        }

        public override void ExecuteEvent(GameTime gameTime, Level level)
        {
            target.Layer = TargetLayer;
            target = null;

            state = EventState.CheckTrigger;

            base.ExecuteEvent(gameTime, level);
        }
    }
}
