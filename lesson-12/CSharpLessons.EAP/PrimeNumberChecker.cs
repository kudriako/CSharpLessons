using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace CSharpLessons.EAP
{
    public class PrimeNumberChecker
    {
        private delegate void WorkerEventHandler(decimal number, AsyncOperation asyncOp);

        private SendOrPostCallback _reportProgressDelegate;

        private SendOrPostCallback _completedDelegate;

        private ConcurrentDictionary<object, AsyncOperation> _userStateToLifetime = new ConcurrentDictionary<object, AsyncOperation>();

        public PrimeNumberChecker()
        {   
            _reportProgressDelegate = new SendOrPostCallback(OnReportProgress);
            _completedDelegate = new SendOrPostCallback(OnCompleted);
        }

        public virtual void IsPrimeAsync(decimal number, object userSuppliedState)
        {
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userSuppliedState);
            if (!_userStateToLifetime.TryAdd(userSuppliedState, asyncOp))
            {
                throw new ArgumentException("Task ID parameter must be unique", "taskId");
            }
            WorkerEventHandler workerDelegate = new WorkerEventHandler(CalculateWorker);
            workerDelegate.BeginInvoke(number, asyncOp, null, null);
        }

        public void CancelAsync(object userSuppliedState)
        {
            _userStateToLifetime.TryRemove(userSuppliedState, out AsyncOperation value);
        }

        public event EventHandler<PrimeNumberCheckProgressChangedEventArgs> ProgressChanged;

        public event EventHandler<PrimeNumberCheckCompletedEventArgs> Completed;

        private void CalculateWorker(decimal number, AsyncOperation asyncOp)
        {
            bool isPrime = false;
            double seconds = 0.0;
            Exception exception = null;
            if (!TaskCanceled(asyncOp.UserSuppliedState))
            {
                try
                {
                    isPrime = IsPrimeInternal(number, out seconds, asyncOp);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }
            if (TaskCanceled(asyncOp.UserSuppliedState))
            {
                _userStateToLifetime.TryRemove(asyncOp.UserSuppliedState, out AsyncOperation value);
            }
            var args = new PrimeNumberCheckCompletedEventArgs(number, isPrime, seconds, exception, 
                TaskCanceled(asyncOp.UserSuppliedState), asyncOp.UserSuppliedState);
            asyncOp.PostOperationCompleted(_completedDelegate, args);
        }

        public bool IsPrimeInternal(decimal number, out double seconds, AsyncOperation asyncOp)
        {
            const int reportingStep = 5;
            if (number < 1)
            {
                throw new InvalidOperationException($"{number} is invalid number.");
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                ReportProgress(number, stopwatch.Elapsed.TotalSeconds, 0, asyncOp);
                if (number == 2)
                    return true;
                if (number % 2 == 0)
                    return false;
                double limit = (uint)Math.Sqrt((double)number);
                int progressToReport = reportingStep;
                for(uint i = 3; i <= limit; i += 2)
                {
                    var progress = (int)(100.0 * i / limit);
                    if (progress >= progressToReport)
                    {
                        ReportProgress(number, stopwatch.Elapsed.TotalSeconds, progress, asyncOp);
                        progressToReport += 10; 
                    }
                    if (number % i == 0)
                        return false;
                    if (TaskCanceled(asyncOp.UserSuppliedState))
                        return false;
                }
                return true;
            }
            finally
            {
                stopwatch.Stop();
                seconds = stopwatch.Elapsed.TotalSeconds;
            }
        }

        private void ReportProgress(decimal number, double seconds, int progress, AsyncOperation asyncOp)
        {
            var args = new PrimeNumberCheckProgressChangedEventArgs(number, seconds, progress, asyncOp.UserSuppliedState);
            asyncOp.Post(_reportProgressDelegate, args);
        }

        private bool TaskCanceled(object userSuppliedState)
        {
            return !_userStateToLifetime.ContainsKey(userSuppliedState);
        }

        private void OnCompleted(object operationState)
        {
            Completed?.Invoke(this, operationState as PrimeNumberCheckCompletedEventArgs);
        }

        private void OnReportProgress(object state)
        {
            ProgressChanged?.Invoke(this, state as PrimeNumberCheckProgressChangedEventArgs);
        }
    }
}