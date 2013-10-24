﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Npgsql;
using System.Collections;

namespace JLLSco.Models
{
    class RemoteDBHandler : DBHandler
    {

        NpgsqlConnection connection;



        public void connect()
        {

            //Build connection string
            string server, databaseName, uid, password, port, ssl, sslFactory;
            server = "ec2-107-22-186-169.compute-1.amazonaws.com";
            databaseName = "d9i4dgrss13cip";
            uid = "eskfywbiijsflm";
            password = "ZWY2-ezz_aDoROo2mmA3c0oPKx";
            port = "5432";
            ssl = "true";
            sslFactory = "org.postgresql.ssl.NonValidatingFactory";

            string connectionString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};ssl={5}",
                server, port, uid, password, databaseName, ssl);

            try
            {
                connection = new NpgsqlConnection(connectionString);
                try
                {
                    connection.Open();
                    Debug.WriteLine("Connection open.");


                }
                catch (NpgsqlException msg)
                {
                    Debug.WriteLine("Connection did not open: " + msg);

                }
            }
            catch
            {
                Debug.WriteLine("Connection String was not accepted");

            }

        }


        public void closeConnection()
        {
            connection.Close();
        }

        public void sendQuery(string request)
        {
            throw new NotImplementedException();
        }

        public string getResponse()
        {
            throw new NotImplementedException();
        }

        public void testConnection()
        {
            NpgsqlConnection connection;
            //Build connection string
            string server, databaseName, uid, password, port, ssl, sslFactory;
            server = "ec2-184-73-162-34.compute-1.amazonaws.com";
            databaseName = "d8gkfgn82k83n1";
            uid = "jeuhypnnmpolvm";
            password = "X5SJdA_McTmIGR2tewHGxCLe4M";
            port = "5432";
            ssl = "true";
            sslFactory = "org.postgresql.ssl.NonValidatingFactory";

            string connectionString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};ssl={5}",
                server, port, uid, password, databaseName, ssl);

            Debug.WriteLine(connectionString);
            try
            {
                connection = new NpgsqlConnection(connectionString);
                Debug.WriteLine("Connection string was accepted.");
                try
                {
                    connection.Open();
                    Debug.WriteLine("Connection Succesful");
                    NpgsqlCommand command = new NpgsqlCommand("select * from users", connection);
                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            Debug.WriteLine("{0} \t", dr[i]);
                        }

                    }
                }
                catch (NpgsqlException msg)
                {
                    Debug.WriteLine("Connection did not open: " + msg);
                    Debug.WriteLine(connection.ToString());
                }
            }
            catch
            {
                Debug.WriteLine("Connection String was not accepted");
            }
        }

        public ArrayList getUserList()
        {
            connect();
            ArrayList userNames = new ArrayList();

            NpgsqlCommand command = new NpgsqlCommand("select fname, sname from users", connection);
            NpgsqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i+=2)
                {
                    //Debug.WriteLine("{0} \t", dr[i]);
                    userNames.Add(dr[i].ToString() + " " + dr[i+1].ToString());
                }

            }
            closeConnection();
            return userNames;

        }

        public ArrayList getUserDetails(string fName, string sName) {

            ArrayList details = new ArrayList();
            connect();
            NpgsqlCommand command = new NpgsqlCommand("SELECT fname, sname, email, phone, type FROM users WHERE fname = '" +fName + "' AND sname = '"+sName + "'", connection);
            NpgsqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i ++)
                {
                    //Debug.WriteLine("{0} \t", dr[i]);
                    details.Add(dr[i].ToString());
                }

            }
            closeConnection();

            return details;
        
        }

        public void addNewUser(string fName, string sName, string email, string pass, string phone, string type)
        {

            connect();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO users VALUES('" + email + "', '" + fName + "', '" + sName + "', '" + pass + "', '" + phone + "', '" + type + "');", connection);
            NpgsqlDataReader dr = command.ExecuteReader();

            Debug.WriteLine("User added");
            closeConnection();

        }

        public void connectToDB()
        {
            throw new NotImplementedException();
        }
    }



}
