﻿using System;

namespace AopSample
{
    public abstract class AbstractAspect : Attribute
    {
        public abstract void Invoke(MethodInvocationContext methodInvocationContext, Action next);
    }

    public class TryInvokeAspect : AbstractAspect
    {
        public override void Invoke(MethodInvocationContext methodInvocationContext, Action next)
        {
            Console.WriteLine($"begin invoke method {methodInvocationContext.ProxyMethod.Name} in {GetType().Name}...");
            try
            {
                next();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invoke {methodInvocationContext.ProxyMethod.DeclaringType?.FullName}.{methodInvocationContext.ProxyMethod.Name} exception");
                Console.WriteLine(e);
            }
            Console.WriteLine($"end invoke method {methodInvocationContext.ProxyMethod.Name} in {GetType().Name}...");
        }
    }

    public class TryInvoke1Aspect : AbstractAspect
    {
        public override void Invoke(MethodInvocationContext methodInvocationContext, Action next)
        {
            Console.WriteLine($"begin invoke method {methodInvocationContext.ProxyMethod.Name} in {GetType().Name}...");
            try
            {
                next();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invoke {methodInvocationContext.ProxyMethod.DeclaringType?.FullName}.{methodInvocationContext.ProxyMethod.Name} exception");
                Console.WriteLine(e);
            }
            Console.WriteLine($"end invoke method {methodInvocationContext.ProxyMethod.Name} in {GetType().Name}...");
        }
    }

    public class TryInvoke2Aspect : AbstractAspect
    {
        public override void Invoke(MethodInvocationContext methodInvocationContext, Action next)
        {
            Console.WriteLine($"begin invoke method {methodInvocationContext.ProxyMethod.Name} in {GetType().Name}...");
            try
            {
                next();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invoke {methodInvocationContext.ProxyMethod.DeclaringType?.FullName}.{methodInvocationContext.ProxyMethod.Name} exception");
                Console.WriteLine(e);
            }
            Console.WriteLine($"end invoke method {methodInvocationContext.ProxyMethod.Name} in {GetType().Name}...");
        }
    }
}
