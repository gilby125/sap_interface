#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.IO;
using System.Xml.Serialization;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Provides a defition of database connection by provider and name.
	/// </summary>
	[Serializable]
	public class DbConnectionDefinition
	{
		/// <summary>
		/// 	A default connection, an MSSQL db on localhost connecting to the "master" database using integrated authentication.
		/// </summary>
		[XmlIgnore] public static readonly DbConnectionDefinition Default;

		/// <summary>
		/// 	Initializes static members of the <see cref = "DbConnectionDefinition" /> class.
		/// </summary>
		static DbConnectionDefinition()
		{
			Default = new DbConnectionDefinition
			          	{
			          		Name = "Default - MSSQL Master@localhost",
			          		ProviderName = "System.Data.SqlClient",
			          		ConnectionString = @"Server=.; Database=Master; Integrated Security=SSPI"
			          	};
		}

		/// <summary>
		/// 	Gets or sets a comment in relation to this connection, e.g. "development..."
		/// </summary>
		/// <value>A comment.</value>
		[XmlElement(IsNullable = true)]
		public string Comment { get; set; }

		/// <summary>
		/// 	Gets or sets the connection string.
		/// </summary>
		/// <value>The connection string.</value>
		[XmlElement(IsNullable = false)]
		public string ConnectionString { get; set; }

		/// <summary>
		/// 	Gets or sets the name.
		/// </summary>
		/// <value>The name of the definition.</value>
		[XmlElement(IsNullable = false)]
		public string Name { get; set; }

		/// <summary>
		/// 	Gets or sets the name of the provider.
		/// </summary>
		/// <value>The name of the provider.</value>
		[XmlElement(IsNullable = false)]
		public string ProviderName { get; set; }

		/// <summary>
		/// 	Converts the definition from an XML string.
		/// </summary>
		/// <param name = "xml">The XML to hydrate from.</param>
		/// <returns>A <see cref="DbConnectionDefinition"/> instance.</returns>
		public static DbConnectionDefinition FromXml(string xml)
		{
			using (var sr = new StringReader(xml))
			{
				var serializer = new XmlSerializer(typeof(DbConnectionDefinition));
				return (DbConnectionDefinition)serializer.Deserialize(sr);
			}
		}

		/// <summary>
		/// 	Returns a <see cref = "T:System.String" /> that represents the current <see cref = "T:System.Object" />.
		/// </summary>
		/// <returns>A <see cref = "T:System.String" /> that represents the current <see cref = "T:System.Object" />.</returns>
		public override string ToString()
		{
			return Name ?? GetType().FullName;
		}

		/// <summary>
		/// 	Serialize the object to XML.
		/// </summary>
		/// <returns>An XML string.</returns>
		public string ToXml()
		{
			return Utility.ToXml(this);
		}
	}
}