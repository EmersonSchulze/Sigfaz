using System;
using System.Web.Mvc;

namespace Sigfaz.Infra.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class CalculatorAttribute : Attribute, IMetadataAware
    {
        public CalculatorAttribute()
        { }
        void IMetadataAware.OnMetadataCreated(ModelMetadata metadata)
        {
            var config = new CalculatorConfiguracao();
            metadata.AdditionalValues["CalculatorConfiguracoes"] = config;
        }
    }
}
