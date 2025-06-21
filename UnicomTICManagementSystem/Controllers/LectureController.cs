using System.Collections.Generic;
using System.Data.SQLite;
//using UnicomTicManagementSystem.Models;
using UnicomTicManagementSystem.Repositories;
using UnicomTICManagementSystem.Models;

namespace UnicomTicManagementSystem.Controllers
{
    internal class LectureController
    {
        public List<Lecture> GetAllLectures()
        {
            var list = new List<Lecture>();
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT * FROM Lectures", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Lecture
                    {
                        LectureId = reader.GetInt32(0),
                        LectureName = reader.GetString(1),
                        LectureAddress = reader.GetString(2),
                        LectureNicNo = reader.GetInt32(3),
                        Subject = reader.GetString(4)
                    });
                }
            }
            return list;
        }

// ==============================================================================================================================================================================

        public void AddLecture(Lecture lecture)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("INSERT INTO Lectures (LectureName, LectureAddress, LectureNicNo, Subject) VALUES (@name, @addr, @nic, @subject)", conn))
            {
                cmd.Parameters.AddWithValue("@name", lecture.LectureName);
                cmd.Parameters.AddWithValue("@addr", lecture.LectureAddress);
                cmd.Parameters.AddWithValue("@nic", lecture.LectureNicNo);
                cmd.Parameters.AddWithValue("@subject", lecture.Subject);
                cmd.ExecuteNonQuery();
            }
        }


// ==============================================================================================================================================================================

        public void UpdateLecture(Lecture lecture)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("UPDATE Lectures SET LectureName = @name, LectureAddress = @addr, LectureNicNo = @nic, Subject = @subject WHERE LectureId = @id", conn))
            {
                cmd.Parameters.AddWithValue("@name", lecture.LectureName);
                cmd.Parameters.AddWithValue("@addr", lecture.LectureAddress);
                cmd.Parameters.AddWithValue("@nic", lecture.LectureNicNo);
                cmd.Parameters.AddWithValue("@subject", lecture.Subject);
                cmd.Parameters.AddWithValue("@id", lecture.LectureId);
                cmd.ExecuteNonQuery();
            }
        }

// ==============================================================================================================================================================================

        public void DeleteLecture(int lectureId)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("DELETE FROM Lectures WHERE LectureId = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", lectureId);
                cmd.ExecuteNonQuery();
            }
        }

// ==============================================================================================================================================================================

        public List<string> GetAllSubjects()
        {
            var subjects = new List<string>();
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT DISTINCT SubjectName FROM Subjects", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    subjects.Add(reader.GetString(0));
                }
            }
            return subjects;
        }
    }
}
