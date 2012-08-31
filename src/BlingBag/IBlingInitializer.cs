namespace BlingBag
{
    public interface IBlingInitializer<TEventType>
    {
        T Initialize<T>(T obj) where T : class;
    }
}