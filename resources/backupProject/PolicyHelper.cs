//-----------------------------------------------------------------------
// <copyright file="Context.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace vscode_azureapim
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Class to represent the context object in APIM
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Context Api
        /// </summary>
        public Api Api { get; }

        /// <summary>
        /// Context deployment
        /// </summary>
        public Deployment Deployment { get; }

        /// <summary>
        /// Context Product
        /// </summary>
        public Product Product { get; }

        /// <summary>
        /// Time interval between the value of Timestamp and current time
        /// </summary>
        public TimeSpan Elapsed { get; }

        /// <summary>
        /// Context Variables
        /// </summary>
        public IVariablesDictionary Variables { get; }

        /// <summary>
        /// Context Request
        /// </summary>
        public Request Request { get; }

        /// <summary>
        /// Context Response
        /// </summary>
        public Response Response { get; }

        /// <summary>
        /// Context Operation
        /// </summary>
        public Operation Operation { get; }

        /// <summary>
        /// Gets the last error.
        /// </summary>
        /// <value>
        /// The last error.
        /// </value>
        public PolicyError LastError { get; }

        /// <summary>
        /// Unique request identifier.
        /// This is also the 'correlationId' assigned by ApiM and is included in their standard telemetry events.
        /// </summary>
        public Guid RequestId { get; }

        /// <summary>
        /// Traces a message
        /// </summary>
        /// <param name="message">The message</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Purely used for model reasons")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Purely used for model reasons")]
        public void Trace(string message)
        {
            throw new NotImplementedException("Used for modeling");
        }
    }

    /// <summary>
    /// Class to represent the context response in APIM
    /// </summary>
    public class Response : IResponse
    {
        /// <summary>
        /// The response body
        /// </summary>
        public IMessageBody Body { get; }

        /// <summary>
        /// The headers in the response
        /// </summary>
        public IMultipleStringValuesDictionary Headers { get; }

        /// <summary>
        /// Response status code
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Response status code reason
        /// </summary>
        public string StatusReason { get; }
    }

     /// <summary>
    /// Class to represent the context request in APIM
    /// </summary>
    public class Request
    {
        /// <summary>
        /// The request body
        /// </summary>
        public IMessageBody Body { get; }

        /// <summary>
        /// Request headers
        /// </summary>
        public IMultipleStringValuesDictionary Headers { get; }

        /// <summary>
        /// Request Ip Address
        /// </summary>
        public string IpAddress { get; }

        /// <summary>
        /// Request matched parameters
        /// </summary>
        public IReadOnlyStringDictionary MatchedParameters { get; }

        /// <summary>
        /// Request method
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// Original url of the request
        /// </summary>
        public IUrl OriginalUrl { get; }

        /// <summary>
        /// Request url
        /// </summary>
        public IUrl Url { get; }
    }

     /// <summary>
    /// Class to represent the Context.Product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The operation id
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The operation name
        /// </summary>
        public string Name { get; }

        // Note, not all members are currently here.
    }

    /// <summary>
    /// Policy Error <see cref="https://docs.microsoft.com/en-us/azure/api-management/api-management-error-handling-policies"/>
    /// </summary>
    public class PolicyError
    {
        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source { get; }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Gets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        public string Scope { get; }

        /// <summary>
        /// Gets the section.
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        public string Section { get; }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; }

        /// <summary>
        /// Gets the policy identifier.
        /// </summary>
        /// <value>
        /// The policy identifier.
        /// </value>
        public string PolicyId { get; }
    }

       /// <summary>
    /// Class to represent the operation object in APIM
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// The operation id
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The operation method
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// The operation name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The url template
        /// </summary>
        public string UrlTemplate { get; }
    }

    internal class MultipleStringValuesDictionary : Dictionary<string, string[]>, IMultipleStringValuesDictionary
    {
        internal MultipleStringValuesDictionary(IDictionary<string, string[]> dictionary)
            : base(dictionary, StringComparer.Ordinal)
        {
        }

        public string GetValueOrDefault(string variableName, string defaultValue)
        {
            string[] value;
            if (!this.TryGetValue(variableName, out value))
            {
                return defaultValue;
            }

            return value == null ? null : string.Join(",", value);
        }

        public string GetValueOrDefault(string variableName)
        {
            return this.GetValueOrDefault(variableName, null);
        }
    }

    public class Jwt
    {
        /// <summary>
        /// 'alg' header parameter
        /// </summary>
        public string Algorithm { get; }

        /// <summary>
        /// 'typ' header parameter
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// 'iss' claim
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// 'sub' claim
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// 'aud' claim
        /// </summary>
        public IEnumerable<string> Audience { get; }

        /// <summary>
        /// 'exp' claim
        /// </summary>
        public DateTime? ExpirationTime { get; }

        /// <summary>
        /// 'nbf' claim
        /// </summary>
        public DateTime? NotBefore { get; }

        /// <summary>
        /// 'jti' claim
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Claims
        /// </summary>
        public IMultipleStringValuesDictionary Claims { get; }
    }

     public interface IVariablesDictionary : IReadOnlyDictionary<string, object>
    {
        /// <summary>
        /// Get the value of the variable if it exists, otherwise returns the defaultValue.
        /// This method throws an exception if the specified type does not match the actual type of the returned variable.
        /// </summary>
        /// <param name="variableName">The key/name of the value to retrieve.</param>
        /// <param name="defaultValue">The value to return when the key is not found.</param>
        /// <typeparam name="T">The expected type of the value of the variable.</typeparam>
        /// <returns>The value for the specified key. Otherwise, defaultValue.</returns>
        T GetValueOrDefault<T>(string variableName, T defaultValue);

        /// <summary>
        /// Get the value of the variable if it exists, otherwise returns default(T).
        /// This method throws an exception if the specified type does not match the actual type of the returned variable.
        /// </summary>
        /// <param name="variableName">The key/name of the value to retrieve.</param>
        /// <typeparam name="T">The expected type of the value of the variable.</typeparam>
        /// <returns>The value for the specified key. Otherwise, default(T).</returns>
        T GetValueOrDefault<T>(string variableName);
    }
    
     /// <summary>
    /// Class to represent the context url in APIM.
    /// </summary>
    public interface IUrl
    {
        /// <summary>
        /// Host portion of the url
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Path portion of the url
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Port of the url
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Query dictionary of the url
        /// </summary>
        IMultipleStringValuesDictionary Query { get; }

        /// <summary>
        /// Raw query string of the url
        /// </summary>
        string QueryString { get; }

        /// <summary>
        /// The scheme of the url
        /// </summary>
        string Scheme { get; }

        /// <summary>
        /// Coverts this to a System.Uri.
        /// </summary>
        /// <returns>A Uri representation of this object</returns>
        Uri ToUri();
    }

    /// <summary>
    /// Contract of a response as a result of a call to send-request policy item.
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// The response body
        /// </summary>
        IMessageBody Body { get; }

        /// <summary>
        /// The headers in the response
        /// </summary>
        IMultipleStringValuesDictionary Headers { get; }

        /// <summary>
        /// Response status code
        /// </summary>
        int StatusCode { get; }

        /// <summary>
        /// Response status code reason
        /// </summary>
        string StatusReason { get; }
    }

     /// <summary>
    /// Type-safe interface for the dictionaries exposed by the Context which have a string value for each key.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "It is a dictionary.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "It is a dictionary.")]
    public interface IReadOnlyStringDictionary : IReadOnlyDictionary<string, string>
    {
        /// <summary>
        /// Get the value if the key exists, otherwise returns the defaultValue.
        /// </summary>
        /// <param name="key">The key/name of the value to retrieve.</param>
        /// <param name="defaultValue">The value to return when the key is not found.</param>
        /// <returns>The value for the specified key. Otherwise, defaultValue.</returns>
        string GetValueOrDefault(string key, string defaultValue);

        /// <summary>
        /// Get the value if the key exists, otherwise returns null.
        /// </summary>
        /// <param name="key">The key/name of the value to retrieve.</param>
        /// <returns>The value for the specified key. Otherwise, null.</returns>
        string GetValueOrDefault(string key);
    }

     /// <summary>
    /// Type-safe interface for the dictionaries exposed by the Context which have multiple possible values per key.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "It is a dictionary.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "It is a dictionary.")]
    public interface IMultipleStringValuesDictionary : IReadOnlyDictionary<string, string[]>
    {
        /// <summary>
        /// Get the value if the key exists, otherwise returns the defaultValue.
        /// If there were multiple values specified for the key, the values are returned as a comma-separated list of values.
        /// </summary>
        /// <param name="key">The key/name of the value to retrieve.</param>
        /// <param name="defaultValue">The value to return when the key is not found.</param>
        /// <returns>The value for the specified key. Otherwise, defaultValue.</returns>
        string GetValueOrDefault(string key, string defaultValue);

        /// <summary>
        /// Get the value if the key exists, otherwise returns null.
        /// If there were multiple values specified for the key, the values are returned as a comma-separated list of values.
        /// </summary>
        /// <param name="key">The key/name of the value to retrieve.</param>
        /// <returns>The value for the specified key. Otherwise, null.</returns>
        string GetValueOrDefault(string key);
    }

     /// <summary>
    /// Class to represent the context message body in APIM
    /// </summary>
    public interface IMessageBody
    {
        /// <summary>
        /// Reads the request or response message bodies in a specified type.
        /// </summary>
        /// <typeparam name="T">Type to output the body content</typeparam>
        /// <returns>T version of the object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "As", Justification = "Modeling")]
        T As<T>();

        /// <summary>
        /// Reads the request or response message bodies in a specified type.
        /// </summary>
        /// <typeparam name="T">Type to output the body content</typeparam>
        /// <param name="preserve">If set to true, the method will operate on a copy of the body stream and the original body stream will still be available. Default is false.</param>
        /// <returns>T version of the object, but preserving content</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "As", Justification = "Modeling")]
        T As<T>(bool preserve);
    }

    
    /// <summary>
    /// Contract for Context IApi.
    /// </summary>
    public interface IApi
    {
        /// <summary>
        /// The Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The Path
        /// </summary>
        string Path { get; }

        /// <summary>
        /// The Protocols
        /// </summary>
        IEnumerable<string> Protocols { get; }

        /// <summary>
        /// The ServiceUrl
        /// </summary>
        IUrl ServiceUrl { get; }

        // TODO: SubscriptionKeyParameterNames
    }

    /// <summary>
    /// Extension methods for policy context.
    /// See: https://docs.microsoft.com/en-us/azure/api-management/api-management-policy-expressions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Tries to parse a Jwt from a string.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        /// <returns>If the input parameter contains a valid JWT token value, the method returns true and the result parameter contains a value of type Jwt; otherwise the method returns false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Modeling")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Modeling")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Modeling.")]
        public static bool TryParseJwt(this string input, out Jwt result)
        {
            throw new NotImplementedException("Used for modeling");
        }
    }

      /// <summary>
    /// Contract for Context Api
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724", Justification = "This class is not used in this project.")]
    public class Deployment
    {
        internal Deployment()
        {
            throw new NotImplementedException("Used for modeling");
        }

        /// <summary>
        /// Region
        /// </summary>
        public string Region { get; }

        /// <summary>
        /// Service Name
        /// </summary>
        public string ServiceName { get; }

        /// <summary>
        /// Certificates
        /// </summary>
        public IReadOnlyDictionary<string, X509Certificate2> Certificates { get; }
    }

    /// <summary>
    /// Contract for Context Api
    /// </summary>
    public class Api : IApi
    {
        /// <summary>
        /// The Id
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The Path
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The Protocols
        /// </summary>
        public IEnumerable<string> Protocols { get; }

        /// <summary>
        /// The ServiceUrl
        /// </summary>
        public IUrl ServiceUrl { get; }

        // TODO: bool IsCurrentRevision
        // TODO: string Revision
        // TODO: string Version
    }

     internal class VariablesDictionary : Dictionary<string, object>, IVariablesDictionary
    {
        internal VariablesDictionary(IDictionary<string, object> dictionary)
            : base(dictionary, StringComparer.Ordinal)
        {
        }

        public T GetValueOrDefault<T>(string variableName, T defaultValue)
        {
            object value;
            if (!this.TryGetValue(variableName, out value))
            {
                return defaultValue;
            }

            return (T)value;
        }

        public T GetValueOrDefault<T>(string variableName)
        {
            object value;
            if (!this.TryGetValue(variableName, out value))
            {
                return default(T);
            }

            return (T)value;
        }
    }
}
