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
    public partial class SWVertex : IDatObjType {
        public Vector3 Origin;

        public Vector3 Normal;

        public List<Vec2Duv> UVs = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            var _numUVs = reader.ReadUInt16();
            Origin = reader.ReadVector3();
            Normal = reader.ReadVector3();
            for (var i=0; i < _numUVs; i++) {
                UVs.Add(reader.ReadItem<Vec2Duv>());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt16((ushort)UVs.Count());
            writer.WriteVector3(Origin);
            writer.WriteVector3(Normal);
            foreach (var item in UVs) {
                writer.WriteItem<Vec2Duv>(item);
            }
            return true;
        }

    }

}
