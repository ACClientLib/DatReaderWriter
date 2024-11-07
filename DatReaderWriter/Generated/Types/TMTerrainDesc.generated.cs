//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
//                                                            //
//                          WARNING                           //
//                                                            //
//           DO NOT MAKE LOCAL CHANGES TO THIS FILE           //
//               EDIT THE .tt TEMPLATE INSTEAD                //
//                                                            //
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//


using System;
using System.Numerics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using DatReaderWriter.Enums;
using DatReaderWriter.Lib;
using DatReaderWriter.Lib.Attributes;
using DatReaderWriter.Lib.IO;

namespace DatReaderWriter.Types {
    public partial class TMTerrainDesc : IDatObjType {
        public TerrainTextureType TerrainType;

        public TerrainTex TerrainTex;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            TerrainType = (TerrainTextureType)reader.ReadInt32();
            TerrainTex = reader.ReadItem<TerrainTex>();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteInt32((int)TerrainType);
            writer.WriteItem<TerrainTex>(TerrainTex);
            return true;
        }

    }

}
