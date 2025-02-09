using Models;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repositories
{
    public class AdoTicketRepository : ITicketRepository
    {
        private readonly string _connectionString;

        List<TicketInRooms> ITicketRepository.GetAllTodayTicketByRoomId(int id)
        {
            var today = DateTime.Now.Date;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = $@"
                                    SELECT tir.Id, d.Name as DepartmanName , r.Name as RoomName  , t.TicketNumber ,t.CreatedAt , StatusId
                                    FROM TicketInRooms tir
                                    Inner join  Rooms r ON tir.RoomId = r.Id
                                    Inner join  Tickets t ON tir.TicketId = t.Id
                                    Inner join Departments d on d.Id = r.DepartmentId
                                    WHERE tir.RoomId = {id} AND t.CreatedAt = '{today}'
                                    ORDER BY tir.StatusId, tir.CalledAt DESC ";
                var command = new SqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    var result = new List<TicketInRooms>();
                    while (reader.Read())
                    {
                        var ticketInRoom = new TicketInRooms
                        {
                            Id = (int)reader["Id"],
                            Room = new Room
                            {
                                Department =
                                            new Department
                                            {
                                                Name = reader["DepartmanName"].ToString(),
                                            },
                                Name = reader["RoomName"].ToString(),
                            },
                            Ticket = new Ticket
                            {
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal(nameof(Ticket.CreatedAt))),
                                TicketNumber = (int)reader[nameof(Ticket.TicketNumber)],
                            },
                            StatusId = (StatusEnum)reader.GetInt32(reader.GetOrdinal("StatusId")),
                            CalledAt = reader.GetDateTime(reader.GetOrdinal(nameof(Ticket.CreatedAt))),
                        };
                        result.Add(ticketInRoom);
                    }
                    return result;
                }
            }
        }
        public int GetLastTicketNumberByDepartmentIdForToday(int departmentId)
        {
            var today = DateTime.Now.Date;
            var ticketNumber = 0;
            string query = @"
                            select top(1) TicketNumber from Tickets as t
                            inner join TicketInRooms as tir
                            on tir.TicketId = t.Id
                            inner join Rooms as r
                            on tir.RoomId = r.Id and r.DepartmentId = @DepartmentId
                            where CreatedAt = @Today
                            order by TicketNumber desc";
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Today", today);
                command.Parameters.AddWithValue("@DepartmentId", departmentId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ticketNumber = reader.GetInt32(reader.GetOrdinal("TicketNumber"));
                }
            }
            return ticketNumber;
        }
        List<TicketInRooms> ITicketRepository.GetTicketsInProgressForToday()
        {
            var today = DateTime.Now.Date;
            var tickets = new List<TicketInRooms>();

            string query = @"
                            SELECT r.Name as RoomName, t.TicketNumber
                            FROM TicketInRooms tir
                            INNER JOIN Rooms r ON tir.RoomId = r.Id
                            INNER JOIN Tickets t ON tir.TicketId = t.Id
                            WHERE t.CreatedAt = @Today
                            AND tir.StatusId = @StatusId
                            ORDER BY tir.CalledAt DESC";

            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Today", today);
                command.Parameters.AddWithValue("@StatusId", (int)StatusEnum.InProgress);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var ticketInRoom = new TicketInRooms
                    {

                        Room = new Room
                        {
                            Name = reader.GetString(reader.GetOrdinal("RoomName"))
                        },
                        Ticket = new Ticket
                        {
                            TicketNumber = reader.GetInt32(reader.GetOrdinal("TicketNumber")),
                        }
                    };

                    tickets.Add(ticketInRoom);
                }
            }

            return tickets;
        }
        TicketInRooms ITicketRepository.GetTicketInRoomById(int id)
        {
            throw new NotImplementedException();
        }
        public void CreateTicketWithTicketInRoom(Ticket moodel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        CreateTicket(moodel);

                        var tir = new TicketInRooms
                        {
                            TicketId = moodel.Id,
                            RoomId = moodel.TicketInRooms.FirstOrDefault().RoomId,
                            CalledAt = moodel.TicketInRooms.FirstOrDefault().CalledAt,
                            StatusId = moodel.TicketInRooms.FirstOrDefault().StatusId,
                        };
                        CreateTicketInRoom(tir);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        void ITicketRepository.CopyToNextRoom(int ticketId)
        {
            throw new NotImplementedException();
        }
        public AdoTicketRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void CreateTicketInRoom(TicketInRooms model)
        {
            var query = $@"INSERT INTO {nameof(Ticket.TicketInRooms)}
                                (
                                 {nameof(TicketInRooms.TicketId)},
                                 {nameof(TicketInRooms.CalledAt)},
                                 {nameof(TicketInRooms.RoomId)},
                                 {nameof(TicketInRooms.StatusId)}
                                ) 
                                VALUES 
                                (
                                 {model.TicketId},
                                 '{model.CalledAt}',
                                 {model.RoomId},
                                 {(int)model.StatusId}
                                )";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        StatusEnum ITicketRepository.GetStatus(int id)
        {
            var statusId = new StatusEnum();
            string query = @"
                            select StatusId from TicketInRooms
                            where Id = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    statusId = (StatusEnum)reader.GetInt32(reader.GetOrdinal("StatusId"));
                }
            }
            return statusId;
        }
        public void UpdateStatus(TicketInRooms model)
        {
            string query = $@"UPDATE {nameof(TicketInRooms)} 
                            SET {nameof(TicketInRooms.StatusId)} = {(int)model.StatusId},
                                {nameof(TicketInRooms.CalledAt)} = '{DateTime.Now}'
                            WHERE {nameof(TicketInRooms.Id)} = {model.Id}";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }

        public Ticket CreateTicket(Ticket model)
        {
            var query = $@"INSERT INTO Tickets
                                ({nameof(Ticket.TicketNumber)}, {nameof(Ticket.CreatedAt)}) 
                                OUTPUT INSERTED.Id
                                VALUES 
                                ({model.TicketNumber}, '{model.CreatedAt}')
                                ";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model.Id = reader.GetInt32(0);
                    }
                }
                return model;
            }
        }
    }
}
