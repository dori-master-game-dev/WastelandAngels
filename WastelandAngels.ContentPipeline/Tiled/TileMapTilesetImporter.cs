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
    [ContentImporter(".tsx", DisplayName = "Tileset Importer - WLA", DefaultProcessor = "PassThroughProcessor")]
    public class TileMapTilesetImporter : ContentImporter<TileMapTilesetContent>
    {
        public override TileMapTilesetContent Import(string filename, ContentImporterContext context)
        {
            XDocument document = XDocument.Load(filename);

            int columns = (int)document.Root.Attribute("columns");
            int rows = (int)document.Root.Attribute("tilecount") / columns;

            return new TileMapTilesetContent("Pictures/" + filename.Split('/').Last().Split('.')[0], columns, rows);
        }
    }
}
