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
    public partial class ObjectDesc : IDatObjType {
        public uint ObjectId;

        public Frame BaseLoc;

        public float Frequency;

        public float DisplaceX;

        public float DisplaceY;

        public float MinScale;

        public float MaxScale;

        public float MaxRotation;

        public float MinSlope;

        public float MaxSlope;

        public int Align;

        public int Orient;

        public uint WeenieObj;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            ObjectId = reader.ReadUInt32();
            BaseLoc = reader.ReadItem<Frame>();
            Frequency = reader.ReadSingle();
            DisplaceX = reader.ReadSingle();
            DisplaceY = reader.ReadSingle();
            MinScale = reader.ReadSingle();
            MaxScale = reader.ReadSingle();
            MaxRotation = reader.ReadSingle();
            MinSlope = reader.ReadSingle();
            MaxSlope = reader.ReadSingle();
            Align = reader.ReadInt32();
            Orient = reader.ReadInt32();
            WeenieObj = reader.ReadUInt32();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32(ObjectId);
            writer.WriteItem<Frame>(BaseLoc);
            writer.WriteSingle(Frequency);
            writer.WriteSingle(DisplaceX);
            writer.WriteSingle(DisplaceY);
            writer.WriteSingle(MinScale);
            writer.WriteSingle(MaxScale);
            writer.WriteSingle(MaxRotation);
            writer.WriteSingle(MinSlope);
            writer.WriteSingle(MaxSlope);
            writer.WriteInt32(Align);
            writer.WriteInt32(Orient);
            writer.WriteUInt32(WeenieObj);
            return true;
        }

    }

}
