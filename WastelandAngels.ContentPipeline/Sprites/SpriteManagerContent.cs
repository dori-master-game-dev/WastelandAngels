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
    class SpriteManagerContent : ContentItem
    {
        public string SheetName { get; }

        public bool Animated { get; }

        public int Count { get; }

        public int[] Sprites { get; }
        public string[] Animations { get; }

        public SpriteManagerContent(string sheetName, int count, int[] sprites)
        {
            SheetName = sheetName;

            Animated = false;

            Count = count;

            Sprites = sprites;
        }

        public SpriteManagerContent(string sheetName, int count, string[] animations)
        {
            SheetName = sheetName;

            Animated = true;

            Count = count;

            Animations = animations;
        }
    }
}
