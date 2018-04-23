using Pulse8_ProgrammingTest.Models;
using System;
using System.Collections.Generic;

namespace Pulse8_ProgrammingTest.DataAccess {
    /// <summary>
    /// Contract for a repository
    /// </summary>
    public interface IRepository : IDisposable {
        IList<MemberDiagnosisDto> GetMemberDiagnosis(int memberID);
    }
}
