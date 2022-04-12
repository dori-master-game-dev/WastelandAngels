using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WLA.GameComponents.Sprites
{
    public class SpriteManagerReader : ContentTypeReader<SpriteManager>
    {
        protected override SpriteManager Read(ContentReader input, SpriteManager existingInstance)
        {
            Spritesheet sheet = input.ReadExternalReference<Spritesheet>();

            bool animated = input.ReadBoolean();

            int count = input.ReadInt32();

            if (animated)
            {
                Animation[] animations = new Animation[count];
                for (int i = 0; i < count; ++i)
                {
                    animations[i] = input.ReadExternalReference<Animation>();
                }

                return new SpriteManager(sheet, count, animations);
            }

            int[] sprites = new int[count];
            for (int i = 0; i < count; ++i)
            {
                sprites[i] = input.ReadInt32();
            }

            return new SpriteManager(sheet, count, sprites);
        }
    }
}
