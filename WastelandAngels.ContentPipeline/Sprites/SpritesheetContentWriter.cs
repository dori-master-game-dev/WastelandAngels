using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace WLA.ContentPipeline.Sprites
{
    [ContentTypeWriter]
    class SpritesheetContentWriter : ContentTypeWriter<SpritesheetContent>
    {
        protected override void Write(ContentWriter output, SpritesheetContent value)
        {
            output.Write(value.SpritesheetName);

            output.Write(value.TileWidth);
            output.Write(value.TileHeight);

            output.Write(value.StartPosition);

            output.Write(value.Columns);
            output.Write(value.Count);

            output.Write(value.Spacing);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform) => "WLA.GameComponents.Sprites.SpritesheetReader, WastelandAngels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
