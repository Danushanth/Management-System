using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTicManagementSystem.Repositories;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Controllers
{
    internal class SubjectController
    {
        public List<Subject> GetAllSubjects()
        {
            var list = new List<Subject>();
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand(@"
                SELECT s.SubjectID, s.SubjectName, s.CourseID, c.CourseName
                FROM Subjects s
                JOIN Courses c ON s.CourseID = c.CourseID", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Subject
                    {
                        SubjectId = reader.GetInt32(0),
                        SubjectName = reader.GetString(1),
                        CourseId = reader.GetInt32(2),
                        CourseName = reader.GetString(3)
                    });
                }
            }
            return list;
        }

// ==============================================================================================================================================================================

        public void AddSubject(string name, int courseId)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("INSERT INTO Subjects (SubjectName, CourseID) VALUES (@name, @cid)", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@cid", courseId);
                cmd.ExecuteNonQuery();
            }
        }

// ==============================================================================================================================================================================

        public void UpdateSubject(int id, string name, int courseId)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("UPDATE Subjects SET SubjectName = @name, CourseID = @cid WHERE SubjectID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@cid", courseId);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

// ==============================================================================================================================================================================
        public void DeleteSubject(int id)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("DELETE FROM Subjects WHERE SubjectID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
