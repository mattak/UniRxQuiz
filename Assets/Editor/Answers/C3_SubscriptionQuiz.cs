using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace UniRxQuiz.Answer
{
    public class C3_SubscriptionQuiz
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
            var hot = new Subject<int>();
            var disposable = hot.Subscribe(value => UnityEngine.Debug.Log("value " + value));
            
            hot.OnNext(1);
            
            disposable.Dispose();
            
            hot.OnNext(2);
        }
        
        [Test]
        public void Q2()
        {
            var hot = new Subject<int>();
            var disposable = hot.Subscribe(value => UnityEngine.Debug.Log("value " + value));
            
            // OnDestroyしたときに Dispose() してくれる
            disposable.AddTo(new GameObject());
            
            hot.OnNext(1);
            
            disposable.Dispose();
            
            hot.OnNext(2);
        }
    }
}