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
    public partial class SoundDesc : IDatObjType {
        public List<AmbientSTBDesc> STBDesc = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            var _numSTBDescs = reader.ReadUInt32();
            for (var i=0; i < _numSTBDescs; i++) {
                STBDesc.Add(reader.ReadItem<AmbientSTBDesc>());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32((uint)STBDesc.Count());
            foreach (var item in STBDesc) {
                writer.WriteItem<AmbientSTBDesc>(item);
            }
            return true;
        }

    }

}
