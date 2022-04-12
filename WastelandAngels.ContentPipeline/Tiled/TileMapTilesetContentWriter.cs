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

namespace WLA.ContentPipeline.Tiled
{
    [ContentTypeWriter]
    class TileMapTilesetContentWriter : ContentTypeWriter<TileMapTilesetContent>
    {
        protected override void Write(ContentWriter output, TileMapTilesetContent value)
        {
            output.Write(value.Name);
            output.Write(value.Columns);
            output.Write(value.Rows);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform) => "WLA.Tiled.TileMapTilesetReader, WastelandAngels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
