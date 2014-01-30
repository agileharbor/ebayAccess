﻿namespace EbayAccess.Models.GetSellerListResponse
{
	public class Item
	{
		public bool AutoPay { get; set; }

		public decimal BuyItNowPrice { get; set; }

		public string Country { get; set; }

		public string Currency { get; set; }

		public long ItemId { get; set; }

		public ListingDetails ListingDetails { get; set; }

		public ListingType ListingType { get; set; }

		public Category PrimaryCategory { get; set; }

		public long Quantity { get; set; }

		public decimal ReservePrice { get; set; }

		public string Site { get; set; }

		public string Title { get; set; }

		public bool HideFromSearch { get; set; }
	}
}