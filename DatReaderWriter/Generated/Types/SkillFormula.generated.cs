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
    /// Formula that dictates how to calculate a skill `(Attribute1 * Attribute1Multiplier + Attribute2 * Attribute2Multiplier) / Divisor + AdditiveBonus`.
    /// </summary>
    public partial class SkillFormula : IDatObjType {
        /// <summary>
        /// Unused in retail data (W)
        /// </summary>
        public int AdditiveBonus;

        /// <summary>
        /// Was never more than 1 in retail data (X)
        /// </summary>
        public int Attribute1Multiplier;

        /// <summary>
        /// Was never more than 1 in retail data (Y)
        /// </summary>
        public int Attribute2Multiplier;

        /// <summary>
        /// The divisor used in the formula (Z)
        /// </summary>
        public int Divisor;

        /// <summary>
        /// The first attribute to use.
        /// </summary>
        public AttributeId Attribute1;

        /// <summary>
        /// The second attribute to use.
        /// </summary>
        public AttributeId Attribute2;

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            AdditiveBonus = reader.ReadInt32();
            Attribute1Multiplier = reader.ReadInt32();
            Attribute2Multiplier = reader.ReadInt32();
            Divisor = reader.ReadInt32();
            Attribute1 = (AttributeId)reader.ReadUInt32();
            Attribute2 = (AttributeId)reader.ReadUInt32();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteInt32(AdditiveBonus);
            writer.WriteInt32(Attribute1Multiplier);
            writer.WriteInt32(Attribute2Multiplier);
            writer.WriteInt32(Divisor);
            writer.WriteUInt32((uint)Attribute1);
            writer.WriteUInt32((uint)Attribute2);
            return true;
        }

    }

}
