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
    [ContentImporter(".smr", DisplayName = "Sprite Manager Importer - WLA", DefaultProcessor = "PassThroughProcessor")]
    class SpriteManagerImporter : ContentImporter<SpriteManagerContent>
    {
        public override SpriteManagerContent Import(string filename, ContentImporterContext context)
        {
            XDocument document = XDocument.Load(filename);

            string sheetName = (string)document.Root.Attribute("name");

            bool animated = (bool)document.Root.Attribute("animated");

            int count = (int)document.Root.Attribute("count");

            if (animated)
            {
                string[] animations = document.Root.Value.Split(',');
                //animations[0] = "PlayerIdleFront";

                return new SpriteManagerContent(sheetName, count, animations);
            }

            int[] sprites = new int[count];

            string[] temp = document.Root.Value.Split(',');
            for (int i = 0; i < count; ++i)
            {
                sprites[i] = Convert.ToInt32(temp[i]);
            }

            return new SpriteManagerContent(sheetName, count, sprites);
        }
    }
}
