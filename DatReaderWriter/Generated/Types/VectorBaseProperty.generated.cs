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
    public partial class VectorBaseProperty : BaseProperty {
        /// <inheritdoc />
        public override BasePropertyType PropertyType => BasePropertyType.Vector;

        public Vector3 Value;

        /// <inheritdoc />
        public override bool Unpack(DatBinReader reader) {
            base.Unpack(reader);
            Value = reader.ReadVector3();
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatBinWriter writer) {
            base.Pack(writer);
            writer.WriteVector3(Value);
            return true;
        }

    }

}
