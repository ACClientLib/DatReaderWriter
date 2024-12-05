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
    public partial class TemplateCG : IDatObjType {
        public string Name;

        public uint IconId;

        public uint Title;

        public int Strength;

        public int Endurance;

        public int Coordination;

        public int Quickness;

        public int Focus;

        public int Self;

        public List<SkillId> NormalSkills = [];

        public List<SkillId> PrimarySkills = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            Name = reader.ReadString();
            IconId = reader.ReadUInt32();
            Title = reader.ReadUInt32();
            Strength = reader.ReadInt32();
            Endurance = reader.ReadInt32();
            Coordination = reader.ReadInt32();
            Quickness = reader.ReadInt32();
            Focus = reader.ReadInt32();
            Self = reader.ReadInt32();
            var _numNormalSkills = reader.ReadCompressedUInt();
            for (var i=0; i < _numNormalSkills; i++) {
                NormalSkills.Add((SkillId)reader.ReadInt32());
            }
            var _numPrimarySkills = reader.ReadCompressedUInt();
            for (var i=0; i < _numPrimarySkills; i++) {
                PrimarySkills.Add((SkillId)reader.ReadInt32());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteString(Name);
            writer.WriteUInt32(IconId);
            writer.WriteUInt32(Title);
            writer.WriteInt32(Strength);
            writer.WriteInt32(Endurance);
            writer.WriteInt32(Coordination);
            writer.WriteInt32(Quickness);
            writer.WriteInt32(Focus);
            writer.WriteInt32(Self);
            writer.WriteCompressedUInt((uint)NormalSkills.Count());
            foreach (var item in NormalSkills) {
                writer.WriteInt32((int)item);
            }
            writer.WriteCompressedUInt((uint)PrimarySkills.Count());
            foreach (var item in PrimarySkills) {
                writer.WriteInt32((int)item);
            }
            return true;
        }

    }

}
