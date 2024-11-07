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
    public partial class AmbientSoundDesc : IDatObjType {
        public uint SType;

        public float Volume;

        public float BaseChance;

        public float MinRate;

        public float MaxRate;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            SType = reader.ReadUInt32();
            Volume = reader.ReadSingle();
            BaseChance = reader.ReadSingle();
            MinRate = reader.ReadSingle();
            MaxRate = reader.ReadSingle();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32(SType);
            writer.WriteSingle(Volume);
            writer.WriteSingle(BaseChance);
            writer.WriteSingle(MinRate);
            writer.WriteSingle(MaxRate);
            return true;
        }

    }

}
