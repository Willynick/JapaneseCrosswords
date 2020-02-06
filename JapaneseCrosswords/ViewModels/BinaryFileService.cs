using JapaneseCrosswords.Models;
using JapaneseCrosswords.Utility.FileManager;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JapaneseCrosswords.ViewModels
{
    class BinaryFileService : IFileService
    {
        public Tables OpenLikeNonogram(string filename)
        {
            Tables tables;

            BinaryFormatter binFormat = new BinaryFormatter();

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                tables = (Tables)binFormat.Deserialize(fs);
            }
            return tables;
        }

        public void SaveLikeNonogram(string filename, Tables tables)
        {

            BinaryFormatter binFormat = new BinaryFormatter();
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                binFormat.Serialize(fs, tables);

            }
        }

        public MainTable OpenAsASolution(string filename)
        {
            MainTable mainTable;

            BinaryFormatter binFormat = new BinaryFormatter();

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                mainTable = (MainTable)binFormat.Deserialize(fs);
            }
            return mainTable;
        }

        public void SaveAsASolution(string filename, MainTable mainTable)
        {

            BinaryFormatter binFormat = new BinaryFormatter();
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                binFormat.Serialize(fs, mainTable);

            }
        }
    }
}
