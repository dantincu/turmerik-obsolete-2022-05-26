using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Cloneable.Nested.Mappers
{
    public interface INestedObjNmrblMapper<TObj> : INestedObjMapper<INestedObjNmrbl<TObj>>
    {
    }

    public interface INestedImmtblObjNmrblMapper<TObj> : INestedObjNmrblMapper<TObj>
    {
    }

    public interface INestedMtblObjNmrblMapper<TObj> : INestedObjNmrblMapper<TObj>
    {
    }

    public class NestedImmtblObjNmrblMapper<TObj> : NestedImmtblObjMapperBase<INestedObjNmrbl<TObj>, ReadOnlyCollection<TObj>, List<TObj>>, INestedImmtblObjNmrblMapper<TObj>
    {
        protected override ReadOnlyCollection<TObj> GetImmtbl(List<TObj> mtbl) => mtbl.RdnlC();
        protected override INestedObjNmrbl<TObj> GetNested(ReadOnlyCollection<TObj> immtbl) => new NestedObjNmrbl<TObj>(immtbl);
    }

    public class NestedMtblObjNmrblMapper<TObj> : NestedMtblObjMapperBase<INestedObjNmrbl<TObj>, ReadOnlyCollection<TObj>, List<TObj>>, INestedMtblObjNmrblMapper<TObj>
    {
        protected override List<TObj> GetMtbl(ReadOnlyCollection<TObj> immtbl) => immtbl.ToList();
        protected override INestedObjNmrbl<TObj> GetNested(List<TObj> mtbl) => new NestedObjNmrbl<TObj>(null, mtbl);
    }
}
