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
    /// DB_TYPE_STRING_TABLE in the client.
    /// </summary>
    [DBObjType(typeof(StringTable), DatFileType.Local, DBObjType.StringTable, DBObjHeaderFlags.HasId, 0x23000000, 0x24FFFFFF, 0x00000000)]
    public partial class StringTable : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.StringTable;

        /// <summary>
        /// This should always be 1 for English
        /// </summary>
        public uint Language;

        public byte Unknown;

        public Dictionary<uint, StringTableData> StringTableData = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            Language = reader.ReadUInt32();
            Unknown = reader.ReadByte();
            var _numEntries = reader.ReadCompressedUInt();
            for (var i=0; i < _numEntries; i++) {
                var _key = reader.ReadUInt32();
                var _val = reader.ReadItem<StringTableData>();
                StringTableData.Add(_key, _val);
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteUInt32(Language);
            writer.WriteByte(Unknown);
            writer.WriteCompressedUInt((uint)StringTableData.Count());
            foreach (var kv in StringTableData) {
                writer.WriteUInt32(kv.Key);
                writer.WriteItem<StringTableData>(kv.Value);
            }
            return true;
        }

    }

}
