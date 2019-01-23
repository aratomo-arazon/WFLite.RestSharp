/*
 * ResponseVariable.cs
 *
 * Copyright (c) 2019 aratomo-arazon
 *
 * This software is released under the MIT License.
 * http://opensource.org/licenses/mit-license.php
 */

using RestSharp;
using System.Collections.Generic;
using System.Linq;
using WFLite.Bases;
using WFLite.Interfaces;

namespace WFLite.RestSharp.Variables
{
    public class ResponseVariable : Variable
    {
        private IRestResponse _response;

        public IVariable Request
        {
            private get;
            set;
        }

        public IVariable ErrorMessage
        {
            private get;
            set;
        }

        public IVariable ResponseStatus
        {
            private get;
            set;
        }

        public IDictionary<string, IVariable> Headers
        {
            private get;
            set;
        }

        public IDictionary<string, IVariable> Cookies
        {
            private get;
            set;
        }

        public IVariable Server
        {
            private get;
            set;
        }

        public IVariable ResponseUri
        {
            private get;
            set;
        }

        public IVariable ErrorException
        {
            private get;
            set;
        }

        public IVariable RawBytes
        {
            private get;
            set;
        }

        public IVariable IsSuccessful
        {
            private get;
            set;
        }

        public IVariable StatusCode
        {
            private get;
            set;
        }

        public IVariable Content
        {
            private get;
            set;
        }

        public IVariable ContentEncoding
        {
            private get;
            set;
        }

        public IVariable ContentLength
        {
            private get;
            set;
        }

        public IVariable ContentType
        {
            private get;
            set;
        }

        public IVariable StatusDescription
        {
            private get;
            set;
        }

        public IVariable ProtocolVersion
        {
            private get;
            set;
        }

        public ResponseVariable()
        {
        }

        public ResponseVariable(
            IVariable request = null,
            IVariable errorMessage = null,
            IVariable responseStatus = null,
            IDictionary<string, IVariable> headers = null,
            IDictionary<string, IVariable> cookies = null,
            IVariable server = null,
            IVariable responseUri = null,
            IVariable errorException = null,
            IVariable rawBytes = null,
            IVariable isSuccessful = null,
            IVariable statusCode = null,
            IVariable content = null,
            IVariable contentEncoding = null,
            IVariable contentLength = null,
            IVariable contentType = null,
            IVariable statusDescription = null,
            IVariable protocolVersion = null)
        {
            Request = request;
            ErrorMessage = errorMessage;
            ResponseStatus = responseStatus;
            Headers = headers;
            Cookies = cookies;
            Server = server;
            ResponseUri = responseUri;
            ErrorException = errorException;
            RawBytes = rawBytes;
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
            Content = content;
            ContentEncoding = contentEncoding;
            ContentLength = contentLength;
            ContentType = contentType;
            StatusDescription = statusDescription;
            ProtocolVersion = protocolVersion;
        }

        protected sealed override object getValue()
        {
            return _response;
        }

        protected sealed override void setValue(object value)
        {
            _response = value as IRestResponse;

            if (Request != null)
            {
                Request.SetValue(_response.Request);
            }

            if (ErrorMessage != null)
            {
                ErrorMessage.SetValue(_response.ErrorMessage);
            }

            if (ResponseStatus != null)
            {
                ResponseStatus.SetValue(_response.ResponseStatus);
            }

            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    var headerValue = _response.Headers.FirstOrDefault(h => h.Name == header.Key);
                    if (headerValue != null)
                    {
                        header.Value.SetValue(headerValue.Value);
                    }
                }
            }

            if (Cookies != null)
            {
                foreach (var cookie in Cookies)
                {
                    var cookieValue = _response.Cookies.FirstOrDefault(c => c.Name == cookie.Key);
                    if (cookieValue != null)
                    {
                        cookie.Value.SetValue(cookieValue.Value);
                    }
                }
            }

            if (Server != null)
            {
                Server.SetValue(_response.Server);
            }

            if (ResponseUri != null)
            {
                ResponseUri.SetValue(_response.ResponseUri);
            }

            if (ErrorException != null)
            {
                ErrorException.SetValue(_response.ErrorException);
            }

            if (RawBytes != null)
            {
                RawBytes.SetValue(_response.RawBytes);
            }

            if (IsSuccessful != null)
            {
                IsSuccessful.SetValue(_response.IsSuccessful);
            }

            if (StatusCode != null)
            {
                StatusCode.SetValue(_response.StatusCode);
            }

            if (Content != null)
            {
                Content.SetValue(_response.Content);
            }

            if (ContentEncoding != null)
            {
                ContentEncoding.SetValue(_response.ContentEncoding);
            }

            if (ContentLength != null)
            {
                ContentLength.SetValue(_response.ContentLength);
            }

            if (ContentType != null)
            {
                ContentType.SetValue(_response.ContentType);
            }

            if (StatusDescription != null)
            {
                StatusDescription.SetValue(_response.StatusDescription);
            }

            if (ProtocolVersion != null)
            {
                ProtocolVersion.SetValue(_response.ProtocolVersion);
            }

            setExtraValues(_response);
        }

        protected virtual void setExtraValues(IRestResponse response)
        {
        }
    }

    public class ResponseVariable<TData> : ResponseVariable
    {
        public IVariable Data
        {
            private get;
            set;
        }

        public ResponseVariable()
        {
        }

        public ResponseVariable(
            IVariable request = null,
            IVariable errorMessage = null,
            IVariable responseStatus = null,
            IDictionary<string, IVariable> headers = null,
            IDictionary<string, IVariable> cookies = null,
            IVariable server = null,
            IVariable responseUri = null,
            IVariable errorException = null,
            IVariable rawBytes = null,
            IVariable isSuccessful = null,
            IVariable statusCode = null,
            IVariable content = null,
            IVariable contentEncoding = null,
            IVariable contentLength = null,
            IVariable contentType = null,
            IVariable statusDescription = null,
            IVariable protocolVersion = null,
            IVariable data = null)
            : base(
                  request,
                  errorMessage,
                  responseStatus,
                  headers,
                  cookies,
                  server,
                  responseUri,
                  errorException,
                  rawBytes,
                  isSuccessful,
                  statusCode,
                  content,
                  contentEncoding,
                  contentLength,
                  contentType,
                  statusDescription,
                  protocolVersion)
        {
            Data = data;
        }

        protected sealed override void setExtraValues(IRestResponse response)
        {
            var responseWithData = response as IRestResponse<TData>;

            if (Data != null)
            {
                Data.SetValue(responseWithData.Data);
            }
        }
    }
}
