using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WLA.Tiled
{
    public class TileMapReader : ContentTypeReader<TileMap>
    {
        protected override TileMap Read(ContentReader input, TileMap existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }

            int columns = input.ReadInt32();
            int rows = input.ReadInt32();
            int layers = input.ReadInt32();

            int tilesetCount = input.ReadInt32();

            TileMapTileset[] tilesets = new TileMapTileset[tilesetCount];
            for (int i = 0; i < tilesetCount; ++i)
            {
                tilesets[i] = input.ReadExternalReference<TileMapTileset>();
                tilesets[i].FirstGid = input.ReadInt32();
            }

            int tileMapLayersCount = input.ReadInt32();

            TileMapLayer[] tileMapLayers = new TileMapLayer[tileMapLayersCount];
            for (int i = 0; i < tileMapLayersCount; ++i)
            {
                int id = input.ReadInt32();

                string name = input.ReadString();

                int layerColumns = input.ReadInt32();
                int layerRows = input.ReadInt32();

                int layer = input.ReadInt32();
                int drawOrder = input.ReadInt32();

                string data = input.ReadString();

                tileMapLayers[i] = new TileMapLayer(id, name, layerColumns, layerRows, layer, drawOrder);
                tileMapLayers[i].SetTiles(data, tilesets);
            }

            TileCollider[,,] tileColliders = new TileCollider[layers, columns, rows];
            for (int i = 0; i < layers; ++i)
            {
                for (int j = 0; j < rows; ++j)
                {
                    for (int k = 0; k < columns; ++k)
                    {
                        int layer = input.ReadInt32();

                        int tileX = input.ReadInt32();
                        int tileY = input.ReadInt32();

                        int tileCollidersCount = input.ReadInt32();

                        Vector2[] offsets = new Vector2[tileCollidersCount];

                        int[] widths = new int[tileCollidersCount];
                        int[] heights = new int[tileCollidersCount];
                        for (int l = 0; l < tileCollidersCount; ++l)
                        {
                            offsets[l] = input.ReadVector2();

                            widths[l] = input.ReadInt32();
                            heights[l] = input.ReadInt32();
                        }

                        tileColliders[i, k, j] = new TileCollider(layer, tileX, tileY, tileCollidersCount, offsets, widths, heights);
                    }
                }
            }

            return new TileMap(input.AssetName, Vector2.Zero, Color.White, columns, rows, layers, tilesets, tileMapLayers, tileColliders);
        }
    }
}
