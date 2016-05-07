/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-08-08 08:39:22
 * @version 1.0.0 
 */
#if CSHARP2  
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace System
{

    public delegate void Action();
    public delegate void Action<in T>(T obj);
    public delegate void Action<in T1, in T2>(T1 arg1, T2 arg2);
    public delegate void Action<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3);
    public delegate void Action<in T1, in T2, in T3, in T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    public delegate TResult Func<out TResult>();
    public delegate TResult Func<in T, out TResult>(T arg);
    public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
    public delegate TResult Func<in T1, in T2, T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);
}

public static class SysEx 
{

    /// <summary>
    /// 获取对象的私有字段的值，感谢Aaron Lee Murgatroyd
    /// </summary>
    /// <typeparam name="TResult">字段的类型</typeparam>
    /// <param name="obj">要从其中获取字段值的对象</param>
    /// <param name="fieldName">字段的名称.</param>
    /// <returns>字段的值</returns>
    /// <exception cref="System.InvalidOperationException">无法找到该字段.</exception>
    /// 
    public static TResult GetPrivateField<TResult>(this object obj, string fieldName)
    {
        if (obj == null) return default(TResult);
        Type ltType = obj.GetType();
        FieldInfo lfiFieldInfo = ltType.GetField(fieldName, System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        if (lfiFieldInfo != null)
            return (TResult)lfiFieldInfo.GetValue(obj);
        else
            throw new InvalidOperationException(string.Format("Instance field '{0}' could not be located in object of type '{1}'.", fieldName, obj.GetType().FullName));
    }
}

namespace System.Runtime.CompilerServices
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
    public sealed class ExtensionAttribute : Attribute { }

}

#endif