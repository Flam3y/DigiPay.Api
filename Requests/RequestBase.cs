using DigiPay.Api.Requests.Abstractions;
using System.Runtime.Serialization;
using System.Web;

namespace DigiPay.Api.Requests;

public abstract class RequestBase(string methodPath) : IRequest
{
    [IgnoreDataMember]
    public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

    [IgnoreDataMember]
    public string MethodPath { get; } = methodPath;

    [IgnoreDataMember]
    public bool IsWebhookResponse { get; set; }

    [IgnoreDataMember]
    internal string? Method => IsWebhookResponse ? MethodPath : default;

    public virtual string ToQueryParameters()
    {
        IEnumerable<string> properties = GetType().GetProperties()
            .Where(x => !Attribute.IsDefined(x, typeof(IgnoreDataMemberAttribute)))
            .Where(x => x.GetValue(this, null) is not null)
            .Select(x => x.Name + "=" + HttpUtility.UrlEncode(x.GetValue(this, null)!.ToString()));

        return string.Join('&', properties);
    }
}

public abstract class ParameterlessRequest(string methodName) : RequestBase(methodName)
{
    public override string ToQueryParameters() => IsWebhookResponse ? base.ToQueryParameters() : "";
}