using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace WLA.ContentPipeline.Sprites
{
    class AnimationContent : ContentItem
    {
        public string AnimationName { get; }

        public int FrameCount { get; }
        public float[] FrameDurations { get; }

        public int StartingIndex { get; }

        public AnimationContent(string animationName, int frameCount, float[] frameDurations, int startingIndex)
        {
            AnimationName = animationName;

            FrameCount = frameCount;
            FrameDurations = frameDurations;

            StartingIndex = startingIndex;
        }
    }
}
