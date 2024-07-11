using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Sqlite;


namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        public DataSet Query(FormattableString formattableQuery)
        {
            //dato che SqliteCommand accetta come parametro query una stringa
            //devo per forza convertire la FormattableString in string
            var queryArguments = formattableQuery.GetArguments();//recupero gli argomenti della query  -> torna un array di SqliteParameter
            var sqliteParameters = new List<SqliteParameter>();//creo una lista di argomenti
            //per ogni argomento revuperato dalla query
            for (var i = 0; i < queryArguments.Length; i++)
            {
                //Il costruttore di SqliteParameter vuole un valore numerico come string e il valore da iniettare nella query
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;

            }

            string query = formattableQuery.ToString();


            using (var conn = new SqliteConnection("Data Source=Data/MyCourse.db"))
            {
                conn.Open();
                using (var cmd = new SqliteCommand(query, conn))
                {
                    //tramite il metodo AddRange aggiungo tutti i parametri recuperati dalla query
                    cmd.Parameters.AddRange(sqliteParameters);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dataSet = new DataSet();
                        dataSet.EnforceConstraints = false;
                        do
                        {
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);
                            dataTable.Load(reader);
                        } while (!reader.IsClosed);//la proprietà IsClosed mi permette di controlare se tutti i dati sono stati letti o meno da db

                        return dataSet;

                    }
                }
                //conn.Dispose();
            }

        }


         public void QueryDelete(FormattableString formattableQuery)
        {
            var queryArguments = formattableQuery.GetArguments(); //recupero gli argomenti della query  -> torna un array di SqliteParameter
            var sqliteParameters = new List<SqliteParameter>();

            for (var i = 0; i < queryArguments.Length; i++)
            {
                //Il costruttore di SqliteParameter vuole un valore numerico come string e il valore da iniettare nella query
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;

            }

            string query = formattableQuery.ToString();


            using (var conn = new SqliteConnection("Data Source=Data/MyCourse.db"))
            {
                conn.Open();

                using (var cmd = new SqliteCommand(query, conn))
                {
                    //tramite il metodo AddRange aggiungo tutti i parametri recuperati dalla query
                    cmd.Parameters.AddRange(sqliteParameters);

                    cmd.ExecuteNonQuery();
                }

            }


        }
    }
}