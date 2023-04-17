
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

//change file paths to access files
string inputFile = @"C:\Users\drew.wagner\Desktop\EntityToProto\input.txt";
string inputFile2 = @"C:\Users\drew.wagner\Desktop\EntityToProto\input.txt";
string outputFile = @"C:\Users\drew.wagner\Desktop\EntityToProto\output.txt";
string outputFile2 = @"C:\Users\drew.wagner\Desktop\EntityToProto\output2.txt";

string dtoName;
Console.WriteLine("Enter Dto Name");
dtoName = Console.ReadLine();

Console.WriteLine(ConvertEntity.ToProto(inputFile, outputFile));

if(dtoName != null)
    Console.WriteLine(ConvertEntity.ToDto(dtoName, inputFile2, outputFile2));
