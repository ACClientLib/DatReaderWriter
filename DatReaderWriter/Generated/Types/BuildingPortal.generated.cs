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
    public partial class BuildingPortal : IDatObjType {
        public PortalFlags Flags;

        public ushort OtherCellId;

        public ushort OtherPortalId;

        public List<ushort> StabList = [];

        /// <inheritdoc />
        public bool Unpack(DatBinReader reader) {
            Flags = (PortalFlags)reader.ReadUInt16();
            OtherCellId = reader.ReadUInt16();
            OtherPortalId = reader.ReadUInt16();
            var _numStabs = reader.ReadUInt16();
            for (var i=0; i < _numStabs; i++) {
                StabList.Add(reader.ReadUInt16());
            }
            reader.Align(4);
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatBinWriter writer) {
            writer.WriteUInt16((ushort)Flags);
            writer.WriteUInt16(OtherCellId);
            writer.WriteUInt16(OtherPortalId);
            writer.WriteUInt16((ushort)StabList.Count());
            foreach (var item in StabList) {
                writer.WriteUInt16(item);
            }
            writer.Align(4);
            return true;
        }

    }

}
