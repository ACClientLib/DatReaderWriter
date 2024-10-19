﻿using ACClientLib.DatReaderWriter.Enums;
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
    public class PortalPolyTests {
        [TestMethod]
        public void CanSerializeAndDeserialize() {
            var writeObj = new PortalPoly() {
                PolygonId = 123,
                PortalIndex = 456,
            };

            var buffer = new byte[5_000_000];
            var writer = new DatFileWriter(buffer);
            writeObj.Pack(writer);

            var readObj = new PortalPoly();
            var reader = new DatFileReader(buffer);
            readObj.Unpack(reader);

            Assert.IsNotNull(readObj);
            Assert.AreEqual(writeObj.PolygonId, readObj.PolygonId);
            Assert.AreEqual(writeObj.PortalIndex, readObj.PortalIndex);
        }
    }
}