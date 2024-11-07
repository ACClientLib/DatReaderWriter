//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
//                                                            //
//                          WARNING                           //
//                                                            //
//           DO NOT MAKE LOCAL CHANGES TO THIS FILE           //
//               EDIT THE .tt TEMPLATE INSTEAD                //
//                                                            //
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//


using System;
namespace DatReaderWriter.Enums {
    public enum PixelFormat : uint {
        PFID_UNKNOWN = 0x00000000,

        PFID_R8G8B8 = 0x00000014,

        PFID_A8R8G8B8 = 0x00000015,

        PFID_X8R8G8B8 = 0x00000016,

        PFID_R5G6B5 = 0x00000017,

        PFID_X1R5G5B5 = 0x00000018,

        PFID_A1R5G5B5 = 0x00000019,

        PFID_A4R4G4B4 = 0x0000001A,

        PFID_R3G3B2 = 0x0000001B,

        PFID_A8 = 0x0000001C,

        PFID_A8R3G3B2 = 0x0000001D,

        PFID_X4R4G4B4 = 0x0000001E,

        PFID_A2B10G10R10 = 0x0000001F,

        PFID_A8B8G8R8 = 0x00000020,

        PFID_X8B8G8R8 = 0x00000021,

        PFID_A2R10G10B10 = 0x00000023,

        PFID_A8P8 = 0x00000028,

        PFID_P8 = 0x00000029,

        PFID_L8 = 0x00000032,

        PFID_A8L8 = 0x00000033,

        PFID_A4L4 = 0x00000034,

        PFID_V8U8 = 0x0000003C,

        PFID_L6V5U5 = 0x0000003D,

        PFID_X8L8V8U8 = 0x0000003E,

        PFID_Q8W8V8U8 = 0x0000003F,

        PFID_V16U16 = 0x00000040,

        PFID_A2W10V10U10 = 0x00000043,

        PFID_D16_LOCKABLE = 0x00000046,

        PFID_D32 = 0x00000047,

        PFID_D15S1 = 0x00000049,

        PFID_D24S8 = 0x0000004B,

        PFID_D24X8 = 0x0000004D,

        PFID_D24X4S4 = 0x0000004F,

        PFID_D16 = 0x00000050,

        PFID_VERTEXDATA = 0x00000064,

        PFID_INDEX16 = 0x00000065,

        PFID_INDEX32 = 0x00000066,

        PFID_CUSTOM_R8G8B8A8 = 0x000000F0,

        PFID_CUSTOM_A8B8G8R8 = 0x000000F1,

        PFID_CUSTOM_B8G8R8 = 0x000000F2,

        PFID_CUSTOM_LSCAPE_R8G8B8 = 0x000000F3,

        PFID_CUSTOM_LSCAPE_ALPHA = 0x000000F4,

        PFID_CUSTOM_RAW_JPEG = 0x000001F4,

        PFID_DXT1 = 0x31545844,

        PFID_DXT2 = 0x32545844,

        PFID_YUY2 = 0x32595559,

        PFID_DXT3 = 0x33545844,

        PFID_DXT4 = 0x34545844,

        PFID_DXT5 = 0x35545844,

        PFID_G8R8_G8B8 = 0x42475247,

        PFID_R8G8_B8G8 = 0x47424752,

        PFID_UYVY = 0x59565955,

        PFID_INVALID = 0x7FFFFFFF,

    };
}
