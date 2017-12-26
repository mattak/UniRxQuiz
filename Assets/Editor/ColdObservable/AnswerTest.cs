using System;
using NUnit.Framework;
using UniRx;

namespace ColdObservable
{
    public class AnswerTest
    {
        // 1. Observable.Return
        [Test]
        public void A1()
        {
            var observer = new TestObserver<int>();

            // A.
            var observable = Observable.Return(1);
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
        }

        // 2. Observable.ReturnUnit
        [Test]
        public void A2()
        {
            var observer = new TestObserver<Unit>();

            // A.
            var observable = Observable.ReturnUnit();
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(Unit.Default, observer.NextList[0]);
        }

        // 3. Observable.Throw
        [Test]
        public void A3()
        {
            var observer = new TestObserver<Unit>();

            // A.
            var observable = Observable.Throw<Unit>(new Exception("error"));
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, observer.CountError);
            Assert.AreEqual("error", observer.ErrorList[0].Message);
        }

        // 4. Observable.Complete
        [Test]
        public void A4()
        {
            var observer = new TestObserver<Unit>();

            // A.
            var observable = Observable.Empty<Unit>();
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, observer.CountComplete);
        }

        // 5. Observable.Never
        [Test]
        public void A5()
        {
            var observer = new TestObserver<Unit>();

            // A.
            var observable = Observable.Never<Unit>();
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(0, observer.CountNext);
            Assert.AreEqual(0, observer.CountError);
            Assert.AreEqual(0, observer.CountComplete);
        }

        // 6. Observable.Create
        [Test]
        public void A6()
        {
            var observer = new TestObserver<int>();

            // A.
            var observable = Observable.Create<int>(_observer =>
            {
                _observer.OnNext(1);
                _observer.OnNext(2);
                _observer.OnNext(3);
                _observer.OnCompleted();
                return Disposable.Empty;
            });
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(3, observer.CountNext);
            Assert.AreEqual(new[] {1, 2, 3}, observer.NextList);
            Assert.AreEqual(1, observer.CountComplete);
        }

        // 7. Observable.Range
        [Test]
        public void A7()
        {
            var observer = new TestObserver<int>();

            // A.
            var observable = Observable.Range(1, 10);
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(10, observer.CountNext);
            Assert.AreEqual(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, observer.NextList);
            Assert.AreEqual(1, observer.CountComplete);
        }
    }
}
