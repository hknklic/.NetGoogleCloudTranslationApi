using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GoogleCloudTranslationApi.Models
{
    /// <summary>
    /// TransactionManager interface
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        // Database Database { get; }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Gets or sets the name of the invariant provider.
        /// </summary>
        /// <value>The name of the invariant provider.</value>
        //string InvariantProviderName { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is open.
        /// </summary>
        /// <value><c>true</c> if this instance is open; otherwise, <c>false</c>.</value>
        bool IsOpen { get; }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Gets the transaction object.
        /// </summary>
        /// <value>The transaction object.</value>
        SqlTransaction TransactionObject { get; }
    }




    /// <summary>
    /// TransactionManager is utility class that decorates a <see cref="IDbTransaction"/> instance.
    /// </summary>
    public class TransactionManager : ITransactionManager, IDisposable
    {
        #region Fields

        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private string _connectionString;
        private bool _transactionOpen = false;

        private bool disposed;
        private static object syncRoot = new object();

        #endregion

        #region Properties
        /// <summary>
        ///	Gets or sets the configuration key for database service.
        /// </summary>
        /// <remark>Do not change during a transaction.</remark>
        /// <exception cref="InvalidOperationException">
        /// If an attempt to set when the connection is currently open.
        /// </exception>
        public string ConnectionString
        {
            get { return this._connectionString; }
            set
            {
                //make sure transaction is open
                if (this.IsOpen)
                {
                    throw new InvalidOperationException("Database cannot be changed during a transaction");
                }

                this._connectionString = value;

                this._connection = new SqlConnection(this.ConnectionString);
                //if (this._connectionString.Length > 0 && this._invariantProviderName.Length > 0)
                //{
                //    this._database = new GenericDatabase(_connectionString, DbProviderFactories.GetFactory(this._invariantProviderName));
                //    this._connection = this._database.CreateConnection();
                //}
            }
        }

        /// <summary>
        /// Gets or sets the name of the invariant provider.
        /// </summary>
        /// <value>The name of the invariant provider.</value>
        //public string InvariantProviderName
        //{
        //    get { return this._invariantProviderName; }
        //    set
        //    {
        //        if (this.IsOpen)
        //        {
        //            throw new InvalidOperationException("Database cannot be changed during a transaction");
        //        }

        //        this._invariantProviderName = value;
        //        if (this._connectionString.Length > 0 && this._invariantProviderName.Length > 0)
        //        {
        //            this._database = new GenericDatabase(this._connectionString, DbProviderFactories.GetFactory(this._invariantProviderName));
        //            this._connection = this._database.CreateConnection();
        //        }
        //    }
        //}

        /// <summary>
        /// Gets the <see cref="Database"/> instance.
        /// </summary>
        /// <value></value>            
        public SqlConnection Connection
        {
            get { return this._connection; }
        }

        /// <summary>
        ///	Gets the underlying <see cref="DbTransaction"/> object.
        /// </summary>
        public SqlTransaction TransactionObject
        {
            get { return this._transaction; }
        }

        /// <summary>
        ///	Gets a value that indicates if a transaction is currently open and operating. 
        /// </summary>
        /// <value>Return true if a transaction session is currently open and operating; otherwise false.</value>
        public bool IsOpen
        {
            get { return this._transactionOpen; }
        }
        #endregion Properties

        #region Constructors
        /// <summary>
        ///	Initializes a new instance of the <see cref="TransactionManager"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        //public TransactionManager(string connectionString)
        //    : this(connectionString, "System.Data.SqlClient")
        //{
        //}
        public TransactionManager(string connectionString)
            : this(connectionString, "System.Data.SqlClient")
        {
        }
        /// <summary>
        ///	Initializes a new instance of the <see cref="TransactionManager"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="providerInvariantName">Name of the provider invariant.</param>
        public TransactionManager(string connectionString, string providerInvariantName)
        {
            this._connectionString = connectionString;
            //this._invariantProviderName = providerInvariantName;
            //this._database = new GenericDatabase(this._connectionString, DbProviderFactories.GetFactory(providerInvariantName));
            //this._connection = this._database.CreateConnection();
            this._connection = new SqlConnection(connectionString);
        }


        #endregion Constructors

        #region Public methods
        /// <summary>
        ///	Begins a transaction.
        /// </summary>
        /// <remarks>The default <see cref="IsolationLevel"/> mode is ReadCommitted</remarks>
        /// <exception cref="InvalidOperationException">If a transaction is already open.</exception>
        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        ///	Begins a transaction.
        /// </summary>
        /// <param name="isolationLevel">The <see cref="IsolationLevel"/> level of the transaction</param>
        /// <exception cref="InvalidOperationException">If a transaction is already open.</exception>
        /// <exception cref="DataException"></exception>
        /// <exception cref="DbException"></exception>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (IsOpen)
            {
                throw new InvalidOperationException("Transaction already open.");
            }

            //Open connection
            try
            {
                this._connection.Open();
                this._transaction = this._connection.BeginTransaction(isolationLevel);
                this._transactionOpen = true;
            }
            catch (Exception)
            {
                // in the event of an error, close the connection and destroy the transaction object.
                if (this._connection != null)
                {
                    this._connection.Close();
                }

                if (this._transaction != null)
                {
                    this._transaction.Dispose();
                }

                this._transactionOpen = false;
                throw;
            }
        }

        /// <summary>
        ///	Commit the transaction to the datasource.
        /// </summary>
        /// <exception cref="InvalidOperationException">If a transaction is not open.</exception>
        public void Commit()
        {
            if (!this.IsOpen)
            {
                throw new InvalidOperationException("Transaction needs to begin first.");
            }

            try
            {
                this._transaction.Commit(); // SqlClient could throw Exception or InvalidOperationException
            }
            finally
            {
                //assuming the commit was sucessful.
                this._connection.Close();
                this._transaction.Dispose();
                this._transactionOpen = false;
            }
        }

        /// <summary>
        ///	Rollback the transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">If a transaction is not open.</exception>
        public void Rollback()
        {
            if (!this.IsOpen)
            {
                throw new InvalidOperationException("Transaction needs to begin first.");
            }

            try
            {
                this._transaction.Rollback(); // SqlClient could throw Exception or InvalidOperationException
            }
            finally
            {
                this._connection.Close();
                this._transaction.Dispose();
                this._transactionOpen = false;
            }
        }

        #endregion Public methods

        #region IDisposable methods
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
            {
                lock (syncRoot)
                {
                    disposed = true;

                    if (this.IsOpen)
                    {
                        this.Rollback();
                    }
                }
            }
        }
        #endregion Public methods
    }



}
