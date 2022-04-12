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
    class SpriteManagerContentWriter : ContentTypeWriter<SpriteManagerContent>
    {
        protected override void Write(ContentWriter output, SpriteManagerContent value)
        {
            output.Write("../../Spritesheets/" + value.SheetName + "/" + value.SheetName + "MetaData");

            output.Write(value.Animated);

            output.Write(value.Count);

            if (value.Animated)
            {
                for (int i = 0; i < value.Count; ++i)
                {
                    output.Write("../../Animations/" + value.Animations[i]);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; ++i)
                {
                    output.Write(value.Sprites[i]);
                }
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform) => "WLA.GameComponents.Sprites.SpriteManagerReader, WastelandAngels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
