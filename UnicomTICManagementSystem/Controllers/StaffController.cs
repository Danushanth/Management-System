using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTicManagementSystem.Repositories;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Controllers
{
    internal class StaffController
    {
        public List<Staff> GetAllStaff()
        {
            var staffList = new List<Staff>();

            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT * FROM Staffs", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    staffList.Add(new Staff
                    {
                        StaffId = reader.GetInt32(0),
                        StaffName = reader.GetString(1),
                        StaffAddress = reader.GetString(2),
                        StaffNicNo = reader.GetInt32(3)
                    });
                }
            }

            return staffList;
        }
        // ADD ============================================================================================================================================
        
        public void AddStaff(string name, string address, int nicNo)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("INSERT INTO Staffs (StaffName, StaffAddress, StaffNicNo) VALUES (@name, @address, @nic)", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@nic", nicNo);
                cmd.ExecuteNonQuery();
            }
        }
        // UPDATE ============================================================================================================================================
        public void UpdateStaff(int id, string name, string address, int nicNo)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("UPDATE Staffs SET StaffName = @name, StaffAddress = @address, StaffNicNo = @nic WHERE StaffId = @id", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@nic", nicNo);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        // DELETE =============================================================================================================================================
        public void DeleteStaff(int id)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("DELETE FROM Staffs WHERE StaffId = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
