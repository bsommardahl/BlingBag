using System.Collections.Generic;

namespace BlingBag.Specs
{
    public class OrgUnit
    {
        public int Id { get; set; }

        public OrgUnit Parent { get; set; }

        List<OrgUnit> _children;
        public List<OrgUnit> Children
        {
            get { return _children ?? (_children = new List<OrgUnit>()); }
            set { _children = value; }
        }

        public event Blinger Notify;

        public void Go()
        {
            Notify(this);
        }
    }
}