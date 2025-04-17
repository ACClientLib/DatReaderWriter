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
    public partial class PhysicsScriptTableData : IDatObjType {
        public List<ScriptAndModData> Scripts = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            var _numScripts = reader.ReadUInt32();
            for (var i=0; i < _numScripts; i++) {
                Scripts.Add(reader.ReadItem<ScriptAndModData>());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32((uint)Scripts.Count());
            foreach (var item in Scripts) {
                writer.WriteItem<ScriptAndModData>(item);
            }
            return true;
        }

    }

}
