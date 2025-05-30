using System;
using DatReaderWriter.Enums;
using DatReaderWriter.Lib.IO;

namespace DatReaderWriter.Types {
    /// <summary>
    /// Base class for all DBObjs
    /// </summary>
    public abstract class DBObj : IDBObj {
        /// <inheritdoc />
        public abstract DBObjHeaderFlags HeaderFlags { get; }

        /// <inheritdoc />
        public abstract DBObjType DBObjType { get; }

        /// <inheritdoc />
        /// <remarks>
        /// Only valid if <see cref="HeaderFlags"/> has <see cref="DBObjHeaderFlags.HasId"/>.
        /// </remarks>
        public virtual uint Id { get; set; }

        /// <remarks>
        /// Only valid if <see cref="HeaderFlags"/> has <see cref="DBObjHeaderFlags.HasDataCategory"/>.
        /// </remarks>
        public virtual uint DataCategory { get; set; }

        /// <inheritdoc />
        public virtual bool Unpack(DatBinReader reader) {
            if (HeaderFlags.HasFlag(DBObjHeaderFlags.HasId)) {
                Id = reader.ReadUInt32();
            }
            if (HeaderFlags.HasFlag(DBObjHeaderFlags.HasDataCategory)) {
                DataCategory = reader.ReadUInt32();
            }
            return true;
        }

        /// <inheritdoc />
        public virtual bool Pack(DatBinWriter writer) {
            if (HeaderFlags.HasFlag(DBObjHeaderFlags.HasId)) {
                writer.WriteUInt32(Id);
            }
            if (HeaderFlags.HasFlag(DBObjHeaderFlags.HasDataCategory)) {
                writer.WriteUInt32(DataCategory);
            }
            return true;
        }

    }

}
