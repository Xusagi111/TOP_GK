using UniRx;

public struct DisposableExtension
{
    public static void Dispose(CompositeDisposable disposable)
    {
        disposable.Clear();
        disposable.Dispose();
    }
}
