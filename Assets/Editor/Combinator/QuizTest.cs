using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UniRx;

namespace Combinator
{
    public class QuizTest
    {
        [Test]
        public void Q1()
        {
            var observer = new TestObserver<long>();

            // Q. 10msごとに値を排出するobservableをつくれ (Scheduler.ThreadPoolを利用すること)
            // XXX: 実行タイミングによっては失敗するかもしれない
            var observable = (UniRx.IObservable<long>) null; // FIXME
            var disposable = observable.Subscribe(observer);

            Assert.AreEqual(0, observer.CountNext);

            Thread.Sleep(TimeSpan.FromMilliseconds(10));
            UnityEngine.Debug.Log("Waiting for thread");
            Assert.AreEqual(1, observer.CountNext);

            Thread.Sleep(TimeSpan.FromMilliseconds(10));
            Assert.AreEqual(2, observer.CountNext);

            disposable.Dispose();
        }

        [Test]
        public void Q2()
        {
            var observer = new TestObserver<string>();
            var subject1 = new Subject<string>();
            var subject2 = new Subject<int>();

            // Q. subject1,subject2の内容をstringでつなげるobservableをつくれ
            var observable = (UniRx.IObservable<string>) null; // FIXME
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

        [Test]
        public void Q3()
        {
            var observer = new TestObserver<int>();
            var subject1 = new Subject<int>();
            var subject2 = new Subject<int>();

            // Q. subject1がcompleteしたあとにsubject2を流すobservableをつくれ
            var observable = (UniRx.IObservable<int>) null; // FIXME
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

        [Test]
        public void Q4()
        {
            var observer = new TestObserver<int>();
            var subject1 = new Subject<int>();
            var subject2 = new Subject<int>();

            // Q. subject1, subject2を組み合わせ、きた順に値を流すobservableをつくれ
            var observable = (UniRx.IObservable<int>) null; // FIXME
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

        [Test]
        public void Q5()
        {
            var observer = new TestObserver<IList<int>>();
            var subject1 = new Subject<int>();
            var subject2 = new Subject<int>();

            // Q. subject1, subject2の値をそれぞれ順番道理にペアで組み合わせて出力するobservableをつくれ
            var observable = (UniRx.IObservable<IList<int>>) null; // FIXME
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

        [Test]
        public void Q6()
        {
            var observer = new TestObserver<int>();

            // A.
            var observable = (UniRx.IObservable<int>) null; // FIXME
            observable.Subscribe(observer);

            Assert.AreEqual(3, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(1, observer.NextList[1]);
            Assert.AreEqual(1, observer.NextList[2]);
        }

        [Test]
        public void Q7()
        {
            var observer = new TestObserver<int[]>();
            var subject1 = new Subject<int>();
            var subject2 = new Subject<int>();
            var subject3 = new Subject<int>();

            // Q. subject1, subject2, subject3 がすべてcompleteしたら、それぞれの最後の値を流すobservableは?
            var observable = (UniRx.IObservable<int[]>) null; // FIXME
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

        [Test]
        public void Q8()
        {
            var observer = new TestObserver<long>();

            // Q. 1ms後に1度だけ値を出力するobservableをつくれ (Scheduler.ThreadPoolを利用すること)
            // XXX: 実行タイミングによっては失敗するかもしれない
            var observable = (UniRx.IObservable<long>) null; // FIXME
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