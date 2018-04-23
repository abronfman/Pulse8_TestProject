using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using Pulse8_ProgrammingTest.Models;
using Pulse8_ProgrammingTest.Utils;

namespace Pulse8_ProgrammingTest.DataAccess {
    /// <summary>
    /// represents an <see cref="SqlRepository"/> that communicates with the database
    /// </summary>
    public class SqlRepository : IRepository, IDisposable {
        #region constants

        private const string CONNECTION_STRING = "Pulse8TestDB";
        private const string GET_MEMBER_DIAGNOSIS_AND_SEVERITY_COMMAND = "member_getDiagnosisAndSeverity";

        #endregion //constants

        #region private fields

        private IDbConnection _connection;
        private bool _disposed;
        private string _connectionString;

        #endregion //private fields

        #region Properties

        protected string connectionString => _connectionString = _connectionString ?? (_connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING].ConnectionString);

        protected IDbConnection connection => _connection ?? (_connection = new SqlConnection(connectionString));

        #endregion //Properties

        #region Constructors
        /// <summary>
        /// create a default instance of <see cref="SqlRepository"/>
        /// </summary>
        public SqlRepository() { }

        /// <summary>
        /// create an instance of <see cref="SqlRepository" /> with injected connection string dependency
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlRepository(string connectionString) {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// create an instance of <see cref="SqlRepository" /> with injected dependencies
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="connection"></param>
        public SqlRepository(string connectionString, IDbConnection connection) {
            this._connectionString = connectionString;
            this._connection = connection;
        }

        #endregion //Constructors

        #region IRepository Implementation

        public IList<MemberDiagnosisDto> GetMemberDiagnosis(int memberId) {
            var resultSet = new List<MemberDiagnosisDto>();
            using (connection) {
                using (IDbCommand cmd = new SqlCommand(GET_MEMBER_DIAGNOSIS_AND_SEVERITY_COMMAND)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;

                    cmd.Parameters.Add(new SqlParameter("@MemberId", memberId));

                    try {
                        connection.Open();

                        var reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            var diagnosis = new MemberDiagnosisDto {
                                MemberId = SmartConvert.ToInt(reader["MemberID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                MostSevereDiagnosisId = SmartConvert.ToNullableInt(reader["MostSevereDiagnosisID"]),
                                MostSevereDiagnosisDescription = reader["MostSevereDiagnosisDescription"].ToString(),
                                DiagnosisCategoryId = SmartConvert.ToInt(reader["CategoryID"]),
                                DiagnosisCategoryDescription = reader["CategoryDescription"].ToString(),
                                DiagnosisCategoryScore = SmartConvert.ToInt(reader["CategoryScore"]),
                                IsMostSevere = SmartConvert.ToBool(reader["IsMostSevereCategory"])
                            };
                            resultSet.Add(diagnosis);
                        }
                    } catch (Exception ex) {
                        connection.Close();
                        throw;
                    }
                }
            }
            return resultSet;
        }

        #endregion //IRepository Implementation

        #region IDisposable Implmentation

        private bool disposed = false;
        protected void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    connection.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion //IDisposable Implmentation
    }
}
