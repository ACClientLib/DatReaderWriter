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
    /// Holds the experience required for different attributes/vitals/skill/levels, as well as the amount of skill credits obtained for each level. DB_TYPE_XP_TABLE_0 in the client.
    /// </summary>
    [DBObjType(typeof(ExperienceTable), DatFileType.Portal, DBObjType.ExperienceTable, DBObjHeaderFlags.HasId, 0x0E000018, 0x0E000018, 0x00000000)]
    public partial class ExperienceTable : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.ExperienceTable;

        /// <summary>
        /// The amount of experience required to train attributes
        /// </summary>
        public uint[] Attributes = [];

        /// <summary>
        /// The amount of experience required to train vitals
        /// </summary>
        public uint[] Vitals = [];

        /// <summary>
        /// The amount of experience required to train trained skills
        /// </summary>
        public uint[] TrainedSkills = [];

        /// <summary>
        /// The amount of experience required to train specialized skills
        /// </summary>
        public uint[] SpecializedSkills = [];

        /// <summary>
        /// The amount of experience required to level a character
        /// </summary>
        public ulong[] Levels = [];

        /// <summary>
        /// The amount of skill credits per level
        /// </summary>
        public uint[] SkillCredits = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            var _attributeCount = reader.ReadInt32();
            var _vitalCount = reader.ReadInt32();
            var _trainedSkillCount = reader.ReadInt32();
            var _specializedSkillCount = reader.ReadInt32();
            var _levelCount = reader.ReadUInt32();
            Attributes = new uint[_attributeCount + 1];
            for (var i=0; i < _attributeCount + 1; i++) {
                Attributes[i] = reader.ReadUInt32();
            }
            Vitals = new uint[_vitalCount + 1];
            for (var i=0; i < _vitalCount + 1; i++) {
                Vitals[i] = reader.ReadUInt32();
            }
            TrainedSkills = new uint[_trainedSkillCount + 1];
            for (var i=0; i < _trainedSkillCount + 1; i++) {
                TrainedSkills[i] = reader.ReadUInt32();
            }
            SpecializedSkills = new uint[_specializedSkillCount + 1];
            for (var i=0; i < _specializedSkillCount + 1; i++) {
                SpecializedSkills[i] = reader.ReadUInt32();
            }
            Levels = new ulong[_levelCount + 1];
            for (var i=0; i < _levelCount + 1; i++) {
                Levels[i] = reader.ReadUInt64();
            }
            SkillCredits = new uint[_levelCount + 1];
            for (var i=0; i < _levelCount + 1; i++) {
                SkillCredits[i] = reader.ReadUInt32();
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteInt32((int)Attributes.Count() - 1);
            writer.WriteInt32((int)Vitals.Count() - 1);
            writer.WriteInt32((int)TrainedSkills.Count() - 1);
            writer.WriteInt32((int)SpecializedSkills.Count() - 1);
            writer.WriteUInt32((uint)Levels.Count() - 1);
            for (var i=0; i < Attributes.Count(); i++) {
                writer.WriteUInt32(Attributes[i]);
            }
            for (var i=0; i < Vitals.Count(); i++) {
                writer.WriteUInt32(Vitals[i]);
            }
            for (var i=0; i < TrainedSkills.Count(); i++) {
                writer.WriteUInt32(TrainedSkills[i]);
            }
            for (var i=0; i < SpecializedSkills.Count(); i++) {
                writer.WriteUInt32(SpecializedSkills[i]);
            }
            for (var i=0; i < Levels.Count(); i++) {
                writer.WriteUInt64(Levels[i]);
            }
            for (var i=0; i < SkillCredits.Count(); i++) {
                writer.WriteUInt32(SkillCredits[i]);
            }
            return true;
        }

    }

}
