using System;
using PitneyTest.DataAccess.DataObjects;

namespace PitneyTest.EventArguments
{
    public class SelectedTransactionChangedEventArgs : EventArgs
    {
        public SelectedTransactionChangedEventArgs(Content currentContent)
        {
            CurrentContent = currentContent;
        }

        public Content CurrentContent { get; private set; }
    }
}