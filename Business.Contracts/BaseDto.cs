using System;

namespace Business.Contracts
{
    public class BaseDto<TI>
    {
        public TI Id { get; set; }
    }
}