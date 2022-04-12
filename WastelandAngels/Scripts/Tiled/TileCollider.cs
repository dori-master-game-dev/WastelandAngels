using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using WLA.System;
using WLA.GameComponents;

namespace WLA.Tiled
{
    public class TileCollider
    {
        public int Layer { get; private set; }

        public int TileX { get; private set; }
        public int TileY { get; private set; }

        public Vector2 Position { get; private set; }

        public int Count { get; private set; }

        public Collider[] TileColliders { get; private set; }

        public TileCollider(int layer, int tileX, int tileY, int count, Vector2[] offsets, int[] widths, int[] heights)
        {
            Layer = layer;

            TileX = tileX;
            TileY = tileY;

            Position = new Vector2(TileX, TileY) * Constants.TILE_SIZE;

            Count = count;

            TileColliders = new Collider[count];
            for (int i = 0; i < count; ++i)
            {
                TileColliders[i] = new RectangleCollider(Position + offsets[i], widths[i], heights[i], true);
                TileColliders[i].CreateTexture(Color.Blue);
            }
        }

        public bool Intersects(Collider target)
        {
            foreach (Collider col in TileColliders)
            {
                if (col.Intersects(target))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
