using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using WLA.GameComponents;

namespace WLA.Events
{
    public interface IEvent
    {
        void Update(GameTime gameTime, Level level);
    }
}
