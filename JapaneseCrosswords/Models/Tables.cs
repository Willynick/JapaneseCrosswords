using JapaneseCrosswords.Utility;
using JapaneseCrosswords.ViewModels.CommonViewModels.LeftTable;
using JapaneseCrosswords.ViewModels.CommonViewModels.MainTable;
using JapaneseCrosswords.ViewModels.CommonViewModels.TopTable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace JapaneseCrosswords.Models
{
    [Serializable]
    public class Tables
    {
        private int _heightTop = 1;
        private int _widthLeft = 1;

        public int widthLeft
        {
            get
            {
                return _widthLeft;
            }
            set
            {
                _widthLeft = value;
            }
        }
        public int heightTop
        {
            get
            {
                return _heightTop;
            }
            set
            {
                _heightTop = value;
            }
        }

        private List<List<int>> topTable = new List<List<int>>();
        private List<List<int>> leftTable = new List<List<int>>();

        public List<List<int>> TopTable
        {
            get
            {
                return topTable;
            }
            set
            {
                topTable = value;
            }
        }
        public List<List<int>> LeftTable
        {
            get
            {
                return leftTable;
            }
            set
            {
                leftTable = value;
            }
        }

        public int CreateTopTable(ObservableCollection<ItemVM> Items, int Width, int Height)
        {
            int counterTable = 0, tempHeight = 0, maxHeight = 1;
            TopTable = new List<List<int>>();
            for (int i = 0; i < Width; i++)
            {
                TopTable.Add(new List<int>());
                for (int j = 0; j < Height; j++)
                {
                    if (Items[i  + j * Width].Color == GridState.Black)
                    {
                        counterTable++;
                    }
                    else if (counterTable != 0)
                    {
                        TopTable[i].Add(counterTable);
                        counterTable = 0;
                    }
                    if (counterTable != 0 && (j == (Height - 1)))
                    {
                        TopTable[i].Add(counterTable);
                        counterTable = 0;
                    }
                }
                tempHeight = TopTable[i].Count();
                if (tempHeight > maxHeight) { maxHeight = tempHeight; }
            }
            heightTop = maxHeight;
            return heightTop;           
        }

        public int CreateLeftTable(ObservableCollection<ItemVM> Items, int Width, int Height)
        {
            int counterTable = 0, tempHeight = 0, maxHeight = 1;
            LeftTable = new List<List<int>>();
            for (int i = 0; i < Height; i++)
            {
                LeftTable.Add(new List<int>());
                for (int j = 0; j < Width; j++)
                {
                    if (Items[i*Width + j].Color == GridState.Black)
                    {
                        counterTable++;
                    }
                    else if (counterTable != 0)
                    {
                        LeftTable[i].Add(counterTable);
                        counterTable = 0;
                    }
                    if (counterTable != 0 && (j == (Width - 1)))
                    {
                        LeftTable[i].Add(counterTable);
                        counterTable = 0;
                    }
                }
                tempHeight = LeftTable[i].Count();
                if (tempHeight > maxHeight) { maxHeight = tempHeight; }
            }
            widthLeft = maxHeight;
            return widthLeft;
        }

        public void CreateTopTable(ObservableCollection<TopTableItem> TopTableItems, int Width, int Height)
        {
            TopTable = new List<List<int>>();
            for (int i = 0; i < Width; i++)
            {
                TopTable.Add(new List<int>());
                for (int j = 0; j < Height; j++)
                {
                    if (TopTableItems[i + j * Width].TopNumber != "" && TopTableItems[i + j * Width].TopNumber != "0")
                    {
                        TopTable[i].Add(Convert.ToInt32(TopTableItems[i + j * Width].TopNumber));
                    }
                }
            }
            heightTop = Height;
        }

        
        public void CreateLeftTable(ObservableCollection<LeftTableItem> LeftTableItems, int Width, int Height)
        {
            LeftTable = new List<List<int>>();
            for (int i = 0; i < Height; i++)
            {
                LeftTable.Add(new List<int>());
                for (int j = 0; j < Width; j++)
                {
                    if (LeftTableItems[i * Width + j].LeftNumber != "" && LeftTableItems[i * Width + j].LeftNumber != "0")
                    {
                        LeftTable[i].Add(Convert.ToInt32(LeftTableItems[i * Width + j].LeftNumber));
                    }
                }
            }
            widthLeft = Width;
        }
    }

}
