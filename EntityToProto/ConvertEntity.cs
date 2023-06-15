using EntityToProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ConvertEntity
{
    readonly static string[] CSharpTypes = { "DateTime?", "DateTime", "string", "string?", "int", "int?", "decimal", "decimal?", "bool", "bool?", "byte", "byte?", 
        "double", "double?", "long", "uint", "ulong", "long?", "uint?", "ulong?", "bytes", "DataTimeOffset", "TimeSpan", "DataTimeOffset?", "TimeSpan?" };

    public static string ToProto(string inputFile, string outputFile)
    {
        string inputLines;
        try
        {
            inputLines = File.ReadAllText(inputFile);
        }
        catch (Exception)
        {
            return "Error: File does not exist";
        }

        string[] tokens = inputLines.Split(' ');
        //foreach (string token in tokens)
        // Console.WriteLine(token);
        string output = "";
        HashSet<string> types = new HashSet<string>();
        foreach (string type in CSharpTypes)
            types.Add(type);
        //string temp;
        for (int i = 0, protoMessagePropertyCount = 1; i < tokens.Length; i++)
        {
            if (types.Contains(tokens[i]))
            {
                string protoLine = "";
                switch (tokens[i])
                {
                    case "DateTime?":
                    case "DateTime":
                    case "DataTimeOffset?":
                    case "DataTimeOffset":
                    case "TimeSpan?":
                    case "TimeSpan":
                        protoLine = "google.protobuf.Timestamp";
                        break;
                    case "string":
                        protoLine = "google.protobuf.Timestamp";
                        break;
                    case "string?":
                        protoLine = "google.protobuf.StringValue";
                        break;
                    case "int":
                    case "byte":
                        protoLine = "int32";
                        break;
                    case "int?":
                    case "byte?":
                        protoLine = "google.protobuf.Int32Value";
                        break;
                    case "decimal":
                    case "decimal?":
                        protoLine = "CustomTypes.DecimalValue";
                        break;
                    case "bool":
                        protoLine = "bool";
                        break;
                    case "bool?":
                        protoLine = "google.protobuf.BoolValue";
                        break;
                    case "double":
                        protoLine = "double";
                        break;
                    case "double?":
                        protoLine = "google.protobuf.DoubleValue";
                        break;
                    case "long":
                        protoLine = "int64";
                        break;
                    case "long?":
                        protoLine = "google.protobuf.Int64Value";
                        break;
                    case "uint":
                        protoLine = "uint32";
                        break;
                    case "uint?":
                        protoLine = "google.protobuf.UInt32Value";
                        break;
                    case "ulong":
                        protoLine = "uint64";
                        break;
                    case "ulong?":
                        protoLine = "google.protobuf.UInt64Value";
                        break;
                    case "bytes":
                        protoLine = "ByteString";
                        break;
                    default: break;
                }
                protoLine += " ";
                i++;
                string temp = "";
                if (i < tokens.Length)
                    temp = string.Concat(tokens[i].Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
                else
                    break;
                temp = temp.ToLower();
                protoLine += temp;
                output += protoLine + $" = {protoMessagePropertyCount};\n";
                protoMessagePropertyCount++;
            }
        }
        //File.WriteAllLines(outputFile, output);
        File.WriteAllText(outputFile, output);
        return "Success";
    }

    public static string ToDto(string dtoName, string inputFile, string outputFile)
    {
        string inputLines;
        try
        {
            inputLines = File.ReadAllText(inputFile);
        }
        catch (Exception)
        {
            return "Error: File does not exist";
        }

        string[] tokens = inputLines.Split(' ');
        //foreach (string token in tokens)
        // Console.WriteLine(token);
        string output = "";
        HashSet<string> types = new HashSet<string>();
        foreach (string type in CSharpTypes)
            types.Add(type);
        //string temp;
        for (int i = 0, protoMessagePropertyCount = 1; i < tokens.Length; i++)
        {
            if (types.Contains(tokens[i]))
            {
                bool isDate = false;
                if (tokens[i] == "DateTime" || tokens[i] == "DateTime?")
                    isDate = true;
                i++;
                output += tokens[i] + " = " + dtoName + "." + tokens[i];
                output += isDate ? $"== null ? null : Timestamp.FromDateTimeOffset((DateTimeOffset){ dtoName}.{ tokens[i] }),\n" : ",\n";
            }
        }
        //File.WriteAllLines(outputFile, output);
        File.WriteAllText(outputFile, output);
        return "Success";
    }

    public static string ToEntity(string dtoName, string inputFile, string outputFile)
    {
        string inputLines;
        //IEnumerable<string> lines;
        try
        {
            //lines = File.ReadLines(inputFile);
            inputLines = File.ReadAllText(inputFile);
        }
        catch (Exception)
        {
            return "Error: File does not exist";
        }

        string[] tokens = inputLines.Split(' ');
        //foreach (string token in tokens)
        // Console.WriteLine(token);
        string output = "";

        HashSet<string> types = new HashSet<string>();
        foreach (string type in CSharpTypes)
            types.Add(type);
        //string temp;
        for (int i = 0, protoMessagePropertyCount = 1; i < tokens.Length; i++)
        {
            if (types.Contains(tokens[i]))
            {
                bool isDate = false;
                bool isNullableDecimal = false;
                if (tokens[i] == "DateTime" || tokens[i] == "DateTime?")
                    isDate = true;
                if (tokens[i] == "decimal?" || tokens[i] == "Decimal?")
                    isNullableDecimal = true;
                i++;
                output += tokens[i] + " = "; 
                if(isNullableDecimal)
                {
                    output += $"ToNullable({dtoName}.{tokens[i]}),\n";
                }
                else if(isDate)
                {
                    output += $"{dtoName}.{tokens[i]} == null ? null : {dtoName}.{tokens[i]}.ToDateTimeOffset().DateTime,\n";
                }
                else
                {
                    output += $"{dtoName}.{tokens[i]},\n";
                }
            }
        }
        
        File.WriteAllText(outputFile, output);

        //List<Property> props = new List<Property>();
        //foreach(var str in lines)
        //{
        //    props.Add(new Property(str));
        //}
        //var ot = "";
        //foreach(var prop in props)
        //{
        //    ot += $"{dtoName}.{prop.Name} = ";
        //}

        return "Success";
    }

    public static string ToUpdate(string dtoName, string inputFile, string outputFile)
    {
        string inputLines;
        //IEnumerable<string> lines;
        try
        {
            //lines = File.ReadLines(inputFile);
            inputLines = File.ReadAllText(inputFile);
        }
        catch (Exception)
        {
            return "Error: File does not exist";
        }

        string[] tokens = inputLines.Split(' ');
        //foreach (string token in tokens)
        // Console.WriteLine(token);
        string output = "";

        HashSet<string> types = new HashSet<string>();
        foreach (string type in CSharpTypes)
            types.Add(type);
        //string temp;
        for (int i = 0, protoMessagePropertyCount = 1; i < tokens.Length; i++)
        {
            if (types.Contains(tokens[i]))
            {
                i++;
                output += $"input.{tokens[i]} = {dtoName}.{tokens[i]};\n";
                
            }
        }

        File.WriteAllText(outputFile, output);

        //List<Property> props = new List<Property>();
        //foreach(var str in lines)
        //{
        //    props.Add(new Property(str));
        //}
        //var ot = "";
        //foreach(var prop in props)
        //{
        //    ot += $"{dtoName}.{prop.Name} = ";
        //}

        return "Success";
    }
}








/*
            if (types.Contains(tokens[i]))
            {
                bool isDate = false;
                bool isNullableDecimal = false;
                bool isNullableDateTime = false;
                if (tokens[i] == "DateTime") 
                    isDate = true;
                if (tokens[i] == "DateTime?") 
                    isNullableDateTime = true;
                if (tokens[i] == "decimal?" || tokens[i] == "Decimal?")
                    isNullableDecimal = true;
                i++;
                output += tokens[i] + " ="; 
                if(isNullableDecimal)
                {
                    output += $" ToNullable({dtoName}.{tokens[i]}),\n";
                }
                else if(isDate)
                {
                    output += $" {dtoName}.ToDateTimeOffset().DateTime,\n";
                }
                else if(isNullableDateTime)
                {
                    output += $"= null ? null : {dtoName}.ToDateTimeOffset().DateTime,\n";
                }
                else
                {
                    output += $" {dtoName}.{tokens[i]},\n";
                }
            }
            */