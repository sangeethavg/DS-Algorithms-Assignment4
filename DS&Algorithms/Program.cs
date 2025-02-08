// See https://aka.ms/new-console-template for more information
using System.Collections.Concurrent;
using System.ComponentModel;

Console.WriteLine("Hello, World!");
//Write an efficient program to find the sum of contiguous subarray within a one-dimensional 
//array of numbers which has the largest sum.
int MaxContinuousSum(int[] input)
{
    var maxSum = 0;
    var partialsum =0;
    for(int i = 0; i < input.Length; i++)
    {
        partialsum = Math.Max(input[i], partialsum + input[i]);
        if (maxSum < partialsum)
        {
            maxSum  = partialsum;
        }
    }
   
    return maxSum;
}
Console.WriteLine(MaxContinuousSum([-2, 1, -3, 4, -1, 2, 1, -5, 4]));

//You are given a list of n-1 integers and these integers are in the range of 1 to n. There are no 
//duplicates in list. One of the integers is missing in the list. Write an efficient code to find the 
//missing integer.

int missingInteger(int[] input)
{
    var missingInteger = 0;
    var n = input.Length + 1;
    missingInteger = (n * (n + 1) / 2) - (input.Sum()); 
    return missingInteger;
}
Console.WriteLine(missingInteger([1,2,3,5,6,7,8,9,10]));

//Given an unsorted array of nonnegative integers, find a contiguous subarray which adds to a 
//given number

(int, int) SubArraySum(int[] input, int sum)
{
    var returnArray = new int[input.Length];
    int tempSum = 0, startIndex = 0, endIndex = 0;
    if (sum > 0)
    {
       
        for (int i = 0; i < input.Length; i++)
        {
            tempSum += input[i];
            if (tempSum == sum)
            {
                endIndex = i;
                return (startIndex, endIndex);
            }
            else if (tempSum > sum)
            {
                i = startIndex + 1;
                startIndex = i;
                tempSum = input[i];
            }
        }
    }
    return (startIndex, endIndex);

}
Console.WriteLine(SubArraySum([1, 4, 0, 0, 3, 10, 5],7));

//Given an unsorted array of integers, find a subarray which adds to a given number. If there are 
//more than one subarrays with sum as the given number, print any of them.

(int, int)subArraySum2(int[] input, int sum)
{
    int startIndex = 0, endIndex = 0;
    var tempSum = 0;
    Dictionary<int, int> dict = new Dictionary<int, int>();
    for (int i = 0; i<input.Length; i++)
    {
        tempSum += input[i];
        if (tempSum == sum)
        {
            return(startIndex, i);
        }
        if (dict.ContainsKey(tempSum-sum))
        {
            startIndex = dict[tempSum - sum]+1;
            //foreach (var item in dict)
            //{
            //    Console.WriteLine($"Key:{item.Key} and Value:{item.Value}");
            //}
            return (startIndex, i);
        }
        if (!dict.ContainsKey(tempSum))
        {
            dict[tempSum] = i;
        }
       
    }
   
    return (startIndex, endIndex);
}
Console.WriteLine(subArraySum2([20, 2, -2, -30, 10], 10));


//Write a program to sort an array of 0's,1's and 2's in ascending order.
int[] SortMyNumArray(int[] inputArr) {
    int zeroIndex = 0;
    int twoIndex = inputArr.Length - 1;
    int oneIndex = 0;
    while(oneIndex<=twoIndex)
    {
        if (inputArr[oneIndex] == 0)
        {
            SwapElements(zeroIndex, oneIndex, ref inputArr);
            zeroIndex++;
            oneIndex++;
        }
        else if (inputArr[oneIndex] == 1)
        {
           oneIndex++;
        }
        else if (inputArr[oneIndex] == 2)
        {
            SwapElements(oneIndex, twoIndex, ref inputArr);
            twoIndex--;
            
        } 
    }
    return inputArr;
}
void SwapElements(int a, int b, ref int[] arr)
{
    int temp = arr[a];
    arr[a] = arr[b];
    arr[b] = temp;
}

SortMyNumArray([2,2,1,1,1,0,2,0]).ToList().ForEach(element => Console.Write($"{element}"));
Console.WriteLine("");

//Write a function int equilibrium(int[] arr, int n); that given a sequence arr[] of size n, returns an 
//equilibrium index (if any) or -1 if no equilibrium indexes exist.
//: A[] = {-7, 1, 5, 2, -4, 3, 0}
int Equilibrium(int[] arr, int n)
{
    int eqbIndex = -1;
    int i = 0, j = n - 1;
    int firstSum = 0, secondSum =0;
    Dictionary<int, int> fCumulative = new Dictionary<int, int>();
    Dictionary<int, int> sCumulative = new Dictionary<int, int>();
    while (i < n && j >= 0) {
        firstSum += arr[i];
        fCumulative[i] = firstSum;
        secondSum += arr[j];
        sCumulative[j] = secondSum;
        i++;
        j--;
    }
    var sKeys = sCumulative.Keys;
    foreach (var item in fCumulative)
    {
        if (sKeys.Contains(item.Key))
        {
            if (fCumulative[item.Key] == sCumulative[item.Key])
            {
                return item.Key;
            }
        }
    }
    return eqbIndex;
}
Console.WriteLine(Equilibrium([1, -1, 2, -2, 3, -3, 4],7));

//Write a program to print all the LEADERS in the array. An element is leader if it is greater than all 
//the elements to its right side. And the rightmost element is always a leader. For example int the 
//array {16, 17, 4, 3, 5, 2}, leaders are 17, 5 and 2.

int[] ArrayLeaders(int[] inputArr)
{
    List<int> leadArr = new List<int>();
    leadArr.Add(inputArr.Last());
    int max = inputArr.Last();
    for (int i = inputArr.Length-1; i>=0;i--)
    {
        if (inputArr[i]>max)
        {
            max = inputArr[i];
        }
        else if (inputArr[i]<max)
        {
            leadArr.Add(max);
            max = inputArr[i];
        }
    }
    return leadArr.ToArray();
}
ArrayLeaders([16, 17, 4, 3, 5, 2]).Reverse().ToList().ForEach(element => Console.WriteLine(element));
Console.WriteLine("-----------------------------------------");
//Given an array and a number k where k is smaller than size of array, we need to find the k’th 
//smallest element in the given array. It is given that all array elements are distinct.

int kThSmallestElement(int[] inputArr,int low, int high, int k)
{
    int index = Partition(inputArr, low, high);
    if (index == k)
    {
        return inputArr[index];
    }
    else if(index < k)
    {
        return kThSmallestElement(inputArr, index+1, high,k);
    }
    else
    {
        return kThSmallestElement(inputArr, low, index - 1, k);
    }
}
int Partition(int[] inputArr, int low, int high)
{
    int pivotIndex = low, pivot = inputArr[high],temp;
    for (int i = low; i <= high; i++)
    {
        if (inputArr[i] < pivot)
        {
            temp = inputArr[i];
            inputArr[i] = inputArr[pivotIndex];
            inputArr[pivotIndex] = temp;
            pivotIndex++;
        }

    }
    temp = inputArr[high];
    inputArr[high] = inputArr[pivotIndex];
    inputArr[pivotIndex] = temp;
    return pivotIndex;
}
int[] arr = [7, 10, 4, 3, 20, 15];
int k = 3;
Console.WriteLine(kThSmallestElement(arr, 0, arr.Length - 1, k-1));
Console.WriteLine("-----------------------------------------------");
//Given a 2D array, print it in spiral form. 
List<int>SpiralTraverse(int[][] matrix)
{
    int m = matrix.Length;
    int n = matrix[1].Length;
    List<int> result = new List<int>();

    // Initialize boundaries
    int top = 0, bottom = m - 1, left = 0, right = n - 1;

    // Iterate until all elements are printed
    while (top <= bottom && left <= right)
    {

        // Print top row from left to right
        for (int i = left; i <= right; ++i)
        {
            result.Add(matrix[top][i]);
        }
        top++;

        // Print right column from top to bottom
        for (int i = top; i <= bottom; ++i)
        {
            result.Add(matrix[i][right]);
        }
        right--;

        // Print bottom row from right to left (if exists)
        if (top <= bottom)
        {
            for (int i = right; i >= left; --i)
            {
                result.Add(matrix[bottom][i]);
            }
            bottom--;
        }

        // Print left column from bottom to top (if exists)
        if (left <= right)
        {
            for (int i = bottom; i >= top; --i)
            {
                result.Add(matrix[i][left]);
            }
            left++;
        }
    }

    return result;
}
int[][] mat = { new int[] { 1, 2, 3, 4 },
                        new int[] { 5, 6, 7, 8 },
                        new int[] { 9, 10, 11, 12 },
                        new int[] { 13, 14, 15, 16 } };
SpiralTraverse(mat).ForEach(e => Console.Write(e));
Console.WriteLine("----------------------------------------");

//10.Print the elements of an array in the decreasing frequency if 2 numbers have same frequency 
//then print the one which came first.

List<int> FrequencyArray(int[] inputArr)
{
    Dictionary<int,int> freqDic = new Dictionary<int,int>();
    Dictionary<int, int> orderDic = new Dictionary<int,int>();
    for (int i = 0; i < inputArr.Length; i++)
    {
        if (freqDic.ContainsKey(inputArr[i]))
        {
            freqDic[inputArr[i]]++;
        }
        else
        {
            freqDic[inputArr[i]] = 1;
            orderDic[inputArr[i]] = i;
        }
    }
    var sorted = inputArr.Distinct()
                       .OrderByDescending(x => freqDic[x]) // Sort by frequency
                       .ThenBy(x => orderDic[x]) // Sort by first occurrence
                       .ToList();
    List<int> result = new List<int>();
    foreach (var item in sorted)
    {
        int j = 0;
        while (j < freqDic[item])
        {
            j++;
            Console.Write(item);
        }
    }
    
    return result;
}
FrequencyArray([2, 5, 2, 6, -1, 9999999, 5, 8, 8, 8]);
