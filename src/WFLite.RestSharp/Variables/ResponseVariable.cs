/*
 * ResponseVariable.cs
 *
 * Copyright (c) 2019 aratomo-arazon
 *
 * This software is released under the MIT License.
 * http://opensource.org/licenses/mit-license.php
 */

using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WFLite.Bases;
using WFLite.Interfaces;

namespace WFLite.RestSharp.Variables
{
    public class ResponseVariable : InVariable<IRestResponse>
    {
        public IInVariable<IRestRequest> Request
        {
            private get;
            set;
        }

        public IInVariable<string> ErrorMessage
        {
            private get;
            set;
        }

        public IInVariable<ResponseStatus> ResponseStatus
        {
            private get;
            set;
        }

        public IDictionary<string, IInVariable> Headers
        {
            private get;
            set;
        }

        public IDictionary<string, IInVariable<string>> Cookies
        {
            private get;
            set;
        }

        public IInVariable<string> Server
        {
            private get;
            set;
        }

        public IInVariable<Uri> ResponseUri
        {
            private get;
            set;
        }

        public IInVariable<Exception> ErrorException
        {
            private get;
            set;
        }

        public IInVariable<byte[]> RawBytes
        {
            private get;
            set;
        }

        public IInVariable<bool> IsSuccessful
        {
            private get;
            set;
        }

        public IInVariable<HttpStatusCode> StatusCode
        {
            private get;
            set;
        }

        public IInVariable<string> Content
        {
            private get;
            set;
        }

        public IInVariable<string> ContentEncoding
        {
            private get;
            set;
        }

        public IInVariable<long> ContentLength
        {
            private get;
            set;
        }

        public IInVariable<string> ContentType
        {
            private get;
            set;
        }

        public IInVariable<string> StatusDescription
        {
            private get;
            set;
        }

        public IInVariable<Version> ProtocolVersion
        {
            private get;
            set;
        }

        public ResponseVariable()
        {
        }

        public ResponseVariable(
            IInVariable<IRestRequest> request = null,
            IInVariable<string> errorMessage = null,
            IInVariable<ResponseStatus> responseStatus = null,
            IDictionary<string, IInVariable> headers = null,
            IDictionary<string, IInVariable<string>> cookies = null,
            IInVariable<string> server = null,
            IInVariable<Uri> responseUri = null,
            IInVariable<Exception> errorException = null,
            IInVariable<byte[]> rawBytes = null,
            IInVariable<bool> isSuccessful = null,
            IInVariable<HttpStatusCode> statusCode = null,
            IInVariable<string> content = null,
            IInVariable<string> contentEncoding = null,
            IInVariable<long> contentLength = null,
            IInVariable<string> contentType = null,
            IInVariable<string> statusDescription = null,
            IInVariable<Version> protocolVersion = null)
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

        protected sealed override void setValue(object value)
        {
            var response = value as IRestResponse;

            if (Request != null)
            {
                Request.SetValue(response.Request);
            }

            if (ErrorMessage != null)
            {
                ErrorMessage.SetValue(response.ErrorMessage);
            }

            if (ResponseStatus != null)
            {
                ResponseStatus.SetValue(response.ResponseStatus);
            }

            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    var headerValue = response.Headers.FirstOrDefault(h => h.Name == header.Key);
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
                    var cookieValue = response.Cookies.FirstOrDefault(c => c.Name == cookie.Key);
                    if (cookieValue != null)
                    {
                        cookie.Value.SetValue(cookieValue.Value);
                    }
                }
            }

            if (Server != null)
            {
                Server.SetValue(response.Server);
            }

            if (ResponseUri != null)
            {
                ResponseUri.SetValue(response.ResponseUri);
            }

            if (ErrorException != null)
            {
                ErrorException.SetValue(response.ErrorException);
            }

            if (RawBytes != null)
            {
                RawBytes.SetValue(response.RawBytes);
            }

            if (IsSuccessful != null)
            {
                IsSuccessful.SetValue(response.IsSuccessful);
            }

            if (StatusCode != null)
            {
                StatusCode.SetValue(response.StatusCode);
            }

            if (Content != null)
            {
                Content.SetValue(response.Content);
            }

            if (ContentEncoding != null)
            {
                ContentEncoding.SetValue(response.ContentEncoding);
            }

            if (ContentLength != null)
            {
                ContentLength.SetValue(response.ContentLength);
            }

            if (ContentType != null)
            {
                ContentType.SetValue(response.ContentType);
            }

            if (StatusDescription != null)
            {
                StatusDescription.SetValue(response.StatusDescription);
            }

            if (ProtocolVersion != null)
            {
                ProtocolVersion.SetValue(response.ProtocolVersion);
            }

            setExtraValues(response);
        }

        protected virtual void setExtraValues(IRestResponse response)
        {
        }
    }

    public class ResponseVariable<TData> : ResponseVariable
    {
        public IInVariable<TData> Data
        {
            private get;
            set;
        }

        public ResponseVariable()
        {
        }

        public ResponseVariable(
            IInVariable<IRestRequest> request = null,
            IInVariable<string> errorMessage = null,
            IInVariable<ResponseStatus> responseStatus = null,
            IDictionary<string, IInVariable> headers = null,
            IDictionary<string, IInVariable<string>> cookies = null,
            IInVariable<string> server = null,
            IInVariable<Uri> responseUri = null,
            IInVariable<Exception> errorException = null,
            IInVariable<byte[]> rawBytes = null,
            IInVariable<bool> isSuccessful = null,
            IInVariable<HttpStatusCode> statusCode = null,
            IInVariable<string> content = null,
            IInVariable<string> contentEncoding = null,
            IInVariable<long> contentLength = null,
            IInVariable<string> contentType = null,
            IInVariable<string> statusDescription = null,
            IInVariable<Version> protocolVersion = null,
            IInVariable<TData> data = null)
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
