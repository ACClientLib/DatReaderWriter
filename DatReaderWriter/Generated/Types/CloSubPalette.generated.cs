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
    public partial class CloSubPalette : IDatObjType {
        public List<CloSubPaletteRange> Ranges = [];

        public uint PaletteSet;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            var _numRanges = reader.ReadUInt32();
            for (var i=0; i < _numRanges; i++) {
                Ranges.Add(reader.ReadItem<CloSubPaletteRange>());
            }
            PaletteSet = reader.ReadUInt32();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32((uint)Ranges.Count());
            foreach (var item in Ranges) {
                writer.WriteItem<CloSubPaletteRange>(item);
            }
            writer.WriteUInt32(PaletteSet);
            return true;
        }

    }

}
