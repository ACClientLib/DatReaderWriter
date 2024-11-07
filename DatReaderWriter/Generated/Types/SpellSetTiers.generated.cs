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
    public partial class SpellSetTiers : IDatObjType {
        public List<uint> Spells = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            var _numSpells = reader.ReadInt32();
            for (var i=0; i < _numSpells; i++) {
                Spells.Add(reader.ReadUInt32());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteInt32((int)Spells.Count());
            foreach (var item in Spells) {
                writer.WriteUInt32(item);
            }
            return true;
        }

    }

}
