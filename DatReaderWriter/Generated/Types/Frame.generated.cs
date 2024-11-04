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
using ACClientLib.DatReaderWriter.Enums;
using ACClientLib.DatReaderWriter.IO;

namespace ACClientLib.DatReaderWriter.Types {
    /// <summary>
    /// Positioning information.
    /// </summary>
    public partial class Frame : IDatObjType {
        /// <summary>
        /// The origin, offset from the parent
        /// </summary>
        public Vector3 Origin;

        /// <summary>
        /// The orientation
        /// </summary>
        public Quaternion Orientation;

        /// <inheritdoc />
        public bool Unpack(DatFileReader reader) {
            Origin = reader.ReadVector3();
            Orientation = reader.ReadQuaternion();
            return true;
        }

        /// <inheritdoc />
        public bool Pack(DatFileWriter writer) {
            writer.WriteVector3(Origin);
            writer.WriteQuaternion(Orientation);
            return true;
        }

    }

}
