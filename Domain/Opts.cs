using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Opts
    {
        public bool allowGet;
        public bool allowPost;
        public bool allowPut;
        public bool allowPatch;
        public bool allowOptions;
        public bool allowDelete;
    }
}