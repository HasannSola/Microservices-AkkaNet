using MSA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSA.Entities.Message
{
    public class AddMessage
    {
        public IEntity Value { get; }
        public AddMessage(IEntity value)
        {
            Value = value;
        }
    }
}
