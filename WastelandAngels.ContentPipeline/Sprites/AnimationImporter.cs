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
    [ContentImporter(".anim", DisplayName = "Animation Importer - WLA", DefaultProcessor = "PassThroughProcessor")]
    class AnimationImporter : ContentImporter<AnimationContent>
    {
        public override AnimationContent Import(string filename, ContentImporterContext context)
        {
            XDocument document = XDocument.Load(filename);

            string animationName = (string)document.Root.Attribute("name");

            int frameCount = (int)document.Root.Attribute("framecount");
            float[] frameDurations = new float[frameCount];

            string[] temp = document.Root.Value.Split(',');
            for (int i = 0; i < frameCount; ++i)
            {
                frameDurations[i] = Convert.ToSingle(temp[i]);
            }

            int startingIndex = (int)document.Root.Attribute("startingindex");

            return new AnimationContent(animationName, frameCount, frameDurations, startingIndex);
        }
    }
}
