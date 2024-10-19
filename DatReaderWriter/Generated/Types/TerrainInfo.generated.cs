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
using ACClientLib.DatReaderWriter.Enums;
using ACClientLib.DatReaderWriter.IO;

namespace ACClientLib.DatReaderWriter.Types {
    public class TerrainInfo : IDatObjType {
        /// <summary>
        /// Road type
        /// </summary>
        public byte Road;

        /// <summary>
        /// Terrain type
        /// </summary>
        public TerrainType Type;

        /// <summary>
        /// Scenery type
        /// </summary>
        public ushort Scenery;

        /// <inheritdoc />
        public bool Unpack(DatFileReader reader) {
            var _rawValue = reader.ReadUInt16();
            Road = (byte)(_rawValue & 0x3);
            Type = (TerrainType)((_rawValue & 0x7C) >> 2);
            Scenery = (ushort)((_rawValue & 0xF800) >> 11);
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatFileWriter writer) {
            ushort _rawValue = default;
            _rawValue |= (ushort)((ushort)Road & 0x3);
            _rawValue |= (ushort)(((ushort)Type << 2) & 0x7C);
            _rawValue |= (ushort)(((ushort)Scenery << 11) & 0xF800);
            writer.WriteUInt16(_rawValue);
            return true;
        }

    }

}