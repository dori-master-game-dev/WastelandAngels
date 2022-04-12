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
    class SpritesheetReader : ContentTypeReader<Spritesheet>
    {
        protected override Spritesheet Read(ContentReader input, Spritesheet existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }

            Texture2D spritesheetTexture = input.ReadExternalReference<Texture2D>();

            int tileWidth = input.ReadInt32();
            int tileHeight = input.ReadInt32();

            Vector2 startingPosition = input.ReadVector2();

            int columns = input.ReadInt32();
            int count = input.ReadInt32();

            Vector2 spacing = input.ReadVector2();

            return new Spritesheet(spritesheetTexture, tileWidth, tileHeight, startingPosition, columns, count, spacing);
        }
    }
}
