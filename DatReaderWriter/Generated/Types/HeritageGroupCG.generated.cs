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
    /// <summary>
    /// Information about a heritage group, used during character creation
    /// </summary>
    public partial class HeritageGroupCG : IDatObjType {
        public string Name;

        public uint IconId;

        /// <summary>
        /// Basic character model
        /// </summary>
        public uint SetupId;

        /// <summary>
        /// This is the background environment used during character creation
        /// </summary>
        public uint EnvironmentSetupId;

        /// <summary>
        /// Starting attribute credits
        /// </summary>
        public uint AttributeCredits;

        /// <summary>
        /// Starting skill credits
        /// </summary>
        public uint SkillCredits;

        public List<int> PrimaryStartAreas = [];

        public List<int> SecondaryStartAreas = [];

        public List<SkillCG> Skills = [];

        public List<TemplateCG> Templates = [];

        public byte UnknownByte;

        public Dictionary<int, SexCG> Genders = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            Name = reader.ReadString();
            IconId = reader.ReadUInt32();
            SetupId = reader.ReadUInt32();
            EnvironmentSetupId = reader.ReadUInt32();
            AttributeCredits = reader.ReadUInt32();
            SkillCredits = reader.ReadUInt32();
            var _numPrimaryStartAreas = reader.ReadCompressedUInt();
            for (var i=0; i < _numPrimaryStartAreas; i++) {
                PrimaryStartAreas.Add(reader.ReadInt32());
            }
            var _numSecondaryStartAreas = reader.ReadCompressedUInt();
            for (var i=0; i < _numSecondaryStartAreas; i++) {
                SecondaryStartAreas.Add(reader.ReadInt32());
            }
            var _numSkills = reader.ReadCompressedUInt();
            for (var i=0; i < _numSkills; i++) {
                Skills.Add(reader.ReadItem<SkillCG>());
            }
            var _numTemplates = reader.ReadCompressedUInt();
            for (var i=0; i < _numTemplates; i++) {
                Templates.Add(reader.ReadItem<TemplateCG>());
            }
            UnknownByte = reader.ReadByte();
            var _numGenders = reader.ReadCompressedUInt();
            for (var i=0; i < _numGenders; i++) {
                var _key = reader.ReadInt32();
                var _val = reader.ReadItem<SexCG>();
                Genders.Add(_key, _val);
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteString(Name);
            writer.WriteUInt32(IconId);
            writer.WriteUInt32(SetupId);
            writer.WriteUInt32(EnvironmentSetupId);
            writer.WriteUInt32(AttributeCredits);
            writer.WriteUInt32(SkillCredits);
            writer.WriteCompressedUInt((uint)PrimaryStartAreas.Count());
            foreach (var item in PrimaryStartAreas) {
                writer.WriteInt32(item);
            }
            writer.WriteCompressedUInt((uint)SecondaryStartAreas.Count());
            foreach (var item in SecondaryStartAreas) {
                writer.WriteInt32(item);
            }
            writer.WriteCompressedUInt((uint)Skills.Count());
            foreach (var item in Skills) {
                writer.WriteItem<SkillCG>(item);
            }
            writer.WriteCompressedUInt((uint)Templates.Count());
            foreach (var item in Templates) {
                writer.WriteItem<TemplateCG>(item);
            }
            writer.WriteByte(UnknownByte);
            writer.WriteCompressedUInt((uint)Genders.Count());
            foreach (var kv in Genders) {
                writer.WriteInt32(kv.Key);
                writer.WriteItem<SexCG>(kv.Value);
            }
            return true;
        }

    }

}
