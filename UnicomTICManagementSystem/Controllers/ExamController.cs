using System;
using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTicManagementSystem.Repositories; 

namespace UnicomTICManagementSystem.Controllers
{
    public class ExamController
    {
        public void AddExam(Exam exam)
        {
            using (var connection = DbConfig.GetConnection())
            {
                string query = "INSERT INTO Exams (ExamName, SubjectId) VALUES (@ExamName, @SubjectId)";
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@ExamName", exam.ExamName);
                command.Parameters.AddWithValue("@SubjectId", exam.SubjectId);
                command.ExecuteNonQuery();
            }
        }

// =============================================================================================================================================================

        public List<Exam> GetAllExams()
        {
            var exams = new List<Exam>();
            using (var connection = DbConfig.GetConnection())
            {
                string query = "SELECT * FROM Exams";
                var command = new SQLiteCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    exams.Add(new Exam
                    {
                        ExamId = Convert.ToInt32(reader["ExamId"]),
                        ExamName = reader["ExamName"].ToString(),
                        SubjectId = Convert.ToInt32(reader["SubjectId"])
                    });
                }
            }
            return exams;
        }

// ==============================================================================================================================================================

        public void UpdateExam(Exam exam)
        {
            using (var connection = DbConfig.GetConnection())
            {
                string query = "UPDATE Exams SET ExamName=@ExamName, SubjectId=@SubjectId WHERE ExamId=@ExamId";
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@ExamName", exam.ExamName);
                command.Parameters.AddWithValue("@SubjectId", exam.SubjectId);
                command.Parameters.AddWithValue("@ExamId", exam.ExamId);
                command.ExecuteNonQuery();
            }
        }

// =============================================================================================================================================================

        public void DeleteExam(int examId)
        {
            using (var connection = DbConfig.GetConnection())
            {
                string query = "DELETE FROM Exams WHERE ExamId=@ExamId";
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@ExamId", examId);
                command.ExecuteNonQuery();
            }
        }
    }
}
