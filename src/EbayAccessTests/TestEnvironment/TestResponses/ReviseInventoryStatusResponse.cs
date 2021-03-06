﻿namespace EbayAccessTests.TestEnvironment.TestResponses
{
	public class ReviseInventoryStatusResponse
	{
		public const string ReplaceableValueError = @"<?xml version=""1.0"" encoding=""UTF-8""?>
														<ReviseInventoryStatusResponse xmlns=""urn:ebay:apis:eBLBaseComponents"">
															<Timestamp>2014-10-17T00:07:36.924Z</Timestamp>
															<Ack>Failure</Ack>
															<Errors>
																<ShortMessage>This listing would cause you to exceed the number of items (25) and amount ($700.00) you can list. </ShortMessage>
																<LongMessage>This listing would cause you to exceed the number of items and amount you can list. You can list up to 12 more items and $401.00 more in total sales this month. Please consider reducing the quantity and starting price or request to list more: https://scgi.ebay.com/ws/eBayISAPI.dll?UpgradeLimits&amp;appId=0&amp;refId=19 .</LongMessage>
																<ErrorCode>21919188</ErrorCode>
																<SeverityCode>Error</SeverityCode>
																<ErrorParameters ParamID=""0"">
																	<Value>This listing would cause you to exceed the number of items (25) and amount ($700.00) you can list. </Value>
																</ErrorParameters>
																<ErrorParameters ParamID=""1"">
																	<Value>This listing would cause you to exceed the number of items and amount you can list. You can list up to 12 more items and $401.00 more in total sales this month. Please consider reducing the quantity and starting price or request to list more: https://scgi.ebay.com/ws/eBayISAPI.dll?UpgradeLimits&amp;appId=0&amp;refId=19 .</Value>
																</ErrorParameters>
																<ErrorParameters ParamID=""2"">
																	<Value>161414202734</Value>
																</ErrorParameters>
																<ErrorParameters ParamID=""3"">
																	<Value>xxx-xxx</Value>
																</ErrorParameters>
																<ErrorParameters ParamID=""SKU"">
																	<Value>xxx-xxx</Value>
																</ErrorParameters>
																<ErrorClassification>RequestError</ErrorClassification>
															</Errors>
															<Version>895</Version>
															<Build>E895_UNI_API5_17103579_R1</Build>
															<InventoryStatus>
																<SKU>xxx</SKU>
																<ItemID>123</ItemID>
																<StartPrice>19.98</StartPrice>
																<Quantity>22</Quantity>
															</InventoryStatus>
														</ReviseInventoryStatusResponse>";
		public const string UnsupportedListingType = @"<ReviseInventoryStatusResponse xmlns=""urn:ebay:apis:eBLBaseComponents"">
														<Timestamp>2014-09-26T15:08:06.150Z</Timestamp>
														<Ack>Failure</Ack>
														<Errors>
															<ShortMessage>Unsupported ListingType.</ShortMessage>
															<LongMessage>Valid Listing type for fixedprice apis are FixedPriceItem and StoresFixedPrice.</LongMessage>
															<ErrorCode>21916286</ErrorCode>
															<SeverityCode>Error</SeverityCode>
															<ErrorParameters ParamID=""SKU"">
																<Value>TPM4PK</Value>
															</ErrorParameters>
															<ErrorClassification>RequestError</ErrorClassification>
														</Errors>
														<Errors>
															<ShortMessage>Unsupported ListingType.</ShortMessage>
															<LongMessage>Valid Listing type for fixedprice apis are FixedPriceItem and StoresFixedPrice.</LongMessage>
															<ErrorCode>21916286</ErrorCode>
															<SeverityCode>Error</SeverityCode>
															<ErrorParameters ParamID=""SKU"">
																<Value>TTV559HP-004</Value>
															</ErrorParameters>
															<ErrorClassification>RequestError</ErrorClassification>
														</Errors>
														<Errors>
															<ShortMessage>Unsupported ListingType.</ShortMessage>
															<LongMessage>Valid Listing type for fixedprice apis are FixedPriceItem and StoresFixedPrice.</LongMessage>
															<ErrorCode>21916286</ErrorCode>
															<SeverityCode>Error</SeverityCode>
															<ErrorParameters ParamID=""SKU"">
																<Value>TR12210</Value>
															</ErrorParameters>
															<ErrorClassification>RequestError</ErrorClassification>
														</Errors>
														<Errors>
															<ShortMessage>Unsupported ListingType.</ShortMessage>
															<LongMessage>Valid Listing type for fixedprice apis are FixedPriceItem and StoresFixedPrice.</LongMessage>
															<ErrorCode>21916286</ErrorCode>
															<SeverityCode>Error</SeverityCode>
															<ErrorParameters ParamID=""SKU"">
																<Value>TTV417-004</Value>
															</ErrorParameters>
															<ErrorClassification>RequestError</ErrorClassification>
														</Errors>
														<Version>891</Version>
														<Build>E891_UNI_API5_17049963_R1</Build>
													</ReviseInventoryStatusResponse>";
		public const string Success = @"<ReviseInventoryStatusResponse xmlns=""urn:ebay:apis:eBLBaseComponents"">
														<Timestamp>2014-01-29T21:32:30.284Z</Timestamp>
														<Ack>Success</Ack>
														<Version>857</Version>
														<Build>E857_UNI_API5_16638863_R1</Build>
														<InventoryStatus>
															<SKU>
															</SKU>
															<ItemID>110136942332</ItemID>
															<StartPrice>1.0</StartPrice>
															<Quantity>101</Quantity>
														</InventoryStatus>
														<Fees>
															<ItemID>110136942332</ItemID>
															<Fee>
																<Name>AuctionLengthFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>BoldFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>BuyItNowFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>CategoryFeaturedFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>FeaturedFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>GalleryPlusFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>FeaturedGalleryFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>FixedPriceDurationFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>GalleryFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>GiftIconFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>HighLightFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>InsertionFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>InternationalInsertionFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>ListingDesignerFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>ListingFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>PhotoDisplayFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>PhotoFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>ReserveFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>SchedulingFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>SubtitleFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>BorderFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>ProPackBundleFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>BasicUpgradePackBundleFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>ValuePackBundleFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>PrivateListingFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>ProPackPlusBundleFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
															<Fee>
																<Name>MotorsGermanySearchFee</Name>
																<Fee currencyID=""USD"">0.0</Fee>
															</Fee>
														</Fees>
													</ReviseInventoryStatusResponse>";
	}
}