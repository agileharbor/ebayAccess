﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EbayAccess.Misc;
using EbayAccess.Models.GetItemResponse;
using EbayAccess.Models.GetSellerListResponse;

namespace EbayAccess.Services.Parsers
{
	public class EbayGetItemResponseParser : EbayXmlParser< GetItemResponse >
	{
		public override GetItemResponse Parse( Stream stream, bool keepStreamPosition = true )
		{
			try
			{
				XNamespace ns = "urn:ebay:apis:eBLBaseComponents";

				var streamStartPos = stream.Position;

				var root = XElement.Load( stream );

				var erros = this.ResponseContainsErrors( root, ns );
				if( erros != null )
					return new GetItemResponse { Errors = erros };

				var x = root.Descendants( ns + "Item" ).First();
				string temp;
				var res = new Item();

				res.ItemId = GetElementValue(x, ns, "ItemID");

				res.AutoPay = GetElementValue( x, ns, "AutoPay" ).ToBool();

				if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "BuyItNowPrice" ) ) )
				{
					res.BuyItNowPrice = temp.ToDecimalDotOrComaSeparated();
					res.BuyItNowPriceCurrencyId = this.GetElementAttribute( "currencyID", x, ns, "BuyItNowPrice" );
				}

				res.Country = GetElementValue( x, ns, "Country" );

				res.Currency = GetElementValue( x, ns, "Currency" );

				res.HideFromSearch = GetElementValue( x, ns, "HideFromSearch" ).ToBool();

				if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingType" ) ) )
					res.ListingType = ( ListingType )Enum.Parse( typeof( ListingType ), temp );

				if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "Quantity" ) ) )
					res.Quantity = long.Parse( temp );

				if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ReservePrice" ) ) )
				{
					res.ReservePrice = temp.ToDecimalDotOrComaSeparated();
					res.ReservePriceCurrencyId = this.GetElementAttribute( "currencyID", x, ns, "ReservePrice" );
				}

				res.Site = GetElementValue( x, ns, "Site" );

				res.Title = GetElementValue( x, ns, "Title" );

				res.Sku = GetElementValue( x, ns, "SKU" );

				var sellingStatus = x.Element( ns + "SellingStatus" );
				if( sellingStatus != null )
				{
					res.SellingStatus = new SellingStatus();
					res.SellingStatus.CurrentPrice = GetElementValue( x, ns, "SellingStatus", "CurrentPrice" ).ToDecimalDotOrComaSeparated();
					res.SellingStatus.CurrentPriceCurrencyId = this.GetElementAttribute( "currencyID", x, ns, "SellingStatus", "CurrentPrice" );
				}

				var listingDetails = x.Element( ns + "ListingDetails" );
				if( listingDetails != null )
				{
					res.ListingDetails = new ListingDetails();

					res.ListingDetails.Adult = GetElementValue( x, ns, "ListingDetails", "Adult" ).ToBool();

					res.ListingDetails.BindingAuction = GetElementValue( x, ns, "ListingDetails", "BindingAuction" ).ToBool();

					res.ListingDetails.CheckoutEnabled = GetElementValue( x, ns, "ListingDetails", "CheckoutEnabled" ).ToBool();

					res.ListingDetails.ConvertedBuyItNowPrice = GetElementValue( x, ns, "ListingDetails", "ConvertedBuyItNowPrice" ).ToDecimalDotOrComaSeparated();

					res.ListingDetails.ConvertedReservePrice = GetElementValue( x, ns, "ListingDetails", "ConvertedReservePrice" ).ToDecimalDotOrComaSeparated();

					res.ListingDetails.ConvertedStartPrice = GetElementValue( x, ns, "ListingDetails", "ConvertedStartPrice" ).ToDecimalDotOrComaSeparated();

					if( !string.IsNullOrWhiteSpace( temp = GetElementValue( x, ns, "ListingDetails", "EndTime" ) ) )
						res.ListingDetails.EndTime = ( DateTime.Parse( temp ) );

					res.ListingDetails.HasPublicMessages = GetElementValue( x, ns, "ListingDetails", "HasPublicMessages" ).ToBool();

					res.ListingDetails.HasReservePrice = GetElementValue( x, ns, "ListingDetails", "HasReservePrice" ).ToBool();

					res.ListingDetails.HasUnansweredQuestions = GetElementValue( x, ns, "ListingDetails", "HasUnansweredQuestions" ).ToBool();

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

				var variations = x.Element( ns + "Variations" );
				if( variations != null )
				{
					res.Variations = new List< Variation >();

					var variationsElem = variations.Descendants( ns + "Variation" );

					var variationsObj = variationsElem.Select( variat =>
					{
						var tempVariation = new Variation();

						tempVariation.Sku = GetElementValue( variat, ns, "SKU" );

						return tempVariation;
					} );

					res.Variations.AddRange( variationsObj );
				}

				if( keepStreamPosition )
					stream.Position = streamStartPos;

				return new GetItemResponse { Item = res };
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