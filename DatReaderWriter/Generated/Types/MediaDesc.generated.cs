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
    public abstract partial class MediaDesc : IDatObjType {
        public abstract MediaType MediaType { get; }

        public MediaType Type;

        /// <inheritdoc />
        public virtual bool Unpack(DatBinReader reader) {
            var _mediaType = (MediaType)reader.ReadInt32();
            Type = (MediaType)reader.ReadInt32();
            return true;
        }

        /// <inheritdoc />
        public virtual bool Pack(DatBinWriter writer) {
            writer.WriteInt32((int)MediaType);
            writer.WriteInt32((int)Type);
            return true;
        }

        /// <summary>
        /// Create a typed instance of this abstract class
        /// </summary>
        public static MediaDesc? Unpack(DatBinReader reader, MediaType type) {
            MediaDesc? instance = null;
            switch(type) {
                case MediaType.Movie:
                    instance = new MediaDescMovie();
                    break;
                case MediaType.Alpha:
                    instance = new MediaDescAlpha();
                    break;
                case MediaType.Animation:
                    instance = new MediaDescAnimation();
                    break;
                case MediaType.Cursor:
                    instance = new MediaDescCursor();
                    break;
                case MediaType.Image:
                    instance = new MediaDescImage();
                    break;
                case MediaType.Jump:
                    instance = new MediaDescJump();
                    break;
                case MediaType.Message:
                    instance = new MediaDescMessage();
                    break;
                case MediaType.Pause:
                    instance = new MediaDescPause();
                    break;
                case MediaType.Sound:
                    instance = new MediaDescSound();
                    break;
                case MediaType.State:
                    instance = new MediaDescState();
                    break;
                case MediaType.Fade:
                    instance = new MediaDescFade();
                    break;
            }
            instance?.Unpack(reader);
            return instance;
        }
    }

}
