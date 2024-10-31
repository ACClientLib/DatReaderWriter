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
    public class LandDefs : IDatObjType {
        public int NumBlockLength;

        public int NumBlockWidth;

        public float SquareLength;

        public int LBlockLength;

        public int VertexPerCell;

        public float MaxObjHeight;

        public float SkyHeight;

        public float RoadWidth;

        public float[] LandHeightTable = [];

        /// <inheritdoc />
        public bool Unpack(DatFileReader reader) {
            NumBlockLength = reader.ReadInt32();
            NumBlockWidth = reader.ReadInt32();
            SquareLength = reader.ReadSingle();
            LBlockLength = reader.ReadInt32();
            VertexPerCell = reader.ReadInt32();
            MaxObjHeight = reader.ReadSingle();
            SkyHeight = reader.ReadSingle();
            RoadWidth = reader.ReadSingle();
            LandHeightTable = new float[256];
            for (var i=0; i < 256; i++) {
                LandHeightTable[i] = reader.ReadSingle();
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatFileWriter writer) {
            writer.WriteInt32(NumBlockLength);
            writer.WriteInt32(NumBlockWidth);
            writer.WriteSingle(SquareLength);
            writer.WriteInt32(LBlockLength);
            writer.WriteInt32(VertexPerCell);
            writer.WriteSingle(MaxObjHeight);
            writer.WriteSingle(SkyHeight);
            writer.WriteSingle(RoadWidth);
            for (var i=0; i < LandHeightTable.Count(); i++) {
                writer.WriteSingle(LandHeightTable[i]);
            }
            return true;
        }

    }

}
