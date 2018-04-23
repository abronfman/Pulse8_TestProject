using Pulse8_ProgrammingTest.DataAccess;
using Pulse8_ProgrammingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulse8_ProgrammingTest {
    /// <summary>
    /// Worker wrapper to manipulate input and return data
    /// </summary>
    public class DoWorkWrapper {
        private IRepository repository;

        #region Constructors

        public DoWorkWrapper() {
            this.repository = new SqlRepository();
        }

        public DoWorkWrapper(IRepository repo) {
            this.repository = repo;
        }

        #endregion //Constructors

        #region Worker Methods
        
        /// <summary>
        /// Get diagnosis details for a given member id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<MemberDiagnosisDto> GetMemberDiagnosisDetails(int id) {
            if (id < 0) {
                return null;
            }
            return repository.GetMemberDiagnosis(id);
        }

        #endregion //Worker Methods

    }
}
