﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Wrappers.Mappers;

namespace Turmerik.Core.Data.Cloneable.Mappers
{
    public interface ICloneableMtblMapper : ICloneableMapperCore
    {
    }

    public class CloneableMtblMapper : CloneableMapperCoreBase, ICloneableMtblMapper
    {
        protected override object GetNestedClonableWrapperTrgPropValue(object srcPropValue, Type srcPropType, Type trgPropType)
        {
            throw new NotImplementedException();
        }
    }
}
