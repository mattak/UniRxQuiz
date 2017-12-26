using NUnit.Framework;
using UniRx;

namespace HotObservable
{
    public class QuizTest
    {
        [Test]
        public void Q1()
        {
            var testObserver = new TestObserver<int>();

            // Q. IObservableであり、IObserverでもあるHotObservableを宣言しろ
            var observableAndObserver = (IObservable<int>) null; // FIXME

            observableAndObserver.Subscribe(testObserver);
            ((IObserver<int>) observableAndObserver).OnNext(1);
            ((IObserver<int>) observableAndObserver).OnCompleted();

            // CHECK
            Assert.AreEqual(1, testObserver.CountNext);
            Assert.AreEqual(1, testObserver.NextList[0]);
            Assert.AreEqual(1, testObserver.CountComplete);
        }

        [Test]
        public void Q2()
        {
            var testObserver1 = new TestObserver<int>();
            var testObserver2 = new TestObserver<int>();

            // Q. 最後に1つ値を保持するようなIObservableであり、IObserverでもあるHotObservableを宣言しろ
            var observableAndObserver = (IObservable<int>) null; // FIXME

            var disposable1 = observableAndObserver.Subscribe(testObserver1);
            ((IObserver<int>) observableAndObserver).OnNext(2);
            disposable1.Dispose();

            var disposable2 = observableAndObserver.Subscribe(testObserver2);
            ((IObserver<int>) observableAndObserver).OnNext(3);
            disposable2.Dispose();

            // CHECK
            Assert.AreEqual(2, testObserver1.CountNext);
            Assert.AreEqual(1, testObserver1.NextList[0]);
            Assert.AreEqual(2, testObserver1.NextList[1]);

            Assert.AreEqual(2, testObserver2.CountNext);
            Assert.AreEqual(2, testObserver2.NextList[0]);
            Assert.AreEqual(3, testObserver2.NextList[1]);
        }

        [Test]
        public void Q3()
        {
            var testObserver1 = new TestObserver<int>();

            // Q. 来た値をすべて記録しておいて、subscribe時に出力するようなObservableを定義しろ
            var observableAndObserver = (IObservable<int>) null; // FIXME

            ((IObserver<int>) observableAndObserver).OnNext(1);
            ((IObserver<int>) observableAndObserver).OnNext(2);
            ((IObserver<int>) observableAndObserver).OnNext(3);
            ((IObserver<int>) observableAndObserver).OnCompleted();

            observableAndObserver.Subscribe(testObserver1).Dispose();

            // CHECK
            Assert.AreEqual(3, testObserver1.CountNext);
            Assert.AreEqual(1, testObserver1.NextList[0]);
            Assert.AreEqual(2, testObserver1.NextList[1]);
            Assert.AreEqual(3, testObserver1.NextList[2]);
            Assert.AreEqual(1, testObserver1.CountComplete);
        }

        [Test]
        public void Q4()
        {
            var testObserver1 = new TestObserver<int>();
            var testObserver2 = new TestObserver<int>();

            // Q. Completeしたときに最後のNextの値を送信する IObservableでもありIObserverでもあるHotObservableを宣言しろ
            var observableAndObserver = (IObservable<int>) null; // FIXME

            // SUBSCRIBE
            ((IObserver<int>) observableAndObserver).OnNext(1);
            observableAndObserver.Subscribe(testObserver1);

            ((IObserver<int>) observableAndObserver).OnNext(2);
            ((IObserver<int>) observableAndObserver).OnCompleted();
            observableAndObserver.Subscribe(testObserver2);

            // CHECK
            Assert.AreEqual(1, testObserver1.CountNext);
            Assert.AreEqual(2, testObserver1.NextList[0]);
            Assert.AreEqual(1, testObserver1.CountComplete);

            Assert.AreEqual(1, testObserver1.CountNext);
            Assert.AreEqual(2, testObserver1.NextList[0]);
            Assert.AreEqual(1, testObserver1.CountComplete);
        }
    }
}