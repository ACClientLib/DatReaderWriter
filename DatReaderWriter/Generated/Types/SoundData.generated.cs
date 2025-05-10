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
    public partial class SoundData : IDatObjType {
        public List<SoundEntry> Entries = [];

        public int Unknown;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            var _numEntries = reader.ReadUInt32();
            for (var i=0; i < _numEntries; i++) {
                Entries.Add(reader.ReadItem<SoundEntry>());
            }
            Unknown = reader.ReadInt32();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32((uint)Entries.Count());
            foreach (var item in Entries) {
                writer.WriteItem<SoundEntry>(item);
            }
            writer.WriteInt32(Unknown);
            return true;
        }

    }

}
