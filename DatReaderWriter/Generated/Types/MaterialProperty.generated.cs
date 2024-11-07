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
    public partial class MaterialProperty : IDatObjType {
        public uint NameId;

        public RMDataType DataType;

        public uint DataLength;

        public uint DataLength2;

        public ushort DataLength3;

        public byte DataLength4;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            NameId = reader.ReadUInt32();
            DataType = (RMDataType)reader.ReadUInt16();
            reader.Align(4);
            DataLength = reader.ReadUInt32();
            DataLength2 = reader.ReadUInt32();
            DataLength3 = reader.ReadUInt16();
            DataLength4 = reader.ReadByte();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32(NameId);
            writer.WriteUInt16((ushort)DataType);
            writer.Align(4);
            writer.WriteUInt32(DataLength);
            writer.WriteUInt32(DataLength2);
            writer.WriteUInt16(DataLength3);
            writer.WriteByte(DataLength4);
            return true;
        }

    }

}
