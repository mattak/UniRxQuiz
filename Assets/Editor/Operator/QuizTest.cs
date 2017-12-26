using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UniRx;

namespace Operator
{
    public class QuizTest
    {
        [Test]
        public void Q1()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. 来た値を2乗して返すOperatorは?
            subject
                // FIXME
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

        [Test]
        public void Q2()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. 偶数のみ通すObservableは?
            subject
                // FIXME
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(2, observer.NextList[0]);
        }

        [Test]
        public void Q3()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. 同じ値を2回繰り返すOperatorは?
            subject
                // FIXME
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

        [Test]
        public void Q4()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. 来た値をそのまま流してからエラーを起こすoperatorは?
            subject
                // FIXME
                .Subscribe(observer);

            subject.OnNext(1);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(1, observer.CountError);
            Assert.AreEqual("error", observer.ErrorList[0].Message);
        }

        [Test]
        public void Q5()
        {
            var observer = new TestObserver<IList<int>>();
            var subject = new Subject<int>();

            // Q. 2個値をまとめてから流すoperatorは？
            //subject
            // FIXME
            //.Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(new[] {1, 2}, observer.NextList[0]);
            Assert.AreEqual(new[] {3, 4}, observer.NextList[1]);
        }

        [Test]
        public void Q6()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();
            var observable = Observable.Return(10);

            // Q. observableの値をsubject終了時の末尾につなげるoperatorは?
            subject
                // FIXME
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnCompleted();

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(10, observer.NextList[1]);
            Assert.AreEqual(1, observer.CountComplete);
        }

        [Test]
        public void Q7()
        {
            var observer = new TestObserver<int>();

            // Q. 別Threadにて1ms待機してから値を流すoperatorは？
            Observable.Return(1)
                // FIXME
                .Subscribe(observer);

            // CHECK
            Assert.AreEqual(0, observer.CountNext);
            Thread.Sleep(TimeSpan.FromMilliseconds(10));
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
        }

        [Test]
        public void Q8()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. 一度流した値を2度流さないoperatorは?
            subject
                // FIXME
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

        [Test]
        public void Q9()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. 直前の値が同じであれば流さないOperatorは?
            subject
                // FIXME
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

        [Test]
        public void Q10()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. はじめの値1つだけ流して、Completeするoperatorは?
            subject
                // FIXME
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(1, observer.CountComplete);
        }

        [Test]
        public void Q11()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. Completeした時の最後の値を流すOperatorは?
            subject
                // FIXME
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

        [Test]
        public void Q12()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. はじめの値2つのみ流す Operatorは?
            subject
                // FIXME
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

        [Test]
        public void Q13()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. はじめの1つを無視するoperatorは?
            subject
                // FIXME
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

        [Test]
        public void Q14()
        {
            var subject = new Subject<int>();

            // Q. 偶数が続く間流すoperatorは?
            var observable = subject
                ; // FIXME

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

        [Test]
        public void Q15()
        {
            var subject = new Subject<int>();

            // Q. 偶数がつづくまで値を流さない operatorは?
            var observable = subject
                ; // FIXME

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

        [Test]
        public void Q16()
        {
            var subject = new Subject<int>();
            var trigger = new Subject<bool>();

            // Q. triggerで値がが流れるまで、subjectの値を流すoperatorは?
            var observable = subject
                ; // FIXME

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

        [Test]
        public void Q17()
        {
            var subject = new Subject<int>();
            var trigger = new Subject<bool>();

            // A.
            var observable = subject
                ; // FIXME

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

        [Test]
        public void Q18()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();

            // Q. はじめに1つ0を流すoperatorは?
            subject
                // FXIME
                .Subscribe(observer);

            subject.OnNext(1);

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(0, observer.NextList[0]);
            Assert.AreEqual(1, observer.NextList[1]);
        }

        [Test]
        public void Q19()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();

            // Q. 値が流れたら1msの間来た値を無視するoperatorは?
            subject
                // FIXME
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            Thread.Sleep(TimeSpan.FromMilliseconds(2));

            subject.OnNext(4);

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(4, observer.NextList[1]);
        }

        [Test]
        public void Q20()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();

            // Q. 最後の値が1msの間で流れなくなるまでまってから流れるoperatorは?
            subject
                // FIXME
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            Thread.Sleep(TimeSpan.FromMilliseconds(1));
            UnityEngine.Debug.Log("Waiting thread");

            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);

            Thread.Sleep(TimeSpan.FromMilliseconds(1));
            UnityEngine.Debug.Log("Waiting thread");

            // CHECK
            Assert.AreEqual(2, observer.CountNext);
            Assert.AreEqual(3, observer.NextList[0]);
            Assert.AreEqual(6, observer.NextList[1]);
        }

        [Test]
        public void Q21()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();
            var list = new List<Unit>();

            // Q. Completeが流れたときに、listに1を加えるようなoperatorを定義
            subject
                // FIXME
                .Subscribe(observer);

            // CHECK
            Assert.AreEqual(0, list.Count);

            subject.OnCompleted();

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Q22()
        {
            var subject = new Subject<int>();
            var observer = new TestObserver<int>();

            // Q. 1ms後の値は流さないような operatorは?
            subject
                // FIXME
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
        
        [Test]
        public void Q23()
        {
            var subject = new Subject<string>();
            var observer = new TestObserver<string>();

            // Q. errorを受け取ってそのmessageを流すoperatorを定義
            subject
                // FIXME
                .Subscribe(observer);

            subject.OnError(new Exception("error1"));

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual("error1", observer.NextList[0]);
        }
        
        [Test]
        public void Q24()
        {
            var observer = new TestObserver<int>();
            var subject = new Subject<int>();

            // Q. エラーを無視するoperatorは?
            subject
                // FIXME
                .Subscribe(observer);

            subject.OnNext(1);
            subject.OnError(new Exception("error"));

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
            Assert.AreEqual(0, observer.CountError);
        }
        
        [Test]
        public void Q25()
        {
            var observer = new TestObserver<int>();
            var count = 0;

            // Q. エラーを2度無視して、3回目を通すoperator
            Observable.Create<int>(_observer =>
                {
                    count++;
                    _observer.OnError(new Exception("error" + count));
                    return Disposable.Empty;
                })
                // FIXME
                .Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, observer.CountError);
            Assert.AreEqual("error3", observer.ErrorList[0].Message);
        }
        
        [Test]
        public void Q26()
        {
            var observer = new TestObserver<int>();
            var list = new List<string>();

            // Q. Errorが来たら無視して、listにExceptionのMessageを追加するoperator
            Observable.Create<int>(_observer =>
                {
                    _observer.OnError(new Exception("error1"));
                    return Disposable.Empty;
                })
                // FIXME
                .Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("error1", list[0]);
            Assert.AreEqual(0, observer.CountError);
        }
    }
}