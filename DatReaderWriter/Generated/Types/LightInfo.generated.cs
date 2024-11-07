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
    public partial class LightInfo : IDatObjType {
        public Frame ViewSpaceLocation;

        public ColorARGB Color;

        public float Intensity;

        public float Falloff;

        public float ConeAngle;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            ViewSpaceLocation = reader.ReadItem<Frame>();
            Color = reader.ReadItem<ColorARGB>();
            Intensity = reader.ReadSingle();
            Falloff = reader.ReadSingle();
            ConeAngle = reader.ReadSingle();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteItem<Frame>(ViewSpaceLocation);
            writer.WriteItem<ColorARGB>(Color);
            writer.WriteSingle(Intensity);
            writer.WriteSingle(Falloff);
            writer.WriteSingle(ConeAngle);
            return true;
        }

    }

}
