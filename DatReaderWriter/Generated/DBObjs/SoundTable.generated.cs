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
    /// DB_TYPE_STABLE in the client.
    /// </summary>
    [DBObjType(typeof(SoundTable), DatFileType.Portal, DBObjType.SoundTable, DBObjHeaderFlags.HasId, 0x20000000, 0x2000FFFF, 0x00000000)]
    public partial class SoundTable : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.SoundTable;

        public int Unknown;

        public Dictionary<uint, SoundHashData> Hashes = [];

        public Dictionary<Sound, SoundData> Sounds = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            Unknown = reader.ReadInt32();
            var _numHashes = reader.ReadInt32();
            for (var i=0; i < _numHashes; i++) {
                var _key = reader.ReadUInt32();
                var _val = reader.ReadItem<SoundHashData>();
                Hashes.Add(_key, _val);
            }
            var _numSounds = reader.ReadInt32();
            for (var i=0; i < _numSounds; i++) {
                var _key = (Sound)reader.ReadUInt32();
                var _val = reader.ReadItem<SoundData>();
                Sounds.Add(_key, _val);
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteInt32(Unknown);
            writer.WriteInt32((int)Hashes.Count());
            foreach (var kv in Hashes) {
                writer.WriteUInt32(kv.Key);
                writer.WriteItem<SoundHashData>(kv.Value);
            }
            writer.WriteInt32((int)Sounds.Count());
            foreach (var kv in Sounds) {
                writer.WriteUInt32((uint)kv.Key);
                writer.WriteItem<SoundData>(kv.Value);
            }
            return true;
        }

    }

}
