using System.Collections.Generic;
using System.Linq;

namespace JapaneseCrosswords.Models
{
    public static class Solution
    {
        private static void InsBlock(ref bool[] lineCondition, ref bool[] lineKnown, List<int> data, ref bool[] currentCondition, ref int numPossible, ref int[] numVariants, int curBlock, int curPos)
        {
            int n = lineCondition.Length;
            if (curBlock >= data.Count())
            {
                for (int i = curPos; i < n; i++)
                {
                    if (lineKnown[i] && lineCondition[i] != currentCondition[i])
                        return;
                }
                numPossible++;
                for (int i = 0; i < n; i++)
                {
                    if (currentCondition[i])
                        numVariants[i]++;
                    else
                        numVariants[i]--;
                }
                return;
            }
            if (curPos + data[curBlock] > n)
                return;
            int lastFree = n;
            for (int i = curBlock + 1; i < data.Count(); i++)
            {
                lastFree -= data[i] + 1;
            }
            for (int i = curPos; i < curPos + data[curBlock]; i++)
                currentCondition[i] = true;
            for (int i = curPos; i + data[curBlock] <= lastFree; i++)
            {
                bool isCan = true;
                for (int j = i; j < i + data[curBlock]; j++)
                    if (lineKnown[j] && currentCondition[j] != lineCondition[j])
                    {
                        isCan = false;
                        break;
                    }
                if (i + data[curBlock] < n && lineKnown[i + data[curBlock]] && currentCondition[i + data[curBlock]] != lineCondition[i + data[curBlock]])
                    isCan = false;
                if (isCan)
                {
                    InsBlock(ref lineCondition, ref lineKnown, data, ref currentCondition, ref numPossible, ref numVariants, curBlock + 1, i + data[curBlock] + 1);
                }
                if (i + data[curBlock] == lastFree)
                {
                    for (int j = curPos; j < i + data[curBlock]; j++)
                        currentCondition[j] = false;
                    break;
                }
                currentCondition[i] = false;
                if (lineKnown[i] && lineCondition[i] != currentCondition[i])
                {
                    for (int j = curPos; j < i + data[curBlock]; j++)
                        currentCondition[j] = false;
                    break;
                }
                currentCondition[i + data[curBlock]] = true;
            }
        }

        private static bool TryDraw(ref bool[] lineCondition, ref bool[] lineKnown, List<int> data)
        {
            bool[] cur = new bool[lineCondition.Length];
            int numPossible = 0;
            int[] numVariants = new int[lineCondition.Length];
            InsBlock(ref lineCondition, ref lineKnown, data, ref cur, ref numPossible, ref numVariants, 0, 0);
            if (numPossible == 0)
                return false;
            for (int i = 0; i < lineCondition.Length; i++)
            {
                if (numVariants[i] == numPossible)
                {
                    lineKnown[i] = true;
                    lineCondition[i] = true;
                }
                if (numVariants[i] == -numPossible)
                {
                    lineKnown[i] = true;
                    lineCondition[i] = false;
                }
            }
            return true;
        }

        public static bool Solve(List<List<int>> dataRows, List<List<int>> dataColumns, List<List<bool>> grid)
        {
            int rows = dataRows.Count();
            int columns = dataColumns.Count();
            bool[][] isKnown = new bool[rows][];
            int numLeftCells = 0;
            for (int i = 0; i < rows; i++)
            {
                isKnown[i] = new bool[columns];
                for (int j = 0; j < columns; j++)
                {
                    grid[i][j] = false;
                    isKnown[i][j] = false;
                }
            }
            bool[] rowCan = new bool[rows];
            bool[] colCan = new bool[columns];
            bool[] rowDone = new bool[rows];
            bool[] colDone = new bool[columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < dataRows[i].Count(); j++)
                {
                    numLeftCells += dataRows[i][j];
                }
                if (dataRows[i].Count() == 0)
                {
                    rowCan[i] = false;
                    rowDone[i] = true;
                    for (int k = 0; k < columns; k++)
                    {
                        isKnown[i][k] = true;
                    }
                }
                else
                    rowCan[i] = true;
            }
            for (int j = 0; j < columns; j++)
            {
                if (dataColumns[j].Count() == 0)
                {
                    colCan[j] = false;
                    colDone[j] = true;
                    for (int k = 0; k < rows; k++)
                    {
                        isKnown[k][j] = true;
                    }
                }
                else
                    colCan[j] = true;
            }
            int numLeftCellsPrev = numLeftCells + 1;
            while (numLeftCells > 0)
            {
                while (numLeftCells < numLeftCellsPrev)
                {
                    numLeftCellsPrev = numLeftCells;
                    for (int i = 0; i < rows; i++)
                    {
                        if (!rowCan[i] || rowDone[i])
                            continue;
                        bool[] line = new bool[columns];
                        bool[] lineKnown = new bool[columns];
                        for (int j = 0; j < columns; j++)
                        {
                            line[j] = grid[i][j];
                            lineKnown[j] = isKnown[i][j];
                        }
                        if (!TryDraw(ref line, ref lineKnown, dataRows[i]))
                        {
                            return false;
                        }
                        int numKnown = 0;
                        for (int j = 0; j < columns; j++)
                        {
                            if (lineKnown[j] != isKnown[i][j])
                            {
                                grid[i][j] = line[j];
                                isKnown[i][j] = true;
                                colCan[j] = true;
                                if (grid[i][j])
                                    numLeftCells--;
                            }
                            if (isKnown[i][j])
                                numKnown++;
                        }
                        if (numKnown == columns)
                            rowDone[i] = true;
                        rowCan[i] = false;
                    }
                    for (int i = 0; i < columns; i++)
                    {
                        if (!colCan[i] || colDone[i])
                            continue;
                        bool[] line = new bool[rows];
                        bool[] lineKnown = new bool[rows];
                        for (int j = 0; j < rows; j++)
                        {
                            line[j] = grid[j][i];
                            lineKnown[j] = isKnown[j][i];
                        }
                        if (!TryDraw(ref line, ref lineKnown, dataColumns[i]))
                        {
                            return false;
                        }
                        int numKnown = 0;
                        for (int j = 0; j < rows; j++)
                        {
                            if (lineKnown[j] != isKnown[j][i])
                            {
                                grid[j][i] = line[j];
                                isKnown[j][i] = true;
                                rowCan[j] = true;
                                if (grid[j][i])
                                    numLeftCells--;
                            }
                            if (isKnown[j][i])
                                numKnown++;
                        }
                        if (numKnown == rows)
                            colDone[i] = true;
                        colCan[i] = false;
                    }
                }
                if (numLeftCells > 0)
                {
                    bool changed = false;
                    for (int i = 0; i < rows && !changed; i++)
                    {
                        for (int j = 0; j < columns && !changed; j++)
                        {
                            if (!isKnown[i][j])
                            {
                                isKnown[i][j] = true;
                                grid[i][j] = true;
                                rowCan[i] = true;
                                colCan[j] = true;
                                changed = true;
                                numLeftCells--;
                            }
                        }
                    }
                    if (!changed)
                        return false;
                }
            }
            return true;
        }
    }
}
