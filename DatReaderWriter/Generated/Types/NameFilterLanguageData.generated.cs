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
    public partial class NameFilterLanguageData : IDatObjType {
        public uint MaximumSameCharactersInARow;

        public uint MaximumVowelsInARow;

        public uint FirstNCharactersMustHaveAVowel;

        public uint VowelContainingSubstringLength;

        public string ExtraAllowedCharacters;

        public List<string> CompoundLetterGroups = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            MaximumSameCharactersInARow = reader.ReadUInt32();
            MaximumVowelsInARow = reader.ReadUInt32();
            FirstNCharactersMustHaveAVowel = reader.ReadUInt32();
            VowelContainingSubstringLength = reader.ReadUInt32();
            ExtraAllowedCharacters = reader.ReadUShortString();
            var _numCompoundLetterGroups = reader.ReadUInt32();
            for (var i=0; i < _numCompoundLetterGroups; i++) {
                CompoundLetterGroups.Add(reader.ReadUShortString());
            }
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt32(MaximumSameCharactersInARow);
            writer.WriteUInt32(MaximumVowelsInARow);
            writer.WriteUInt32(FirstNCharactersMustHaveAVowel);
            writer.WriteUInt32(VowelContainingSubstringLength);
            writer.WriteUShortString(ExtraAllowedCharacters);
            writer.WriteUInt32((uint)CompoundLetterGroups.Count());
            foreach (var item in CompoundLetterGroups) {
                writer.WriteUShortString(item);
            }
            return true;
        }

    }

}
