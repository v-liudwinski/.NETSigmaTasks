namespace Homework9_LiudvynskyiV.S;

public class FastSortingAlgorithm
{
    public int[] SortArray(int[] array, int leftIndex, int rightIndex)
    {
        var i = leftIndex;
        var j = rightIndex;
        var pivot = array[leftIndex];

        while (i <= j)
        {
            while (array[i] < pivot)
            {
                i++;
            }
        
            while (array[j] > pivot)
            {
                j--;
            }

            if (i <= j)
            {
                (array[i], array[j]) = (array[j], array[i]);
                i++;
                j--;
            }
        }
    
        if (leftIndex < j)
            SortArray(array, leftIndex, j);

        if (i < rightIndex)
            SortArray(array, i, rightIndex);

        return array;
    }
}