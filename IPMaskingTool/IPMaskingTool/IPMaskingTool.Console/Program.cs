
using IPMaskingToll.Logic.Classes;
using IPMaskingTool.Console;
using System.Text.RegularExpressions;

try
{
    StorageFile storageFile = new StorageFile();
    string? filePath=null;
    do {
        Console.WriteLine("Please Enter a File Path");
        filePath = Console.ReadLine();
    }
    while (filePath==null);
    string[] dataLoad = await storageFile.Load(filePath);
    do {
        Console.WriteLine("Please Enter Where Do You Want To Store The File ");
        filePath = Console.ReadLine();
    }
    while(filePath == null);
    await storageFile.Save(filePath, dataLoad[1]);
    Console.WriteLine("The File Was Saved Successfully");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}