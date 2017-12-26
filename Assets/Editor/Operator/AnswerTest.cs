using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace Operator
{
    public class AnswerTest
    {
        // Select
        [Test]
        public void A1()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .Select(x => x * x)
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            // CHECK
            Assert.AreEqual(3, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(4, observer.NextList[1]);
            Assert.AreEqual(9, observer.NextList[2]);
        }

        // Where
        [Test]
        public void A2()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .Where(x => x % 2 == 0)
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(2, observer.NextList[0]);
        }

        // SelectMany
        [Test]
        public void A3()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .SelectMany(x => new[] {x, x})
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            // CHECK
            Assert.AreEqual(6, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(1, observer.NextList[1]);
            Assert.AreEqual(2, observer.NextList[2]);
            Assert.AreEqual(2, observer.NextList[3]);
            Assert.AreEqual(3, observer.NextList[4]);
            Assert.AreEqual(3, observer.NextList[5]);
        }

        // SelectMany
        [Test]
        public void A4()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .SelectMany(x => Observable.Create<int>(_observer =>
                {
                    _observer.OnNext(x);
                    _observer.OnError(new Exception("error"));
                    return Disposable.Empty;
                }))
                .Subscribe(observer);

            subject.OnNext(1);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(1, observer.CountError);
            Assert.AreEqual("error", observer.ErrorList[0].Message);
        }

        // Buffer
        [Test]
        public void A5()
        {
            var observer = new TestObserver<IList<int>>();
            var subject = new Subject<int>();

            // A.
            subject
                .Buffer(2)
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(new[] {1, 2}, observer.NextList[0]);
            Assert.AreEqual(new[] {3, 4}, observer.NextList[1]);
        }

        // Concat
        [Test]
        public void A6()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();
            var observable = Observable.Return(10);

            // A.
            subject
                .Concat(observable)
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnCompleted();

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(10, observer.NextList[1]);
            Assert.AreEqual(1, observer.CountComplete);
        }

        // Delay
        [Test]
        public void A7()
        {
            var observer = new TestObserver<int>();

            // A.
            Observable.Return(1)
                .Delay(TimeSpan.FromMilliseconds(1), Scheduler.ThreadPool)
                .Subscribe(observer);

            // CHECK
            Assert.AreEqual(0, observer.CountNext);
            Thread.Sleep(TimeSpan.FromMilliseconds(10));
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
        }

        // Distinct
        [Test]
        public void A8()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .Distinct()
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            // CHECK
            Assert.AreEqual(3, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(2, observer.NextList[1]);
            Assert.AreEqual(3, observer.NextList[2]);
        }

        // DistinctUntilChanged
        [Test]
        public void A9()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .DistinctUntilChanged()
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(2);
            subject.OnNext(1);
            subject.OnNext(2);

            // CHECK
            Assert.AreEqual(4, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(2, observer.NextList[1]);
            Assert.AreEqual(1, observer.NextList[2]);
            Assert.AreEqual(2, observer.NextList[3]);
        }

        // First
        [Test]
        public void A10()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .First()
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(1, observer.CountComplete);
        }

        // Last
        [Test]
        public void A11()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .Last()
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(3, observer.NextList[0]);
            Assert.AreEqual(1, observer.CountComplete);
        }

        // Take
        [Test]
        public void A12()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .Take(2)
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(2, observer.NextList[1]);
            Assert.AreEqual(1, observer.CountComplete);
        }

        // Skip
        [Test]
        public void A13()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A.
            subject
                .Skip(1)
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(2, observer.NextList[0]);
            Assert.AreEqual(3, observer.NextList[1]);
            Assert.AreEqual(1, observer.CountComplete);
        }

        // TakeWhile
        [Test]
        public void A14()
        {
            var subject = new Subject<int>();

            // A.
            var observable = subject
                .TakeWhile(it => it % 2 == 0);

            // test cases
            {
                var observer = new TestObserver<int>();
                observable.Subscribe(observer);

                subject.OnNext(0);
                subject.OnNext(1);

                // CHECK
                Assert.AreEqual(1, observer.CountNext);
                Assert.AreEqual(0, observer.NextList[0]);
                Assert.AreEqual(1, observer.CountComplete);
            }

            {
                var observer = new TestObserver<int>();
                observable.Subscribe(observer);

                subject.OnNext(0);
                subject.OnNext(2);
                subject.OnNext(3);

                // CHECK
                Assert.AreEqual(2, observer.CountNext);
                Assert.AreEqual(0, observer.NextList[0]);
                Assert.AreEqual(2, observer.NextList[1]);
                Assert.AreEqual(1, observer.CountComplete);
            }
        }

        // SkipWhile
        [Test]
        public void A15()
        {
            var subject = new Subject<int>();

            // A.
            var observable = subject
                .SkipWhile(it => it % 2 == 0);

            // test cases
            {
                var observer = new TestObserver<int>();
                observable.Subscribe(observer);

                subject.OnNext(0);
                subject.OnNext(1);

                // CHECK
                Assert.AreEqual(1, observer.CountNext);
                Assert.AreEqual(1, observer.NextList[0]);
            }

            {
                var observer = new TestObserver<int>();
                observable.Subscribe(observer);

                subject.OnNext(0);
                subject.OnNext(2);
                subject.OnNext(3);
                subject.OnNext(4);

                // CHECK
                Assert.AreEqual(2, observer.CountNext);
                Assert.AreEqual(3, observer.NextList[0]);
                Assert.AreEqual(4, observer.NextList[1]);
            }
        }

        // TakeUntil
        [Test]
        public void A16()
        {
            var subject = new Subject<int>();
            var trigger = new Subject<bool>();

            // A.
            var observable = subject
                .TakeUntil(trigger);

            // test cases
            {
                var observer = new TestObserver<int>();
                observable.Subscribe(observer);

                subject.OnNext(0);
                trigger.OnNext(true);
                subject.OnNext(1);

                // CHECK
                Assert.AreEqual(1, observer.CountNext);
                Assert.AreEqual(0, observer.NextList[0]);
                Assert.AreEqual(1, observer.CountComplete);
            }

            {
                var observer = new TestObserver<int>();
                observable.Subscribe(observer);

                subject.OnNext(0);
                subject.OnNext(1);
                trigger.OnNext(true);
                subject.OnNext(2);

                // CHECK
                Assert.AreEqual(2, observer.CountNext);
                Assert.AreEqual(0, observer.NextList[0]);
                Assert.AreEqual(1, observer.NextList[1]);
                Assert.AreEqual(1, observer.CountComplete);
            }
        }

        // SkipUntil
        [Test]
        public void A17()
        {
            var subject = new Subject<int>();
            var trigger = new Subject<bool>();

            // A.
            var observable = subject
                .SkipUntil(trigger);

            // test cases
            {
                var observer = new TestObserver<int>();
                observable.Subscribe(observer);

                subject.OnNext(0);
                trigger.OnNext(true);
                subject.OnNext(1);

                // CHECK
                Assert.AreEqual(1, observer.CountNext);
                Assert.AreEqual(1, observer.NextList[0]);
            }

            {
                var observer = new TestObserver<int>();
                observable.Subscribe(observer);

                subject.OnNext(0);
                subject.OnNext(1);
                trigger.OnNext(true);
                subject.OnNext(2);
                subject.OnNext(3);

                // CHECK
                Assert.AreEqual(2, observer.CountNext);
                Assert.AreEqual(2, observer.NextList[0]);
                Assert.AreEqual(3, observer.NextList[1]);
            }
        }

        // StartWith
        [Test]
        public void A18()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();

            // A.
            subject
                .StartWith(0)
                .Subscribe(observer);

            subject.OnNext(1);

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(0, observer.NextList[0]);
            Assert.AreEqual(1, observer.NextList[1]);
        }

        // ThrottleFirst
        [Test]
        public void A19()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();

            // A.
            subject
                .ThrottleFirst(TimeSpan.FromMilliseconds(1), Scheduler.ThreadPool)
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            Thread.Sleep(TimeSpan.FromMilliseconds(2));
            Debug.Log("Waiting thread");

            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(4, observer.NextList[1]);
        }

        // Throttle (Debounce)
        [Test]
        public void A20()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();

            // A.
            subject
                .Throttle(TimeSpan.FromMilliseconds(1), Scheduler.ThreadPool)
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            Thread.Sleep(TimeSpan.FromMilliseconds(1));
            Debug.Log("Waiting thread");

            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);

            Thread.Sleep(TimeSpan.FromMilliseconds(1));
            Debug.Log("Waiting thread");

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(3, observer.NextList[0]);
            Assert.AreEqual(6, observer.NextList[1]);
        }

        // Finally
        [Test]
        public void A21()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();
            var list = new List<Unit>();

            // A.
            subject
                .Finally(() => list.Add(Unit.Default))
                .Subscribe(observer);

            // CHECK
            Assert.AreEqual(0, list.Count);
            subject.OnCompleted();
            Assert.AreEqual(1, list.Count);
        }

        // Timeout
        [Test]
        public void A22()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();

            // A. 
            subject
                .Timeout(TimeSpan.FromMilliseconds(1), Scheduler.ThreadPool)
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            Thread.Sleep(TimeSpan.FromMilliseconds(2));

            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);

            // CHECK
            Assert.AreEqual(3, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(2, observer.NextList[1]);
            Assert.AreEqual(3, observer.NextList[2]);
        }

        // Catch
        [Test]
        public void A23()
        {
            var subject = new Subject<string>();
            var observer = new TestObserver<string>();

            // A. 
            subject
                .Catch<string, Exception>(error => Observable.Return(error.Message))
                .Subscribe(observer);

            subject.OnError(new Exception("error1"));

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual("error1", observer.NextList[0]);
        }

        // CatchIgnore
        [Test]
        public void A24()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // A. 
            subject
                .CatchIgnore()
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnError(new Exception("error"));

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(0, observer.CountError);
        }

        // Retry
        [Test]
        public void A25()
        {
            var observer = new TestObserver<int>();
            var count = 0;

            // A. 
            Observable.Create<int>(_observer =>
                {
                    count++;
                    _observer.OnError(new Exception("error" + count));
                    return Disposable.Empty;
                })
                .Retry(3)
                .Subscribe(observer);


            // CHECK
            Assert.AreEqual(1, observer.CountError);
            Assert.AreEqual("error3", observer.ErrorList[0].Message);
        }

        // OnErrorRetry
        [Test]
        public void A26()
        {
            var observer = new TestObserver<int>();
            var list = new List<string>();

            // A. 
            Observable.Create<int>(_observer =>
                {
                    _observer.OnError(new Exception("error1"));
                    return Disposable.Empty;
                })
                .OnErrorRetry<int, Exception>(error => list.Add(error.Message))
                .Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("error1", list[0]);
            Assert.AreEqual(0, observer.CountError);
        }

        // Share
        [Test]
        public void A27()
        {
            var subject = new Subject<int>();
            var observer1 = new TestObserver<int>();
            var observer2 = new TestObserver<int>();
            var count = 0;

            // A. 
            var observable = subject
                .Do(_ => count++)
                .Share();

            observable.Subscribe(observer1);
            observable.Subscribe(observer2);
            
            subject.OnNext(1);

            // CHECK
            Assert.AreEqual(1, count);
            Assert.AreEqual(1, observer1.CountNext);
            Assert.AreEqual(1, observer2.CountNext);
            Assert.AreEqual(1, observer1.NextList[0]);
            Assert.AreEqual(1, observer2.NextList[0]);
        }
    }
}