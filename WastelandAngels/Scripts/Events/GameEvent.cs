using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework;
using WLA.GameComponents;

namespace WLA.Events
{
    class GameEvent : IEvent
    {
        protected enum EventState { CheckTrigger, ExecuteEvent };

        public int Layer { get; private set; }

        protected EventState state;

        public GameEvent(int layer)
        {
            Layer = layer;

            state = EventState.CheckTrigger;
        }

        public virtual void Update(GameTime gameTime, Level level)
        {
            if (state == EventState.CheckTrigger)
            {
                CheckEventTrigger(gameTime, level);
            }

            if (state == EventState.ExecuteEvent)
            {
                ExecuteEvent(gameTime, level);
            }
        }

        public virtual void CheckEventTrigger(GameTime gameTime, Level level)
        {

        }

        public virtual void ExecuteEvent(GameTime gameTime, Level level)
        {

        }
    }
}
