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
    public class RequestVariable : OutVariable<IRestRequest>
    {
        public IOutVariable Resource
        {
            private get;
            set;
        }

        public IOutVariable<Method> Method
        {
            private get;
            set;
        }

        public IOutVariable<DataFormat> DataFormat
        {
            private get;
            set;
        }
        public IDictionary<string, IOutVariable> Headers
        {
            private get;
            set;
        }

        public IDictionary<string, IOutVariable> QueryParameters
        {
            private get;
            set;
        }

        public IDictionary<string, IOutVariable> Cookies
        {
            private get;
            set;
        }

        public IOutVariable Body
        {
            private get;
            set;
        }

        public IDictionary<string, IOutVariable> UrlSegments
        {
            private get;
            set;
        }

        public RequestVariable()
        {
        }

        public RequestVariable(
            IOutVariable resource = null,
            IOutVariable<Method> method = null,
            IOutVariable<DataFormat> dataFormat = null,
            IDictionary<string, IOutVariable> headers = null,
            IDictionary<string, IOutVariable> queryParameters = null,
            IDictionary<string, IOutVariable> cookies = null,
            IOutVariable body = null,
            IConverter<IRestRequest> converter = null)
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
            IRestRequest request = null;

            if (Resource != null)
            {
                var resource = Resource.GetValueAsObject();
                if (resource is Uri)
                {
                    if (Method != null && DataFormat != null)
                    {
                        request = new RestRequest(resource as Uri, Method.GetValue(), DataFormat.GetValue());
                    }
                    else if (Method != null)
                    {
                        request = new RestRequest(resource as Uri, Method.GetValue());
                    }
                    else
                    {
                        request = new RestRequest(resource as Uri);
                    }
                }
                else if (resource is string)
                {
                    if (Method != null && DataFormat != null)
                    {
                        request = new RestRequest(resource as string, Method.GetValue(), DataFormat.GetValue());
                    }
                    else if (Method != null)
                    {
                        request = new RestRequest(resource as string, Method.GetValue());
                    }
                    else
                    {
                        request = new RestRequest(resource as string);
                    }
                }
                else
                {
                    request = new RestRequest();
                }
            }
            else
            {
                request = new RestRequest();
            }

            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    request.AddParameter(header.Key, header.Value.GetValueAsObject(), ParameterType.HttpHeader);
                }
            }

            if (QueryParameters != null)
            {
                foreach (var queryParameter in QueryParameters)
                {
                    request.AddParameter(queryParameter.Key, queryParameter.Value.GetValueAsObject(), ParameterType.QueryString);
                }
            }
            
            if (Cookies != null)
            {
                foreach (var cookie in Cookies)
                {
                    request.AddParameter(cookie.Key, cookie.Value.GetValueAsObject(), ParameterType.Cookie);
                }
            }

            if (Body != null)
            {
                request.AddParameter("application/json", Body.GetValueAsObject(), ParameterType.RequestBody);
            }

            if (UrlSegments != null)
            {
                foreach (var urlSegment in UrlSegments)
                {
                    request.AddParameter(urlSegment.Key, urlSegment.Value.GetValueAsObject(), ParameterType.UrlSegment);
                }
            }

            return request;
        }
    }
}
