using JapaneseCrosswords.Models;

namespace JapaneseCrosswords.Utility.FileManager
{
    public interface IFileService
    {
        Tables OpenLikeNonogram(string filename);
        void SaveLikeNonogram(string filename, Tables tables);

       MainTable OpenAsASolution(string filename);
        void SaveAsASolution(string filename, MainTable table);
    }
}
