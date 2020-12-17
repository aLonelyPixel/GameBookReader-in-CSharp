using GameBook.Domain;

namespace GameBook.io
{
    public interface IBookLoader
    {
        public IBook LoadBook(string path);
    }
}