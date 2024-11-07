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
    public partial class DayGroup : IDatObjType {
        public float ChanceOfOccur;

        public string DayName;

        public List<SkyObject> SkyObjects = [];

        public List<SkyTimeOfDay> SkyTime = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            ChanceOfOccur = reader.ReadSingle();
            DayName = reader.ReadString16L();
            reader.Align(4);
            var _numSkyObjects = reader.ReadUInt32();
            for (var i=0; i < _numSkyObjects; i++) {
                SkyObjects.Add(reader.ReadItem<SkyObject>());
            }
            var _numSkyTimeOfDays = reader.ReadUInt32();
            for (var i=0; i < _numSkyTimeOfDays; i++) {
                SkyTime.Add(reader.ReadItem<SkyTimeOfDay>());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteSingle(ChanceOfOccur);
            writer.WriteString16L(DayName);
            writer.Align(4);
            writer.WriteUInt32((uint)SkyObjects.Count());
            foreach (var item in SkyObjects) {
                writer.WriteItem<SkyObject>(item);
            }
            writer.WriteUInt32((uint)SkyTime.Count());
            foreach (var item in SkyTime) {
                writer.WriteItem<SkyTimeOfDay>(item);
            }
            return true;
        }

    }

}
