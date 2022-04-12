using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.GameComponents;

namespace WLA.Tiled
{
    public class TileMapTilesetTile
    {
        public int Identifier { get; private set; }

        public Texture2D Texture { get; private set; }

        public TileMapTilesetTile(int id, Texture2D tex)
        {
            Identifier = id;

            Texture = tex;
        }

        public void Draw(Vector2 position, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        }
    }
}
