using GameBook.Domain;

namespace GameBook.io
{
    public interface IJsonLoader
    {
        public IBook LoadBook(string path);
    }
}