using JapaneseCrosswords.Models;
using JapaneseCrosswords.Utility;
using JapaneseCrosswords.Utility.FileManager;
using JapaneseCrosswords.ViewModels.CommonViewModels.LeftTable;
using JapaneseCrosswords.ViewModels.CommonViewModels.TopTable;
using JapaneseCrosswords.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace JapaneseCrosswords.ViewModels.CommonViewModels.MainTable
{

    class MainVM : OnPropertyChangedClass
    {
        static public MainVM mainVm = null;

        public bool isEmpty = true;
        public bool createNewTables = true;

        private Tables _tables;
        private Models.MainTable _mainTable;

        public Tables tables
        {
            get
            {
                return _tables;
            }
            set
            {
                _tables = value;
            }
        }
        public Models.MainTable mainTable
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

        public MainVM(IDialogService dialogService, IFileService fileService)
        {
            tables = new Tables();
            mainTable = new Models.MainTable();
            mainVm = this;

            CreateItems();
            CreateTablesItems();

            this.dialogService = dialogService;
            this.fileService = fileService;

            LoadDataMethod();
        }

        private RelayCommand _OpenWindowHowToSolveCommand;
        public RelayCommand OpenWindowHowToSolveCommand
        {
            get
            {
                return _OpenWindowHowToSolveCommand ??
                  (_OpenWindowHowToSolveCommand = new RelayCommand(obj =>
                  {
                      new HowToSolveJapaneseCrosswordsWindow().Show();
                  }));
            }
        }


        private ObservableCollection<ItemVM> _Items { get; set; } = new ObservableCollection<ItemVM>();
        private ObservableCollection<LeftTableItem> _LeftTableItems { get; set; } = new ObservableCollection<LeftTableItem>();
        private ObservableCollection<TopTableItem> _TopTableItems { get; set; } = new ObservableCollection<TopTableItem>();

        public ObservableCollection<ItemVM> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
                OnPropertyChanged("Items");
            }
        }
        public ObservableCollection<LeftTableItem> LeftTableItems
        {
            get
            {
                return _LeftTableItems;
            }
            set
            {
                _LeftTableItems = value;
                OnPropertyChanged("LeftTableItems");
            }
        }
        public ObservableCollection<TopTableItem> TopTableItems
        {
            get
            {
                return _TopTableItems;
            }
            set
            {
                _TopTableItems = value;
                OnPropertyChanged("TopTableItems");
            }
        }



        //!!!!!!!!!!--------------------------------------

        public string HowToSolve
        {
            get
            {
                using (StreamReader sr = new StreamReader(@"D:\Лабараторные\Языки программирования\JapaneseCrosswords\JapaneseCrosswords\Information\HowToSolveJapaneseCrosswords.txt"))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private int _SelectIndex = 1;
        public int SelectIndex
        {
            get
            {
                return _SelectIndex;
            }
            set
            {
                _SelectIndex = value;
                OnPropertyChanged("SelectIndex");
            }
        }


        private RelayCommand _SelectIndexCommand;
        public RelayCommand SelectIndexCommand
        {
            get
            {
                return _SelectIndexCommand ??
                  (_SelectIndexCommand = new RelayCommand(obj =>
                  {
                      SelectIndex = Convert.ToInt32(obj);
                  }));
            }
        }

        //------------------------------------------------

        private int _width = 10;
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value <= 50)
                {
                    _width = value;

                    if (value != 10)
                    {
                        isEmpty = false;
                    }
                    CreateItems();
                    CreateTablesItems();

                    OnPropertyChanged("Width");

                }
                else
                {
                    MessageBox.Show("Maximum is 50");
                }
            }
        }

        private int _height = 10;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value <= 50)
                {
                    _height = value;

                    if (value != 10)
                    {
                        isEmpty = false;
                    }

                    CreateItems();
                    CreateTablesItems();

                    OnPropertyChanged("Height");
                }
                else
                {
                    MessageBox.Show("Maximum is 50");
                }
            }
        }

        public int rectengleWidth
        {
            get
            {
                return _widthLeft * 22;
            }
        }

        public int rectengleHeight
        {
            get
            {
                return _heightTop * 22;
            }
        }

        //****************************************

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
                if (value <= _width / 2)
                {
                    _widthLeft = value;

                        CreateLeftTableItems(false);
                    OnPropertyChanged("widthLeft");
                    OnPropertyChanged("rectengleWidth");
                }
                else if (value != 0 && value != 1)
                {
                    MessageBox.Show("The width of the left table cannot be greater than half the length of the main table");
                }
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
                if (value <= _height / 2)
                {
                    _heightTop = value;

                        CreateTopTableItems(false);

                    OnPropertyChanged("heightTop");
                    OnPropertyChanged("rectengleHeight");
                }
                else if(value != 0 && value != 1)
                {
                    MessageBox.Show("The height of the right table cannot be greater than half the length of the main table"); 
                }
            }
        }



        //*****************************************

        /*public ItemVM this[int i, int j]
        {
            get { return Items[i * _height + j]; }
            set { Items[i * _width + j] = value; }
        }*/



        public MainVM()
        {

        }

        public void CreateItems(bool FillFromFile = false)
        {
            Items.Clear();

            Visibility horizontalLineVisibility = new Visibility();
            Visibility verticalLineVisibility = new Visibility();
            horizontalLineVisibility = Visibility.Collapsed;
            verticalLineVisibility = Visibility.Collapsed;

            for (int i = 0; i < _height; i++)
            {
                if ((i+1) % 5 == 0 && i+1!=_height)
                {
                    horizontalLineVisibility = Visibility.Visible;
                }
                for (int j = 0; j < _width; j++)
                {
                    GridState color;

                    if ((j+1) % 5 == 0 && j+1!=_width)
                    {
                        verticalLineVisibility = Visibility.Visible;
                    }

                    if (FillFromFile == true)
                    {
                        if (mainTable.mainTable[i][j] == true)
                        {
                            color = GridState.Black;
                        }
                        else
                        {
                            color = GridState.White; 
                        }
                    }
                    else
                    {
                        color = GridState.White;
                    }
                    Items.Add(new ItemVM { Color = color, HorizontalLineVisibility = horizontalLineVisibility, VerticalLineVisibility = verticalLineVisibility });
                    verticalLineVisibility = Visibility.Collapsed;
                }

                horizontalLineVisibility = Visibility.Collapsed;
            }
        }


        public void CreateTablesItems(bool CreateNewTables = true)
        {

            CreateTopTableItems(CreateNewTables);
            CreateLeftTableItems(CreateNewTables);
        }

        public void CreateTopTableItems(bool CreateNewTables)
        {
            if (CreateNewTables == true)
            {
               heightTop = tables.CreateTopTable(Items, Width, Height);
            }

            TopTableItems.Clear();
            string item = "";

            Visibility verticalLineVisibility = new Visibility();
            verticalLineVisibility = Visibility.Collapsed;

            for (int j = 0; j < _heightTop; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    if ((i + 1) % 5 == 0 && i + 1 != _width)
                    {
                        verticalLineVisibility = Visibility.Visible;
                    }
                    if (tables.TopTable.Count() == 0 || (tables.TopTable[i].Count() - 1 < j) || tables.TopTable[i][j] == 0)
                    {
                        item = "";
                        
                    }
                    else
                    {
                        item = tables.TopTable[i][j].ToString();
                    }
                    TopTableItems.Add(new TopTableItem
                    {
                        TopNumber = item,
                        VerticalLineVisibility = verticalLineVisibility
                    });
                    verticalLineVisibility = Visibility.Collapsed;
                }
            }
        }

        public void CreateLeftTableItems(bool CreateNewTables)
        {
            if (CreateNewTables == true)
            {
                widthLeft = tables.CreateLeftTable(Items, Width, Height);
            }

            LeftTableItems.Clear();
            string item = "";

            int numOfCellsBefore;

            Visibility horizontalLineVisibility = new Visibility();
            horizontalLineVisibility = Visibility.Collapsed;

            for (int i = 0; i < _height; i++)
            {
                if ((i + 1) % 5 == 0 && i + 1 != _height)
                {
                    horizontalLineVisibility = Visibility.Visible;
                }
                numOfCellsBefore = _widthLeft - tables.LeftTable[i].Count();
                for (int j = 0; j < _widthLeft; j++)
                {

                    if (tables.LeftTable.Count() == 0 || (tables.LeftTable[i].Count() - 1 < j) || tables.LeftTable[i][j] == 0)
                    {
                        item = "";
                    }
                    else
                    {
                        item = tables.LeftTable[i][j].ToString();
                    }
                    LeftTableItems.Add(new LeftTableItem
                    {
                        LeftNumber = item,
                        HorizontalLineVisibility = horizontalLineVisibility
                    });
                }
                horizontalLineVisibility = Visibility.Collapsed;
            }
        }

        //SolutionCommand--------------------------------------------------------------

        private RelayCommand solveACrossword;
        public RelayCommand SolveACrossword
        {
            get
            {
                return solveACrossword ??
                  (solveACrossword = new RelayCommand(obj =>
                  {
                      CreateItems();

                      mainTable.CreateNumberMainTable(Items, Width, Height);
                      tables.CreateTopTable(TopTableItems, Width, heightTop);
                      tables.CreateLeftTable(LeftTableItems, widthLeft, Height);

                      bool IsSolved = Solution.Solve(tables.LeftTable, tables.TopTable, mainTable.mainTable);
                      if (!IsSolved)
                      {
                          MessageBox.Show("There is a result, but errors are possible due to errors in the condition");
                      }
                      CreateItems(true);
                  }));
            }
        }

        //-----------------------------------------------------------------------------


        //PLAYWINDOW------------------------------------------------------------------------------
        private ObservableCollection<FolderElement> _data;

        public ObservableCollection<FolderElement> Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private Visibility _TablesVisibility = Visibility.Collapsed;
        public Visibility TablesVisibility
        {
            get
            {
                return _TablesVisibility;
            }
            set
            {
                _TablesVisibility = value;
                OnPropertyChanged("TablesVisibility");
            }
        }
        private void LoadDataMethod()
        {
            var folderInf = new FolderData(@"D:\Лабараторные\Языки программирования\JapaneseCrosswords\JapaneseCrosswords\Crosswords");
            Data = new ObservableCollection<FolderElement>(folderInf.GetFiles());
        }

        public Models.MainTable mainTableFromFile = new Models.MainTable();

        private int _SelectedIndexData = -1;
        public int SelectedIndexData
        {
            get { return _SelectedIndexData; }
            set
            {
                createNewTables = false;

                _SelectedIndexData = value;

                TablesVisibility = Visibility.Visible;

                mainTableFromFile = fileService.OpenAsASolution(Data[value].Path);
                mainTable = (Models.MainTable)mainTableFromFile.Clone();
                if (mainTable.mainTable.Count() != 0)
                {
                    Height = mainTable.mainTable.Count();
                    Width = mainTable.mainTable[0].Count();
                }
                CreateItems(true);
                CreateTablesItems();
                CreateItems();
                mainTable.CreateNumberMainTable(Items, _width, _height);

                OnPropertyChanged("SelectedIndexData");
            }
        }

        private ICommand _ClearCrosswordCommand;

        public ICommand ClearCrosswordCommand
        {

            get
            {
                return _ClearCrosswordCommand ??
                    (_ClearCrosswordCommand = new RelayCommand(obj =>
                    {
                        CreateItems();
                        mainTable.CreateNumberMainTable(Items, Width, Height);
                    }));
            }
        }

        private ICommand _CkeckCrosswordCommand;

        public ICommand CkeckCrosswordCommand
        {

            get
            {
                return _CkeckCrosswordCommand ??
                    (_CkeckCrosswordCommand = new RelayCommand(obj =>
                    {
                        if (mainTableFromFile.mainTable.Count() != 0)
                        {
                            bool thereIsError = false;

                            mainTable.CreateNumberMainTable(Items, Width, Height);

                            for (int i = 0; i < _height; i++)
                            {
                                for (int j = 0; j < _width; j++)
                                {
                                    if (mainTableFromFile.mainTable[i][j] != mainTable.mainTable[i][j])
                                    {
                                        Items[i * Width + j].RedCrossVisibility = Visibility.Visible;
                                        thereIsError = true;
                                    }

                                }
                            }

                            if (thereIsError)
                            {
                                MessageBox.Show("There are some errors");
                            }
                            else
                            {
                                MessageBox.Show("You solved the crossword correctly!");
                                for (int i = 0; i < _height; i++)
                                {
                                    for (int j = 0; j < _width; j++)
                                    {
                                        Items[i * Width + j].CrossVisibility = Visibility.Visible;
                                    }
                                }
                            }
                        }

                    }));
            }
        }



        private ICommand _ShowErrorsCommand;

        public ICommand ShowErrorsCommand
        {

            get
            {
                return _ShowErrorsCommand ??
                    (_ShowErrorsCommand = new RelayCommand(obj =>
                    {
                        if (mainTableFromFile.mainTable.Count() != 0)
                        {
                            bool thereIsError = false;

                            mainTable.CreateNumberMainTable(Items, Width, Height);

                            for (int i = 0; i < _height; i++)
                            {
                                for (int j = 0; j < _width; j++)
                                {
                                    if ((mainTableFromFile.mainTable[i][j] != mainTable.mainTable[i][j]) && mainTable.mainTable[i][j] == true)
                                    {
                                        Items[i * Width + j].RedCrossVisibility = Visibility.Visible;
                                        thereIsError = true;
                                    }

                                }
                            }

                            if (thereIsError)
                            {
                                MessageBox.Show("There are some errors");
                            }
                            else
                            {
                                MessageBox.Show("There is no errors");
                            }
                        }

                    }));
            }
        }



        //----------------------------------------------------------------------------------------



        IFileService fileService;
        IDialogService dialogService;

        private RelayCommand saveLikeNonogramCommand;
        public RelayCommand SaveLikeNonogramCommand
        {
            get
            {
                return saveLikeNonogramCommand ??
                  (saveLikeNonogramCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              tables.CreateTopTable(TopTableItems, Width, heightTop);
                              tables.CreateLeftTable(LeftTableItems, widthLeft, Height);

                              fileService.SaveLikeNonogram(dialogService.FilePath, tables);
                              dialogService.ShowMessage("File Saved");
                          }
                      }
                      catch
                      {
                          dialogService.ShowMessage("This is the wrong file, maybe this is the solution file");
                      }
                  }));
            }
        }

        private RelayCommand openLikeNonogramCommand;
        public RelayCommand OpenLikeNonogramCommand
        {
            get
            {
                return openLikeNonogramCommand ??
                  (openLikeNonogramCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              tables = fileService.OpenLikeNonogram(dialogService.FilePath);

                              _heightTop = tables.heightTop;
                              _widthLeft = tables.widthLeft;

                              _width = tables.TopTable.Count();
                              _height = tables.LeftTable.Count();
                              OnPropertyChanged("Width"); OnPropertyChanged("Height"); OnPropertyChanged("widthLeft"); OnPropertyChanged("heightTop");

                              CreateItems();

                              CreateTablesItems(false);


                              dialogService.ShowMessage("File Open");
                          }
                      }
                      catch
                      {
                          dialogService.ShowMessage("This is the wrong file, maybe this is the solution file");
                      }
                  }));
            }
        }

        private RelayCommand saveAsASolutionCommand;
        public RelayCommand SaveAsASolutionCommand
        {
            get
            {
                return saveAsASolutionCommand ??
                  (saveAsASolutionCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {

                              mainTable.CreateNumberMainTable(Items, Width, Height);
                              fileService.SaveAsASolution(dialogService.FilePath, mainTable);
                              dialogService.ShowMessage("File Saved");
                          }
                      }
                      catch
                      {
                          dialogService.ShowMessage("This is the wrong file, maybe this is the nonogram file");
                      }
                  }));
            }
        }

        private RelayCommand openAsASolutionCommand;
        public RelayCommand OpenAsASolutionCommand
        {
            get
            {
                return openAsASolutionCommand ??
                  (openAsASolutionCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              mainTable = fileService.OpenAsASolution(dialogService.FilePath);
                              if (mainTable.mainTable.Count() != 0)
                              {
                                  Height = mainTable.mainTable.Count();
                                  Width = mainTable.mainTable[0].Count();
                              }
                              CreateItems(true);
                              CreateTablesItems();

                              mainTable.CreateNumberMainTable(Items, _width, _height);

                              dialogService.ShowMessage("File Open");
                          }
                      }
                      catch
                      {
                          dialogService.ShowMessage("This is the wrong file, maybe this is the nonogram file");
                      }
                  }));
            }
        }

    }
}
