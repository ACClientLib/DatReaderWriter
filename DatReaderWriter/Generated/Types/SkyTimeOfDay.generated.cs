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
    public partial class SkyTimeOfDay : IDatObjType {
        public float Begin;

        public float DirBright;

        public float DirHeading;

        public float DirPitch;

        public ColorARGB DirColor;

        public float AmbBright;

        public ColorARGB AmbColor;

        public float MinWorldFog;

        public float MaxWorldFog;

        public ColorARGB WorldFogColor;

        public uint WorldFog;

        public List<SkyObjectReplace> SkyObjReplace = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            Begin = reader.ReadSingle();
            DirBright = reader.ReadSingle();
            DirHeading = reader.ReadSingle();
            DirPitch = reader.ReadSingle();
            DirColor = reader.ReadItem<ColorARGB>();
            AmbBright = reader.ReadSingle();
            AmbColor = reader.ReadItem<ColorARGB>();
            MinWorldFog = reader.ReadSingle();
            MaxWorldFog = reader.ReadSingle();
            WorldFogColor = reader.ReadItem<ColorARGB>();
            WorldFog = reader.ReadUInt32();
            var _numSkyObjReplaces = reader.ReadUInt32();
            for (var i=0; i < _numSkyObjReplaces; i++) {
                SkyObjReplace.Add(reader.ReadItem<SkyObjectReplace>());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteSingle(Begin);
            writer.WriteSingle(DirBright);
            writer.WriteSingle(DirHeading);
            writer.WriteSingle(DirPitch);
            writer.WriteItem<ColorARGB>(DirColor);
            writer.WriteSingle(AmbBright);
            writer.WriteItem<ColorARGB>(AmbColor);
            writer.WriteSingle(MinWorldFog);
            writer.WriteSingle(MaxWorldFog);
            writer.WriteItem<ColorARGB>(WorldFogColor);
            writer.WriteUInt32(WorldFog);
            writer.WriteUInt32((uint)SkyObjReplace.Count());
            foreach (var item in SkyObjReplace) {
                writer.WriteItem<SkyObjectReplace>(item);
            }
            return true;
        }

    }

}
