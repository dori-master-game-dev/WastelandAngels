using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLA.GameComponents;

namespace WLA.Tiled
{
    public class TileMapLayerTile
    {
        public int GlobalIdentifier { get; private set; }
        public int TileIdentifier { get; private set; }
        public int TilesetIdentifier { get; private set; }

        public TileMapLayerTile(int gid)
        {
            GlobalIdentifier = gid;
        }

        public void SetTile(TileMapTileset[] tilesets)
        {
            for (int i = tilesets.Count() - 1; i >= 0; --i)
            {
                if (GlobalIdentifier >= tilesets[i].FirstGid)
                {
                    TileIdentifier = GlobalIdentifier - tilesets[i].FirstGid;
                    TilesetIdentifier = i;

                    return;
                }
            }
        }
    }
}
