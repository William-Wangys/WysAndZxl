using System;

namespace DI.Attributes
{
    /// <summary>
    /// 依赖注入特性，范围：构造函数、属性、方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor |
                   AttributeTargets.Property |
                   AttributeTargets.Method,
                   AllowMultiple = false)]
    public class InjectionAttribute : Attribute { }
}
