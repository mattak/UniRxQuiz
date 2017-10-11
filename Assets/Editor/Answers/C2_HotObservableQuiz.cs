using System;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace UniRxQuiz.Answer
{
    public class C2_HotObservableQuiz
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
            var cold = Observable.Return(1);
            var hot = new Subject<int>();

            // 呼ばれた瞬間に1が流れます.
            cold.Subscribe(value => UnityEngine.Debug.Log("cold value: " + value));
            
            // 購読開始
            var subscription = hot.Subscribe(value => UnityEngine.Debug.Log("hot value: " + value));
            
            // 流れる 1
            hot.OnNext(1);
            
            // 購読辞め
            subscription.Dispose();
            
            // 流れない 2
            hot.OnNext(2);
            
            // 購読開始
            subscription = hot.Subscribe(value => UnityEngine.Debug.Log("hot value: " + value));
            
            // 流れる 3
            hot.OnNext(3);
            
            hot.OnCompleted();
        }

        [Test]
        public void Q2()
        {
            var hot = new Subject<int>();
            hot.OnNext(1);
            hot.OnCompleted();
        }
        
        [Test]
        public void Q3()
        {
            var hot = new Subject<int>();
            hot.OnNext(1);
            hot.OnError(new Exception("Exception raised"));
        }
        
        [Test]
        public void Q4()
        {
            var hot = new BehaviorSubject<int>(1);
            // new ReplaySubject<int>();
            // new AsyncSubject<int>();
            
            hot.OnNext(2);

            hot.Subscribe(value => Debug.Log("value " + value));
            
            hot.OnNext(3);
        }
    }
}