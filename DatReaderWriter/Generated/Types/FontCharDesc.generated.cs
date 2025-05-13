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
    public partial class FontCharDesc : IDatObjType {
        public ushort Unicode;

        public ushort OffsetX;

        public ushort OffsetY;

        public byte Width;

        public byte Height;

        public sbyte HorizontalOffsetBefore;

        public sbyte HorizontalOffsetAfter;

        public sbyte VerticalOffsetBefore;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            Unicode = reader.ReadUInt16();
            OffsetX = reader.ReadUInt16();
            OffsetY = reader.ReadUInt16();
            Width = reader.ReadByte();
            Height = reader.ReadByte();
            HorizontalOffsetBefore = reader.ReadSByte();
            HorizontalOffsetAfter = reader.ReadSByte();
            VerticalOffsetBefore = reader.ReadSByte();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt16(Unicode);
            writer.WriteUInt16(OffsetX);
            writer.WriteUInt16(OffsetY);
            writer.WriteByte(Width);
            writer.WriteByte(Height);
            writer.WriteSByte(HorizontalOffsetBefore);
            writer.WriteSByte(HorizontalOffsetAfter);
            writer.WriteSByte(VerticalOffsetBefore);
            return true;
        }

    }

}
