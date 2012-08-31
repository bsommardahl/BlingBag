using System;
using System.Collections.Generic;

namespace BlingBag.Specs
{
    public class OrgUnit
    {
        List<OrgUnit> _children;
        public int Id { get; set; }

        public OrgUnit Parent { get; set; }

        public List<OrgUnit> Children
        {
            get { return _children ?? (_children = new List<OrgUnit>()); }
            set { _children = value; }
        }

        public event Action<object> Notify;

        public void Go()
        {
            Notify(this);
        }
    }
}