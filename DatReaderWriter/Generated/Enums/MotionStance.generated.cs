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
    public enum MotionStance : uint {
        Invalid = 0x80000000,

        HandCombat = 0x8000003C,

        NonCombat = 0x8000003D,

        SwordCombat = 0x8000003E,

        BowCombat = 0x8000003F,

        SwordShieldCombat = 0x80000040,

        CrossbowCombat = 0x80000041,

        UnusedCombat = 0x80000042,

        SlingCombat = 0x80000043,

        TwoHandedSwordCombat = 0x80000044,

        TwoHandedStaffCombat = 0x80000045,

        DualWieldCombat = 0x80000046,

        ThrownWeaponCombat = 0x80000047,

        Graze = 0x80000048,

        Magic = 0x80000049,

        BowNoAmmo = 0x800000E8,

        CrossBowNoAmmo = 0x800000E9,

        AtlatlCombat = 0x8000013B,

        ThrownShieldCombat = 0x8000013C,

    };
}
