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
    /// DB_TYPE_CHAR_GEN_0 in the client.
    /// </summary>
    [DBObjType(typeof(CharGen), DatFileType.Portal, DBObjType.CharGen, DBObjHeaderFlags.HasId, 0x0E000002, 0x0E000002, 0x00000000)]
    public partial class CharGen : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.CharGen;

        public uint Unknown;

        /// <summary>
        /// A list of starting areas available during character creation
        /// </summary>
        public List<StartingArea> StartingAreas = [];

        public byte UnknownByte;

        public Dictionary<uint, HeritageGroupCG> HeritageGroups = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            Unknown = reader.ReadUInt32();
            var _numStarterAreas = reader.ReadCompressedUInt();
            for (var i=0; i < _numStarterAreas; i++) {
                StartingAreas.Add(reader.ReadItem<StartingArea>());
            }
            UnknownByte = reader.ReadByte();
            var _numHeritageGroups = reader.ReadCompressedUInt();
            for (var i=0; i < _numHeritageGroups; i++) {
                var _key = reader.ReadUInt32();
                var _val = reader.ReadItem<HeritageGroupCG>();
                HeritageGroups.Add(_key, _val);
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteUInt32(Unknown);
            writer.WriteCompressedUInt((uint)StartingAreas.Count());
            foreach (var item in StartingAreas) {
                writer.WriteItem<StartingArea>(item);
            }
            writer.WriteByte(UnknownByte);
            writer.WriteCompressedUInt((uint)HeritageGroups.Count());
            foreach (var kv in HeritageGroups) {
                writer.WriteUInt32(kv.Key);
                writer.WriteItem<HeritageGroupCG>(kv.Value);
            }
            return true;
        }

    }

}
