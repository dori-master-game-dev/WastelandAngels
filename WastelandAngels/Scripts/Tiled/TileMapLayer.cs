using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using WLA.GameComponents;
using WLA.System;

namespace WLA.Tiled
{
    public class TileMapLayer
    {
        public int Identifier { get; private set; }

        public string Name { get; private set; }

        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public int Layer { get; private set; }
        public int DrawOrder { get; private set; }

        public TileMapLayerTile[,] Tiles { get; set; }

        public TileMapLayer(int id, string name, int columns, int rows, int layer, int drawOrder)
        {
            Identifier = id;

            Name = name;

            Columns = columns;
            Rows = rows;

            Layer = layer;
            DrawOrder = drawOrder;

            Tiles = new TileMapLayerTile[Columns, Rows];
        }

        public void SetTiles(string data, TileMapTileset[] tilesets)
        {
            string[] tilesData = data.Split(',');

            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    Tiles[j, i] = new TileMapLayerTile(Convert.ToInt32(tilesData[i * Columns + j]));
                    Tiles[j, i].SetTile(tilesets);
                }
            }
        }
    }
}
