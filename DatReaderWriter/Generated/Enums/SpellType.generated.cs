//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
//                                                            //
//                          WARNING                           //
//                                                            //
//           DO NOT MAKE LOCAL CHANGES TO THIS FILE           //
//               EDIT THE .tt TEMPLATE INSTEAD                //
//                                                            //
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//


using System;
namespace ACClientLib.DatReaderWriter.Enums {
    public enum SpellType : uint {
        Undef = 0x00000000,

        Enchantment = 0x00000001,

        Projectile = 0x00000002,

        Boost = 0x00000003,

        Transfer = 0x00000004,

        PortalLink = 0x00000005,

        PortalRecall = 0x00000006,

        PortalSummon = 0x00000007,

        PortalSending = 0x00000008,

        Dispel = 0x00000009,

        LifeProjectile = 0x0000000A,

        FellowBoost = 0x0000000B,

        FellowEnchantment = 0x0000000C,

        FellowPortalSending = 0x0000000D,

        FellowDispel = 0x0000000E,

        EnchantmentProjectile = 0x0000000F,

    };
}
