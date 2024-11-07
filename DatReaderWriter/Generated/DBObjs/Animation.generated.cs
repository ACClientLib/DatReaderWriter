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
    /// DB_TYPE_ANIM in the client.
    /// </summary>
    [DBObjType(typeof(Animation), DatFileType.Portal, DBObjType.Animation, DBObjHeaderFlags.HasId, 0x03000000, 0x0300FFFF, 0x00000000)]
    public partial class Animation : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.Animation;

        public AnimationFlags Flags;

        public uint NumParts;

        public List<Frame> PosFrames = [];

        public List<AnimationFrame> PartFrames = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            Flags = (AnimationFlags)reader.ReadUInt32();
            NumParts = reader.ReadUInt32();
            var _numFrames = reader.ReadUInt32();
            if (Flags.HasFlag(AnimationFlags.PosFrames)) {
                for (var i=0; i < _numFrames; i++) {
                    PosFrames.Add(reader.ReadItem<Frame>());
                }
            }
            for (var i=0; i < _numFrames; i++) {
                var _val = reader.ReadItem<AnimationFrame>(NumParts);
                PartFrames.Add(_val);
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteUInt32((uint)Flags);
            writer.WriteUInt32(NumParts);
            writer.WriteUInt32((uint)PartFrames.Count());
            if (Flags.HasFlag(AnimationFlags.PosFrames)) {
                foreach (var item in PosFrames) {
                    writer.WriteItem<Frame>(item);
                }
            }
            foreach (var item in PartFrames) {
                writer.WriteItem<AnimationFrame>(item);
            }
            return true;
        }

    }

}
