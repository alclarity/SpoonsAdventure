using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content.Pipeline;

namespace MapPipeline
{
    [ContentImporter(".tide", DefaultProcessor = "", DisplayName = "Map Importer")]
    class MapImporter : ContentImporter<Map>
    {
        public override Map Import(string filename, ContentImporterContext context)
        {
            Map map = new Map();
            return map;
        }
    }
}
