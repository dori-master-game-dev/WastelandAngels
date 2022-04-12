using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.System;
using WLA.System.IO;
using WLA.GameComponents;
using WLA.GameComponents.Entities;

namespace WLA.Tiled
{
    public class TileMap
    {
        public string MapName { get; set; }

        public Vector2 Position { get; set; }

        public Color Tint { get; set; }

        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public int Layers { get; private set; }

        public TileMapTileset[] Tilesets { get; private set; }
        public TileMapLayer[] TileMapLayers { get; private set; }
        public TileCollider[,,] TileColliders { get; private set; }

        public TileMap(string mapName, Vector2 position, Color tint, int columns, int rows, int layers, TileMapTileset[] tilesets, TileMapLayer[] tileMapLayers, TileCollider[,,] tileColliders)
        {
            MapName = mapName;

            Position = position;

            Tint = tint;

            Columns = columns;
            Rows = rows;
            Layers = layers;

            Tilesets = tilesets;
            TileMapLayers = tileMapLayers;

            TileColliders = tileColliders;

            // Array.Sort(TileMapLayers, delegate (TileMapLayer a, TileMapLayer b) { if (a.Layer == b.Layer) return a.DrawOrder.CompareTo(b.DrawOrder); return a.Layer.CompareTo(b.Layer); });
        }

        public void Initialize()
        {

        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Layers; ++i)
            {
                Draw(i, spriteBatch);
            }
        }

        public void Draw(int layer, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Rows; ++i)
            {
                Draw(layer, i, spriteBatch);
            }
        }

        public void Draw(int layer, int row, SpriteBatch spriteBatch)
        {
            foreach (TileMapLayer tilemapLayer in TileMapLayers)
            {
                if (tilemapLayer.Layer < layer)
                    continue;

                if (tilemapLayer.Layer > layer)
                    break;

                for (int i = 0; i < tilemapLayer.Columns; ++i)
                {
                    TileMapLayerTile tile = tilemapLayer.Tiles[i, row];

                    if (tile.GlobalIdentifier == 0)
                        continue;

                    TileMapTileset tileset = Tilesets[tile.TilesetIdentifier];
                    try
                    {
                        TileMapTilesetTile tilesetTile = tileset.Tiles[tile.TileIdentifier % tileset.Columns, tile.TileIdentifier / tileset.Columns];
                        tilesetTile.Draw(Position + new Vector2(i, row) * Constants.TILE_SIZE, spriteBatch);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Debug.Print("{0} {1} {2} {3} {4}", tile.TileIdentifier % tileset.Columns, tile.TileIdentifier / tileset.Columns, tileset.Name, tile.TileIdentifier, tile.GlobalIdentifier);
                    }
                }
            }
        }

        public List<Collider> GetNearbyColliders(Entity entity, int thickness)
        {
            if (entity.Layer < 0 || entity.Layer >= Layers)
            {
                return new List<Collider>();
            }

            List<Collider> colliders = new List<Collider>();

            int columns = (int)(entity.Hitbox.Position.X - Position.X) / Constants.TILE_SIZE;
            int rows = (int)(entity.Hitbox.Position.Y - Position.Y) / Constants.TILE_SIZE;

            Vector2 leftCorner = new Vector2(Math.Max(columns - thickness, 0), Math.Max(rows - thickness, 0));
            Vector2 rightCorner = new Vector2(Math.Min(columns + entity.Hitbox.Width / Constants.TILE_SIZE + thickness, Columns - 1), Math.Min(rows + entity.Hitbox.Height / Constants.TILE_SIZE + thickness, Rows - 1));

            for (int i = (int)leftCorner.Y; i <= rightCorner.Y; ++i)
            {
                for (int j = (int)leftCorner.X; j <= rightCorner.X; ++j)
                {
                    colliders.AddRange(TileColliders[entity.Layer, j, i].TileColliders);
                }
            }

            return colliders;
        }
    }
}
