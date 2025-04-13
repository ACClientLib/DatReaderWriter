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
using DatReaderWriter.Types;
using DatReaderWriter.Lib;
using DatReaderWriter.Lib.Attributes;
using DatReaderWriter.Lib.IO;

namespace DatReaderWriter.DBObjs {
    /// <summary>
    /// DB_TYPE_NAME_FILTER_TABLE in the client.
    /// </summary>
    [DBObjType(typeof(NameFilterTable), DatFileType.Portal, DBObjType.NameFilterTable, DBObjHeaderFlags.HasId, 0x0E000020, 0x0E000020, 0x00000000)]
    public partial class NameFilterTable : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.NameFilterTable;

        public Dictionary<uint, NameFilterLanguageData> LanguageData = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            var _numNameFilterLanguageDatas = reader.ReadByte();
            var _numNameFilterLanguageDataBuckets = reader.ReadByte();
            for (var i=0; i < _numNameFilterLanguageDatas; i++) {
                var _key = reader.ReadUInt32();
                var _val = reader.ReadItem<NameFilterLanguageData>();
                LanguageData.Add(_key, _val);
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteByte((byte)LanguageData.Count());
            writer.WriteByte(1);
            foreach (var kv in LanguageData) {
                writer.WriteUInt32(kv.Key);
                writer.WriteItem<NameFilterLanguageData>(kv.Value);
            }
            return true;
        }

    }

}
