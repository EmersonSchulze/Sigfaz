using System;
using System.Web.Mvc;

namespace Sigfaz.Infra.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class CepAttribute : Attribute, IMetadataAware
    {
        public CepAttribute()
        { }
        void IMetadataAware.OnMetadataCreated(ModelMetadata metadata)
        {
            var config = new CepConfiguracao();
            metadata.AdditionalValues["CepConfiguracoes"] = config;
        }
    }
}
