namespace Homework2_LiudvynskyiV.S;

public class Task1
{
    private readonly int[,] _arr;
    private readonly int _height;
    private readonly int _width;
    private int _num = 0;

    public Task1(int width, int height)
    {
        _width = width;
        _height = height;
        _arr = new int[height, width];
    }

    public void PrintVerticalSnakeTypeMatrix()
    {
        for (var i = 0; i < _height; i++)
        {
            for (var j = 0; j < _width; j++)
            {
                _num = i + 1;
                Console.Write($"{_num + _height * j} ");
            }
            Console.WriteLine();
        }
    }

    public void PrintDiagonalSnakeTypeMatrix()
    {
        var N = _height;
        
        if (_height == _width)
        {
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    _arr[i, j] = 0;
                }
            }
            for (var ik = 0; ik < _height; ik++)
            {
                for (var jk = 0; jk < _width; jk++)
                {
                    var i = ik + 1;
                    var j = jk + 1;
                    
                    int D = i + j - 1;
                    int Ma = (D * D + D) / 2;
                    int Mb =  (N * N + N) / 2 + ((3 * N - D - 1) * (D - N)) / 2; 
                    int Ca = Math.Abs(D / (N + 1) - 1);
                    int Cb = D / (N + 1);
                    int Co = D % 2;
                    int Ce = (D + 1) % 2;
                    _arr[ik, jk] = Ca *((Ma - j + 1)* Ce + (Ma - i + 1) * Co) 
                                + Cb * ((Mb - (N - i)) * Ce + (Mb - (N - j)) * Co);
                }   
            }

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Console.Write($"{_arr[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Your matrix is not square!");
        }
    }

    public void PrintSpiralSnakeTypeMatrix()
    {
        int value = 1;
         
        int minCol = 0;
         
        int maxCol = _width - 1;
         
        int minRow = 0;
         
        int maxRow = _height - 1;

        while (value <= _width * _height)
        {
            for (int i = minRow; i <= maxRow; i++)
            {
                _arr[i, minCol] = value;

                value++;
            }

            for (int i = minCol + 1; i <= maxCol; i++)
            {
                _arr[maxRow, i] = value;

                value++;
            }

            for (int i = maxRow - 1; i >= minRow; i--)
            {
                _arr[i, maxCol] = value;

                value++;
            }

            for (int i = maxCol - 1; i >= minCol + 1; i--)
            {
                _arr[minRow, i] = value;

                value++;
            }

            minCol++;

            minRow++;

            maxCol--;

            maxRow--;
        }
        
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                Console.Write($"{_arr[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}