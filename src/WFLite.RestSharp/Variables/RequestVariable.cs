/*
 * RequestVariable.cs
 *
 * Copyright (c) 2019 aratomo-arazon
 *
 * This software is released under the MIT License.
 * http://opensource.org/licenses/mit-license.php
 */

using RestSharp;
using System;
using System.Collections.Generic;
using WFLite.Bases;
using WFLite.Interfaces;

namespace WFLite.RestSharp.Variables
{
    public class RequestVariable : Variable
    {
        private IRestRequest _request;

        public IVariable Resource
        {
            private get;
            set;
        }

        public IVariable Method
        {
            private get;
            set;
        }

        public IVariable DataFormat
        {
            private get;
            set;
        }
        public IDictionary<string, IVariable> Headers
        {
            private get;
            set;
        }

        public IDictionary<string, IVariable> QueryParameters
        {
            private get;
            set;
        }

        public IDictionary<string, IVariable> Cookies
        {
            private get;
            set;
        }

        public IVariable Body
        {
            private get;
            set;
        }

        public IDictionary<string, IVariable> UrlSegments
        {
            private get;
            set;
        }

        public RequestVariable()
        {
        }

        public RequestVariable(
            IVariable resource = null,
            IVariable method = null,
            IVariable dataFormat = null,
            IDictionary<string, IVariable> headers = null,
            IDictionary<string, IVariable> queryParameters = null,
            IDictionary<string, IVariable> cookies = null,
            IVariable body = null,
            IConverter converter = null)
            : base(converter)
        {
            Resource = resource;
            Method = method;
            DataFormat = dataFormat;
            Headers = headers;
            QueryParameters = queryParameters;
            Cookies = cookies;
            Body = body;
        }

        protected sealed override object getValue()
        {
            if (Resource != null)
            {
                var resource = Resource.GetValue();
                if (resource is Uri)
                {
                    if (Method != null && DataFormat != null)
                    {
                        _request = new RestRequest(resource as Uri, Method.GetValue<Method>(), DataFormat.GetValue<DataFormat>());
                    }
                    else if (Method != null)
                    {
                        _request = new RestRequest(resource as Uri, Method.GetValue<Method>());
                    }
                    else
                    {
                        _request = new RestRequest(resource as Uri);
                    }
                }
                else if (resource is string)
                {
                    if (Method != null && DataFormat != null)
                    {
                        _request = new RestRequest(resource as string, Method.GetValue<Method>(), DataFormat.GetValue<DataFormat>());
                    }
                    else if (Method != null)
                    {
                        _request = new RestRequest(resource as string, Method.GetValue<Method>());
                    }
                    else
                    {
                        _request = new RestRequest(resource as string);
                    }
                }
                else
                {
                    _request = new RestRequest();
                }
            }
            else
            {
                _request = new RestRequest();
            }

            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    _request.AddParameter(header.Key, header.Value.GetValue(), ParameterType.HttpHeader);
                }
            }

            if (QueryParameters != null)
            {
                foreach (var queryParameter in QueryParameters)
                {
                    _request.AddParameter(queryParameter.Key, queryParameter.Value.GetValue(), ParameterType.QueryString);
                }
            }
            
            if (Cookies != null)
            {
                foreach (var cookie in Cookies)
                {
                    _request.AddParameter(cookie.Key, cookie.Value.GetValue(), ParameterType.Cookie);
                }
            }

            if (Body != null)
            {
                _request.AddParameter("application/json", Body.GetValue(), ParameterType.RequestBody);
            }

            if (UrlSegments != null)
            {
                foreach (var urlSegment in UrlSegments)
                {
                    _request.AddParameter(urlSegment.Key, urlSegment.Value.GetValue(), ParameterType.UrlSegment);
                }
            }

            return _request;
        }

        protected sealed override void setValue(object value)
        {
            _request = value as IRestRequest;
        }
    }
}
