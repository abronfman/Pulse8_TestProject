
using Pulse8_ProgrammingTest.DataAccess;
using System;
using System.Linq;

namespace Pulse8_ProgrammingTest {
    class Program {
        static void Main(string[] args) {
            Console.Write("Member ID: ");
            var id = Console.ReadLine();

            int memberID = 0;
            while (!int.TryParse(id, out memberID)) {
                Console.Write("Please enter a valid integer: ");
                id = Console.ReadLine();
            }

            try {
                var result = new DoWorkWrapper().GetMemberDiagnosisDetails(memberID);
                if (result.Any()) {
                    foreach (var diagnosis in result) {
                        Console.WriteLine(diagnosis.ToString());
                        Console.WriteLine("---------------------------------");
                    }
                } else {
                    Console.WriteLine($"No information found for Member with ID: {id}");
                }
            } catch (System.Data.SqlClient.SqlException ex) {
                Console.WriteLine($"Oooopps, a SQL Exception? - {ex.Message}. Please make sure you execute member_getDiagnosisAndSeverity.sproc.sql before rerunning this app.");
            } catch (Exception e) {
                Console.WriteLine($"Oooopps, something horrible happened - {e.Message}");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
