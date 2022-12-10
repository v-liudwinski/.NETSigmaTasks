using Homework9_LiudvynskyiV.S;

var file = File.ReadAllLines
    (@"D:\VisualStudio\.NETSigmaTasks\Homework9_LiudvynskyiV.S\numbers.txt");
var array = file.First().Split(',')
    .Select(x => int.TryParse
        (x, out var n) ? n : 0)
    .ToArray();

var splitMergeAlgorithm = new SplitMergeAlgorithm();
var res = splitMergeAlgorithm.SortArray(array, 0, array.Length - 1);

File.WriteAllText
    (@"D:\VisualStudio\.NETSigmaTasks\Homework9_LiudvynskyiV.S\RESULT.txt",
    string.Join(",", res));
    
array = new int[] { 73, 57, 49, 99, 133, 20, 1 };
var fastAlgorithm = new FastSortingAlgorithm();
res = fastAlgorithm.SortArray(array, 0, array.Length - 1);
foreach (var n in res)
{
    Console.Write($"{n} ");
}