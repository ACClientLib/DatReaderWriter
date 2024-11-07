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
    public abstract partial class AnimationHook : IDatObjType {
        public abstract AnimationHookType HookType { get; }

        public AnimationHookDir Direction;

        /// <inheritdoc />
        public virtual bool Unpack(DatBinReader reader) {
            var _hookType = (AnimationHookType)reader.ReadUInt32();
            Direction = (AnimationHookDir)reader.ReadUInt32();
            return true;
        }

        /// <inheritdoc />
        public virtual bool Pack(DatBinWriter writer) {
            writer.WriteUInt32((uint)HookType);
            writer.WriteUInt32((uint)Direction);
            return true;
        }

        /// <summary>
        /// Create a typed instance of this abstract class
        /// </summary>
        public static AnimationHook? Unpack(DatBinReader reader, AnimationHookType type) {
            AnimationHook? instance = null;
            switch(type) {
                case AnimationHookType.Sound:
                    instance = new SoundHook();
                    break;
                case AnimationHookType.SoundTable:
                    instance = new SoundTableHook();
                    break;
                case AnimationHookType.Attack:
                    instance = new AttackHook();
                    break;
                case AnimationHookType.ReplaceObject:
                    instance = new ReplaceObjectHook();
                    break;
                case AnimationHookType.Ethereal:
                    instance = new EtherealHook();
                    break;
                case AnimationHookType.TransparentPart:
                    instance = new TransparentPartHook();
                    break;
                case AnimationHookType.Luminous:
                    instance = new LuminousHook();
                    break;
                case AnimationHookType.LuminousPart:
                    instance = new LuminousPartHook();
                    break;
                case AnimationHookType.Diffuse:
                    instance = new DiffuseHook();
                    break;
                case AnimationHookType.DiffusePart:
                    instance = new DiffusePartHook();
                    break;
                case AnimationHookType.Scale:
                    instance = new ScaleHook();
                    break;
                case AnimationHookType.CreateParticle:
                    instance = new CreateParticleHook();
                    break;
                case AnimationHookType.DestroyParticle:
                    instance = new DestroyParticleHook();
                    break;
                case AnimationHookType.StopParticle:
                    instance = new StopParticleHook();
                    break;
                case AnimationHookType.NoDraw:
                    instance = new NoDrawHook();
                    break;
                case AnimationHookType.DefaultScriptPart:
                    instance = new DefaultScriptPartHook();
                    break;
                case AnimationHookType.CallPES:
                    instance = new CallPESHook();
                    break;
                case AnimationHookType.Transparent:
                    instance = new TransparentHook();
                    break;
                case AnimationHookType.SoundTweaked:
                    instance = new SoundTweakedHook();
                    break;
                case AnimationHookType.SetOmega:
                    instance = new SetOmegaHook();
                    break;
                case AnimationHookType.TextureVelocity:
                    instance = new TextureVelocityHook();
                    break;
                case AnimationHookType.TextureVelocityPart:
                    instance = new TextureVelocityPartHook();
                    break;
                case AnimationHookType.SetLight:
                    instance = new SetLightHook();
                    break;
                case AnimationHookType.CreateBlockingParticle:
                    instance = new CreateBlockingParticleHook();
                    break;
                case AnimationHookType.AnimationDone:
                    instance = new AnimationDoneHook();
                    break;
                case AnimationHookType.DefaultScript:
                    instance = new DefaultScriptHook();
                    break;
            }
            instance?.Unpack(reader);
            return instance;
        }
    }

}
