using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Core
{
    /// <summary>
    /// Generic service base class.  This is where all the business logic resides.
    /// </summary>
    /// <typeparam name="T">Type of service</typeparam>
    public class Service<T> : IService<T>
    {
    }
}
