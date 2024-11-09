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
    public partial class CloSubPalEffect : IDatObjType {
        public uint Icon;

        public List<CloSubPalette> CloSubPalettes = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            Icon = reader.ReadUInt32();
            var _numCloSubPalettes = reader.ReadUInt32();
            for (var i=0; i < _numCloSubPalettes; i++) {
                CloSubPalettes.Add(reader.ReadItem<CloSubPalette>());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32(Icon);
            writer.WriteUInt32((uint)CloSubPalettes.Count());
            foreach (var item in CloSubPalettes) {
                writer.WriteItem<CloSubPalette>(item);
            }
            return true;
        }

    }

}
