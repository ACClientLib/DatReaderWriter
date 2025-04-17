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
    /// DB_TYPE_STRING_STATE in the client.
    /// </summary>
    [DBObjType(typeof(LanguageInfo), DatFileType.Local, DBObjType.LanguageInfo, DBObjHeaderFlags.None, 0x41000000, 0x41FFFFFF, 0x00000000)]
    public partial class LanguageInfo : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.None;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.LanguageInfo;

        public int Version;

        public ushort Base;

        public ushort NumDecimalDigits;

        public bool LeadingZero;

        public ushort GroupingSize;

        public string Numerals;

        public string DecimalSeperator;

        public string GroupingSeperator;

        public string NegativeNumberFormat;

        public bool IsZeroSingular;

        public bool IsOneSingular;

        public bool IsNegativeOneSingular;

        public bool IsTwoOrMoreSingular;

        public bool IsNegativeTwoOrLessSingular;

        public string TreasurePrefixLetters;

        public string TreasureMiddleLetters;

        public string TreasureSuffixLetters;

        public string MalePlayerLetters;

        public string FemalePlayerLetters;

        public uint IMEEnabledSetting;

        public uint SymbolColor;

        public uint SymbolColorText;

        public uint SymbolHeight;

        public uint SymbolTranslucence;

        public uint SymbolPlacement;

        public uint CandColorBase;

        public uint CandColorBorder;

        public uint CandColorText;

        public uint CompColorInput;

        public uint CompColorTargetConv;

        public uint CompColorConverted;

        public uint CompColorTargetNotConv;

        public uint CompColorInputErr;

        public uint CompTranslucence;

        public uint CompColorText;

        public uint OtherIME;

        public int WordWrapOnSpace;

        public string AdditionalSettings;

        public uint AdditionalFlags;

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            Version = reader.ReadInt32();
            Base = reader.ReadUInt16();
            NumDecimalDigits = reader.ReadUInt16();
            LeadingZero = reader.ReadBool(1);
            GroupingSize = reader.ReadUInt16();
            Numerals = reader.ReadUShortString();
            DecimalSeperator = reader.ReadUShortString();
            GroupingSeperator = reader.ReadUShortString();
            NegativeNumberFormat = reader.ReadUShortString();
            IsZeroSingular = reader.ReadBool(1);
            IsOneSingular = reader.ReadBool(1);
            IsNegativeOneSingular = reader.ReadBool(1);
            IsTwoOrMoreSingular = reader.ReadBool(1);
            IsNegativeTwoOrLessSingular = reader.ReadBool(1);
            reader.Align(4);
            TreasurePrefixLetters = reader.ReadUShortString();
            TreasureMiddleLetters = reader.ReadUShortString();
            TreasureSuffixLetters = reader.ReadUShortString();
            MalePlayerLetters = reader.ReadUShortString();
            FemalePlayerLetters = reader.ReadUShortString();
            IMEEnabledSetting = reader.ReadUInt32();
            SymbolColor = reader.ReadUInt32();
            SymbolColorText = reader.ReadUInt32();
            SymbolHeight = reader.ReadUInt32();
            SymbolTranslucence = reader.ReadUInt32();
            SymbolPlacement = reader.ReadUInt32();
            CandColorBase = reader.ReadUInt32();
            CandColorBorder = reader.ReadUInt32();
            CandColorText = reader.ReadUInt32();
            CompColorInput = reader.ReadUInt32();
            CompColorTargetConv = reader.ReadUInt32();
            CompColorConverted = reader.ReadUInt32();
            CompColorTargetNotConv = reader.ReadUInt32();
            CompColorInputErr = reader.ReadUInt32();
            CompTranslucence = reader.ReadUInt32();
            CompColorText = reader.ReadUInt32();
            OtherIME = reader.ReadUInt32();
            WordWrapOnSpace = reader.ReadInt32();
            AdditionalSettings = reader.ReadString16LByte();
            AdditionalFlags = reader.ReadUInt32();
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteInt32(Version);
            writer.WriteUInt16(Base);
            writer.WriteUInt16(NumDecimalDigits);
            writer.WriteBool(LeadingZero, 1);
            writer.WriteUInt16(GroupingSize);
            writer.WriteUShortString(Numerals);
            writer.WriteUShortString(DecimalSeperator);
            writer.WriteUShortString(GroupingSeperator);
            writer.WriteUShortString(NegativeNumberFormat);
            writer.WriteBool(IsZeroSingular, 1);
            writer.WriteBool(IsOneSingular, 1);
            writer.WriteBool(IsNegativeOneSingular, 1);
            writer.WriteBool(IsTwoOrMoreSingular, 1);
            writer.WriteBool(IsNegativeTwoOrLessSingular, 1);
            writer.Align(4);
            writer.WriteUShortString(TreasurePrefixLetters);
            writer.WriteUShortString(TreasureMiddleLetters);
            writer.WriteUShortString(TreasureSuffixLetters);
            writer.WriteUShortString(MalePlayerLetters);
            writer.WriteUShortString(FemalePlayerLetters);
            writer.WriteUInt32(IMEEnabledSetting);
            writer.WriteUInt32(SymbolColor);
            writer.WriteUInt32(SymbolColorText);
            writer.WriteUInt32(SymbolHeight);
            writer.WriteUInt32(SymbolTranslucence);
            writer.WriteUInt32(SymbolPlacement);
            writer.WriteUInt32(CandColorBase);
            writer.WriteUInt32(CandColorBorder);
            writer.WriteUInt32(CandColorText);
            writer.WriteUInt32(CompColorInput);
            writer.WriteUInt32(CompColorTargetConv);
            writer.WriteUInt32(CompColorConverted);
            writer.WriteUInt32(CompColorTargetNotConv);
            writer.WriteUInt32(CompColorInputErr);
            writer.WriteUInt32(CompTranslucence);
            writer.WriteUInt32(CompColorText);
            writer.WriteUInt32(OtherIME);
            writer.WriteInt32(WordWrapOnSpace);
            writer.WriteString16LByte(AdditionalSettings);
            writer.WriteUInt32(AdditionalFlags);
            return true;
        }

    }

}
