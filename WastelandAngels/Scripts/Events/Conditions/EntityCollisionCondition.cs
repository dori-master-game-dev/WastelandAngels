using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLA.GameComponents;

namespace WLA.Events.Conditions
{
    public class EntityCollisionCondition : ICondition
    {
        private readonly int layer;
        private readonly Collider collisionDetector;

        public EntityCollisionCondition(int layer, Collider collisionDetector)
        {
            this.layer = layer;
            this.collisionDetector = collisionDetector;
        }

        public bool CheckCondition(Level level)
        {
            return level.Player.Layer == layer && collisionDetector.Intersects(level.Player.Hitbox);
        }
    }
}
