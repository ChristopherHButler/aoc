using System.Globalization;
using System.Runtime.ExceptionServices;
using csharp.utils;

namespace csharp.Puzzles;


public class FileObj
{
    public int Id { get; set; }
    public int FileLength { get; set; }
    public string FileBlockId { get; set; } = string.Empty;
}


public static class Dec09
{
    public static long solvePart1(string? date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 09, Part 1: \n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        Console.WriteLine("Disk Map: ");
        for (int i = 0; i < dfr.Lines.Count; i++) 
        {
            Console.WriteLine(dfr.Lines[i]);
        }

        var blockMap = ConvertDiskMapToBlockMap(dfr.Lines[0]);

        // Console.WriteLine("Block Map: ");
        // for (int i = 0; i < blockMap.Count; i++) 
        // {
        //     Console.Write(blockMap[i].Item1);
        // }
        // Console.WriteLine();
        // for (int i = 0; i < blockMap.Count; i++) 
        // {
        //     Console.Write(blockMap[i].Item2);
        // }
        // Console.WriteLine();

				// Console.WriteLine("Move File Blocks...");
        MoveFileBlocks(blockMap);

        Console.WriteLine("re-ordered Block Map: ");
        for (int i = 0; i < blockMap.Count; i++) 
        {
            Console.Write(blockMap[i].Item1);
        }
        Console.WriteLine();
        
        var total = ComputeChecksum(blockMap);

        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

				return total;
    }

    public static long solvePart2(string date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 09, Part 2: \n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        // Console.WriteLine("Disk Map: ");
        // for (int i = 0; i < dfr.Lines.Count; i++) 
        // {
        //     Console.WriteLine(dfr.Lines[i]);
        // }

        var blockMap = ConvertDiskMapToFileBlockMap(dfr.Lines[0]);

        Console.WriteLine("Block map created");

        // Console.WriteLine("Block Map: ");
        // for (int i = 0; i < blockMap.Count; i++) 
        // {
        //     Console.Write(blockMap[i].FileBlockId);
        // }
        // Console.WriteLine();

        MoveCompleteFiles(blockMap);

        Console.WriteLine("Files moved");

        // Console.WriteLine("re-ordered Block Map: ");
        // for (int i = 0; i < blockMap.Count; i++) 
        // {
        //     Console.Write(blockMap[i].FileBlockId);
        // }
        // Console.WriteLine();

				var total = ComputeNewChecksum(blockMap);

        
        
        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

		return total;
    }


    public static List<Tuple<string, int>> ConvertDiskMapToBlockMap(string diskMap)
    {
        // split the string into an array of strings
        var diskMapArray = diskMap.ToCharArray();

        // just create a list of strings. we'll add the id number for the current block or a . for an empty space
        // string is the block number and int is the file id
        // when the id starts getting into double or triple digits the id will be lost if we just use a string
        List<Tuple<string, int>> blockMap = new List<Tuple<string, int>>();

        int count = 0;

        // loop through the diskMapArray and add the block id or a . to the blockMap
        for (int i = 0; i < diskMapArray.Length; i++)
        {
            // check if the current char is a file or free space
            // parse the digit
            var digit = int.Parse(diskMapArray[i].ToString());

            // Console.WriteLine($"Digit: {digit}");

            for (int j = 0; j < digit; j++)
            {
                // it's a file
                if (i % 2 == 0)
                {
                  // Console.WriteLine($"its a file");
                  // Console.WriteLine($"i = {i}");
                    // add the id as many number of times as the digit in the diskmap
                    // blockMap.Add((count).ToString());
                    blockMap.Add(new Tuple<string, int>((count).ToString(), count));
                }
                // it's free space
                else
                {
                  // Console.WriteLine("its NOT a file");
                    // add a . for free space
                    // blockMap.Add(".");
                    blockMap.Add(new Tuple<string, int>(".", -1));
                }
            }
            // the count needs to be updated after each digit to get the right id
            if (i%2 ==0) count++;
        }
        return blockMap;
    }

    public static void MoveFileBlocks(List<Tuple<string, int>> blockMap)
    {
        // move file blocks one at a time from the end of the disk to the left most free space block until there are no gaps

        // loop through the blockMap in reverse
        for (int i = blockMap.Count - 1; i >=0; i--)
        {
            // Console.WriteLine($"File Block: {blockMap[i].Item1} - {blockMap[i].Item2}");
            // skip if it's a free space block
            if (blockMap[i].Item2 == -1) continue; 

            // now every block should be a file block

            // loop through the block map from the beginning and find the first free space block
            for (int j = 0; j < blockMap.Count; j++)
            {
                if (j >= i) break;
                // if it's a free space block, move the current file block
                if (blockMap[j].Item2 == -1)
                {
                    // Console.WriteLine($"Free Block: {blockMap[j].Item1} - {blockMap[j].Item2}");
                    // move the file block to the free space
                    blockMap[j] = blockMap[i];
                    // set the current block to a free space
                    blockMap[i] = new Tuple<string, int>(".", -1);
                    break;
                }
            }
        }
        
    }


    public static List<FileObj> ConvertDiskMapToFileBlockMap(string diskMap)
    {
        // split the string into an array of strings
        var diskMapArray = diskMap.ToCharArray();

        // just create a list of strings. we'll add the id number for the current block or a . for an empty space
        // string is the block number and int is the file id
        // when the id starts getting into double or triple digits the id will be lost if we just use a string
        List<FileObj> blockMap = new List<FileObj>();

        int count = 0;

        // loop through the diskMapArray and add the block id or a . to the blockMap
        for (int i = 0; i < diskMapArray.Length; i++)
        {
            // check if the current char is a file or free space
            // parse the digit
            var digit = int.Parse(diskMapArray[i].ToString());

            // Console.WriteLine($"Digit: {digit}");

            for (int j = 0; j < digit; j++)
            {
                // it's a file
                if (i % 2 == 0)
                {
                  // Console.WriteLine($"its a file");
                  // Console.WriteLine($"i = {i}");
                    // add the id as many number of times as the digit in the diskmap
                    // blockMap.Add((count).ToString());
                    // blockMap.Add(new Tuple<string, int>((count).ToString(), count));
                    blockMap.Add(new FileObj { Id = count, FileLength = digit, FileBlockId = count.ToString() });
                }
                // it's free space
                else
                {
                  // Console.WriteLine("its NOT a file");
                    // add a . for free space
                    // blockMap.Add(".");
                    // blockMap.Add(new Tuple<string, int>(".", -1));
                    blockMap.Add(new FileObj { Id = -1, FileLength = digit, FileBlockId = "." });
                }
            }
            // the count needs to be updated after each digit to get the right id
            if (i%2 ==0) count++;
        }
        return blockMap;
    }


    public static void MoveCompleteFiles(List<FileObj> blockMap)
    {
        // move complete files to the left most free space of blocks
        for (int i = blockMap.Count - 1; i >= 0; i--)
        {
            // skip if it's a free space block
            if (blockMap[i].Id == -1) continue;

            // loop through the block map from the beginning and find the first free space big enough to hold the file
            for (int j = 0; j < blockMap.Count; j++)
            {
                if (j >= i) break;
                // if there is a free space check there are enough free blocks to hold the file
                if (blockMap[j].Id == -1)
                {
                    // check if there are enough free blocks to hold the file
                    // if there are enough free blocks, move the file
                    // if there are not enough free blocks, continue to the next free block
                    var fileLength = blockMap[i].FileLength;

                    for (int b = j; b < j + fileLength; b++)
                    {
                        // break as soon as you know there is not enough space
                        if (b >= blockMap.Count || blockMap[b].Id != -1) break;

                        // if there is enough space, move the file
                        if (b == j + fileLength - 1)
                        {
                            // move the file
                            int fileStartIndex = i - fileLength + 1;
                            for (int k = j; k < j + fileLength; k++)
                            {
                                blockMap[k] = blockMap[fileStartIndex];
                                blockMap[fileStartIndex++] = new FileObj { Id = -1, FileLength = fileLength, FileBlockId = "." };
                            }
                            break;
                        }
                    }
                    
                }
            }

        }
    }

    public static long ComputeChecksum(List<Tuple<string, int>> blockMap)
    {   
        long count = 0;

        for (int i = 0; i < blockMap.Count; i++) 
        {
            if (blockMap[i].Item2 != -1)
            {
                count += (long)(i * blockMap[i].Item2);
            }
        }
        return count;
    }


    public static long ComputeNewChecksum(List<FileObj> blockMap)
    {   
        long count = 0;

        for (int i = 0; i < blockMap.Count; i++) 
        {
            if (blockMap[i].Id != -1)
            {
                count += (long)(i * blockMap[i].Id);
            }
        }
        return count;
    }



}