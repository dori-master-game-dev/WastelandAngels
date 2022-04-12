using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace WLA.ContentPipeline.Tiled
{
    [ContentImporter(".tmx", DisplayName = "Tilemap Importer - WLA", DefaultProcessor = "PassThroughProcessor")]
    public class TileMapImporter : ContentImporter<TileMapContent>
    {
        public override TileMapContent Import(string filename, ContentImporterContext context)
        {
            int index;

            XDocument document = XDocument.Load(filename);

            int columns = (int)document.Root.Attribute("width");
            int rows = (int)document.Root.Attribute("height");
            int layers = (int)document.Root.Element("properties").Element("property").Attribute("value");

            int length = document.Root.Elements("tileset").Count();
            string[] tilesetPaths = new string[length];
            int[] firstGids = new int[length];

            index = 0;
            foreach (XElement tileset in document.Root.Elements("tileset"))
            {
                string source = (string)tileset.Attribute("source");
                int firstGid = (int)tileset.Attribute("firstgid");

                tilesetPaths[index] = "../Tilesets/" + source.Split('/').Last().Split('.')[0];
                firstGids[index] = firstGid;

                ++index;
            }

            int count = 0;
            foreach (XElement group in document.Root.Element("group").Elements("group"))
            {
                count += group.Elements("layer").Count();
            }

            TileMapLayerContent[] tileMapLayers = new TileMapLayerContent[count];
            TileColliderContent[,,] tileColliders = new TileColliderContent[layers, columns, rows];

            index = 0;
            int layer = 0;
            foreach (XElement group in document.Root.Element("group").Elements("group"))
            {
                int drawOrder = 0;

                foreach (XElement layerElement in group.Elements("layer"))
                {
                    int id = (int)layerElement.Attribute("id");

                    string name = (string)layerElement.Attribute("name");

                    int layerColumns = (int)layerElement.Attribute("width");
                    int layerRows = (int)layerElement.Attribute("height");

                    string data = layerElement.Element("data").Value;

                    tileMapLayers[index++] = new TileMapLayerContent(id, name, layerColumns, layerRows, layer, drawOrder++, data);
                }

                for (int i = 0; i < rows; ++i)
                {
                    for (int j = 0; j < columns; ++j)
                    {
                        tileColliders[layer, j, i] = new TileColliderContent(j, i);
                    }
                }

                int tileSize = 16;
                foreach (XElement col in group.Element("objectgroup").Elements("object"))
                {
                    int x = (int)col.Attribute("x");
                    int y = (int)col.Attribute("y");
                    int width = (int)col.Attribute("width");
                    int height = (int)col.Attribute("height");

                    int tileX = x / tileSize;
                    int tileY = y / tileSize;

                    tileColliders[layer, tileX, tileY].AddNewCollider(new Vector2(x % tileSize, y % tileSize), width, height);
                }

                ++layer;
            }

            return new TileMapContent(columns, rows, layers, tilesetPaths, firstGids, tileMapLayers, tileColliders);
        }

        /*
        public static TileMapContent BuildMapContent(string path)
        {
            XDocument document = TileMapLoader.LoadDocument(path);

            int columns = (int)document.Root.Attribute("width");
            int rows = (int)document.Root.Attribute("height");
            int layers = (int)document.Root.Element("properties").Element("property").Attribute("value");

            int index = 0;
            TileMapTileset[] tilesets = new TileMapTileset[document.Root.Elements("tileset").Count()];
            foreach (XElement tileset in document.Root.Elements("tileset"))
            {
                int firstGid = (int)tileset.Attribute("firstgid");
                string source = (string)tileset.Attribute("source");

                XElement newTileset = TileMapLoader.LoadDocument("Content/Assets/Pictures/Tilesets/" + source.Split('/').Last()).Root;

                string tilesetName = (string)newTileset.Attribute("name");

                int tilesetColumns = (int)newTileset.Attribute("columns");
                int tilesetRows = (int)newTileset.Attribute("tilecount") / tilesetColumns;

                tilesets[index] = new TileMapTileset(firstGid, tilesetName, tilesetColumns, tilesetRows, "Tilesets/" + tilesetName);

                foreach (XElement tile in newTileset.Elements("tile"))
                {
                    int id = (int)tile.Attribute("id");
                }

                ++index;
            }

            int count = 0;
            foreach (XElement group in document.Root.Element("group").Elements("group"))
            {
                count += group.Elements("layer").Count();
            }

            index = 0;
            int tilemapLayer = 0;
            TileMapLayer[] tilemapLayers = new TileMapLayer[count];
            List<Collider>[,,] colliders = new List<Collider>[layers, columns, rows];
            foreach (XElement group in document.Root.Element("group").Elements("group"))
            {
                int tilemapDrawOrder = 0;

                for (int i = 0; i < rows; ++i)
                {
                    for (int j = 0; j < columns; ++j)
                    {
                        colliders[tilemapLayer, j, i] = new List<Collider>();
                    }
                }

                foreach (XElement layer in group.Elements("layer"))
                {
                    int id = (int)layer.Attribute("id");

                    string name = (string)layer.Attribute("name");

                    int layerColumns = (int)layer.Attribute("width");
                    int layerRows = (int)layer.Attribute("height");

                    tilemapLayers[index] = new TileMapLayer(id, name, layerColumns, layerRows, tilemapLayer, tilemapDrawOrder++);

                    string data = layer.Element("data").Value;

                    tilemapLayers[index++].SetTiles(data, tilesets);
                }

                foreach (XElement col in group.Element("objectgroup").Elements("object"))
                {
                    try
                    {
                        int x = (int)col.Attribute("x");
                        int y = (int)col.Attribute("y");
                        int width = (int)col.Attribute("width");
                        int height = (int)col.Attribute("height");

                        colliders[tilemapLayer, x / Constants.TILE_SIZE, y / Constants.TILE_SIZE].Add(new Collider(new Vector2(x, y), width, height, true));
                        colliders[tilemapLayer, x / Constants.TILE_SIZE, y / Constants.TILE_SIZE].Last().CreateTexture();
                    }
                    catch (Exception e)
                    {
                        Debug.Print(tilemapLayers[index - 1].Name);
                    }
                }

                ++tilemapLayer;
            }

            return new TileMapContent(columns, rows, layers, tilesets, tilemapLayers, colliders);
        }
        */
    }
}
