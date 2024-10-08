namespace Template.Net.NUnit.Test.Core.Http;

/// <summary>
/// This class used for mocking http client.
/// <remarks>
/// HttpClient does not have a interface and HttpMessageHandler SendAsync method is protected.
/// This is the simplest way to solve this problem
/// </remarks>
/// </summary>
public class MockHttpMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(MockSend(request, cancellationToken));
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return MockSend(request, cancellationToken);
    }

    /// <summary>
    /// Method to mock send message
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException">If mock was not configured</exception>
    public virtual HttpResponseMessage MockSend(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException($"{nameof(MockSend)} mock method is not configured");
    }
}