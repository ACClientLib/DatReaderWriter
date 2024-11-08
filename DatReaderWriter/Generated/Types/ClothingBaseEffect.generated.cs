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
    public partial class ClothingBaseEffect : IDatObjType {
        public List<CloObjectEffect> CloObjectEffects = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            var _numCloObjectEffects = reader.ReadUInt32();
            for (var i=0; i < _numCloObjectEffects; i++) {
                CloObjectEffects.Add(reader.ReadItem<CloObjectEffect>());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32((uint)CloObjectEffects.Count());
            foreach (var item in CloObjectEffects) {
                writer.WriteItem<CloObjectEffect>(item);
            }
            return true;
        }

    }

}
