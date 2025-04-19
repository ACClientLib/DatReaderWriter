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
    /// DB_TYPE_QUALITY_FILTER_0 in the client.
    /// </summary>
    [DBObjType(typeof(QualityFilter), DatFileType.Portal, DBObjType.QualityFilter, DBObjHeaderFlags.HasId, 0x0E010000, 0x0E01FFFF, 0x00000000)]
    public partial class QualityFilter : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.QualityFilter;

        public uint[] IntStatFilter = [];

        public uint[] Int64StatFilter = [];

        public uint[] BoolStatFilter = [];

        public uint[] FloatStatFilter = [];

        public uint[] DataIdStatFilter = [];

        public uint[] InstanceIdStatFilter = [];

        public uint[] StringStatFilter = [];

        public uint[] PositionStatFilter = [];

        public uint[] AttributeStatFilter = [];

        public uint[] Attribute2ndStatFilter = [];

        public uint[] SkillStatFilter = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            var _numInts = reader.ReadUInt32();
            var _numInt64 = reader.ReadUInt32();
            var _numBools = reader.ReadUInt32();
            var _numFloats = reader.ReadUInt32();
            var _numDataIds = reader.ReadUInt32();
            var _numInstanceIds = reader.ReadUInt32();
            var _numStrings = reader.ReadUInt32();
            var _numPositions = reader.ReadUInt32();
            IntStatFilter = new uint[_numInts];
            for (var i=0; i < _numInts; i++) {
                IntStatFilter[i] = reader.ReadUInt32();
            }
            Int64StatFilter = new uint[_numInt64];
            for (var i=0; i < _numInt64; i++) {
                Int64StatFilter[i] = reader.ReadUInt32();
            }
            BoolStatFilter = new uint[_numBools];
            for (var i=0; i < _numBools; i++) {
                BoolStatFilter[i] = reader.ReadUInt32();
            }
            FloatStatFilter = new uint[_numFloats];
            for (var i=0; i < _numFloats; i++) {
                FloatStatFilter[i] = reader.ReadUInt32();
            }
            DataIdStatFilter = new uint[_numDataIds];
            for (var i=0; i < _numDataIds; i++) {
                DataIdStatFilter[i] = reader.ReadUInt32();
            }
            InstanceIdStatFilter = new uint[_numInstanceIds];
            for (var i=0; i < _numInstanceIds; i++) {
                InstanceIdStatFilter[i] = reader.ReadUInt32();
            }
            StringStatFilter = new uint[_numStrings];
            for (var i=0; i < _numStrings; i++) {
                StringStatFilter[i] = reader.ReadUInt32();
            }
            PositionStatFilter = new uint[_numPositions];
            for (var i=0; i < _numPositions; i++) {
                PositionStatFilter[i] = reader.ReadUInt32();
            }
            var _numAttributes = reader.ReadUInt32();
            var _numAttribute2nds = reader.ReadUInt32();
            var _numSkills = reader.ReadUInt32();
            AttributeStatFilter = new uint[_numAttributes];
            for (var i=0; i < _numAttributes; i++) {
                AttributeStatFilter[i] = reader.ReadUInt32();
            }
            Attribute2ndStatFilter = new uint[_numAttribute2nds];
            for (var i=0; i < _numAttribute2nds; i++) {
                Attribute2ndStatFilter[i] = reader.ReadUInt32();
            }
            SkillStatFilter = new uint[_numSkills];
            for (var i=0; i < _numSkills; i++) {
                SkillStatFilter[i] = reader.ReadUInt32();
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteUInt32((uint)IntStatFilter.Count());
            writer.WriteUInt32((uint)Int64StatFilter.Count());
            writer.WriteUInt32((uint)BoolStatFilter.Count());
            writer.WriteUInt32((uint)FloatStatFilter.Count());
            writer.WriteUInt32((uint)DataIdStatFilter.Count());
            writer.WriteUInt32((uint)InstanceIdStatFilter.Count());
            writer.WriteUInt32((uint)StringStatFilter.Count());
            writer.WriteUInt32((uint)PositionStatFilter.Count());
            for (var i=0; i < IntStatFilter.Count(); i++) {
                writer.WriteUInt32(IntStatFilter[i]);
            }
            for (var i=0; i < Int64StatFilter.Count(); i++) {
                writer.WriteUInt32(Int64StatFilter[i]);
            }
            for (var i=0; i < BoolStatFilter.Count(); i++) {
                writer.WriteUInt32(BoolStatFilter[i]);
            }
            for (var i=0; i < FloatStatFilter.Count(); i++) {
                writer.WriteUInt32(FloatStatFilter[i]);
            }
            for (var i=0; i < DataIdStatFilter.Count(); i++) {
                writer.WriteUInt32(DataIdStatFilter[i]);
            }
            for (var i=0; i < InstanceIdStatFilter.Count(); i++) {
                writer.WriteUInt32(InstanceIdStatFilter[i]);
            }
            for (var i=0; i < StringStatFilter.Count(); i++) {
                writer.WriteUInt32(StringStatFilter[i]);
            }
            for (var i=0; i < PositionStatFilter.Count(); i++) {
                writer.WriteUInt32(PositionStatFilter[i]);
            }
            writer.WriteUInt32((uint)AttributeStatFilter.Count());
            writer.WriteUInt32((uint)Attribute2ndStatFilter.Count());
            writer.WriteUInt32((uint)SkillStatFilter.Count());
            for (var i=0; i < AttributeStatFilter.Count(); i++) {
                writer.WriteUInt32(AttributeStatFilter[i]);
            }
            for (var i=0; i < Attribute2ndStatFilter.Count(); i++) {
                writer.WriteUInt32(Attribute2ndStatFilter[i]);
            }
            for (var i=0; i < SkillStatFilter.Count(); i++) {
                writer.WriteUInt32(SkillStatFilter[i]);
            }
            return true;
        }

    }

}
