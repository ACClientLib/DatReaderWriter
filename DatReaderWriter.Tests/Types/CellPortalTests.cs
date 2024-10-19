﻿using System;
using ACClientLib.DatReaderWriter.Enums;
using ACClientLib.DatReaderWriter.IO;
using ACClientLib.DatReaderWriter.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DatReaderWriter.Tests.Types {
    [TestClass]
    public class CellPortalTests {
        [TestMethod]
        public void CanSerializeAndDeserialize() {
            var writeObj = new CellPortal() {
                Flags = PortalFlags.PortalSide,
                OtherCellId = 789,
                OtherPortalId = 987,
                PolygonId = 456,
            };

            var buffer = new byte[5_000_000];
            var writer = new DatFileWriter(buffer);
            writeObj.Pack(writer);

            var readObj = new CellPortal();
            var reader = new DatFileReader(buffer);
            readObj.Unpack(reader);

            Assert.IsNotNull(readObj);
            Assert.AreEqual(writeObj.Flags, readObj.Flags);
            Assert.AreEqual(writeObj.OtherCellId, readObj.OtherCellId);
            Assert.AreEqual(writeObj.OtherPortalId, readObj.OtherPortalId);
            Assert.AreEqual(writeObj.PolygonId, readObj.PolygonId);
        }
    }
}
