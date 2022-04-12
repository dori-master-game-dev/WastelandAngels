using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using WLA.GameComponents;
using WLA.Events.GameEvents;

namespace WLA.Events
{
    public class EventManager
    {
        private List<IEvent> Events { get; set; }

        private readonly Level level;

        public EventManager(Level level)
        {
            this.level = level;

            Events = new List<IEvent>();
        }

        public void UpdateEvents(GameTime gameTime)
        {
            foreach (IEvent ev in Events)
            {
                ev.Update(gameTime, level);
            }
        }

        public void AddEvent(IEvent ev)
        {
            Events.Add(ev);
        }
    }
}
