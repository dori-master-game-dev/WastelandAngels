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
    class AnimationContentWriter : ContentTypeWriter<AnimationContent>
    {
        protected override void Write(ContentWriter output, AnimationContent value)
        {
            output.Write(value.AnimationName);

            output.Write(value.FrameCount);
            for (int i = 0; i < value.FrameCount; ++i)
            {
                output.Write(value.FrameDurations[i]);
            }

            output.Write(value.StartingIndex);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform) => "WLA.GameComponents.Sprites.AnimationReader, WastelandAngels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
