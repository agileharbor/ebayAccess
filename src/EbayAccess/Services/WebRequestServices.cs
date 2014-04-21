﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using EbayAccess.Models.Credentials;

namespace EbayAccess.Services
{
	public class WebRequestServices : IWebRequestServices
	{
		private readonly EbayUserCredentials _userCredentials;
		private readonly EbayDevCredentials _ebayDevCredentials;

		public WebRequestServices( EbayUserCredentials userCredentials, EbayDevCredentials ebayDevCredentials )
		{
			Condition.Requires( userCredentials, "userCredentials" ).IsNotNull();
			Condition.Requires( ebayDevCredentials, "ebayDevCredentials" ).IsNotNull();

			this._userCredentials = userCredentials;
			this._ebayDevCredentials = ebayDevCredentials;
		}

		#region BaseRequests
		public WebRequest CreateServiceGetRequest( string serviceUrl, IEnumerable< Tuple< string, string > > rawUrlParameters )
		{
			var parametrizedServiceUrl = serviceUrl;

			if( rawUrlParameters.Any() )
			{
				parametrizedServiceUrl += "?" + rawUrlParameters.Aggregate( string.Empty,
					( accum, item ) => accum + "&" + string.Format( "{0}={1}", item.Item1, item.Item2 ) );
			}

			var serviceRequest = WebRequest.Create( parametrizedServiceUrl );
			serviceRequest.Method = WebRequestMethods.Http.Get;
			return serviceRequest;
		}

		public async Task< WebRequest > CreateServicePostRequestAsync( string serviceUrl, string body, Dictionary< string, string > rawHeaders )
		{
			var encoding = new UTF8Encoding();
			var encodedBody = encoding.GetBytes( body );

			var serviceRequest = ( HttpWebRequest )WebRequest.Create( serviceUrl );
			serviceRequest.Method = WebRequestMethods.Http.Post;
			serviceRequest.ContentType = "text/xml";
			serviceRequest.ContentLength = encodedBody.Length;
			serviceRequest.KeepAlive = true;

			foreach( var rawHeadersKey in rawHeaders.Keys )
			{
				serviceRequest.Headers.Add( rawHeadersKey, rawHeaders[ rawHeadersKey ] );
			}

			using( var newStream = await serviceRequest.GetRequestStreamAsync().ConfigureAwait( false ) )
				newStream.Write( encodedBody, 0, encodedBody.Length );

			return serviceRequest;
		}
		#endregion

		#region logging
		private void LogParseReportError( MemoryStream stream )
		{
			// todo: add loging
			//this.Log().Error("Failed to parse file for account '{0}':\n\r{1}", this._credentials.AccountName, rawTeapplixExport);
		}

		private void LogUploadHttpError( string status )
		{
			// todo: add loging
			//this.Log().Error("Failed to to upload file for account '{0}'. Request status is '{1}'", this._credentials.AccountName, status);
		}
		#endregion

		#region ResponseHanding
		public Stream GetResponseStream( WebRequest webRequest )
		{
			using( var response = ( HttpWebResponse )webRequest.GetResponse() )
			using( var dataStream = response.GetResponseStream() )
			{
				var memoryStream = new MemoryStream();
				if( dataStream != null )
					dataStream.CopyTo( memoryStream, 0x100 );
				memoryStream.Position = 0;
				return memoryStream;
			}
		}

		public async Task< Stream > GetResponseStreamAsync( WebRequest webRequest )
		{
			MemoryStream memoryStream;
			using( var response = ( HttpWebResponse )await webRequest.GetResponseAsync().ConfigureAwait( false ) )
			using( var dataStream = await new TaskFactory< Stream >().StartNew( () =>
			{
				return response.GetResponseStream();
			} ).ConfigureAwait( false ) )
			{
				memoryStream = new MemoryStream();
				await dataStream.CopyToAsync( memoryStream, 0x100 ).ConfigureAwait( false );
				memoryStream.Position = 0;
				return memoryStream;
			}
		}
		#endregion
	}
}