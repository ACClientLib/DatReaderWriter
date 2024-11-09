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
    /// DB_TYPE_WAVE in the client.
    /// </summary>
    [DBObjType(typeof(Wave), DatFileType.Portal, DBObjType.Wave, DBObjHeaderFlags.HasId, 0x0A000000, 0x0A00FFFF, 0x00000000)]
    public partial class Wave : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.Wave;

        public byte[] Header = [];

        public byte[] Data = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            var _headerSize = reader.ReadInt32();
            var _dataSize = reader.ReadInt32();
            Header = new byte[_headerSize];
            for (var i=0; i < _headerSize; i++) {
                Header[i] = reader.ReadByte();
            }
            Data = new byte[_dataSize];
            for (var i=0; i < _dataSize; i++) {
                Data[i] = reader.ReadByte();
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteInt32((int)Header.Count());
            writer.WriteInt32((int)Data.Count());
            for (var i=0; i < Header.Count(); i++) {
                writer.WriteByte(Header[i]);
            }
            for (var i=0; i < Data.Count(); i++) {
                writer.WriteByte(Data[i]);
            }
            return true;
        }

    }

}
