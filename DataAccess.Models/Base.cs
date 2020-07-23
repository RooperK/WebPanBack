using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Base<TId>
    {
        public TId Id { get; set; }
    }
}