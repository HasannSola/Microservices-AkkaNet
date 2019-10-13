using MSA.Entities.Interfaces;
using System.Collections.Generic;

namespace MSA.Entities.Model
{
    public class CResult<T> where T : class, IEntity
    {
        public string Message { get; set; }
        public bool Succeed { get; set; }
        public T Object { get; set; }
        public List<T> Objects { get; set; }
    }
}
