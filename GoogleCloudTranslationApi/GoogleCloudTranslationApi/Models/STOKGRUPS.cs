using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GoogleCloudTranslationApi.Models
{
   #region STOKGRUPS
   public partial class STOKGRUPS
   {
       #region Private Members
       private Guid? _SGR_ID;
       private int? _SGR_NO;
       private int? _SGR_PARENTNO;
       private string _SGR_KODU;
       private string _SGR_ADI;
       private string _SGR_URLKODU;
       private short? _SGR_TIPI;
       private int? _SGR_URUNSAYISI;
       private short? _SGR_AKTIFPASIF;
       private Guid? _SGR_CUID;
       private Guid? _SGR_MUID;
       private DateTime? _SGR_CD;
       private DateTime? _SGR_MD;
       private int state;
       #endregion

       #region Public Properties
       public Guid? SGR_ID
       {
           get {return _SGR_ID;}
           set {_SGR_ID = value;}
       }

       public int? SGR_NO
       {
           get {return _SGR_NO;}
           set {_SGR_NO = value;}
       }

       public int? SGR_PARENTNO
       {
           get {return _SGR_PARENTNO;}
           set {_SGR_PARENTNO = value;}
       }

       public string SGR_KODU
       {
           get {return _SGR_KODU;}
           set {_SGR_KODU = value;}
       }

       public string SGR_ADI
       {
           get {return _SGR_ADI;}
           set {_SGR_ADI = value;}
       }

       public string SGR_URLKODU
       {
           get {return _SGR_URLKODU;}
           set {_SGR_URLKODU = value;}
       }

       public short? SGR_TIPI
       {
           get {return _SGR_TIPI;}
           set {_SGR_TIPI = value;}
       }

       public int? SGR_URUNSAYISI
       {
           get {return _SGR_URUNSAYISI;}
           set {_SGR_URUNSAYISI = value;}
       }

       public short? SGR_AKTIFPASIF
       {
           get {return _SGR_AKTIFPASIF;}
           set {_SGR_AKTIFPASIF = value;}
       }

       public Guid? SGR_CUID
       {
           get {return _SGR_CUID;}
           set {_SGR_CUID = value;}
       }

       public Guid? SGR_MUID
       {
           get {return _SGR_MUID;}
           set {_SGR_MUID = value;}
       }

       public DateTime? SGR_CD
       {
           get {return _SGR_CD;}
           set {_SGR_CD = value;}
       }

       public DateTime? SGR_MD
       {
           get {return _SGR_MD;}
           set {_SGR_MD = value;}
       }


       public int State
       {
         get {return state;}
         set {state = value;}
       }
       #endregion
   }
   #endregion


   #region SqlSTOKGRUPSProvider
   public abstract partial class SqlSTOKGRUPSProviderBase
   {
       #region Declarations

       string _connectionString;

       #endregion


       #region Constructors

       public SqlSTOKGRUPSProviderBase(string connectionString)
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

       public void fKaydetSTOKGRUPS ( STOKGRUPS cSTOKGRUPSS ,TransactionManager transactionManager )
       {
           if( cSTOKGRUPSS.State==1)
           {
               updateSTOKGRUPS ( cSTOKGRUPSS ,transactionManager);
           }
           else
           {
               insertSTOKGRUPS ( cSTOKGRUPSS ,transactionManager);
           }
       }

       #endregion Kaydet

       #region Insert

       private void  insertSTOKGRUPS ( STOKGRUPS  cSTOKGRUPSS  ,TransactionManager transactionManager )
       {

           try
           {
               SqlCommand cmd = new SqlCommand("usp_InsertSTOKGRUPS" );
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@SGR_ID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@SGR_ID"].Value = cSTOKGRUPSS.SGR_ID;
               cmd.Parameters.Add("@SGR_NO" ,SqlDbType.Int );
               cmd.Parameters["@SGR_NO"].Value = cSTOKGRUPSS.SGR_NO;
               cmd.Parameters.Add("@SGR_PARENTNO" ,SqlDbType.Int );
               cmd.Parameters["@SGR_PARENTNO"].Value = cSTOKGRUPSS.SGR_PARENTNO;
               cmd.Parameters.Add("@SGR_KODU" ,SqlDbType.NVarChar );
               cmd.Parameters["@SGR_KODU"].Value = cSTOKGRUPSS.SGR_KODU;
               cmd.Parameters.Add("@SGR_ADI" ,SqlDbType.NVarChar );
               cmd.Parameters["@SGR_ADI"].Value = cSTOKGRUPSS.SGR_ADI;
               cmd.Parameters.Add("@SGR_URLKODU" ,SqlDbType.NVarChar );
               cmd.Parameters["@SGR_URLKODU"].Value = cSTOKGRUPSS.SGR_URLKODU;
               cmd.Parameters.Add("@SGR_TIPI" ,SqlDbType.SmallInt );
               cmd.Parameters["@SGR_TIPI"].Value = cSTOKGRUPSS.SGR_TIPI;
               cmd.Parameters.Add("@SGR_URUNSAYISI" ,SqlDbType.Int );
               cmd.Parameters["@SGR_URUNSAYISI"].Value = cSTOKGRUPSS.SGR_URUNSAYISI;
               cmd.Parameters.Add("@SGR_AKTIFPASIF" ,SqlDbType.SmallInt );
               cmd.Parameters["@SGR_AKTIFPASIF"].Value = cSTOKGRUPSS.SGR_AKTIFPASIF;
               cmd.Parameters.Add("@SGR_CUID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@SGR_CUID"].Value = cSTOKGRUPSS.SGR_CUID;
               cmd.Parameters.Add("@SGR_MUID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@SGR_MUID"].Value = cSTOKGRUPSS.SGR_MUID;

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

       private void  updateSTOKGRUPS ( STOKGRUPS  cSTOKGRUPSS  ,TransactionManager transactionManager )
       {

           try
           {
               SqlCommand cmd = new SqlCommand("usp_UpdateSTOKGRUPS" );
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@SGR_ID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@SGR_ID"].Value = cSTOKGRUPSS.SGR_ID;
               cmd.Parameters.Add("@SGR_NO" ,SqlDbType.Int );
               cmd.Parameters["@SGR_NO"].Value = cSTOKGRUPSS.SGR_NO;
               cmd.Parameters.Add("@SGR_PARENTNO" ,SqlDbType.Int );
               cmd.Parameters["@SGR_PARENTNO"].Value = cSTOKGRUPSS.SGR_PARENTNO;
               cmd.Parameters.Add("@SGR_KODU" ,SqlDbType.NVarChar );
               cmd.Parameters["@SGR_KODU"].Value = cSTOKGRUPSS.SGR_KODU;
               cmd.Parameters.Add("@SGR_ADI" ,SqlDbType.NVarChar );
               cmd.Parameters["@SGR_ADI"].Value = cSTOKGRUPSS.SGR_ADI;
               cmd.Parameters.Add("@SGR_URLKODU" ,SqlDbType.NVarChar );
               cmd.Parameters["@SGR_URLKODU"].Value = cSTOKGRUPSS.SGR_URLKODU;
               cmd.Parameters.Add("@SGR_TIPI" ,SqlDbType.SmallInt );
               cmd.Parameters["@SGR_TIPI"].Value = cSTOKGRUPSS.SGR_TIPI;
               cmd.Parameters.Add("@SGR_URUNSAYISI" ,SqlDbType.Int );
               cmd.Parameters["@SGR_URUNSAYISI"].Value = cSTOKGRUPSS.SGR_URUNSAYISI;
               cmd.Parameters.Add("@SGR_AKTIFPASIF" ,SqlDbType.SmallInt );
               cmd.Parameters["@SGR_AKTIFPASIF"].Value = cSTOKGRUPSS.SGR_AKTIFPASIF;
               cmd.Parameters.Add("@SGR_CUID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@SGR_CUID"].Value = cSTOKGRUPSS.SGR_CUID;
               cmd.Parameters.Add("@SGR_MUID" ,SqlDbType.UniqueIdentifier );
               cmd.Parameters["@SGR_MUID"].Value = cSTOKGRUPSS.SGR_MUID;

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

       public void  DeleteSTOKGRUPS ( Guid SGR_ID   ,TransactionManager transactionManager )
       {

           try
           {
               SqlCommand cmd = new SqlCommand("usp_DeleteSTOKGRUPS" );
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@SGR_ID" ,SqlDbType.UniqueIdentifier  ,16  )  ;	
               cmd.Parameters["@SGR_ID"].Value = SGR_ID;	

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

       public STOKGRUPS  GetElementByKod (string[] _KeyFields, object[] _Values ,TransactionManager transactionManager   )
       {

           if (_KeyFields == null || _KeyFields.Length == 0) return null;
           if (_Values == null || _Values.Length == 0) return null;
           if (_KeyFields.Length != _Values.Length) return null;

           STOKGRUPS cSTOKGRUPSS =null;
           SqlDataReader reader = null;
           string selectsql = "select top 1 * from STOKGRUPS WITH (NOLOCK) WHERE ";
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
               cSTOKGRUPSS=  fReadToClass( ref reader);
           }
           else
           {
               using (SqlConnection conn = new SqlConnection(this._connectionString))
               {
                   conn.Open();
                   cmd.Connection = conn;
                   reader = cmd.ExecuteReader();
                   cSTOKGRUPSS  = fReadToClass( ref reader);
               }
           }
        return cSTOKGRUPSS;
       }



       #endregion GetElementByKod

       #region GetElementById

       public STOKGRUPS  GetElementById(Guid SGR_ID  , TransactionManager transactionManager   )
       {

           STOKGRUPS cSTOKGRUPSS =null;
           SqlDataReader reader = null;
           string selectsql = "select * from STOKGRUPS WITH (NOLOCK) WHERE SGR_ID=@SGR_ID";
           SqlCommand cmd = new SqlCommand(selectsql );

           cmd.Parameters.Add("@SGR_ID" ,SqlDbType.UniqueIdentifier  ,16  )  ;	
           cmd.Parameters["@SGR_ID"].Value = SGR_ID;	

           fAssignNull(cmd);

           if (transactionManager != null)
           {
               cmd.Connection = transactionManager.Connection;
               cmd.Transaction = transactionManager.TransactionObject;
               reader =cmd.ExecuteReader();
               cSTOKGRUPSS=  fReadToClass( ref reader);
           }
           else
           {
               using (SqlConnection conn = new SqlConnection(this._connectionString))
               {
                   conn.Open();
                   cmd.Connection = conn;
                   reader = cmd.ExecuteReader();
                   cSTOKGRUPSS  = fReadToClass( ref reader);
               }
           }
        return cSTOKGRUPSS;
       }

       #endregion GetElementById

       #region fReadToClass

       public STOKGRUPS fReadToClass(ref SqlDataReader reader  )
       {

           STOKGRUPS cSTOKGRUPSS = null;
           if (reader.Read())
           {

               if (reader != null && !reader.IsClosed)
               {
                   cSTOKGRUPSS = new STOKGRUPS();

                   if (!reader.IsDBNull(0)) cSTOKGRUPSS.SGR_ID = reader.GetGuid(0);
                   if (!reader.IsDBNull(1)) cSTOKGRUPSS.SGR_NO = reader.GetInt32(1);
                   if (!reader.IsDBNull(2)) cSTOKGRUPSS.SGR_PARENTNO = reader.GetInt32(2);
                   if (!reader.IsDBNull(3)) cSTOKGRUPSS.SGR_KODU = reader.GetString(3);
                   if (!reader.IsDBNull(4)) cSTOKGRUPSS.SGR_ADI = reader.GetString(4);
                   if (!reader.IsDBNull(5)) cSTOKGRUPSS.SGR_URLKODU = reader.GetString(5);
                   if (!reader.IsDBNull(6)) cSTOKGRUPSS.SGR_TIPI = reader.GetInt16(6);
                   if (!reader.IsDBNull(7)) cSTOKGRUPSS.SGR_URUNSAYISI = reader.GetInt32(7);
                   if (!reader.IsDBNull(8)) cSTOKGRUPSS.SGR_AKTIFPASIF = reader.GetInt16(8);
                   if (!reader.IsDBNull(9)) cSTOKGRUPSS.SGR_CUID = reader.GetGuid(9);
                   if (!reader.IsDBNull(10)) cSTOKGRUPSS.SGR_MUID = reader.GetGuid(10);
                   if (!reader.IsDBNull(11)) cSTOKGRUPSS.SGR_CD = reader.GetDateTime(11);
                   if (!reader.IsDBNull(12)) cSTOKGRUPSS.SGR_MD = reader.GetDateTime(12);

                   cSTOKGRUPSS.State = 1;
               }

           }
           reader.Close();

           return cSTOKGRUPSS;
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
