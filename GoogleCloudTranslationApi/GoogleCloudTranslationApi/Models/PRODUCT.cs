using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GoogleCloudTranslationApi.Models
{
   #region PRODUCT
   public partial class PRODUCT
   {
       #region Private Members
       private Guid? _PRODUCT_ID;
       private string _PRODUCT_TITLE;
       private string _PRODUCT_CONTENT;
       private DateTime? _PRODUCT_MD;
       private DateTime? _PRODUCT_CD;
       private int state;
       #endregion

       #region Public Properties
       public Guid? PRODUCT_ID
       {
           get {return _PRODUCT_ID;}
           set {_PRODUCT_ID = value;}
       }

       public string PRODUCT_TITLE
       {
           get {return _PRODUCT_TITLE;}
           set {_PRODUCT_TITLE = value;}
       }

       public string PRODUCT_CONTENT
       {
           get {return _PRODUCT_CONTENT;}
           set {_PRODUCT_CONTENT = value;}
       }

       public DateTime? PRODUCT_MD
       {
           get {return _PRODUCT_MD;}
           set {_PRODUCT_MD = value;}
       }

       public DateTime? PRODUCT_CD
       {
           get {return _PRODUCT_CD;}
           set {_PRODUCT_CD = value;}
       }


       public int State
       {
         get {return state;}
         set {state = value;}
       }
       #endregion
   }
   #endregion


   #region SqlPRODUCTProvider
   public abstract partial class SqlPRODUCTProviderBase
   {
       #region Declarations

       string _connectionString;

       #endregion


       #region Constructors

       public SqlPRODUCTProviderBase(string connectionString)
       {
           this._connectionString = connectionString;
       }

       #endregion

       #region Public properties

           public string ConnectionString
           {
               get {return this._connectionString;}
               set {this._connectionString = value;}
           }

       #endregion

       #region Kaydet

       public void fKaydetPRODUCT ( PRODUCT cPRODUCTS ,TransactionManager transactionManager )
       {
           if( cPRODUCTS.State==1)
           {
               updatePRODUCT ( cPRODUCTS ,transactionManager);
           }
           else
           {
               insertPRODUCT ( cPRODUCTS ,transactionManager);
           }
       }

       #endregion Kaydet

       #region Insert

       private void  insertPRODUCT ( PRODUCT  cPRODUCTS  ,TransactionManager transactionManager )
       {

           try
           {
               SqlCommand cmd = new SqlCommand("usp_InsertPRODUCT" );
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@PRODUCT_ID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@PRODUCT_ID"].Value = cPRODUCTS.PRODUCT_ID;
               cmd.Parameters.Add("@PRODUCT_TITLE" ,SqlDbType.NVarChar );
               cmd.Parameters["@PRODUCT_TITLE"].Value = cPRODUCTS.PRODUCT_TITLE;
               cmd.Parameters.Add("@PRODUCT_CONTENT" ,SqlDbType.NVarChar );
               cmd.Parameters["@PRODUCT_CONTENT"].Value = cPRODUCTS.PRODUCT_CONTENT;

               fAssignNull(cmd);

               if (transactionManager != null)
               {
                 cmd.Connection = transactionManager.Connection;
                 cmd.Transaction = transactionManager.TransactionObject;
                 cmd.ExecuteNonQuery();
               }
               else
               {
                 using(SqlConnection conn = new SqlConnection( this._connectionString ))
                 {
                     conn.Open();
                     cmd.Connection = conn;
                     cmd.ExecuteNonQuery();
                 }
               }
           }
           catch
           {
             throw;
           }

       }

       #endregion Insert

       #region Update

       private void  updatePRODUCT ( PRODUCT  cPRODUCTS  ,TransactionManager transactionManager )
       {

           try
           {
               SqlCommand cmd = new SqlCommand("usp_UpdatePRODUCT" );
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@PRODUCT_ID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@PRODUCT_ID"].Value = cPRODUCTS.PRODUCT_ID;
               cmd.Parameters.Add("@PRODUCT_TITLE" ,SqlDbType.NVarChar );
               cmd.Parameters["@PRODUCT_TITLE"].Value = cPRODUCTS.PRODUCT_TITLE;
               cmd.Parameters.Add("@PRODUCT_CONTENT" ,SqlDbType.NVarChar );
               cmd.Parameters["@PRODUCT_CONTENT"].Value = cPRODUCTS.PRODUCT_CONTENT;

               fAssignNull(cmd);

               if (transactionManager != null)
               {
                 cmd.Connection = transactionManager.Connection;
                 cmd.Transaction = transactionManager.TransactionObject;
                 cmd.ExecuteNonQuery();
               }
               else
               {
                 using(SqlConnection conn = new SqlConnection( this._connectionString ))
                 {
                     conn.Open();
                     cmd.Connection = conn;
                     cmd.ExecuteNonQuery();
                 }
               }
           }
           catch
           {
             throw;
           }

       }

       #endregion Update

       #region Delete

       public void  DeletePRODUCT ( Guid PRODUCT_ID   ,TransactionManager transactionManager )
       {

           try
           {
               SqlCommand cmd = new SqlCommand("usp_DeletePRODUCT" );
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@PRODUCT_ID" ,SqlDbType.UniqueIdentifier  ,16  )  ;	
               cmd.Parameters["@PRODUCT_ID"].Value = PRODUCT_ID;	

               if (transactionManager != null)
               {
                 cmd.Connection = transactionManager.Connection;
                 cmd.Transaction = transactionManager.TransactionObject;
                 cmd.ExecuteNonQuery();
               }
               else
               {
                 using(SqlConnection conn = new SqlConnection( this._connectionString ))
                 {
                     conn.Open();
                     cmd.Connection = conn;
                     cmd.ExecuteNonQuery();
                 }
               }
           }
           catch
           {
             throw;
           }

       }

       #endregion Delete

       #region GetElementByKod

       public PRODUCT  GetElementByKod (string[] _KeyFields, object[] _Values ,TransactionManager transactionManager   )
       {

           if (_KeyFields == null || _KeyFields.Length == 0) return null;
           if (_Values == null || _Values.Length == 0) return null;
           if (_KeyFields.Length != _Values.Length) return null;

           PRODUCT cPRODUCTS =null;
           SqlDataReader reader = null;
           string selectsql = "select top 1 * from PRODUCT WITH (NOLOCK) WHERE ";
           string wherestr = "";

           for (int _i = 0; _i <= _KeyFields.Length - 1; _i++)
           {
               if (wherestr.Trim() != "") wherestr += " AND ";
               wherestr += _KeyFields[_i] + "=@" + _KeyFields[_i];
           }

           selectsql += wherestr;

           SqlCommand cmd = new SqlCommand(selectsql);

           for (int _z = 0; _z <= _Values.Length - 1;_z++ )
           {
               cmd.Parameters.Add(new SqlParameter("@"+_KeyFields[_z], _Values[_z]));
           }

           fAssignNull(cmd);

           if (transactionManager != null)
           {
               cmd.Connection = transactionManager.Connection;
               cmd.Transaction = transactionManager.TransactionObject;
               reader =cmd.ExecuteReader();
               cPRODUCTS=  fReadToClass( ref reader);
           }
           else
           {
               using (SqlConnection conn = new SqlConnection(this._connectionString))
               {
                   conn.Open();
                   cmd.Connection = conn;
                   reader = cmd.ExecuteReader();
                   cPRODUCTS  = fReadToClass( ref reader);
               }
           }
        return cPRODUCTS;
       }



       #endregion GetElementByKod

       #region GetElementById

       public PRODUCT  GetElementById(Guid PRODUCT_ID  , TransactionManager transactionManager   )
       {

           PRODUCT cPRODUCTS =null;
           SqlDataReader reader = null;
           string selectsql = "select * from PRODUCT WITH (NOLOCK) WHERE PRODUCT_ID=@PRODUCT_ID";
           SqlCommand cmd = new SqlCommand(selectsql );

           cmd.Parameters.Add("@PRODUCT_ID" ,SqlDbType.UniqueIdentifier  ,16  )  ;	
           cmd.Parameters["@PRODUCT_ID"].Value = PRODUCT_ID;	

           fAssignNull(cmd);

           if (transactionManager != null)
           {
               cmd.Connection = transactionManager.Connection;
               cmd.Transaction = transactionManager.TransactionObject;
               reader =cmd.ExecuteReader();
               cPRODUCTS=  fReadToClass( ref reader);
           }
           else
           {
               using (SqlConnection conn = new SqlConnection(this._connectionString))
               {
                   conn.Open();
                   cmd.Connection = conn;
                   reader = cmd.ExecuteReader();
                   cPRODUCTS  = fReadToClass( ref reader);
               }
           }
        return cPRODUCTS;
       }

       #endregion GetElementById

       #region fReadToClass

       public PRODUCT fReadToClass(ref SqlDataReader reader  )
       {

           PRODUCT cPRODUCTS = null;
           if (reader.Read())
           {

               if (reader != null && !reader.IsClosed)
               {
                   cPRODUCTS = new PRODUCT();

                   if (!reader.IsDBNull(0)) cPRODUCTS.PRODUCT_ID = reader.GetGuid(0);
                   if (!reader.IsDBNull(1)) cPRODUCTS.PRODUCT_TITLE = reader.GetString(1);
                   if (!reader.IsDBNull(2)) cPRODUCTS.PRODUCT_CONTENT = reader.GetString(2);
                   if (!reader.IsDBNull(3)) cPRODUCTS.PRODUCT_MD = reader.GetDateTime(3);
                   if (!reader.IsDBNull(4)) cPRODUCTS.PRODUCT_CD = reader.GetDateTime(4);

                   cPRODUCTS.State = 1;
               }

           }
           reader.Close();

           return cPRODUCTS;
       }

       #endregion fReadToClass

       #region AssignNull

       public void fAssignNull(SqlCommand cmd)
       {
           for (int i = 0; i < cmd.Parameters.Count; i++)
           {
               if ( object.Equals(  cmd.Parameters[i].Value ,null))
               {
                   cmd.Parameters[i].Value = DBNull.Value;
               }
           }
       }

       #endregion AssignNull

   }



   #endregion

}
