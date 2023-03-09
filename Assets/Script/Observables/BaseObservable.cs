using System;
using UniRx;

public class BaseObservable : IDisposable
{
    protected CompositeDisposable _disposable = new CompositeDisposable();
    public void Dispose()
    {
        _disposable.Clear();
        _disposable.Dispose();
    }
}
