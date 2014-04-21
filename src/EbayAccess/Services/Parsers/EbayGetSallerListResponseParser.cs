﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EbayAccess.Infrastructure;
using EbayAccess.Models.GetSellerListResponse;

namespace EbayAccess.Services.Parsers
{
	public class EbayGetSallerListResponseParser : EbayXmlParser< List< Item > >
	{
		public override List< Item > Parse( Stream stream, bool keepStremPosition = true )
		{
			try
			{
				XNamespace ns = "urn:ebay:apis:eBLBaseComponents";

				var streamStartPos = stream.Position;

				var root = XElement.Load( stream );

				var xmlItems = root.Descendants( ns + "Item" );

				var orders = xmlItems.Select( x =>
				{
					string temp;
					var res = new Item();

					if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "AutoPay" ) ) )
						res.AutoPay = bool.Parse( temp );

					if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "BuyItNowPrice" ) ) )
						res.BuyItNowPrice = decimal.Parse( temp.Replace( '.', ',' ) );

					res.Country = GetElementValue( x, ns, "Country" );

					res.Currency = GetElementValue( x, ns, "Currency" );

					if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "HideFromSearch" ) ) )
						res.HideFromSearch = bool.Parse( temp );

					if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ItemID" ) ) )
						res.ItemId = long.Parse( temp );

					if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingType" ) ) )
						res.ListingType = ( ListingType )Enum.Parse( typeof( ListingType ), temp );

					if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "Quantity" ) ) )
						res.Quantity = long.Parse( temp );

					if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ReservePrice" ) ) )
						res.ReservePrice = temp.ToDecimalDotOrComaSeparated();

					res.Site = GetElementValue( x, ns, "Site" );

					res.Title = GetElementValue( x, ns, "Title" );

					var listingDetails = x.Element( ns + "ListingDetails" );
					if( listingDetails != null )
					{
						res.ListingDetails = new ListingDetails();

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "Adult" ) ) )
							res.ListingDetails.Adult = bool.Parse( temp );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "BindingAuction" ) ) )
							res.ListingDetails.BindingAuction = bool.Parse( temp );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "CheckoutEnabled" ) ) )
							res.ListingDetails.CheckoutEnabled = bool.Parse( temp );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "ConvertedBuyItNowPrice" ) ) )
							res.ListingDetails.ConvertedBuyItNowPrice = decimal.Parse( temp.Replace( '.', ',' ) );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "ConvertedReservePrice" ) ) )
							res.ListingDetails.ConvertedReservePrice = decimal.Parse( temp.Replace( '.', ',' ) );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "ConvertedStartPrice" ) ) )
							res.ListingDetails.ConvertedStartPrice = decimal.Parse( temp.Replace( '.', ',' ) );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "EndTime" ) ) )
							res.ListingDetails.EndTime = ( DateTime.Parse( temp ) );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "HasPublicMessages" ) ) )
							res.ListingDetails.HasPublicMessages = bool.Parse( temp );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "HasReservePrice" ) ) )
							res.ListingDetails.HasReservePrice = bool.Parse( temp );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "HasUnansweredQuestions" ) ) )
							res.ListingDetails.HasUnansweredQuestions = bool.Parse( temp );

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "StartTime" ) ) )
							res.ListingDetails.StartTime = ( DateTime.Parse( temp ) );

						res.ListingDetails.ViewItemUrl = GetElementValue( x, ns, "ListingDetails", "ViewItemURL" );

						res.ListingDetails.ViewItemUrlForNaturalSearch = GetElementValue( x, ns, "ListingDetails", "ViewItemURLForNaturalSearch" );
					}

					var primaryCategory = x.Element( ns + "PrimaryCategory" );
					if( primaryCategory != null )
					{
						res.PrimaryCategory = new Category();

						if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "PrimaryCategory", "CategoryID" ) ) )
							res.PrimaryCategory.CategoryId = long.Parse( temp );

						res.PrimaryCategory.CategoryName = GetElementValue( x, ns, "PrimaryCategory", "CategoryName" );
					}

					if( keepStremPosition )
						stream.Position = streamStartPos;

					return res;
				} ).ToList();

				return orders;
			}
			catch( Exception ex )
			{
				var buffer = new byte[ stream.Length ];
				stream.Read( buffer, 0, ( int )stream.Length );
				var utf8Encoding = new UTF8Encoding();
				var bufferStr = utf8Encoding.GetString( buffer );
				throw new Exception( "Can't parse: " + bufferStr, ex );
			}
		}
	}
}