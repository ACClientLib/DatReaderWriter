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
    /// DB_TYPE_UI_LAYOUT in the client.
    /// </summary>
    [DBObjType(typeof(LayoutDesc), DatFileType.Local, DBObjType.LayoutDesc, DBObjHeaderFlags.HasId, 0x21000000, 0x21FFFFFF, 0x00000000)]
    public partial class LayoutDesc : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.LayoutDesc;

        public uint Width;

        public uint Height;

        public Dictionary<uint, ElementDesc> Elements = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            Width = reader.ReadUInt32();
            Height = reader.ReadUInt32();
            var _elementsBucketSizeIndex = reader.ReadByte();
            var _numElements = reader.ReadCompressedUInt();
            for (var i=0; i < _numElements; i++) {
                var _key = reader.ReadUInt32();
                var _val = reader.ReadItem<ElementDesc>();
                Elements.Add(_key, _val);
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteUInt32(Width);
            writer.WriteUInt32(Height);
            writer.WriteByte(1);
            writer.WriteCompressedUInt((uint)Elements.Count());
            foreach (var kv in Elements) {
                writer.WriteUInt32(kv.Key);
                writer.WriteItem<ElementDesc>(kv.Value);
            }
            return true;
        }

    }

}
