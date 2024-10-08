using Pepegov.MicroserviceFramework.ApiResults;

namespace Template.Net.NUnit.Test.Core.Asserts;

/// <summary>
/// Custom class for ApiResult assertion
/// </summary>
public static class ResultAssert
{
    /// <summary>
    /// Ensure that ApiResult contains required exception
    /// </summary>
    /// <param name="result">ApiResult to check</param>
    /// <typeparam name="TException">Exception to check</typeparam>
    public static void ContainsException<TException>(ApiResult result)
        where TException : Exception
    {
        Assert.That(result.IsSuccessful, Is.False, $"ApiResult is successful");
        Assert.That(result.Exceptions, Is.Not.Null.Or.Empty, "ApiResult does not have exceptions");
        Assert.That(result.Exceptions!.Any(ex => ex.TypeData.Name == typeof(TException).Name), Is.True,
            $"ApiResult does not contains exception with type: {typeof(TException).Name} | Actual exceptions: {result.GetExceptionMessages()}");
    }
    
    public static void Success(ApiResult result)
    {
        Assert.That(result.IsSuccessful, Is.True, $"ApiResult is not successful | exceptions: {result.GetExceptionMessages()}");
    }
    
    public static void Failure(ApiResult result)
    {
        Assert.That(result.IsSuccessful, Is.False, $"ApiResult is successful");
    }
}