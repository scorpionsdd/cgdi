using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace BComponents.DataAccessLayer
{
    /// <summary>
    /// Summary description for OracleHelper.
    /// </summary>
    public sealed class OracleHelper
    {
        public OracleHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static OracleDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {

            //Create the command and connection
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connString);

            cmd.Parameters.Clear();
            //Prepare the command to execute
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

            //Execute the query, stating that the connection should close when the resulting datareader has been read
            OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();

            return rdr;




        }

        //*********************************************************************
        //
        // Execute a OracleCommand (that returns no resultset) against the database specified in the connection string 
        // using the provided parameters.
        //
        // e.g.:  
        //  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter("@prodid", 24));
        //
        // param name="connectionString" a valid connection string for a OracleConnection
        // param name="commandType" the CommandType (stored procedure, text, etc.)
        // param name="commandText" the stored procedure name or T-SQL command
        // param name="commandParameters" an array of SqlParamters used to execute the command
        // returns an int representing the number of rows affected by the command
        //
        //*********************************************************************

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create & open a OracleConnection, and dispose of it after we are done.
            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(cn, commandType, commandText, commandParameters);
            }
        }

        //*********************************************************************
        //
        // Execute a OracleCommand (that returns no resultset) against the database specified in the connection string 
        // using the provided parameters.
        //
        // e.g.:  
        //  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter("@prodid", 24));
        //
        // param name="connectionString" a valid connection string for a OracleConnection
        // param name="commandType" the CommandType (stored procedure, text, etc.)
        // param name="commandText" the stored procedure name or T-SQL command
        // param name="commandParameters" an array of SqlParamters used to execute the command
        // returns an int representing the number of rows affected by the command
        //
        //*********************************************************************

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            //create & open a OracleConnection, and dispose of it after we are done.
            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(cn, commandType, commandText);
            }
        }


        //*********************************************************************
        //
        // Execute a stored procedure via a OracleCommand (that returns no resultset) against the database specified in 
        // the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // 
        // This method provides no access to output parameters or the stored procedure's return value parameter.
        // 
        // e.g.:  
        //  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
        //
        // param name="connectionString" a valid connection string for a OracleConnection
        // param name="spName" the name of the stored prcedure
        // param name="parameterValues" an array of objects to be assigned as the input values of the stored procedure
        // returns an int representing the number of rows affected by the command
        //
        //*********************************************************************

        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        //*********************************************************************
        //
        // Execute a OracleCommand (that returns no resultset) against the specified OracleConnection 
        // using the provided parameters.
        // 
        // e.g.:  
        //  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter("@prodid", 24));
        // 
        // param name="connection" a valid OracleConnection 
        // param name="commandType" the CommandType (stored procedure, text, etc.) 
        // param name="commandText" the stored procedure name or T-SQL command 
        // param name="commandParameters" an array of SqlParamters used to execute the command 
        // returns an int representing the number of rows affected by the command
        //
        //*********************************************************************

        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters);

            //finally, execute the command.
            int retval = cmd.ExecuteNonQuery();

            // detach the OracleParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();

            return retval;
        }

        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);


                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            }
        }


        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create & open a OracleConnection, and dispose of it after we are done.
            try
            {
using (OracleConnection cn = new OracleConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteDataset(cn, commandType, commandText, commandParameters);


            }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public static DataSet fail(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters);

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            // detach the OracleParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            connection.Close();
            return ds;
        }

        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            try
            {
                var fail_var = fail(connection, commandType, "select * from nls_session_parameters", commandParameters);

                //create a command and prepare it for execution
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters);
                //if (cmd.CommandText == "select * from sof_tipo_documento  Where id_empleado =  And eliminado = '0' order by tipo_documento ")
                //{
                //    commandText = "select * from sof_tipo_documento  Where id_empleado ='134921'  And eliminado = '0' order by tipo_documento ";
                //    PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters);
                //}

                //create the DataAdapter & DataSet
                OracleDataAdapter da = new OracleDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                cmd.Parameters.Clear();

                //return the dataset
                connection.Close();
                return ds;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
                // Expected output: ReferenceError: nonExistentFunction is not defined
                // (Note: the exact output may be browser-dependent)
            }


            return null;
            //fill the DataSet using default values for DataTable names, etc.


            // detach the OracleParameters from the command object, so they can be used again.			

        }

        public static DataView ExecuteDataset1(string connectionString, string spName, params object[] parameterValues)
        {
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);


                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteDataset1(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteDataset1(connectionString, CommandType.StoredProcedure, spName);
            }
        }


        public static DataView ExecuteDataset1(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create & open a OracleConnection, and dispose of it after we are done.
            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteDataset1(cn, commandType, commandText, commandParameters);

            }
        }

        public static DataView ExecuteDataset1(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters);


            //create the DataAdapter & DataSet
            OracleDataAdapter da = new OracleDataAdapter(cmd);

            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.

            da.Fill(ds);

            DataTable usersTable = ds.Tables[0];
            DataView dv = new DataView(usersTable);

            /*			
                        DataView dv = new DataView();
                        dv.Table = ds.Tables["newDataset"];
            */
            // detach the OracleParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            return (dv);

        }



        private static void AssignParameterValues(OracleParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                //do nothing if we get no data
                return;
            }

            // we must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            //iterate through the OracleParameters, assigning the values from the corresponding position in the 
            //value array
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                commandParameters[i].Value = parameterValues[i];
            }
        }


        private static void AttachParameters(OracleCommand command, OracleParameter[] commandParameters)
        {
            command.Parameters.Clear();
            foreach (OracleParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                command.Parameters.Add(p);
            }
        }
        private static void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //associate the connection with the command
            command.Connection = connection;

            //set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            return;
        }

        //*********************************************************************
        //
        // Execute a OracleCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        // using the provided parameters.
        // 
        // e.g.:  
        //  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new OracleParameter("@prodid", 24));
        // 
        // param name="connectionString" a valid connection string for a OracleConnection 
        // param name="commandType" the CommandType (stored procedure, text, etc.) 
        // param name="commandText" the stored procedure name or T-SQL command 
        // param name="commandParameters" an array of SqlParamters used to execute the command 
        // returns an object containing the value in the 1x1 resultset generated by the command
        //
        //*********************************************************************

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create & open a OracleConnection, and dispose of it after we are done.
            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteScalar(cn, commandType, commandText, commandParameters);
            }
        }

        //*********************************************************************
        //
        // Execute a stored procedure via a OracleCommand (that returns a 1x1 resultset) against the database specified in 
        // the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // 
        // This method provides no access to output parameters or the stored procedure's return value parameter.
        // 
        // e.g.:  
        //  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
        // 
        // param name="connectionString" a valid connection string for a OracleConnection 
        // param name="spName" the name of the stored procedure 
        // param name="parameterValues" an array of objects to be assigned as the input values of the stored procedure 
        // returns an object containing the value in the 1x1 resultset generated by the command
        //
        //*********************************************************************

        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        //*********************************************************************
        //
        // Execute a OracleCommand (that returns a 1x1 resultset) against the specified OracleConnection 
        // using the provided parameters.
        // 
        // e.g.:  
        //  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new OracleParameter("@prodid", 24));
        // 
        // param name="connection" a valid OracleConnection 
        // param name="commandType" the CommandType (stored procedure, text, etc.) 
        // param name="commandText" the stored procedure name or T-SQL command 
        // param name="commandParameters" an array of SqlParamters used to execute the command 
        // returns an object containing the value in the 1x1 resultset generated by the command
        //
        //*********************************************************************

        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters);

            //execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // detach the OracleParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();

            return retval;

        }

    }
    public sealed class OracleHelperParameterCache
    {
        //*********************************************************************
        //
        // Since this class provides only static methods, make the default constructor private to prevent 
        // instances from being created with "new SqlHelperParameterCache()".
        //
        //*********************************************************************

        private OracleHelperParameterCache() { }

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        //*********************************************************************
        //
        // resolve at run time the appropriate set of OracleParameters for a stored procedure
        // 
        // param name="connectionString" a valid connection string for a OracleConnection 
        // param name="spName" the name of the stored procedure 
        // param name="includeReturnValueParameter" whether or not to include their return value parameter 
        //
        //*********************************************************************

        private static OracleParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            using (OracleConnection cn = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand(spName, cn))
            {
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                OracleCommandBuilder.DeriveParameters(cmd);

                if (!includeReturnValueParameter)
                {
                    cmd.Parameters.RemoveAt(0);
                }

                OracleParameter[] discoveredParameters = new OracleParameter[cmd.Parameters.Count];

                cmd.Parameters.CopyTo(discoveredParameters, 0);

                return discoveredParameters;
            }
        }

        private static OracleParameter[] CloneParameters(OracleParameter[] originalParameters)
        {
            //deep copy of cached OracleParameter array
            OracleParameter[] clonedParameters = new OracleParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (OracleParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        //*********************************************************************
        //
        // add parameter array to the cache
        //
        // param name="connectionString" a valid connection string for a OracleConnection 
        // param name="commandText" the stored procedure name or T-SQL command 
        // param name="commandParameters" an array of SqlParamters to be cached 
        //
        //*********************************************************************

        public static void CacheParameterSet(string connectionString, string commandText, params OracleParameter[] commandParameters)
        {
            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        //*********************************************************************
        //
        // Retrieve a parameter array from the cache
        // 
        // param name="connectionString" a valid connection string for a OracleConnection 
        // param name="commandText" the stored procedure name or T-SQL command 
        // returns an array of SqlParamters
        //
        //*********************************************************************

        public static OracleParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            string hashKey = connectionString + ":" + commandText;

            OracleParameter[] cachedParameters = (OracleParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        //*********************************************************************
        //
        // Retrieves the set of OracleParameters appropriate for the stored procedure
        // 
        // This method will query the database for this information, and then store it in a cache for future requests.
        // 
        // param name="connectionString" a valid connection string for a OracleConnection 
        // param name="spName" the name of the stored procedure 
        // returns an array of OracleParameters
        //
        //*********************************************************************

        public static OracleParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        //*********************************************************************
        //
        // Retrieves the set of OracleParameters appropriate for the stored procedure
        // 2
        // This method will query the database for this information, and then store it in a cache for future requests.
        // 
        // param name="connectionString" a valid connection string for a OracleConnection 
        // param name="spName" the name of the stored procedure 
        // param name="includeReturnValueParameter" a bool value indicating whether the return value parameter should be included in the results 
        // returns an array of OracleParameters
        //
        //*********************************************************************

        public static OracleParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            string hashKey = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            OracleParameter[] cachedParameters;


            cachedParameters = (OracleParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                cachedParameters = (OracleParameter[])(paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter));

            }

            return CloneParameters(cachedParameters);
        }
    }


}
