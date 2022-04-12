using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.System;
using WLA.GameComponents;

namespace WLA.Tiled
{
    public class TileMapTileset
    {
        public int FirstGid { get; set; }

        public string Name { get; private set; }

        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public TileMapTilesetTile[,] Tiles { get; private set; }

        public TileMapTileset(string name, int columns, int rows)
        {
            Name = name;

            Columns = columns;
            Rows = rows;

            Tiles = new TileMapTilesetTile[columns, rows];

            CreateTiles(name);
        }

        private void CreateTiles(string path)
        {
            Texture2D tileset = ViewManager.content.Load<Texture2D>(path);
            Color[] data = new Color[Columns * Rows * Constants.TILE_SIZE * Constants.TILE_SIZE];
            tileset.GetData(data);

            Color[,] data2d = new Color[Columns * Constants.TILE_SIZE, Rows * Constants.TILE_SIZE];
            for (int i  = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    for (int y = 0; y < Constants.TILE_SIZE; ++y)
                    {
                        for (int x = 0; x < Constants.TILE_SIZE; ++x)
                        {
                            data2d[j * Constants.TILE_SIZE + x, i * Constants.TILE_SIZE + y] = data[i * Columns * Constants.TILE_SIZE * Constants.TILE_SIZE + j * Constants.TILE_SIZE + y * Columns * Constants.TILE_SIZE + x];
                        }
                    }
                }
            }

            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    Texture2D tex = new Texture2D(ViewManager.graphics.GraphicsDevice, Constants.TILE_SIZE, Constants.TILE_SIZE);
                    data = new Color[Constants.TILE_SIZE * Constants.TILE_SIZE];
                    
                    for (int k = 0; k < Constants.TILE_SIZE * Constants.TILE_SIZE; ++k)
                    {
                        data[k] = data2d[j * Constants.TILE_SIZE + k % Constants.TILE_SIZE, i * Constants.TILE_SIZE + k / Constants.TILE_SIZE];
                    }

                    tex.SetData(data);
                    Tiles[j, i] = new TileMapTilesetTile(i * Constants.TILE_SIZE + j, tex);
                }
            }
        }
    }
}
