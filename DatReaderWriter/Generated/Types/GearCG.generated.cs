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
    public partial class GearCG : IDatObjType {
        public string Name;

        public uint ClothingTable;

        public uint WeenieDefault;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            Name = reader.ReadString();
            ClothingTable = reader.ReadUInt32();
            WeenieDefault = reader.ReadUInt32();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteString(Name);
            writer.WriteUInt32(ClothingTable);
            writer.WriteUInt32(WeenieDefault);
            return true;
        }

    }

}
