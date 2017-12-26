using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UniRx;

namespace Combinator
{
    public class AnswerTest
    {
        // Interval
        [Test]
        public void A1()
        {
            var observer = new TestObserver<long>();

            // A.
            // XXX: 実行タイミングによっては失敗するかもしれない
            var observable = Observable.Interval(TimeSpan.FromMilliseconds(10), Scheduler.ThreadPool);
            var disposable = observable.Subscribe(observer);

            Assert.AreEqual(0, observer.CountNext);

            Thread.Sleep(TimeSpan.FromMilliseconds(10));
            UnityEngine.Debug.Log("Waiting for thread");
            Assert.AreEqual(1, observer.CountNext);

            Thread.Sleep(TimeSpan.FromMilliseconds(10));
            Assert.AreEqual(2, observer.CountNext);

            disposable.Dispose();
        }

        // CombineLatest
        [Test]
        public void A2()
        {
            var observer = new TestObserver<string>();
            var subject1 = new Subject<string>();
            var subject2 = new Subject<int>();

            // A.
            var observable =
                Observable.CombineLatest(subject1, subject2, (name, id) => string.Format("{0}{1}", name, id));
            observable.Subscribe(observer);

            subject1.OnNext("a");
            subject1.OnNext("b");
            subject2.OnNext(1);
            subject2.OnNext(2);
            subject1.OnNext("c");
            subject2.OnNext(3);

            Assert.AreEqual(4, observer.CountNext);
            Assert.AreEqual("b1", observer.NextList[0]);
            Assert.AreEqual("b2", observer.NextList[1]);
            Assert.AreEqual("c2", observer.NextList[2]);
            Assert.AreEqual("c3", observer.NextList[3]);
        }

        // Concat
        [Test]
        public void A3()
        {
            var observer = new TestObserver<int>();
            var subject1 = new Subject<int>();
            var subject2 = new Subject<int>();

            // A.
            var observable = Observable.Concat(subject1, subject2);
            observable.Subscribe(observer);

            subject1.OnNext(1);
            subject2.OnNext(10);
            subject1.OnNext(2);
            subject2.OnNext(20);
            subject1.OnCompleted();
            subject2.OnNext(30);

            Assert.AreEqual(3, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(2, observer.NextList[1]);
            Assert.AreEqual(30, observer.NextList[2]);
        }

        // Merge
        [Test]
        public void A4()
        {
            var observer = new TestObserver<int>();
            var subject1 = new Subject<int>();
            var subject2 = new Subject<int>();

            // A.
            var observable = Observable.Merge(subject1, subject2);
            observable.Subscribe(observer);

            subject1.OnNext(1);
            subject2.OnNext(10);
            subject1.OnNext(2);
            subject2.OnNext(20);
            subject1.OnCompleted();
            subject2.OnNext(30);

            Assert.AreEqual(5, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(10, observer.NextList[1]);
            Assert.AreEqual(2, observer.NextList[2]);
            Assert.AreEqual(20, observer.NextList[3]);
            Assert.AreEqual(30, observer.NextList[4]);
        }

        // Zip
        [Test]
        public void A5()
        {
            var observer = new TestObserver<IList<int>>();
            var subject1 = new Subject<int>();
            var subject2 = new Subject<int>();

            // A.
            var observable = Observable.Zip(subject1, subject2);
            observable.Subscribe(observer);

            subject1.OnNext(1);
            subject1.OnNext(2);
            subject2.OnNext(10);
            subject2.OnNext(20);
            subject2.OnNext(30);

            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(new[] {1, 10}, observer.NextList[0]);
            Assert.AreEqual(new[] {2, 20}, observer.NextList[1]);
        }

        // Repeat
        [Test]
        public void A6()
        {
            var observer = new TestObserver<int>();

            // A.
            var observable = Observable.Repeat(1, 3);
            observable.Subscribe(observer);

            Assert.AreEqual(3, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(1, observer.NextList[1]);
            Assert.AreEqual(1, observer.NextList[2]);
        }

        // WhenAll
        [Test]
        public void A7()
        {
            var observer = new TestObserver<int[]>();
            var subject1 = new Subject<int>();
            var subject2 = new Subject<int>();
            var subject3 = new Subject<int>();

            // A.
            var observable = Observable.WhenAll(subject1, subject2, subject3);
            observable.Subscribe(observer);

            subject1.OnNext(1);
            subject2.OnNext(2);
            subject3.OnNext(3);
            subject3.OnNext(4);

            Assert.AreEqual(0, observer.CountNext);

            subject1.OnCompleted();
            subject2.OnCompleted();
            subject3.OnCompleted();

            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(new[] {1, 2, 4}, observer.NextList[0]);
        }

        // Timer
        [Test]
        public void A8()
        {
            var observer = new TestObserver<long>();

            // A.
            // XXX: 実行タイミングによっては失敗するかもしれない
            var observable = Observable.Timer(TimeSpan.FromMilliseconds(1), Scheduler.ThreadPool);
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(0, observer.CountNext);
            
            Thread.Sleep(TimeSpan.FromMilliseconds(1));
            UnityEngine.Debug.Log("Waiting for timer");
            Assert.AreEqual(1, observer.CountNext);
            
            Thread.Sleep(TimeSpan.FromMilliseconds(1));
            UnityEngine.Debug.Log("Waiting for timer");
            Assert.AreEqual(1, observer.CountNext);
        }
    }
}