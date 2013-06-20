#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>The sql client schema service.</summary>
	public class SqlClientSchemaService : GenericSchemaService
	{
		/// <summary>The get db types.</summary>
		/// <param name="connection">The connection.</param>
		/// <returns></returns>
		public override Dictionary<string, DbModelType> GetDbTypes(DbConnection connection)
		{
			var types = base.GetDbTypes(connection);

			var date = types["datetime"];
			date.LiteralPrefix = "'";
			date.LiteralSuffix = "'";

			return types;
		}

		/// <summary>The get foreign key references for table.</summary>
		/// <param name="dbConn">The db conn.</param>
		/// <param name="dbTable">The db table.</param>
		protected override void GetForeignKeyReferencesForTable(DbConnection dbConn, DbModelTable dbTable)
		{
			ForeignKeyInformationAvailable = true;
			try
			{
				using (var cmd = dbConn.CreateCommand())
				{
					cmd.CommandText = string.Format(
						@"SELECT 
	OBJECT_SCHEMA_NAME(f.parent_object_id) AS TableSchemaName,
	OBJECT_NAME(f.parent_object_id) AS TableName,
	COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
	f.name AS ForeignKeyName,
	OBJECT_SCHEMA_NAME(f.referenced_object_id) AS ReferenceTableSchemaName,
	OBJECT_NAME(f.referenced_object_id) AS ReferenceTableName,
	COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferenceColumnName,
	f.update_referential_action_desc,
	f.delete_referential_action_desc
FROM 
	sys.foreign_keys AS f INNER JOIN sys.foreign_key_columns AS fc
		ON f.OBJECT_ID = fc.constraint_object_id
WHERE OBJECT_SCHEMA_NAME(f.parent_object_id) = '{0}' AND OBJECT_NAME(f.parent_object_id) = '{1}'
", 
						dbTable.Schema, dbTable.Name);
					cmd.CommandType = CommandType.Text;
					using (var dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							dbTable.Constraints.Add(new DbModelConstraint
							                        	{
							                        		ConstraintTableSchema = (string)dr["TableSchemaName"], 
							                        		ConstraintTableName = (string)dr["TableName"], 
							                        		ColumnName = (string)dr["ColumnName"], 
							                        		ConstraintName = (string)dr["ForeignKeyName"], 
							                        		UniqueConstraintTableSchema = (string)dr["ReferenceTableSchemaName"], 
							                        		UniqueConstraintTableName = (string)dr["ReferenceTableName"], 
							                        		UniqueColumnName = (string)dr["ReferenceColumnName"], 
							                        		UpdateRule = (string)dr["update_referential_action_desc"], 
							                        		DeleteRule = (string)dr["delete_referential_action_desc"], 
							                        	});
						}
					}
				}
			}
			catch (SqlException)
			{
				ForeignKeyInformationAvailable = false;
			}
		}

		/// <summary>The process foreign key references for table.</summary>
		/// <param name="dbConn">The db conn.</param>
		/// <param name="dbTable">The db table.</param>
		protected override void ProcessForeignKeyReferencesForTable(DbConnection dbConn, DbModelTable dbTable)
		{
			// todo - check GetGroupForeingKeys
			foreach (DbModelConstraint constraint in dbTable.Constraints)
			{
				var column = dbTable.Columns.Find(c => c.Name == constraint.ColumnName);
				var refTable = dbTable.ParentDb.FindTable(
					constraint.UniqueConstraintTableSchema + "." + constraint.UniqueConstraintTableName);
				var refColumn = refTable.Columns.Find(c => c.Name == constraint.UniqueColumnName);
				DbModelForeignKeyReference fk = new DbModelForeignKeyReference(column, refTable, refColumn);
				fk.UpdateRule = constraint.UpdateRule;
				fk.DeleteRule = constraint.DeleteRule;
				column.ForeignKeyReference = fk;
			}
		}
	}
}