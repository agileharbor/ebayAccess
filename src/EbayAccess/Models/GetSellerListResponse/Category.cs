﻿using System;

namespace EbayAccess.Models.GetSellerListResponse
{
	[ Serializable ]
	public class Category
	{
		public long CategoryId { get; set; }

		public string CategoryName { get; set; }
	}
}