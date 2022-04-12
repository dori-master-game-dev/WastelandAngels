using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace WLA.Tiled
{
    class TileMapTilesetReader : ContentTypeReader<TileMapTileset>
    {
        protected override TileMapTileset Read(ContentReader input, TileMapTileset existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }

            return new TileMapTileset(input.ReadString(), input.ReadInt32(), input.ReadInt32());
        }
    }
}
