using System;
using System.Data.SqlClient;

namespace OrganizationPanel
{
	/// <summary>
	/// Предоставляет настроенный доступ к базе данных.
	/// </summary>
	public class DbContext : IDisposable
	{
		private bool _disposed;
		private string _conectionString;
		private SqlConnection _dateBaseConnection;
		private int _timeOut;

		/// <summary>
		/// Соединение с базой данных.
		/// </summary>
		public SqlConnection DateBaseConnection => _dateBaseConnection;

		/// <summary>
		/// Таймаут запросов.
		/// </summary>
		public int QueryTimeOut => _timeOut;

		public DbContext(string connectionString, int queryTimeOut)
		{
			_conectionString = connectionString;
			_dateBaseConnection = new SqlConnection(_conectionString);
			_dateBaseConnection.Open();
			_timeOut = queryTimeOut;
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}
		
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_dateBaseConnection.Dispose();
				}
			}

			_disposed = true;
		}
	}
}