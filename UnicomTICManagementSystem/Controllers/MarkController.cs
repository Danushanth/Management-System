using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTicManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class MarkController
    {
        public List<Mark> GetAllMarks()
        {
            var list = new List<Mark>();

            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT * FROM Marks", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Mark
                    {
                        MarkId = reader.GetInt32(0),     
                        StudentId = reader.GetInt32(1),
                        ExamId = reader.GetInt32(2),
                        Score = reader.GetInt32(3)
                    });
                }
            }

            return list;
        }

// =======================================================================================================================================================================================

        public void AddMark(Mark mark)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("INSERT INTO Marks (StudentId, ExamId, Score) VALUES (@StudentId, @ExamId, @Score)", conn))
            {
                cmd.Parameters.AddWithValue("@StudentId", mark.StudentId);
                cmd.Parameters.AddWithValue("@ExamId", mark.ExamId);
                cmd.Parameters.AddWithValue("@Score", mark.Score);
                cmd.ExecuteNonQuery();
            }
        }

// =======================================================================================================================================================================================

        public void UpdateMark(Mark mark)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("UPDATE Marks SET StudentId = @StudentId, ExamId = @ExamId, Score = @Score WHERE MarkID = @MarkId", conn))
            {
                cmd.Parameters.AddWithValue("@StudentId", mark.StudentId);
                cmd.Parameters.AddWithValue("@ExamId", mark.ExamId);
                cmd.Parameters.AddWithValue("@Score", mark.Score);
                cmd.Parameters.AddWithValue("@MarkId", mark.MarkId); // changed
                cmd.ExecuteNonQuery();
            }
        }

// =======================================================================================================================================================================================
        public void DeleteMark(int markId)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("DELETE FROM Marks WHERE MarkID = @MarkId", conn))
            {
                cmd.Parameters.AddWithValue("@MarkId", markId); // changed
                cmd.ExecuteNonQuery();
            }
        }
    }
}
