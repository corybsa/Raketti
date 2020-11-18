using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Raketti.Server.Data;

namespace Raketti.Server
{
	public class Helper
	{
		private readonly SqlConnectionConfiguration _sql;

		public Helper(SqlConnectionConfiguration sql)
		{
			_sql = sql;
		}

		public async Task<IEnumerable<T>> ExecStoredProcedure<T>(string proc, DynamicParameters parameters = null)
		{
			IEnumerable<T> results;

			using (var conn = new SqlConnection(_sql.Value))
			{
				if (conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}

				try
				{
					results = await conn.QueryAsync<T>(proc, parameters, commandType: CommandType.StoredProcedure);
				}
				catch (Exception e)
				{
					throw e;
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();
					}
				}
			}

			return results;
		}
	}
}
