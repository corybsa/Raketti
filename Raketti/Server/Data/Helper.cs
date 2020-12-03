using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using Raketti.Server.Data;
using Raketti.Shared;
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

		public async Task<DbResponse<T>> ExecStoredProcedure<T>(string proc, DynamicParameters parameters = null)
		{
			var response = new DbResponse<T>();

			using (var conn = new SqlConnection(_sql.Value))
			{
				if (conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}

				try
				{
					response.Success = true;
					response.Data = (await conn.QueryAsync<T>(proc, parameters, commandType: CommandType.StoredProcedure)).ToList();
				}
				catch (SqlException e)
				{
					string info = GetStatement(proc, parameters, e);
					response.Success = false;
					response.Info = info;
					Console.WriteLine(info);
				}
				catch (Exception e)
				{
					response.Success = false;
					response.Info = $"Unknown error: {e.Message}";
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();
					}
				}
			}

			return response;
		}

		private string GetStatement(string proc, DynamicParameters parameters, SqlException e) {
			var sb = new StringBuilder();

			if (e != null)
			{
				sb.AppendLine(e.Message);
			}

			sb.Append($"EXEC {proc}");

			if (parameters != null)
			{
				foreach (var name in parameters.ParameterNames)
				{
					sb.Append(" '").Append(parameters.Get<dynamic>(name)).Append("'");
				}
			}

			return sb.ToString();
		}
	}
}
