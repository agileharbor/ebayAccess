﻿using System.Collections.Generic;
using System.Linq;
using EbayAccess.Models.Credentials;
using LINQtoCSV;

namespace EbayAccessTests.Integration
{
	public class TestCredentials
	{
		private readonly FlatCsvLine _flatCsvLine;
		private readonly FlatDevCredentialCsvLine _flatDevCredentialCsvLine;
		private readonly List< FlatSaleItemsCsvLine > _flatSaleItemsCsvLines;

		public TestCredentials( string credentialsFilePath, string devCredentialsFilePath, string saleItemsIdsFilePath )
		{
			var cc = new CsvContext();
			this._flatCsvLine = Enumerable.FirstOrDefault( cc.Read< FlatCsvLine >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ) );
			this._flatDevCredentialCsvLine = Enumerable.FirstOrDefault( cc.Read< FlatDevCredentialCsvLine >( devCredentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ) );
			this._flatSaleItemsCsvLines = cc.Read< FlatSaleItemsCsvLine >( saleItemsIdsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).ToList();
		}

		public EbayUserCredentials GetEbayUserCredentials()
		{
			return new EbayUserCredentials( this._flatCsvLine.AccountName, this._flatCsvLine.Token );
		}

		public EbayDevCredentials GetEbayDevCredentials()
		{
			return new EbayDevCredentials( this._flatDevCredentialCsvLine.AppName, this._flatDevCredentialCsvLine.DevName, this._flatDevCredentialCsvLine.CertName );
		}

		public IEnumerable< long > GetSaleItemsIds()
		{
			return this._flatSaleItemsCsvLines.Select( x => long.Parse( x.Id ) ).ToList();
		}

		public string GetEbayEndPoint()
		{
			return "https://api.sandbox.ebay.com/ws/api.dll";
		}

		internal class FlatCsvLine
		{
			public FlatCsvLine()
			{
			}

			[ CsvColumn( Name = "AccountName", FieldIndex = 1 ) ]
			public string AccountName { get; set; }

			[ CsvColumn( Name = "Token", FieldIndex = 2 ) ]
			public string Token { get; set; }
		}

		internal class FlatDevCredentialCsvLine
		{
			public FlatDevCredentialCsvLine()
			{
			}

			[ CsvColumn( Name = "AppName", FieldIndex = 1 ) ]
			public string AppName { get; set; }

			[ CsvColumn( Name = "DevName", FieldIndex = 2 ) ]
			public string DevName { get; set; }

			[ CsvColumn( Name = "CertName", FieldIndex = 3 ) ]
			public string CertName { get; set; }
		}

		internal class FlatSaleItemsCsvLine
		{
			public FlatSaleItemsCsvLine()
			{
			}

			[ CsvColumn( Name = "Id", FieldIndex = 1 ) ]
			public string Id { get; set; }
		}
	}
}