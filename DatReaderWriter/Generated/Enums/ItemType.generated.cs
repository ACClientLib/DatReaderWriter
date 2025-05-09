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
    public enum ItemType : uint {
        None = 0x00000000,

        MeleeWeapon = 0x00000001,

        Armor = 0x00000002,

        Clothing = 0x00000004,

        Vestements = 0x00000006,

        Jewelry = 0x00000008,

        Creature = 0x00000010,

        Food = 0x00000020,

        Money = 0x00000040,

        Misc = 0x00000080,

        MissileWeapon = 0x00000100,

        Weapon = 0x00000101,

        Container = 0x00000200,

        LockableMagicTarget = 0x00000280,

        Useless = 0x00000400,

        Gem = 0x00000800,

        SpellComponents = 0x00001000,

        Writable = 0x00002000,

        Key = 0x00004000,

        Caster = 0x00008000,

        WeaponOrCaster = 0x00008101,

        RedirectableItemEnchantmentTarget = 0x00008107,

        Portal = 0x00010000,

        Lockable = 0x00020000,

        PromissoryNote = 0x00040000,

        ManaStone = 0x00080000,

        ItemEnchantableTarget = 0x00088B8F,

        Service = 0x00100000,

        MagicWieldable = 0x00200000,

        Item = 0x002DFBEF,

        CraftCookingBase = 0x00400000,

        VendorGrocer = 0x00446220,

        CraftAlchemyBase = 0x00800000,

        CraftFletchingBase = 0x02000000,

        CraftAlchemyIntermediate = 0x04000000,

        CraftFletchingIntermediate = 0x08000000,

        LifeStone = 0x10000000,

        PortalMagicTarget = 0x10010000,

        TinkeringTool = 0x20000000,

        TinkeringMaterial = 0x40000000,

        VendorShopKeep = 0x480467A7,

        Gameboard = 0x80000000,

    };
}
