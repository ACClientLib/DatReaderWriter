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
using ACClientLib.DatReaderWriter.Types;
using ACClientLib.DatReaderWriter.Attributes;

namespace ACClientLib.DatReaderWriter.DBObjs {
    /// <summary>
    /// DB_TYPE_REGION in the client.
    /// </summary>
    [DBObjType(typeof(Region), DatFileType.Portal, DBObjType.Region, DBObjHeaderFlags.HasId, 0x13000000, 0x1300FFFF, 0x00000000)]
    public partial class Region : DBObj {
        /// <inheritdoc />
        public override DBObjHeaderFlags HeaderFlags => DBObjHeaderFlags.HasId;

        /// <inheritdoc />
        public override DBObjType DBObjType => DBObjType.Region;

        public uint RegionNumber;

        public uint Version;

        public string RegionName;

        public LandDefs LandDefs;

        public GameTime GameTime;

        public PartsMask PartsMask;

        public SkyDesc SkyInfo;

        public SoundDesc SoundInfo;

        public SceneDesc SceneInfo;

        public TerrainDesc TerrainInfo;

        public RegionMisc RegionMisc;

        /// <inheritdoc />
        public override bool Unpack(DatFileReader reader) {
            base.Unpack(reader);
            RegionNumber = reader.ReadUInt32();
            Version = reader.ReadUInt32();
            RegionName = reader.ReadString16L();
            reader.Align(4);
            LandDefs = reader.ReadItem<LandDefs>();
            GameTime = reader.ReadItem<GameTime>();
            PartsMask = (PartsMask)reader.ReadUInt32();
            if (PartsMask.HasFlag(PartsMask.HasSkyInfo)) {
                SkyInfo = reader.ReadItem<SkyDesc>();
            }
            if (PartsMask.HasFlag(PartsMask.HasSoundInfo)) {
                SoundInfo = reader.ReadItem<SoundDesc>();
            }
            if (PartsMask.HasFlag(PartsMask.HasSceneInfo)) {
                SceneInfo = reader.ReadItem<SceneDesc>();
            }
            TerrainInfo = reader.ReadItem<TerrainDesc>();
            if (PartsMask.HasFlag(PartsMask.HasRegionMisc)) {
                RegionMisc = reader.ReadItem<RegionMisc>();
            }
            return true;
        }

        /// <inheritdoc />
        public override bool Pack(DatFileWriter writer) {
            base.Pack(writer);
            writer.WriteUInt32(RegionNumber);
            writer.WriteUInt32(Version);
            writer.WriteString16L(RegionName);
            writer.Align(4);
            writer.WriteItem<LandDefs>(LandDefs);
            writer.WriteItem<GameTime>(GameTime);
            writer.WriteUInt32((uint)PartsMask);
            if (PartsMask.HasFlag(PartsMask.HasSkyInfo)) {
                writer.WriteItem<SkyDesc>(SkyInfo);
            }
            if (PartsMask.HasFlag(PartsMask.HasSoundInfo)) {
                writer.WriteItem<SoundDesc>(SoundInfo);
            }
            if (PartsMask.HasFlag(PartsMask.HasSceneInfo)) {
                writer.WriteItem<SceneDesc>(SceneInfo);
            }
            writer.WriteItem<TerrainDesc>(TerrainInfo);
            if (PartsMask.HasFlag(PartsMask.HasRegionMisc)) {
                writer.WriteItem<RegionMisc>(RegionMisc);
            }
            return true;
        }

    }

}
