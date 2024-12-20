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
using DatReaderWriter.Types;
using DatReaderWriter.Lib;
using DatReaderWriter.Lib.Attributes;
using DatReaderWriter.Lib.IO;

namespace DatReaderWriter.DBObjs {
    /// <summary>
    /// DB_TYPE_LAND_BLOCK in the client.
    /// </summary>
    [DBObjType(typeof(LandBlock), DatFileType.Cell, DBObjType.LandBlock, DBObjHeaderFlags.HasId, 0x00000000, 0x00000000, 0x0000FFFF)]
    public partial class LandBlock : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.LandBlock;

        public bool HasObjects;

        public TerrainInfo[] Terrain = [];

        public byte[] Height = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            HasObjects = reader.ReadBool();
            Terrain = new TerrainInfo[81];
            for (var i=0; i < 81; i++) {
                Terrain[i] = reader.ReadItem<TerrainInfo>();
            }
            Height = new byte[81];
            for (var i=0; i < 81; i++) {
                Height[i] = reader.ReadByte();
            }
            reader.Align(4);
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteBool(HasObjects);
            for (var i=0; i < Terrain.Count(); i++) {
                writer.WriteItem<TerrainInfo>(Terrain[i]);
            }
            for (var i=0; i < Height.Count(); i++) {
                writer.WriteByte(Height[i]);
            }
            writer.Align(4);
            return true;
        }

    }

}
