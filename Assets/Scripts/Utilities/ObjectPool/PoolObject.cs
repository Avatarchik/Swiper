public interface IPoolObject<T>
{
    T Group { get; }
    void Create();
    void OnPush();
    void FailedPush();
}