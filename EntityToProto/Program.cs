
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

//change file paths to access files
//string inputFile = @"C:\Users\drew.wagner\Desktop\EntityToProto\input.txt";
string inputFile = @"C:\Users\drew.wagner\Documents\Visual Studio 2022\BlazorPartsPortalv3\GrpcPartsPortalApi\Models\Flex2kSqlModels\TblPartsOrder.cs";
//string inputFile2 = @"C:\Users\drew.wagner\Desktop\EntityToProto\input2.txt";
//string inputFile3 = @"C:\Users\drew.wagner\Desktop\EntityToProto\input3.txt";
//string inputFile4 = @"C:\Users\drew.wagner\Desktop\EntityToProto\input4.txt";
//string inputFile5 = @"C:\Users\drew.wagner\Desktop\EntityToProto\input5.txt";
//string inputFile6 = @"C:\Users\drew.wagner\Desktop\EntityToProto\input6.txt";
string outputFile = @"C:\Users\drew.wagner\Desktop\EntityToProto\output.txt";
string outputFile2 = @"C:\Users\drew.wagner\Desktop\EntityToProto\output2.txt";
string outputFile3 = @"C:\Users\drew.wagner\Desktop\EntityToProto\output3.txt";
string outputFile4 = @"C:\Users\drew.wagner\Desktop\EntityToProto\output4.txt";
//string outputFile5 = @"C:\Users\drew.wagner\Desktop\EntityToProto\output5.txt";
//string outputFile6 = @"C:\Users\drew.wagner\Desktop\EntityToProto\output6.txt";

string dtoName;
Console.WriteLine("Enter Dto Name");
dtoName = Console.ReadLine();

Console.WriteLine(ConvertEntity.ToProto(inputFile, outputFile));
//Console.WriteLine(ConvertEntity.ToProto(inputFile2, outputFile2));
//Console.WriteLine(ConvertEntity.ToProto(inputFile3, outputFile3));
//Console.WriteLine(ConvertEntity.ToProto(inputFile4, outputFile4));
//Console.WriteLine(ConvertEntity.ToProto(inputFile5, outputFile5));
//Console.WriteLine(ConvertEntity.ToProto(inputFile6, outputFile6));
Console.WriteLine(ConvertEntity.ToDto(dtoName, inputFile, outputFile2));
Console.WriteLine(ConvertEntity.ToEntity(dtoName, inputFile, outputFile3));
Console.WriteLine(ConvertEntity.ToUpdate(dtoName, inputFile, outputFile4));
//Console.WriteLine(ConvertEntity.ToDto(dtoName, inputFile2, outputFile2));
//Console.WriteLine(ConvertEntity.ToDto(dtoName, inputFile3, outputFile3));
//Console.WriteLine(ConvertEntity.ToDto(dtoName, inputFile4, outputFile4));
//Console.WriteLine(ConvertEntity.ToDto(dtoName, inputFile5, outputFile5));
//Console.WriteLine(ConvertEntity.ToDto(dtoName, inputFile6, outputFile6));
//if(dtoName != null)
//  Console.WriteLine(ConvertEntity.ToDto(dtoName, inputFile, outputFile2));
