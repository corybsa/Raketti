using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using Raketti.Server.Data;
using System.Text;

namespace Raketti.Server
{
	public class Helper
	{
		private readonly SqlConfiguration _sql;

		public Helper(SqlConfiguration sql)
		{
			_sql = sql;
		}

		public async Task<List<T>> ExecStoredProcedure<T>(string proc, DynamicParameters parameters = null)
		{
			List<T> results = new List<T>();

			using (var conn = new SqlConnection(_sql.Value))
			{
				if (conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}

				try
				{
					results = (await conn.QueryAsync<T>(proc, parameters, commandType: CommandType.StoredProcedure)).ToList();
				}
				catch (SqlException e)
				{
					var sb = new StringBuilder();
					sb.AppendLine(e.Message);
					sb.Append($"EXEC {proc}");

					if (parameters != null)
					{
						foreach (var name in parameters.ParameterNames)
						{
							sb.Append(" '").Append(parameters.Get<string>(name)).Append("'");
						}
					}

					Console.WriteLine(sb.ToString());

					throw e;
				}
				catch (Exception e)
				{
					Console.WriteLine($"Unknown error: {e.Message}");
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
