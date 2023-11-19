public interface IFileHandlerStrategy<T>
{
    T ReadData();
    void SaveData(T dataList);
}
