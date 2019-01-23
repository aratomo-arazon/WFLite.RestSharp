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
using WFLite.Interfaces;

namespace WFLite.RestSharp.Activities
{
    public class RestActivity : SyncActivity
    {
        public IVariable BaseUrl
        {
            private get;
            set;
        }

        public IVariable Request
        {
            protected get;
            set;
        }

        public IVariable Response
        {
            protected get;
            set;
        }

        public RestActivity()
        {
        }

        public RestActivity(IVariable request, IVariable response, IVariable baseUrl = null)
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
                var baseUrl = BaseUrl.GetValue();
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

            var request = Request.GetValue<RestRequest>();

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
        protected override object execute(IRestClient client, IRestRequest request)
        {
            return client.Execute<TData>(request);
        }
    }
}
