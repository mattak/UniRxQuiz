using NUnit.Framework;
using UniRx;

namespace ColdObservable
{
    public class QuizTest
    {
        [Test]
        public void Q1()
        {
            var observer = new TestObserver<int>();

            // Q. 1を出力する Cold Observable をつくれ
            var observable = (IObservable<int>) null; // FIXME
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(1, observer.NextList[0]);
        }

        [Test]
        public void Q2()
        {
            var observer = new TestObserver<Unit>();

            // Q. Unitを出力する Cold Observableをつくれ
            var observable = (IObservable<Unit>) null; // FIXME
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, observer.CountNext);
            Assert.AreEqual(Unit.Default, observer.NextList[0]);
        }

        [Test]
        public void Q3()
        {
            var observer = new TestObserver<Unit>();

            // Q. errorというmessageのExceptionを出力する Cold Observableをつくれ
            var observable = (IObservable<Unit>) null; // FIXME
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, observer.CountError);
            Assert.AreEqual("error", observer.ErrorList[0].Message);
        }

        [Test]
        public void Q4()
        {
            var observer = new TestObserver<Unit>();

            // Q. Completeを出力する Cold Observableをつくれ
            var observable = (IObservable<Unit>) null; // FIXME
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(1, observer.CountComplete);
        }
        
        [Test]
        public void Q5()
        {
            var observer = new TestObserver<Unit>();

            // Q. 何も出力しない Cold Observableをつくれ
            var observable = (IObservable<Unit>) null; // FIXME
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(0, observer.CountNext);
            Assert.AreEqual(0, observer.CountError);
            Assert.AreEqual(0, observer.CountComplete);
        }

        [Test]
        public void Q6()
        {
            var observer = new TestObserver<int>();

            // Q. 1,2,3を出力して、Completeする Cold Observableを作れ
            var observable = (IObservable<int>) null; // FIXME
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(3, observer.CountNext);
            Assert.AreEqual(new[] {1, 2, 3}, observer.NextList);
            Assert.AreEqual(1, observer.CountComplete);
        }
        
        [Test]
        public void Q7()
        {
            var observer = new TestObserver<int>();

            // Q. 1から10まで出力する Cold Observableをつくれ
            var observable = (IObservable<int>) null; // FIXME
            observable.Subscribe(observer);

            // CHECK
            Assert.AreEqual(10, observer.CountNext);
            Assert.AreEqual(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, observer.NextList);
            Assert.AreEqual(1, observer.CountComplete);
        }
    }
}