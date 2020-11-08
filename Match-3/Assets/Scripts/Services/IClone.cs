namespace Match3.Assets.Scripts.Services
{
    public interface IClone<T>
    {
        T GetOriginal();

        void SetOriginal(T value);

        void Reset();
    }
}
