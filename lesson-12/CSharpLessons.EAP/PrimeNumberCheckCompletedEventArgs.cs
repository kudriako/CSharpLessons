using System;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace CSharpLessons.EAP
{
    public class PrimeNumberCheckCompletedEventArgs : AsyncCompletedEventArgs
    {
        private bool _isPrime;

        public PrimeNumberCheckCompletedEventArgs(decimal number, bool isPrime, double seconds, Exception e, bool canceled, object userSuppliedState) 
            : base(e, canceled, userSuppliedState)
        {
            this.Number = number;
            this.Seconds = seconds;
            this._isPrime = isPrime;
        }

        public decimal Number { get; private set; }

        public double Seconds { get; private set; }

        public bool IsPrime
        {
            get
            {
                RaiseExceptionIfNecessary();
                return _isPrime;
            }
        }
    }
}