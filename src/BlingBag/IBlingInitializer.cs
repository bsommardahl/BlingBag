namespace BlingBag
{
    public interface IBlingInitializer
    {
        T Initialize<T>(T obj) where T : class;
    }
}