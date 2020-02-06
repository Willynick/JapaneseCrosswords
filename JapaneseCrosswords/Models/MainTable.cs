using JapaneseCrosswords.Utility;
using JapaneseCrosswords.ViewModels.CommonViewModels.MainTable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JapaneseCrosswords.Models
{
    [Serializable]
    public class MainTable : ICloneable
    {
        private List<List<bool>> _mainTable = new List<List<bool>>();

        public List<List<bool>> mainTable
        {
            get
            {
                return _mainTable;
            }
            set
            {
                _mainTable = value;
            }
        }

        public object Clone()
        {
            return new MainTable { _mainTable = this._mainTable};
        }

        public void CreateNumberMainTable(ObservableCollection<ItemVM> Items, int Width, int Height)
        {
            mainTable = new List<List<bool>>();
            for (int i = 0; i < Height; i++)
            {
                mainTable.Add(new List<bool>());
                for (int j = 0; j < Width; j++)
                {
                    if (Items[i * Width + j].Color == GridState.Black)
                    {
                        mainTable[i].Add(true);
                    }
                    else
                    {
                        mainTable[i].Add(false);
                    }
                }
            }
        }
    }
}
