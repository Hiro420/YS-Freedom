using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp.Headers;

namespace uhttpsharp
{
    public class HttpResponseError : IHttpResponse
    {
        private Stream ContentStream { get; set; }

        private readonly Stream _headerStream = new MemoryStream();
        private readonly bool _closeConnection;
        private readonly IHttpHeaders _headers;
        private readonly HttpResponseCode _responseCode;

        private string template = "<html>" +
                                            "<head><title>{0} {1}</title></head>" +
                                            "<body bgcolor=\"white\">" +
                                                "<center><h1> {0} {1}</h1></center>" +
                                                "<hr>" +
                                                "<center>uHttp Server</center>" +
                                            "</body>" +
                                        "</html>";

        public HttpResponseError(IHttpRequest request, HttpResponseCode responseCode)
        {
            ContentStream = StringToStream(String.Format(template, (int)responseCode, responseCode.ToString()));

            _closeConnection = request.Headers.KeepAliveConnection() ? false : true;

            _responseCode = responseCode;
            _headers = new ListHttpHeaders(new[]
{
                new KeyValuePair<string, string>("Date", DateTime.UtcNow.ToString("R")),
                new KeyValuePair<string, string>("Connection", _closeConnection ? "Close" : "Keep-Alive"),
                new KeyValuePair<string, string>("Content-Type", "text/html"),
                new KeyValuePair<string, string>("Content-Length", ContentStream.Length.ToString(CultureInfo.InvariantCulture)),
            });
        }

        private static MemoryStream StringToStream(string content)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(content);
            writer.Flush();

            return stream;
        }

        public async Task WriteBody(StreamWriter writer)
        {
            ContentStream.Position = 0;
            await ContentStream.CopyToAsync(writer.BaseStream).ConfigureAwait(false);
        }
        public HttpResponseCode ResponseCode
        {
            get { return _responseCode; }
        }
        public IHttpHeaders Headers
        {
            get { return _headers; }
        }

        public bool CloseConnection
        {
            get { return _closeConnection; }
        }

        public async Task WriteHeaders(StreamWriter writer)
        {
            _headerStream.Position = 0;
            await _headerStream.CopyToAsync(writer.BaseStream).ConfigureAwait(false);
        }
    }
}
