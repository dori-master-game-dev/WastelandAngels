using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace WLA.ContentPipeline.Sprites
{
    [ContentImporter(".sst", DisplayName = "Spritesheet Importer - WLA", DefaultProcessor = "PassThroughProcessor")]
    class SpritesheetImporter : ContentImporter<SpritesheetContent>
    {
        public override SpritesheetContent Import(string filename, ContentImporterContext context)
        {
            XDocument document = XDocument.Load(filename);

            string spritesheetName = (string)document.Root.Element("image").Attribute("name");

            int tileWidth = (int)document.Root.Attribute("tilewidth");
            int tileHeight = (int)document.Root.Attribute("tileheight");

            Vector2 startingPosition = new Vector2((int)document.Root.Element("startingposition").Attribute("x"), (int)document.Root.Element("startingposition").Attribute("y"));

            int columns = (int)document.Root.Attribute("columns");
            int count = (int)document.Root.Attribute("count");

            Vector2 spacing = new Vector2((int)document.Root.Element("spacing").Attribute("x"), (int)document.Root.Element("spacing").Attribute("y"));

            return new SpritesheetContent(spritesheetName, tileWidth, tileHeight, startingPosition, columns, count, spacing);
        }
    }
}
