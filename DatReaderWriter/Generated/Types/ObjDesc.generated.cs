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
    public partial class ObjDesc : IDatObjType {
        public byte UnknownByte;

        public uint PaletteId;

        public List<SubPalette> SubPalettes = [];

        public List<TextureMapChange> TextureChanges = [];

        public List<AnimationPartChange> AnimPartChanges = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            reader.Align(4);
            UnknownByte = reader.ReadByte();
            var _numSubPalettes = reader.ReadByte();
            var _numTextureMapChanges = reader.ReadByte();
            var _numAnimPartChanges = reader.ReadByte();
            if (_numSubPalettes > 0) {
                PaletteId = reader.ReadDataIdOfKnownType(0x04000000);
            }
            for (var i=0; i < _numSubPalettes; i++) {
                SubPalettes.Add(reader.ReadItem<SubPalette>());
            }
            for (var i=0; i < _numTextureMapChanges; i++) {
                TextureChanges.Add(reader.ReadItem<TextureMapChange>());
            }
            for (var i=0; i < _numAnimPartChanges; i++) {
                AnimPartChanges.Add(reader.ReadItem<AnimationPartChange>());
            }
            reader.Align(4);
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.Align(4);
            writer.WriteByte(UnknownByte);
            writer.WriteByte((byte)SubPalettes.Count());
            writer.WriteByte((byte)TextureChanges.Count());
            writer.WriteByte((byte)AnimPartChanges.Count());
            if (SubPalettes != null && SubPalettes.Count() > 0) {
                writer.WriteDataIdOfKnownType(PaletteId, 0x04000000);
            }
            foreach (var item in SubPalettes) {
                writer.WriteItem<SubPalette>(item);
            }
            foreach (var item in TextureChanges) {
                writer.WriteItem<TextureMapChange>(item);
            }
            foreach (var item in AnimPartChanges) {
                writer.WriteItem<AnimationPartChange>(item);
            }
            writer.Align(4);
            return true;
        }

    }

}
