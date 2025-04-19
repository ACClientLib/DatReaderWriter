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
    public partial class CInputMap : IDatObjType {
        public List<QualifiedControl> Mappings = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            var _numMappings = reader.ReadUInt32();
            for (var i=0; i < _numMappings; i++) {
                Mappings.Add(reader.ReadItem<QualifiedControl>());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32((uint)Mappings.Count());
            foreach (var item in Mappings) {
                writer.WriteItem<QualifiedControl>(item);
            }
            return true;
        }

    }

}
