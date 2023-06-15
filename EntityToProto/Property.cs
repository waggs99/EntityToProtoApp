using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityToProto
{
    internal class Property
    {
        public string AccessModifier { get; set; } = "private";
        public string Type { get; set; }
        public string Name { get; set; }
        public bool Nullable { get; set; } = false;
        public string GrpcType { get; set; }
        public bool RequiresExplicitCast { get; set; } = false;
        public string? ExplicitCastType { get; set; }
        public bool IsDateTime { get; set; } = false;
        public string? DefaultValue { get; set; }

        public Property(string declaration)
        {
            string[] tokens = declaration.Split(' ');
            int length = tokens.Length;
            if (length >= 3)
            {
                //throw new Exception("Error with given model.");

                AccessModifier = tokens[0];
                Type = tokens[1];
                Name = tokens[2];
                if (GRPCtoCStypeMappings.typePairs.ContainsKey(Type)) 
                {
                    GrpcType = GRPCtoCStypeMappings.typePairs[Type];
                }
                else if(GRPCtoCStypeMappings.typePairs.ContainsKey(Type.ToLower()))
                {
                    GrpcType = GRPCtoCStypeMappings.typePairs[Type.ToLower()];
                }
                else
                {
                    return;
                }

                if(Type.EndsWith('?'))
                    Nullable = true;


                int i = 3;
                while (i < length)
                {
                    if (tokens[i].Contains("}"))
                    {
                        i++;

                        if (i >= length)
                            return;
                        else
                        {
                            if (tokens[i].StartsWith('='))
                            {
                                if (tokens[i].EndsWith("="))
                                    i++;
                                else
                                {
                                    tokens[i] = tokens[i].Substring(1);
                                }
                            }
                            else
                            {
                                return;
                            }
                            DefaultValue = "";
                            if (tokens[i].EndsWith(";"))
                            {
                                DefaultValue += tokens[i].Substring(0, length-1);
                                return;
                            }
                            while (i < length)
                            {
                                if (tokens[i].EndsWith(';'))
                                {
                                    DefaultValue += tokens[i].Substring(0, length - 1);
                                    return;
                                }
                                else
                                {
                                    DefaultValue += tokens[i];
                                    i++;
                                }
                            }
                        }
                    }
                }
            }
        }

        public Property(string[] tokens)
        {
            int length = tokens.Length;
            if (length >= 3)
            {
                //throw new Exception("Error with given model.");

                AccessModifier = tokens[0];
                Type = tokens[1];
                Name = tokens[2];
                if (GRPCtoCStypeMappings.typePairs.ContainsKey(Type))
                {
                    GrpcType = GRPCtoCStypeMappings.typePairs[Type];
                }
                else if (GRPCtoCStypeMappings.typePairs.ContainsKey(Type.ToLower()))
                {
                    GrpcType = GRPCtoCStypeMappings.typePairs[Type.ToLower()];
                }
                else
                {
                    return;
                }

                if (Type.EndsWith('?'))
                    Nullable = true;


                int i = 3;
                while (i < length)
                {
                    if (tokens[i].Contains("}"))
                    {
                        i++;

                        if (i >= length)
                            return;
                        else
                        {
                            if (tokens[i].StartsWith('='))
                            {
                                if (tokens[i].EndsWith("="))
                                    i++;
                                else
                                {
                                    tokens[i] = tokens[i].Substring(1);
                                }
                            }
                            else
                            {
                                return;
                            }
                            DefaultValue = "";
                            if (tokens[i].EndsWith(";"))
                            {
                                DefaultValue += tokens[i].Substring(0, length - 1);
                                return;
                            }
                            while (i < length)
                            {
                                if (tokens[i].EndsWith(';'))
                                {
                                    DefaultValue += tokens[i].Substring(0, length - 1);
                                    return;
                                }
                                else
                                {
                                    DefaultValue += tokens[i];
                                    i++;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static class GRPCtoCStypeMappings
        {
            public static Dictionary<string, string> typePairs = new Dictionary<string, string>() { 
                //Date Types
                {"DateTime?", "google.protobuf.Timestamp" }, { "DateTime", "google.protobuf.Timestamp" }, 
                { "DataTimeOffset", "google.protobuf.Timestamp" }, { "DataTimeOffset?", "google.protobuf.Timestamp" },
                { "TimeSpan", "google.protobuf.Timestamp" }, { "TimeSpan?", "google.protobuf.Timestamp" },

                //Strings
                { "string", "string" }, { "string?", "google.protobuf.StringValue" }, 
                
                //Numeric
                { "int", "int32" }, { "int?", "google.protobuf.Int32Value" },
                { "decimal", "CustomTypes.DecimalValue" }, { "decimal?", "CustomTypes.DecimalValue" }, 
                { "byte", "int32" },{ "byte?", "google.protobuf.Int32Value" },
                { "double", "double" },{ "double?", "google.protobuf.DoubleValue" },
                { "double", "double" },{ "double?", "google.protobuf.DoubleValue" },
                { "long", "int64" }, { "long?", "google.protobuf.Int64Value" },
                { "uint", "uint32" }, { "uint?", "google.protobuf.UInt32Value" },
                { "ulong", "uInt64" },{ "ulong?", "google.protobuf.UInt64Value" },

                //Boolean
                { "bool", "bool" }, { "bool?", "google.protobuf.BoolValue" },

                //Byte Collection
                { "bytes", "ByteString" },
                 
                
                
                //,{ "", "" },{ "", "" },
            };
        }
    }

    
}
