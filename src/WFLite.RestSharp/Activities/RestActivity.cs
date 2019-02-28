/*
 * RestActivity.cs
 *
 * Copyright (c) 2019 aratomo-arazon
 *
 * This software is released under the MIT License.
 * http://opensource.org/licenses/mit-license.php
 */

using RestSharp;
using System;
using WFLite.Activities;
using WFLite.Bases;
using WFLite.Interfaces;

namespace WFLite.RestSharp.Activities
{
    public class RestActivity : SyncActivity
    {
        public IOutVariable BaseUrl
        {
            private get;
            set;
        }

        public IOutVariable<IRestRequest> Request
        {
            protected get;
            set;
        }

        public IInVariable<IRestResponse> Response
        {
            protected get;
            set;
        }

        public RestActivity()
        {
        }

        public RestActivity(IOutVariable baseUrl, IOutVariable<IRestRequest> request, IInVariable<IRestResponse> response)
        {
            BaseUrl = baseUrl;
            Request = request;
            Response = response;
        }

        protected override bool run()
        {
            var client = default(RestClient);

            if (BaseUrl != null)
            {
                var baseUrl = BaseUrl.GetValueAsObject();
                if (baseUrl is Uri)
                {
                    client = new RestClient(baseUrl as Uri);
                }
                else if (baseUrl is string)
                {
                    client = new RestClient(baseUrl as string);
                }
                else
                {
                    client = new RestClient();
                }
            }
            else
            {
                client = new RestClient();
            }

            var request = Request.GetValue();

            var response = execute(client, request);

            Response.SetValue(response);

            return true;
        }

        protected virtual object execute(IRestClient client, IRestRequest request)
        {
            return client.Execute(request);
        }
    }

    public class RestActivity<TData> : RestActivity
        where TData : new()
    {
        public RestActivity()
        {
        }

        public RestActivity(IOutVariable baseUrl, IOutVariable<IRestRequest> request, IInVariable<IRestResponse> response)
            : base(baseUrl, request, response)
        {
        }

        protected override object execute(IRestClient client, IRestRequest request)
        {
            return client.Execute<TData>(request);
        }
    }
}
