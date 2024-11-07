﻿using DatReaderWriter.DBObjs;
using DatReaderWriter.Enums;
using DatReaderWriter.Lib;
using DatReaderWriter.Lib.IO.BlockAllocators;
using DatReaderWriter.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatReaderWriter {
    /// <summary>
    /// Used to read / write from the portal database. This is just a specialized <see cref="DatDatabase"/>
    /// that has collections exposed for the contained DBObjs.
    /// </summary>
    public partial class PortalDatabase : DatDatabase {
        /// <summary>
        /// Open a <see cref="PortalDatabase"/> (client_portal.dat)
        /// </summary>
        /// <param name="options">An action that lets you configure the options</param>
        /// <param name="blockAllocator"></param>
        public PortalDatabase(Action<DatDatabaseOptions> options, IDatBlockAllocator? blockAllocator = null) : base(options, blockAllocator) {
            if (BlockAllocator.HasHeaderData && Header.Type != DatFileType.Portal) {
                BlockAllocator.Dispose();
                Tree.Dispose();
                throw new ArgumentException($"Tried to open {Options.FilePath} as a portal database, but it's type is {Header.Type}");
            }
        }

        /// <summary>
        /// Open a <see cref="CellDatabase"/> for reading.
        /// </summary>
        /// <param name="datFilePath">The path to the cell dat file</param>
        /// <param name="accessType"></param>
        public PortalDatabase(string datFilePath, DatAccessType accessType = DatAccessType.Read) : this(options => {
            options.FilePath = datFilePath;
            options.AccessType = accessType;
        }) {

        }
    }
}
