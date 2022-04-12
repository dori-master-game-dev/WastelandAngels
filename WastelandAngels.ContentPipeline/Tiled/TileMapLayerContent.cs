using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLA.ContentPipeline.Tiled
{
    public class TileMapLayerContent
    {
        public int Identifier { get; private set; }

        public string Name { get; private set; }

        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public int Layer { get; private set; }
        public int DrawOrder { get; private set; }

        public string Data { get; private set; }

        public TileMapLayerContent(int identifier, string name, int columns, int rows, int layer, int drawOrder, string data)
        {
            Identifier = identifier;

            Name = name;

            Columns = columns;
            Rows = rows;

            Layer = layer;
            DrawOrder = drawOrder;

            Data = data;
        }
    }
}
