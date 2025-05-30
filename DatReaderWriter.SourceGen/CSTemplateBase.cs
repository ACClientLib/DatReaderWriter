﻿using DatReaderWriter.SourceGen.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DatReaderWriter.SourceGen {
    /// <summary>
    /// Template base for generate csharp code
    /// </summary>
    public abstract class CSTemplateBase : TextTemplateBase {

        public string GetTypeDeclaration(ACDataMember member) {
            var simplifiedType = SimplifyType(member.MemberType);
            if (XMLDefParser.ACTemplatedTypes.ContainsKey(member.MemberType)) {
                simplifiedType = XMLDefParser.ACTemplatedTypes[member.MemberType].ParentType;
            }
            if (!string.IsNullOrWhiteSpace(member.GenericKey) && !string.IsNullOrWhiteSpace(member.GenericValue))
                return $"{simplifiedType}<{SimplifyType(member.GenericKey)}, {SimplifyType(member.GenericValue)}>";
            if (!string.IsNullOrWhiteSpace(member.GenericType))
                return $"{simplifiedType}<{SimplifyType(member.GenericType)}>";
            return simplifiedType;
        }

        public string GetTypeDeclaration(ACDataType member) {
            foreach (var child in member.Children) {
                if (child is ACVector) {
                    var memberTypes = child.Children.Where(c => c is ACDataMember).Select(
                        c => SimplifyType((c as ACDataMember).MemberType)
                    );
                    return member.Name + "<" + string.Join(", ", memberTypes) + ">";
                }
            }

            return member.Name;
        }

        public string GetTypeDeclaration(ACVector vector) {
            if (!string.IsNullOrEmpty(vector.GenericKey)) {
                return $"{SimplifyType(vector.Type)}<{SimplifyType(vector.GenericKey)}, {SimplifyType(vector.GenericValue)}>";
            }
            else if (!string.IsNullOrEmpty(vector.GenericValue)) {
                return $"{SimplifyType(vector.Type)}<{SimplifyType(vector.GenericValue)}>";
            }
            else {
                return $"{SimplifyType(vector.Type)}[]";
            }
        }

        public override void WriteUsingAliases(Dictionary<string, string> typeAliases) {
            //foreach (var kv in typeAliases) {
            //    WriteLine($"using {kv.Key} = {kv.Value};");
            //}
        }

        private void WriteVectorItemClassDefinition(ACVector vector) {
            WriteSummary($"Vector collection data for the {vector.Name} vector");
            WriteLine($"public class {GetVectorClassName(vector)} {{");
            using (new IndentHelper(this)) {
                var usedNames = new List<string>();
                foreach (var child in vector.Children) {
                    GenerateClassProperties(child, ref usedNames);
                }
            }
            WriteLine("}\n");
        }

        private string GetVectorClassName(ACVector vector, bool fullyQualified = false) {
            var parent = "";
            if (fullyQualified && vector.HasParentOfType(typeof(ACVector))) {
                var p = vector.GetParentOfType<ACVector>();
                while (p != null) {
                    parent = GetVectorClassName(p) + "." + parent;
                    p = p.GetParentOfType<ACVector>();
                }
            }
            return vector.Type;
        }

        /// <summary>
        /// Writes out a csharp xml summary comment
        /// </summary>
        /// <param name="text"></param>
        public override void WriteSummary(string text) {
            if (string.IsNullOrWhiteSpace(text))
                return;

            WriteLine("/// <summary>");
            WriteLine("/// " + WebUtility.HtmlEncode(text));
            WriteLine("/// </summary>");
        }

        /// <summary>
        /// Writes out a csharp class property definition
        /// </summary>
        /// <param name="member"></param>
        public override void WriteClassProperty(ACDataMember member) {
            if (member.Name.StartsWith("_") && member.Parent is ACDataType dType && dType.TypeSwitch == member.Name) {
                WriteSummary(member.Text);
                WriteLine("public abstract " + GetTypeDeclaration(member) + " " + member.Name.Substring(1, 1).ToUpper() + member.Name.Substring(2) + " { get; }\n");
                return;
            }
            if (member.Name.StartsWith("_")) return;
            WriteSummary(member.Text);
            if (XMLDefParser.ACTemplatedTypes.ContainsKey(member.MemberType)) {
                WriteLine("public " + GetTypeDeclaration(member) + " " + member.Name + " = new();\n");
            }
            else {
                WriteLine("public " + GetTypeDeclaration(member) + " " + member.Name + ";\n");
            }
        }

        /// <summary>
        /// Write out a class property definition for a submember
        /// </summary>
        /// <param name="member"></param>
        public override void WriteClassProperty(ACSubMember member) {
            if (member.Name.StartsWith("_")) return;

            var parent = member.Parent as ACDataMember;
            var simplifiedType = SimplifyType(member.Type);
            var getter = member.Value;
            if (!string.IsNullOrWhiteSpace(member.Shift))
                getter = "(" + parent.Name + " >> " + member.Shift + ")";
            if (!string.IsNullOrWhiteSpace(member.Mask))
                getter = "(" + (string.IsNullOrWhiteSpace(getter) ? parent.Name : getter) + " & " + member.Mask + ")";
            WriteSummary(member.Text);
            WriteLine("public " + simplifiedType + " " + member.Name + ";\n");
        }

        /// <summary>
        /// Write out a class property definition for a vector
        /// </summary>
        /// <param name="vector"></param>
        public override void WriteClassVectorProperty(ACVector vector) {
            if (vector.Children.Count > 1)
                WriteVectorItemClassDefinition(vector);

            WriteSummary(vector.Text);
            WriteLine($"public {GetTypeDeclaration(vector)} {vector.Name} = [];\n");
        }

        /// <summary>
        /// Writes out csharp to read an ACEnum
        /// </summary>
        /// <param name="member"></param>
        public override void WriteEnumReader(ACDataMember member) {
            var reader = GetBinaryReaderForType(XMLDefParser.ACEnums[member.MemberType].ParentType);
            if (member.HasParentOfType(typeof(ACVector)) || member.Name.StartsWith("_")) {
                WriteLine("var " + member.Name + " = (" + member.MemberType + ")" + reader + ";");
            }
            else {
                WriteLine(member.Name + " = (" + member.MemberType + ")" + reader + ";");
            }
        }

        /// <summary>
        /// Writes out csharp to read an ACDataType
        /// </summary>
        /// <param name="member"></param>
        public override void WriteDataReader(ACDataMember member) {
            if (member.MemberType == "Vector3") {
                WriteLine($"{member.Name} = reader.ReadVector3();");
                return;
            }
            if (member.MemberType == "Quaternion") {
                WriteLine($"{member.Name} = reader.ReadQuaternion();");
                return;
            }

            /*
            if (member.HasParentOfType(typeof(ACVector))) {
                WriteLine("var " + member.Name + " = new " + GetTypeDeclaration(member) + "()" + ";");
            }
            else {
                WriteLine(member.Name + " = new " + GetTypeDeclaration(member) + "()" + ";");
            }
            */

            WriteLine($"{member.Name} = reader.ReadItem<{member.MemberType}>();");
        }

        /// <summary>
        /// Writes out csharp to read a primitive
        /// </summary>
        /// <param name="member"></param>
        public override void WritePrimitiveReader(ACDataMember member) {
            if (member.Parent is ACVector || member.IsLength || member.Name.StartsWith("_")) {
                WriteLine("var " + member.Name + " = " + GetBinaryReaderForType(member.MemberType, member.Size) + ";");
                if (member.SubMembers.Count > 0) {
                    foreach (var sub in member.SubMembers) {
                        if (!string.IsNullOrEmpty(sub.Mask) && !string.IsNullOrEmpty(sub.Shift)) {
                            WriteLine($"{sub.Name} = ({SimplifyType(sub.Type)})(({member.Name} & {sub.Mask}) >> {sub.Shift});");
                        }
                        else if (!string.IsNullOrEmpty(sub.Mask)) {
                            WriteLine($"{sub.Name} = ({SimplifyType(sub.Type)})({member.Name} & {sub.Mask});");
                        }
                        else if (!string.IsNullOrEmpty(sub.Shift)) {
                            WriteLine($"{sub.Name} = ({SimplifyType(sub.Type)})({member.Name} >> {sub.Shift});");
                        }
                        else {
                            WriteLine($"{sub.Name} = ({SimplifyType(sub.Type)})({member.Name});");
                        }
                    }
                }
            }
            else {
                if (!string.IsNullOrEmpty(member.KnownType)) {
                    WriteLine(member.Name + " = " + GetBinaryReaderForType(member.MemberType, member.KnownType) + ";");
                }
                else {
                    WriteLine(member.Name + " = " + GetBinaryReaderForType(member.MemberType, member.Size) + ";");
                }
            }
        }

        /// <summary>
        /// Write out a csharp if statement condition
        /// </summary>
        /// <param name="condition"></param>
        public override void WriteIfStatementStart(string condition) {
            WriteLine($"if ({condition}) {{");
            Indent();
        }

        /// <summary>
        /// Write out a csharp if statement ending
        /// </summary>
        /// <param name="condition"></param>
        public override void WriteIfStatementEnding(string condition) {
            Outdent();
            WriteLine("}");
        }

        /// <summary>
        /// Write out a csharp else statement condition
        /// </summary>
        public override void WriteElseStatementStart() {
            WriteLine("else {");
            Indent();
        }

        /// <summary>
        /// Write out a csharp else statement ending
        /// </summary>
        public override void WriteElseStatementEnding() {
            Outdent();
            WriteLine("}");
        }

        /// <summary>
        /// Write out csharp code for the start of a mask map check
        /// </summary>
        /// <param name="mask"></param>
        public override void WriteMaskMapCheckStart(ACMaskMap maskMap, ACMask mask) {
            if (!string.IsNullOrEmpty(maskMap.XOR)) {
                WriteIfStatementStart($"(((uint){maskMap.Name} ^ {maskMap.XOR}) & {mask.Value}) != 0");
            }
            else if (mask.Value.StartsWith("0x")) {
                WriteIfStatementStart($"((uint){maskMap.Name} & (uint){mask.Value}) != 0");
            }
            else {
                var parts = mask.Value
                    .Split('|').Select(v => v.Trim())
                    .Select(a => {
                        if (a.StartsWith("!")) {
                            return $"!{maskMap.Name}.HasFlag({a.Substring(1)})";
                        }
                        else {
                            return $"{maskMap.Name}.HasFlag({a})";
                        }
                    });
                WriteIfStatementStart(string.Join(" || ", parts));
            }
        }

        /// <summary>
        /// Write out csharp code for the ending of a mask map check
        /// </summary>
        /// <param name="mask"></param>
        public override void WriteMaskMapCheckEnding(ACMaskMap maskMap, ACMask mask) {
            if (!string.IsNullOrEmpty(maskMap.XOR))
                WriteIfStatementEnding($"((uint)({maskMap.Name} ^ {maskMap.XOR}) & {mask.Value}) != 0");
            else
                WriteIfStatementEnding($"((uint){(mask.Parent as ACMaskMap).Name} & {mask.Value}) != 0");
        }

        /// <summary>
        /// Write out the code to align the buffer
        /// </summary>
        /// <param name="align"></param>
        public override void WriteAlignmentCheck(ACAlign align) {
            WriteLine($"reader.Align(4);");
        }

        /// <summary>
        /// Write out the start of a switch statement
        /// </summary>
        /// <param name="acswitch"></param>
        public override void WriteSwitchStart(ACSwitch acswitch) {
            WriteLine($"switch({acswitch.Name}) {{");
            Indent();
        }

        /// <summary>
        /// Write out csharp code for the ending of a switch statement
        /// </summary>
        /// <param name="acswitch"></param>
        public override void WriteSwitchEnding(ACSwitch acswitch) {
            Outdent();
            WriteLine("}");
        }

        /// <summary>
        /// Write out csharp code for the start of a switch case statement
        /// </summary>
        /// <param name="scase"></param>
        public override void WriteSwitchCaseStart(ACSwitchCase scase) {
            var cases = scase.Value.Split(new string[] { " | " }, StringSplitOptions.None);
            foreach (var s in cases) {
                WriteLine("case " + s + ":");
            }
            Indent();
        }

        /// <summary>
        /// Write out csharp code for the ending of a switch case statement
        /// </summary>
        /// <param name="scase"></param>
        public override void WriteSwitchCaseEnding(ACSwitchCase scase) {
            WriteLine("break;");
            Outdent();
        }

        /// <summary>
        /// Write out the csharp code for the start of a for loop
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="depth"></param>
        public override string WriteForLoopStart(ACVector vector, int depth) {
            var indexVar = new string[] { "i", "x", "y", "z", "q", "p", "t", "r", "f", "g", "h", "k", "u", "v" }[depth];

            if (!string.IsNullOrEmpty(vector.Skip)) {
                WriteLine($"for (var {indexVar}=0; {indexVar} < {vector.Length} - {vector.Skip}; {indexVar}++) {{");
            }
            else if (!string.IsNullOrEmpty(vector.LengthMod)) {
                WriteLine($"for (var {indexVar}=0; {indexVar} < {vector.Length} + {vector.LengthMod}; {indexVar}++) {{");
            }
            else {
                WriteLine($"for (var {indexVar}=0; {indexVar} < {vector.Length.Replace("parent.", "")}; {indexVar}++) {{");
            }
            Indent();
            return indexVar;
        }

        /// <summary>
        /// Write out the csharp code for the end of a for loop
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="depth"></param>
        public override void WriteForLoopEnding(ACVector vector, int depth) {
            Outdent();
            WriteLine("}");
        }

        /// <summary>
        /// Write out the csharp code to push a single item to a vector
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="child"></param>
        public override void WriteVectorPusher(ACVector vector, ACBaseModel child) {
            var name = child is ACVector ? (child as ACVector).Name : (child as ACDataMember).Name;
            WriteLine($"{vector.Name}.Add({name});");
        }

        /// <summary>
        /// Write out the csharp code to push a complex item to a vector
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="children"></param>
        public override void WriteVectorPusher(ACVector vector, List<ACBaseModel> children, string loopChar) {
            if (string.IsNullOrEmpty(vector.GenericKey) && string.IsNullOrEmpty(vector.GenericValue)) {
                WriteLine($"{vector.Name}[{loopChar}] = {GetBinaryReaderForType(vector.Type)};");
            }
            else if (string.IsNullOrEmpty(vector.GenericKey)) {
                var type = XMLDefParser.ACDataTypes.Values
                    .FirstOrDefault(t => t.Name == vector.GenericValue);
                if (type != null && type.IsAbstract) {
                    var field = type.Children
                        .FirstOrDefault(c => c is ACDataMember m && m.Name == type.TypeSwitch)
                        as ACDataMember;

                    if (XMLDefParser.ACEnums.TryGetValue(field.MemberType, out var en)) {
                        var reader = GetBinaryReaderForType(XMLDefParser.ACEnums[field.MemberType].ParentType);
                        WriteLine($"var _peekedValue = (" + field.MemberType + ")" + reader + ";");
                    }
                    else {
                        WriteLine($"var _peekedValue = {GetBinaryReaderForType(field.MemberType)};");
                    }
                    WriteLine($"reader.Skip(-sizeof({field.MemberType}));");
                    WriteLine($"{vector.Name}.Add({vector.GenericValue}.Unpack(reader, _peekedValue));");
                }
                else {
                    XMLDefParser.ACDataTypes.TryGetValue(vector.GenericValue, out var vType);
                    if (vType is not null && vType.AllChildren.Any(c => c is ACVector m && m.Length.Contains("parent."))) {
                        var child = vType.AllChildren.First(c => c is ACVector m && m.Length.Contains("parent.")) as ACVector;
                        WriteLine($"var _val = {GetBinaryReaderForType(vector.GenericValue).TrimEnd(')')}{child.Length.Substring(7)});");
                        WriteLine($"{vector.Name}.Add(_val);");
                    }
                    else {

                        if (XMLDefParser.ACEnums.TryGetValue(vector.GenericValue, out var en)) {
                            WriteLine($"{vector.Name}.Add(({vector.GenericValue}){GetBinaryReaderForType(en.ParentType)});");
                        }
                        else {
                            WriteLine($"{vector.Name}.Add({GetBinaryReaderForType(vector.GenericValue)});");
                        }
                    }
                }
            }
            else {
                if (XMLDefParser.ACEnums.ContainsKey(vector.GenericKey)) {
                    var reader = GetBinaryReaderForType(XMLDefParser.ACEnums[vector.GenericKey].ParentType);
                    WriteLine("var _key = (" + vector.GenericKey + ")" + reader + ";");
                }
                else {
                    WriteLine($"var _key = {GetBinaryReaderForType(vector.GenericKey)};");
                }

                var type = XMLDefParser.ACDataTypes.Values
                    .FirstOrDefault(t => t.Name == vector.GenericValue);
                if (type != null && type.IsAbstract) {
                    var field = type.Children
                        .FirstOrDefault(c => c is ACDataMember m && m.Name == type.TypeSwitch)
                        as ACDataMember;


                    if (!string.IsNullOrEmpty(field.Size)) {
                        WriteLine($"reader.ReadBytes({field.Size});");
                    }

                    if (XMLDefParser.ACEnums.TryGetValue(field.MemberType, out var en)) {
                        var reader = GetBinaryReaderForType(XMLDefParser.ACEnums[field.MemberType].ParentType);
                        WriteLine($"var _peekedValue = (" + field.MemberType + ")" + reader + ";");
                    }
                    else {
                        WriteLine($"var _peekedValue = {GetBinaryReaderForType(field.MemberType)};");
                    }

                    if (!string.IsNullOrEmpty(field.Size)) {
                        WriteLine($"reader.Skip(-sizeof({field.MemberType}) + {field.Size});");
                    }
                    else {
                        WriteLine($"reader.Skip(-sizeof({field.MemberType}));");
                    }
                    WriteLine($"var _val = {vector.GenericValue}.Unpack(reader, _peekedValue);");
                    WriteLine($"{vector.Name}.Add(_key, _val);");
                }
                else {
                    XMLDefParser.ACDataTypes.TryGetValue(vector.GenericValue, out var vType);
                    if (vType is not null && vType.AllChildren.Any(c => c is ACVector m && m.Length.Contains("parent."))) {
                        var child = vType.AllChildren.First(c => c is ACVector m && m.Length.Contains("parent.")) as ACVector;
                        WriteLine($"var _val = {GetBinaryReaderForType(vector.GenericValue).TrimEnd(')')}{child.Length.Substring(7)});");
                    }
                    else {
                        if (XMLDefParser.ACEnums.ContainsKey(vector.GenericValue)) {
                            var reader = GetBinaryReaderForType(XMLDefParser.ACEnums[vector.GenericValue].ParentType);
                            WriteLine("var _val = (" + vector.GenericValue + ")" + reader + ";");
                        }
                        else {
                            WriteLine($"var _val = {GetBinaryReaderForType(vector.GenericValue)};");
                        }
                    }
                    WriteLine($"{vector.Name}.Add(_key, _val);");
                }
            }
        }

        private void WriteVectorChildDefinition(ACBaseModel child) {
            if (child.GetType() != typeof(ACVector) && child.GetType() != typeof(ACDataMember) && child.GetType() != typeof(ACSwitch)) {
                WriteLine("Unknown Child: " + child.GetType());
                return;
            }

            if (child is ACSwitch) {
                foreach (var cchild in (child as ACSwitch).Cases) {
                    foreach (var ccchild in cchild.Children) {
                        WriteVectorChildDefinition(ccchild);
                    }
                }
                return;
            }

            var name = child is ACVector ? (child as ACVector).Name : (child as ACDataMember).Name;
            WriteLine($"{name} = {name},");
        }

        /// <summary>
        /// Write out the csharp code to define a local child vector
        /// </summary>
        /// <param name="vector"></param>
        public override void WriteLocalVectorDefinition(ACVector vector) {
            WriteLine($"var {vector.Name} = new {GetTypeDeclaration(vector)}();");
        }

        /// <summary>
        /// Write out the code to define a local child variable
        /// </summary>
        /// <param name="child"></param>
        public override void WriteLocalChildDefinition(ACDataMember child) {
            var type = SimplifyType(child.MemberType);
            switch (type) {
                case "SpellID":
                case "ushort":
                case "short":
                case "ObjectID":
                case "uint":
                case "int":
                case "ulong":
                case "long":
                case "float":
                case "double":
                case "sbyte":
                case "byte":
                    WriteLine($"{type} {child.Name} = 0;");
                    break;
                case "bool":
                    WriteLine($"{type} {child.Name} = false;");
                    break;
                case "string":
                case "bytestring":
                case "rawstring":
                case "compressedstring":
                case "ushortstring":
                case "WString":
                    WriteLine($"{type} {child.Name} = \"\";");
                    break;
                default:
                    WriteLine($"{type} {child.Name} = new();");
                    break;
            }
        }

        /// <summary>
        /// Writes out csharp code to write a type from a buffer
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override string GetBinaryWriterForType(string type) {
            switch (type) {
                case "WORD":
                case "SpellId":
                case "ushort":
                    return "WriteUInt16";
                case "short":
                    return "WriteInt16";
                case "DWORD":
                case "ObjectId":
                case "LandcellId":
                case "uint":
                    return "WriteUInt32";
                case "int":
                    return "WriteInt32";
                case "ulong":
                    return "WriteUInt64";
                case "long":
                    return "WriteInt64";
                case "float":
                    return "WriteSingle";
                case "double":
                    return "WriteDouble";
                case "bool":
                    return "WriteBool";
                case "byte":
                    return "WriteByte";
                case "sbyte":
                    return "WriteSByte";
                case "ushortstring":
                    return "WriteUShortString";
                case "rawstring":
                    return "WriteString";
                case "string":
                    return "WriteString16L";
                case "compressedstring":
                    return "WriteStringCompressed";
                case "bytestring":
                    return "WriteString16LByte";
                case "WString":
                    return "WriteString32L";
                case "Vector3":
                    return "WriteVector3";
                case "Quaternion":
                    return "WriteQuaternion";
                case "CompressedUInt":
                    return "WriteCompressedUInt";
                case "DataIdOfKnownType":
                    return $"WriteDataIdOfKnownType";
                case "obfuscatedstring":
                    return $"WriteObfuscatedString";
                case "guid":
                    return $"WriteGuid";
                default:
                    return $"WriteItem<{type}>";
            }
        }

        /// <summary>
        /// Writes out csharp code to read a type from a buffer
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override string GetBinaryReaderForType(string type, string? size = null) {
            switch (type) {
                case "WORD":
                case "SpellId":
                case "ushort":
                    return "reader.ReadUInt16()";
                case "short":
                    return "reader.ReadInt16()";
                case "DWORD":
                case "ObjectId":
                case "LandcellId":
                case "uint":
                    return "reader.ReadUInt32()";
                case "int":
                    return "reader.ReadInt32()";
                case "ulong":
                    return "reader.ReadUInt64()";
                case "long":
                    return "reader.ReadInt64()";
                case "float":
                    return "reader.ReadSingle()";
                case "double":
                    return "reader.ReadDouble()";
                case "bool":
                    return $"reader.ReadBool({size})";
                case "byte":
                    return "reader.ReadByte()";
                case "sbyte":
                    return "reader.ReadSByte()";
                case "string":
                    return "reader.ReadString16L()";
                case "bytestring":
                    return "reader.ReadString16LByte()";
                case "compressedstring":
                    return "reader.ReadStringCompressed()";
                case "WString":
                    return "reader.ReadString32L()";
                case "PackedWORD":
                    return "reader.ReadPackedWORD()";
                case "Vector3":
                    return "reader.ReadVector3()";
                case "Quaternion":
                    return "reader.ReadQuaternion()";
                case "DataID":
                case "DataId":
                case "PackedDWORD":
                    return "reader.ReadPackedDWORD()";
                case "CompressedUInt":
                    return "reader.ReadCompressedUInt()";
                case "DataIdOfKnownType":
                    return $"reader.ReadDataIdOfKnownType({size})";
                case "obfuscatedstring":
                    return $"reader.ReadObfuscatedString()";
                case "rawstring":
                    return $"reader.ReadString()";
                case "ushortstring":
                    return $"reader.ReadUShortString()";
                case "guid":
                    return $"reader.ReadGuid()";
                default:
                    return $"reader.ReadItem<{type}>()";
            }
        }
    }
}
