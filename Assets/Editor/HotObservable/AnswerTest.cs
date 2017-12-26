using NUnit.Framework;
using UniRx;

namespace HotObservable
{
    public class AnswerTest
    {
        // Subject
        [Test]
        public void A1()
        {
            var testObserver = new TestObserver<int>();
            
            // A.
            var observableAndObserver = new Subject<int>();
            
            observableAndObserver.Subscribe(testObserver);
            ((IObserver<int>)observableAndObserver).OnNext(1);
            ((IObserver<int>)observableAndObserver).OnCompleted();
            
            // CHECK
            Assert.AreEqual(1, testObserver.CountNext);
            Assert.AreEqual(1, testObserver.NextList[0]);
            Assert.AreEqual(1, testObserver.CountComplete);
        }
        
        // BehaviorSubject
        [Test]
        public void A2()
        {
            var testObserver1 = new TestObserver<int>();
            var testObserver2 = new TestObserver<int>();
            
            // A.
            var observableAndObserver = new BehaviorSubject<int>(1);
            
            var disposable1 = observableAndObserver.Subscribe(testObserver1);
            ((IObserver<int>)observableAndObserver).OnNext(2);
            disposable1.Dispose();

            var disposable2 = observableAndObserver.Subscribe(testObserver2);
            ((IObserver<int>)observableAndObserver).OnNext(3);
            disposable2.Dispose();
            
            // CHECK
            Assert.AreEqual(2, testObserver1.CountNext);
            Assert.AreEqual(1, testObserver1.NextList[0]);
            Assert.AreEqual(2, testObserver1.NextList[1]);
            
            Assert.AreEqual(2, testObserver2.CountNext);
            Assert.AreEqual(2, testObserver2.NextList[0]);
            Assert.AreEqual(3, testObserver2.NextList[1]);
        }
        
        // ReplaySubject
        [Test]
        public void A3()
        {
            var testObserver1 = new TestObserver<int>();
            
            // A.
            var observableAndObserver = new ReplaySubject<int>();
            
            ((IObserver<int>)observableAndObserver).OnNext(1);
            ((IObserver<int>)observableAndObserver).OnNext(2);
            ((IObserver<int>)observableAndObserver).OnNext(3);
            ((IObserver<int>)observableAndObserver).OnCompleted();
            
            observableAndObserver.Subscribe(testObserver1).Dispose();
            
            // CHECK
            Assert.AreEqual(3, testObserver1.CountNext);
            Assert.AreEqual(1, testObserver1.NextList[0]);
            Assert.AreEqual(2, testObserver1.NextList[1]);
            Assert.AreEqual(3, testObserver1.NextList[2]);
            Assert.AreEqual(1, testObserver1.CountComplete);
        }
        
        // AsyncSubject
        [Test]
        public void A4()
        {
            var testObserver1 = new TestObserver<int>();
            var testObserver2 = new TestObserver<int>();
            
            // A.
            var observableAndObserver = new AsyncSubject<int>();
            
            // SUBSCRIBE
            ((IObserver<int>)observableAndObserver).OnNext(1);
            observableAndObserver.Subscribe(testObserver1);
            
            ((IObserver<int>)observableAndObserver).OnNext(2);
            ((IObserver<int>)observableAndObserver).OnCompleted();
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
