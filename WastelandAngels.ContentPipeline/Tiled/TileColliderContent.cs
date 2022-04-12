using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WLA.ContentPipeline.Tiled
{
    public class TileColliderContent
    {
        public int Layer { get; private set; }

        public int TileX { get; private set; }
        public int TileY { get; private set; }

        public int Count { get; private set; }

        public List<Vector2> Offsets { get; private set; }

        public List<int> Widths { get; private set; }
        public List<int> Heights { get; private set; }

        public TileColliderContent(int tileX, int tileY)
        {
            TileX = tileX;
            TileY = tileY;

            Count = 0;

            Offsets = new List<Vector2>();

            Widths = new List<int>();
            Heights = new List<int>();
        }

        public void AddNewCollider(Vector2 offset, int width, int height)
        {
            Offsets.Add(offset);

            Widths.Add(width);
            Heights.Add(height);

            ++Count;
        }
    }
}
