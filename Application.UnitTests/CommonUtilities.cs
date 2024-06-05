using System.Reflection;

namespace Application.UnitTests;

internal static class CommonUtilities
{
    public static async Task<T?> InvokePrivateMethodAsync<T>(object instance, string methodName, params object[] parameters)
    {
        var methodInfo = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (methodInfo == null)
        {
            throw new ArgumentException($"Method '{methodName}' not found on the type '{instance.GetType().FullName}'.");
        }

        object? result = methodInfo.Invoke(instance, parameters);

        if (result is Task<T> taskResult)
        {
            return await taskResult.ConfigureAwait(false);
        }
        else if (result is Task task)
        {
            await task.ConfigureAwait(false);
            return default; // Non-generic Task, return default(T)
        }
        else
        {
            return (T?)result;
        }
    }

    public static async Task InvokePrivateMethodAsync(object instance, string methodName, params object[] parameters)
    {
        var methodInfo = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (methodInfo == null)
        {
            throw new ArgumentException($"Method '{methodName}' not found on the type '{instance.GetType().FullName}'.");
        }

        object? result = methodInfo.Invoke(instance, parameters);

        if (result is Task task)
        {
            await task.ConfigureAwait(false);
        }
    }

    public static T? InvokePrivateMethod<T>(object instance, string methodName, params object[] parameters)
    {
        var methodInfo = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (methodInfo == null)
        {
            throw new ArgumentException($"Method '{methodName}' not found on the type '{instance.GetType().FullName}'.");
        }

        return (T?)methodInfo.Invoke(instance, parameters);
    }

}
