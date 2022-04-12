using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WLA.GameComponents.Sprites
{
    public class Spritesheet
    {
        public Texture2D SpritesheetTexture { get; }

        public int Width { get; }
        public int Height { get; }

        public int TileWidth { get; }
        public int TileHeight { get; }

        public Vector2 StartPosition { get; }

        public int Columns { get; }
        public int Count { get; }

        public Vector2 Spacing { get; }

        public Spritesheet(Texture2D spritesheetTexture, int tileWidth, int tileHeight, Vector2 startPosition, int columns, int count, Vector2 spacing)
        {
            SpritesheetTexture = spritesheetTexture;

            Width = SpritesheetTexture.Width;
            Height = SpritesheetTexture.Height;

            TileWidth = tileWidth;
            TileHeight = tileHeight;

            StartPosition = startPosition;

            Columns = columns;
            Count = count;

            Spacing = spacing;
        }
    }
}
