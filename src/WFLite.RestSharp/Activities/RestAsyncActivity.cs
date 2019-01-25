/*
 * RestAsyncActivity.cs
 *
 * Copyright (c) 2019 aratomo-arazon
 *
 * This software is released under the MIT License.
 * http://opensource.org/licenses/mit-license.php
 */
 
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;
using WFLite.Activities;
using WFLite.Interfaces;

namespace WFLite.RestSharp.Activities.Activities
{
    public class RestAsyncActivity : AsyncActivity
    {
        public IVariable BaseUrl
        {
            private get;
            set;
        }

        public IVariable Request
        {
            private get;
            set;
        }

        public IVariable Response
        {
            private get;
            set;
        }

        public RestAsyncActivity()
        {
        }

        public RestAsyncActivity(IVariable baseUrl, IVariable request, IVariable response)
        {
            BaseUrl = baseUrl;
            Request = request;
            Response = response;
        }

        protected sealed override async Task<bool> run(CancellationToken cancellationToken)
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

            var response = await executeTaskAsync(client, request);

            Response.SetValue(response);

            return true;
        }

        protected virtual async Task<object> executeTaskAsync(IRestClient client, IRestRequest request)
        {
            return await client.ExecuteTaskAsync(request);
        }
    }

    public class RestAsyncActivity<TData> : RestAsyncActivity
    {
        public RestAsyncActivity(IVariable baseUrl = null, IVariable request = null, IVariable response = null)
            : base(baseUrl, request, response)
        {
        }

        protected override async Task<object> executeTaskAsync(IRestClient client, IRestRequest request)
        {
            return await client.ExecuteTaskAsync<TData>(request);
        }
    }
}