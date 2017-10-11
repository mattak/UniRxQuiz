using NUnit.Framework;
using UniRx;

namespace UniRxQuiz.Answer
{
    public class C4_OperationQuiz
    {
        private int QuizNumber = 0;

        [SetUp]
        public void Setup()
        {
            this.QuizNumber++;
            UnityEngine.Debug.Log(string.Format("{0} / Q{1}", this.GetType().Name, this.QuizNumber));
        }
        
        [Test]
        public void Q1()
        {
            // perl: map
            // ruby: collection
            // kotlin: map
            Observable.Return(1)
                .Select(number => number.ToString())
                .Subscribe();
        }

        [Test]
        public void Q2()
        {
            // perl: grep
            // ruby: filter
            // kotlin: filter
            Observable.Return(9)
                .Where(number => number >= 10)
                .Subscribe();
        }

        [Test]
        public void Q3()
        {
            // rxjava: FlatMap
            Observable.Return(100)
                .SelectMany(number =>
                {
                    return Observable.Create<int>(observer =>
                    {
                        observer.OnNext(number);
                        observer.OnNext(number * 2);
                        observer.OnCompleted();
                        return Disposable.Empty;
                    });
                })
                .Subscribe(
                    number => UnityEngine.Debug.Log("number: " + number),
                    error => { },
                    () => UnityEngine.Debug.Log("completed")
                );
        }
    }
}