using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDD
{
    public class HotelDatabase
    {
        public static string ConnectionString = Path.Combine(AppContext.BaseDirectory, @"..\..\..\DoDoParipope.db");

        public static List<Rooms> GetRooms()
        {

            using var conn = new SQLiteConnection($"Data Source={ConnectionString};Version=3;");
            conn.Open();

            List<Rooms> rooms = new();
            using var cmd = new SQLiteCommand("SELECT * FROM Room", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                rooms.Add(new Rooms
                {
                    Id = reader.GetInt32(0),
                    Number = reader.GetString(1),
                    Layer = reader.GetString(2),
                    Category = reader.GetInt32(3),
                    Locked = reader.GetString(4)
                });
            }

            return rooms;
        }

        public static List<Guests> GetGuests()
        {
            using var conn = new SQLiteConnection($"Data Source={ConnectionString};Version=3;");
            conn.Open();

            List<Guests> guests = new();
            using var cmd = new SQLiteCommand("SELECT * FROM Guest", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                guests.Add(new Guests
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(2),
                    Surname = reader.GetString(1),
                    LastName = reader.GetString(3),
                    Phone = reader.GetString(4),
                    Passport = reader.GetString(5),
                    RoomID = reader.GetInt32(6)
                });
            }
            return guests;
        }
    }
}