using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLA.ContentPipeline.Tiled
{
    public class TileMapTilesetContent
    {
        public string Name { get; private set; }

        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public TileMapTilesetContent(string name, int columns, int rows)
        {
            Name = name;

            Columns = columns;
            Rows = rows;
        }
    }
}
