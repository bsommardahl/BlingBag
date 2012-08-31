using NHibernate;
using NHibernate.Type;

namespace BlingBag.NHibernate
{
    public class BlingInterceptor<TEventType> : EmptyInterceptor
    {
        readonly IBlingInitializer<TEventType> _blingInitializer;

        public BlingInterceptor(IBlingInitializer<TEventType> blingInitializer)
        {
            _blingInitializer = blingInitializer;
        }

        public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            _blingInitializer.Initialize(entity);
            return base.OnLoad(entity, id, state, propertyNames, types);
        }

        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            _blingInitializer.Initialize(entity);
            return base.OnSave(entity, id, state, propertyNames, types);
        }
    }
}