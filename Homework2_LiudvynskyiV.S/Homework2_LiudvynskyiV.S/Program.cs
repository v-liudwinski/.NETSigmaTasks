using Homework2_LiudvynskyiV.S;

var task1 = new Task1(4, 4);
task1.PrintVerticalSnakeTypeMatrix();
Console.WriteLine();
task1.PrintDiagonalSnakeTypeMatrix();
Console.WriteLine();
task1.PrintSpiralSnakeTypeMatrix();
Console.WriteLine();

var task2 = new Task2(new Colors[][]
{
    new [] {Colors.Green, Colors.Black, Colors.Blue},
    new [] { Colors.Black, Colors.Orange},
    new [] { Colors.Pink }
});
Console.WriteLine(task2.MaxLineWithStartEndIndexes());