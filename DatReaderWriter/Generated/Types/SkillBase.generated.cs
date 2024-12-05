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
    public partial class SkillBase : IDatObjType {
        public string Description;

        public string Name;

        public uint IconId;

        public int TrainedCost;

        public int SpecializedCost;

        public SkillCategory Category;

        public bool ChargenUse;

        public uint MinLevel;

        public SkillFormula Formula;

        public double UpperBound;

        public double LowerBound;

        public double LearnMod;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            Description = reader.ReadString16L();
            Name = reader.ReadString16L();
            IconId = reader.ReadUInt32();
            TrainedCost = reader.ReadInt32();
            SpecializedCost = reader.ReadInt32();
            Category = (SkillCategory)reader.ReadUInt32();
            ChargenUse = reader.ReadBool(4);
            MinLevel = reader.ReadUInt32();
            Formula = reader.ReadItem<SkillFormula>();
            UpperBound = reader.ReadDouble();
            LowerBound = reader.ReadDouble();
            LearnMod = reader.ReadDouble();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteString16L(Description);
            writer.WriteString16L(Name);
            writer.WriteUInt32(IconId);
            writer.WriteInt32(TrainedCost);
            writer.WriteInt32(SpecializedCost);
            writer.WriteUInt32((uint)Category);
            writer.WriteBool(ChargenUse, 4);
            writer.WriteUInt32(MinLevel);
            writer.WriteItem<SkillFormula>(Formula);
            writer.WriteDouble(UpperBound);
            writer.WriteDouble(LowerBound);
            writer.WriteDouble(LearnMod);
            return true;
        }

    }

}
