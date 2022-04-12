using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace WLA.ContentPipeline.Tiled
{
    public class TileMapContent : ContentItem
    {
        public int Columns { get; }
        public int Rows { get; }
        public int Layers { get; }

        public string[] TilesetPaths { get; }
        public int[] FirstGids { get; }

        public TileMapLayerContent[] TileMapLayers { get; }
        public TileColliderContent[,,] TileColliders { get; }

        public TileMapContent(int columns, int rows, int layers, string[] tilesetPaths, int[] firstGids, TileMapLayerContent[] tileMapLayers, TileColliderContent[,,] colliders)
        {
            Columns = columns;
            Rows = rows;
            Layers = layers;

            TilesetPaths = tilesetPaths;
            FirstGids = firstGids;

            TileMapLayers = tileMapLayers;
            TileColliders = colliders;
        }
    }
}
