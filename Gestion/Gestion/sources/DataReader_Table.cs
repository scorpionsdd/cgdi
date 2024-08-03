using System;
using System.Data;
using System.Data.Common;

namespace Gestion.Sources
{	

	public class DataReader_Table:DbDataAdapter

	{/// <summary>

		/// Use this Method to fill the DataTable from the DataReader/// </summary>/// <param name=”dataTable”></param>/// <param name=”dataReader”></param>/// <returns></returns>

		public int FillFromReader(DataTable dataTable,IDataReader dataReader)

		{     

			return this.Fill(dataTable,dataReader);

		}

		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType,DataTableMapping tableMapping ){return null;}

		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping ){          return null;} 

		protected override void OnRowUpdated( RowUpdatedEventArgs value ){} protected override void OnRowUpdating( RowUpdatingEventArgs value){} 
	}
}
