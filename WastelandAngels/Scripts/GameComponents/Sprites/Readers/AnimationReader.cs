using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WLA.GameComponents.Sprites
{
    class AnimationReader : ContentTypeReader<Animation>
    {
        protected override Animation Read(ContentReader input, Animation existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }

            string name = input.ReadString();

            int frameCount = input.ReadInt32();

            float[] frameDurations = new float[frameCount];
            for (int i = 0; i < frameCount; ++i)
            {
                frameDurations[i] = input.ReadSingle();
            }

            int startingIndex = input.ReadInt32();

            return new Animation(name, frameCount, frameDurations, startingIndex);
        }
    }
}
