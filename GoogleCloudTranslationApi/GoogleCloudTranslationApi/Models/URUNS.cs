using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GoogleCloudTranslationApi.Models
{
   #region URUNS
   public partial class URUNS
   {
       #region Private Members
       private Guid? _URUN_ID;
       private string _URUN_BASLIK;
       private string _URUN_ICERIK;
       private DateTime? _URUN_MD;
       private DateTime? _URUN_CD;
       private int state;
       #endregion

       #region Public Properties
       public Guid? URUN_ID
       {
           get {return _URUN_ID;}
           set {_URUN_ID = value;}
       }

       public string URUN_BASLIK
       {
           get {return _URUN_BASLIK;}
           set {_URUN_BASLIK = value;}
       }

       public string URUN_ICERIK
       {
           get {return _URUN_ICERIK;}
           set {_URUN_ICERIK = value;}
       }

       public DateTime? URUN_MD
       {
           get {return _URUN_MD;}
           set {_URUN_MD = value;}
       }

       public DateTime? URUN_CD
       {
           get {return _URUN_CD;}
           set {_URUN_CD = value;}
       }


       public int State
       {
         get {return state;}
         set {state = value;}
       }
       #endregion
   }
   #endregion


   #region SqlURUNSProvider
   public abstract partial class SqlURUNSProviderBase
   {
       #region Declarations

       string _connectionString;

       #endregion


       #region Constructors

       public SqlURUNSProviderBase(string connectionString)
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

       public void fKaydetURUNS ( URUNS cURUNSS ,TransactionManager transactionManager )
       {
           if( cURUNSS.State==1)
           {
               updateURUNS ( cURUNSS ,transactionManager);
           }
           else
           {
               insertURUNS ( cURUNSS ,transactionManager);
           }
       }

       #endregion Kaydet

       #region Insert

       private void  insertURUNS ( URUNS  cURUNSS  ,TransactionManager transactionManager )
       {

           try
           {
               SqlCommand cmd = new SqlCommand("usp_InsertURUNS" );
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@URUN_ID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@URUN_ID"].Value = cURUNSS.URUN_ID;
               cmd.Parameters.Add("@URUN_BASLIK" ,SqlDbType.NVarChar );
               cmd.Parameters["@URUN_BASLIK"].Value = cURUNSS.URUN_BASLIK;
               cmd.Parameters.Add("@URUN_ICERIK" ,SqlDbType.NVarChar );
               cmd.Parameters["@URUN_ICERIK"].Value = cURUNSS.URUN_ICERIK;

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

       private void  updateURUNS ( URUNS  cURUNSS  ,TransactionManager transactionManager )
       {

           try
           {
               SqlCommand cmd = new SqlCommand("usp_UpdateURUNS" );
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@URUN_ID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@URUN_ID"].Value = cURUNSS.URUN_ID;
               cmd.Parameters.Add("@URUN_BASLIK" ,SqlDbType.NVarChar );
               cmd.Parameters["@URUN_BASLIK"].Value = cURUNSS.URUN_BASLIK;
               cmd.Parameters.Add("@URUN_ICERIK" ,SqlDbType.NVarChar );
               cmd.Parameters["@URUN_ICERIK"].Value = cURUNSS.URUN_ICERIK;

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

       public void  DeleteURUNS ( Guid URUN_ID   ,TransactionManager transactionManager )
       {

           try
           {
               SqlCommand cmd = new SqlCommand("usp_DeleteURUNS" );
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@URUN_ID" ,SqlDbType.UniqueIdentifier  ,16  )  ;	
               cmd.Parameters["@URUN_ID"].Value = URUN_ID;	

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

       public URUNS  GetElementByKod (string[] _KeyFields, object[] _Values ,TransactionManager transactionManager   )
       {

           if (_KeyFields == null || _KeyFields.Length == 0) return null;
           if (_Values == null || _Values.Length == 0) return null;
           if (_KeyFields.Length != _Values.Length) return null;

           URUNS cURUNSS =null;
           SqlDataReader reader = null;
           string selectsql = "select top 1 * from URUNS WITH (NOLOCK) WHERE ";
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
               cURUNSS=  fReadToClass( ref reader);
           }
           else
           {
               using (SqlConnection conn = new SqlConnection(this._connectionString))
               {
                   conn.Open();
                   cmd.Connection = conn;
                   reader = cmd.ExecuteReader();
                   cURUNSS  = fReadToClass( ref reader);
               }
           }
        return cURUNSS;
       }



       #endregion GetElementByKod

       #region GetElementById

       public URUNS  GetElementById(Guid URUN_ID  , TransactionManager transactionManager   )
       {

           URUNS cURUNSS =null;
           SqlDataReader reader = null;
           string selectsql = "select * from URUNS WITH (NOLOCK) WHERE URUN_ID=@URUN_ID";
           SqlCommand cmd = new SqlCommand(selectsql );

           cmd.Parameters.Add("@URUN_ID" ,SqlDbType.UniqueIdentifier  ,16  )  ;	
           cmd.Parameters["@URUN_ID"].Value = URUN_ID;	

           fAssignNull(cmd);

           if (transactionManager != null)
           {
               cmd.Connection = transactionManager.Connection;
               cmd.Transaction = transactionManager.TransactionObject;
               reader =cmd.ExecuteReader();
               cURUNSS=  fReadToClass( ref reader);
           }
           else
           {
               using (SqlConnection conn = new SqlConnection(this._connectionString))
               {
                   conn.Open();
                   cmd.Connection = conn;
                   reader = cmd.ExecuteReader();
                   cURUNSS  = fReadToClass( ref reader);
               }
           }
        return cURUNSS;
       }

       #endregion GetElementById

       #region fReadToClass

       public URUNS fReadToClass(ref SqlDataReader reader  )
       {

           URUNS cURUNSS = null;
           if (reader.Read())
           {

               if (reader != null && !reader.IsClosed)
               {
                   cURUNSS = new URUNS();

                   if (!reader.IsDBNull(0)) cURUNSS.URUN_ID = reader.GetGuid(0);
                   if (!reader.IsDBNull(1)) cURUNSS.URUN_BASLIK = reader.GetString(1);
                   if (!reader.IsDBNull(2)) cURUNSS.URUN_ICERIK = reader.GetString(2);
                   if (!reader.IsDBNull(3)) cURUNSS.URUN_MD = reader.GetDateTime(3);
                   if (!reader.IsDBNull(4)) cURUNSS.URUN_CD = reader.GetDateTime(4);

                   cURUNSS.State = 1;
               }

           }
           reader.Close();

           return cURUNSS;
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
