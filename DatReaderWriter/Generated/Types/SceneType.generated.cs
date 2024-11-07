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
    public partial class SceneType : IDatObjType {
        public uint StbIndex;

        public List<uint> Scenes = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            StbIndex = reader.ReadUInt32();
            var _numScenes = reader.ReadUInt32();
            for (var i=0; i < _numScenes; i++) {
                Scenes.Add(reader.ReadUInt32());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32(StbIndex);
            writer.WriteUInt32((uint)Scenes.Count());
            foreach (var item in Scenes) {
                writer.WriteUInt32(item);
            }
            return true;
        }

    }

}
