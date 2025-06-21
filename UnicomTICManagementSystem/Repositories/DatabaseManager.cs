using System;
using System.Data.SQLite;
using UnicomTicManagementSystem.Repositories;

namespace SchoolManageSystem.Data
{
    public static class DatabaseInitializer
    {
        public static void CreateTables()
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT,
                        Password TEXT,
                        Role TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Courses (
                        CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CourseName TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Subjects (
                        SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SubjectName TEXT,
                        CourseID INTEGER,
                        FOREIGN KEY(CourseID) REFERENCES Courses(CourseID)
                    );

                    CREATE TABLE IF NOT EXISTS Students (
                        StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT,
                        CourseID INTEGER,
                        Address TEXT,
                        Age INTEGER,
                        UserID INTEGER,
                        FOREIGN KEY(CourseID) REFERENCES Courses(CourseID),
                        FOREIGN KEY(UserID) REFERENCES Users(UserID)
                    );

                    CREATE TABLE IF NOT EXISTS Exams (
                        ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ExamName TEXT,
                        SubjectID INTEGER,
                        FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)
                    );

                  CREATE TABLE IF NOT EXISTS Marks (
                        MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentID INTEGER,
                        ExamID INTEGER,
                        Score INTEGER
                       
                    );


                    CREATE TABLE IF NOT EXISTS Rooms (
                        RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                        RoomName TEXT,
                        RoomType TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Timetables (
                        TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SubjectID INTEGER,
                        RoomType TEXT,
                        TimeSlot TEXT,
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
                    );



                    CREATE TABLE IF NOT EXISTS Staffs (
                        StaffId INTEGER PRIMARY KEY AUTOINCREMENT,
                        StaffName TEXT NOT NULL,
                        StaffAddress TEXT NOT NULL,
                        StaffNicNo INTEGER NOT NULL
                        
                    );
                       
                    CREATE TABLE IF NOT EXISTS Lectures (
                        LectureId INTEGER PRIMARY KEY AUTOINCREMENT,
                        LectureName TEXT NOT NULL,
                        LectureAddress TEXT NOT NULL,
                        LectureNicNo INTEGER NOT NULL,
                        Subject TEXT NOT NULL
                    );
                   CREATE TABLE IF NOT EXISTS Exams (
                        ExamId INTEGER PRIMARY KEY AUTOINCREMENT,
                        ExamName TEXT NOT NULL,
                        SubjectName TEXT NOT NULL,
                        FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId)
                    );

                ";


                cmd.ExecuteNonQuery();
            }
        }
    }
}
