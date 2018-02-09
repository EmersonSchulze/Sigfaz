using System;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Atributo que indica que determinado Action ou Controller não deve ser validado pela segurança.
    /// Os métodos que começam com _Handle são implicitamente marcados com este atributo.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class InternalAttribute : Attribute
    {
    }
}
