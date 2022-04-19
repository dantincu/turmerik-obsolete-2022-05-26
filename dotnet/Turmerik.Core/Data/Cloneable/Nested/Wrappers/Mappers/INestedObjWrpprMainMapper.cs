using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers
{
    public interface INestedObjWrpprMainMapper : INestedObjWrpprMapperCore
    {
    }

    public abstract class NestedObjWrpprMainMapperBase : ComponentBase, INestedObjWrpprMainMapper
    {
        public NestedObjWrpprMainMapperBase(IServiceProvider services) : base(services)
        {
        }

        public INestedObjWrpprCore GetTrgPropValue(INestedObjMapOpts opts)
        {
            INestedObjWrpprMapperCore mapper;

            if (typeof(INestedObjDictnrWrppr).IsAssignableFrom(opts.TrgPropType))
            {
                mapper = GetNestedObjDictnrWrpprMapper(opts);
            }
            else if (typeof(INestedObjNmrblWrppr).IsAssignableFrom(opts.TrgPropType))
            {
                mapper = GetNestedObjNmrblWrpprMapper(opts);
            }
            else if (typeof(INestedObjWrppr).IsAssignableFrom(opts.TrgPropType))
            {
                mapper = GetNestedObjWrpprMapper(opts);
            }
            else
            {
                throw new ArgumentException($"Type {opts.TrgPropType.FullName} cannot be mapped as nested cloneable wrapper");
            }

            INestedObjWrpprCore trgPropValue = mapper.GetTrgPropValue(opts);
            return trgPropValue;
        }

        protected abstract INestedObjDictnrWrpprMapper GetNestedObjDictnrWrpprMapper(INestedObjMapOpts opts);
        protected abstract INestedObjNmrblWrpprMapper GetNestedObjNmrblWrpprMapper(INestedObjMapOpts opts);
        protected abstract INestedObjWrpprMapper GetNestedObjWrpprMapper(INestedObjMapOpts opts);
    }
}
