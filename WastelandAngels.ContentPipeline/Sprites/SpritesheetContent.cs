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
    class SpritesheetContent : ContentItem
    {
        public string SpritesheetName { get; }

        public int TileWidth { get; }
        public int TileHeight { get; }

        public Vector2 StartPosition { get; }

        public int Columns { get; }
        public int Count { get; }

        public Vector2 Spacing { get; }

        public SpritesheetContent(string spritesheetName, int tileWidth, int tileHeight, Vector2 startPosition, int columns, int count, Vector2 spacing)
        {
            SpritesheetName = spritesheetName;

            TileWidth = tileWidth;
            TileHeight = tileHeight;

            StartPosition = startPosition;

            Columns = columns;
            Count = count;

            Spacing = spacing;
        }
    }
}
