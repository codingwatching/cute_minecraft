using System;

namespace NW
{
    public interface ICore
    {
        public void SetMediator(IMediatorCore mediator);
        public void InstreamData(byte[] data);
        public DataReader DataReader { get; }
    }

}