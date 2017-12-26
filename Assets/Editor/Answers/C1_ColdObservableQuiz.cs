using System;
using NUnit.Framework;
using UniRx;

namespace UniRxQuiz.Answer
{
    public class C1_ColdObservableQuiz
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
            var observable = Observable.Return(1);
            observable.Subscribe(value => UnityEngine.Debug.Log("number is " + value));
        }

        [Test]
        public void Q2()
        {
            // var observable = Observable.Return(Unit.Default);
            var observable = Observable.ReturnUnit();
            observable.Subscribe(value => UnityEngine.Debug.Log("next"));
        }

        [Test]
        public void Q3()
        {
            var observable = Observable.Throw<string>(new Exception("Exception raised"));

            observable.Subscribe(
                value => UnityEngine.Debug.Log("next"),
                error => UnityEngine.Debug.Log("error: " + error.Message),
                () => UnityEngine.Debug.Log("compled")
            );
        }

        [Test]
        public void Q4()
        {
            var observable = Observable.Empty<string>();

            observable.Subscribe(
                value => UnityEngine.Debug.Log("next"),
                error => UnityEngine.Debug.Log("error"),
                () => UnityEngine.Debug.Log("compled")
            );
        }


        [Test]
        public void Q5()
        {
            var observable = Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                observer.OnNext(2);
                observer.OnCompleted();
                return Disposable.Empty;
            });

            observable.Subscribe(
                value => UnityEngine.Debug.Log("next: " + value),
                error => UnityEngine.Debug.Log("error"),
                () => UnityEngine.Debug.Log("compled")
            );
        }
    }
}