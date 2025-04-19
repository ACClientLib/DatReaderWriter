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
    public partial class TabooTableEntry : IDatObjType {
        public uint Unknown1;

        public ushort Unknown2;

        public List<string> BannedPatterns = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            Unknown1 = reader.ReadUInt32();
            Unknown2 = reader.ReadUInt16();
            var _numBannedPatterns = reader.ReadUInt32();
            for (var i=0; i < _numBannedPatterns; i++) {
                BannedPatterns.Add(reader.ReadString());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32(Unknown1);
            writer.WriteUInt16(Unknown2);
            writer.WriteUInt32((uint)BannedPatterns.Count());
            foreach (var item in BannedPatterns) {
                writer.WriteString(item);
            }
            return true;
        }

    }

}
