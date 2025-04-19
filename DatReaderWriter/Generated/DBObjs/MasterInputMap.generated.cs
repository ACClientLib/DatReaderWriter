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
    /// DB_TYPE_KEYMAP in the client.
    /// </summary>
    [DBObjType(typeof(MasterInputMap), DatFileType.Portal, DBObjType.MasterInputMap, DBObjHeaderFlags.HasId, 0x14000000, 0x1400FFFF, 0x00000000)]
    public partial class MasterInputMap : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.MasterInputMap;

        /// <summary>
        /// The name of the keymap
        /// </summary>
        public string Name;

        public Guid GuidMap;

        /// <summary>
        /// Device list
        /// </summary>
        public List<DeviceKeyMapEntry> Devices = [];

        /// <summary>
        /// Meta / Modifier key definitions
        /// </summary>
        public List<ControlSpecification> MetaKeys = [];

        public Dictionary<uint, CInputMap> InputMaps = [];

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            Name = reader.ReadString();
            GuidMap = reader.ReadGuid();
            var _numDeviceEntries = reader.ReadUInt32();
            for (var i=0; i < _numDeviceEntries; i++) {
                Devices.Add(reader.ReadItem<DeviceKeyMapEntry>());
            }
            var _numMetaKeys = reader.ReadUInt32();
            for (var i=0; i < _numMetaKeys; i++) {
                MetaKeys.Add(reader.ReadItem<ControlSpecification>());
            }
            var _numInputMaps = reader.ReadUInt32();
            for (var i=0; i < _numInputMaps; i++) {
                var _key = reader.ReadUInt32();
                var _val = reader.ReadItem<CInputMap>();
                InputMaps.Add(_key, _val);
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteString(Name);
            writer.WriteGuid(GuidMap);
            writer.WriteUInt32((uint)Devices.Count());
            foreach (var item in Devices) {
                writer.WriteItem<DeviceKeyMapEntry>(item);
            }
            writer.WriteUInt32((uint)MetaKeys.Count());
            foreach (var item in MetaKeys) {
                writer.WriteItem<ControlSpecification>(item);
            }
            writer.WriteUInt32((uint)InputMaps.Count());
            foreach (var kv in InputMaps) {
                writer.WriteUInt32(kv.Key);
                writer.WriteItem<CInputMap>(kv.Value);
            }
            return true;
        }

    }

}
