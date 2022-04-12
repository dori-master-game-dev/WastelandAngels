using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework;
using WLA.GameComponents;
using WLA.Events.Conditions;

namespace WLA.Events
{
    public class ConditionalEvent : IEvent
    {
        public int Layer { get; private set; }
        private readonly ICondition[] conditions;
        public event EventHandler<WLAEventArgs> RaiseEventTriggered;

        public ConditionalEvent(int layer, params ICondition[] conditions)
        {
            Layer = layer;
            this.conditions = conditions;
        }

        public virtual void Update(GameTime gameTime, Level level)
        {
            foreach(ICondition condition in conditions)
            {
                if (condition.CheckCondition(level))
                {
                    OnEventTriggered(this, new WLAEventArgs());
                }
            }
        }

        protected virtual void OnEventTriggered(object sender, WLAEventArgs e)
        {
            RaiseEventTriggered?.Invoke(sender, e);
        }
    }

    public class WLAEventArgs : EventArgs
    {

    }
}
