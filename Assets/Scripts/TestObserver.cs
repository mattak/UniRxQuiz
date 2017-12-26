using System;
using System.Collections.Generic;
using UniRx;

public class TestObserver<TNext> : UniRx.IObserver<TNext>
{
    public IList<TNext> NextList = new List<TNext>();
    public IList<Exception> ErrorList = new List<Exception>();
    public IList<Unit> CompleteList = new List<Unit>();

    public int CountNext
    {
        get { return this.NextList.Count; }
    }

    public int CountError
    {
        get { return this.ErrorList.Count; }
    }

    public int CountComplete
    {
        get { return this.CompleteList.Count; }
    }

    public void OnCompleted()
    {
        this.CompleteList.Add(Unit.Default);
    }

    public void OnError(Exception error)
    {
        this.ErrorList.Add(error);
    }

    public void OnNext(TNext value)
    {
        this.NextList.Add(value);
    }
}