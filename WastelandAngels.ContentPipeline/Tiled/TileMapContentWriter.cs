using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace WLA.ContentPipeline.Tiled
{
    [ContentTypeWriter]
    class TileMapContentWriter : ContentTypeWriter<TileMapContent>
    {
        protected override void Write(ContentWriter output, TileMapContent value)
        {
            output.Write(value.Columns);
            output.Write(value.Rows);
            output.Write(value.Layers);

            int tilesetCount = value.TilesetPaths.Count();

            output.Write(tilesetCount);
            for (int i = 0; i < tilesetCount; ++i)
            {
                output.Write(value.TilesetPaths[i]);
                output.Write(value.FirstGids[i]);
            }

            int tileMapLayersCount = value.TileMapLayers.Count();

            output.Write(tileMapLayersCount);
            for (int i = 0; i < tileMapLayersCount; ++i)
            {
                output.Write(value.TileMapLayers[i].Identifier);

                output.Write(value.TileMapLayers[i].Name);

                output.Write(value.TileMapLayers[i].Columns);
                output.Write(value.TileMapLayers[i].Rows);

                output.Write(value.TileMapLayers[i].Layer);
                output.Write(value.TileMapLayers[i].DrawOrder);

                output.Write(value.TileMapLayers[i].Data);
            }

            for (int i = 0; i < value.Layers; ++i)
            {
                for (int j = 0; j < value.Rows; ++j)
                {
                    for (int k = 0; k < value.Columns; ++k)
                    {
                        output.Write(value.TileColliders[i, k, j].Layer);

                        output.Write(value.TileColliders[i, k, j].TileX);
                        output.Write(value.TileColliders[i, k, j].TileY);

                        int tileCollidersCount = value.TileColliders[i, k, j].Count;

                        output.Write(value.TileColliders[i, k, j].Count);
                        for (int l = 0; l < tileCollidersCount; ++l)
                        {
                            output.Write(value.TileColliders[i, k, j].Offsets[l]);

                            output.Write(value.TileColliders[i, k, j].Widths[l]);
                            output.Write(value.TileColliders[i, k, j].Heights[l]);
                        }
                    }
                }
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform) => "WLA.Tiled.TileMapReader, WastelandAngels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
