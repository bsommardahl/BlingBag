namespace BlingBag
{
    public interface IBlingHandler<in T>
    {
        void Handle(T @event);
    }
}