using Models;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Repositories
{
    public class AdoTicketRepository : ITicketRepository
    {
        private readonly string _connectionString;

        public AdoTicketRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Ticket CreateTicket(Ticket model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"INSERT INTO {nameof(Ticket)} ({nameof(Ticket.TicketNumber)}, {nameof(Ticket.CreatedAt)}) VALUES ({model.TicketNumber}, {model.CreatedAt})", connection);
                command.ExecuteNonQuery();
            }
            return model;
        }

        public int GetLastTicketNumber(DateTime date)
        {
            int ticketNumber = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM Tickets WHERE {nameof(Ticket.CreatedAt)} = {date} ", connection);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ticketNumber = (int)reader["Id"];
                    }
                }
            }
            return ticketNumber;
        }

        public void UpdateStatus(TicketInRooms model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"UPDATE {nameof(TicketInRooms)} SET {nameof(TicketInRooms.StatusId)} = {model.StatusId} WHERE {nameof(TicketInRooms.Id)} = {model.Id}", connection);
                command.ExecuteNonQuery();
            }
        }

        void ITicketRepository.CopyToNextRoom(int ticketId)
        {
            throw new NotImplementedException();
        }

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
                            Room = new Room
                            {
                                Department =
                                            new Department
                                            {
                                                Id = (int)reader["Id"],
                                                Name = reader["DepartmanName"].ToString(),
                                            },
                                Name = reader["RoomName"].ToString(),


                            },
                            Ticket = new Ticket
                            {
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal(nameof(Ticket.CreatedAt))),
                                TicketNumber = (int)reader["TicketNumber"],
                            },
                            //StatusId = reader.GetInt32(reader.GetOrdinal(nameof(TicketInRooms.StatusId))),
                            CalledAt = reader.GetDateTime(reader.GetOrdinal(nameof(Ticket.CreatedAt))),
                        };
                        result.Add(ticketInRoom);
                    }
                    return result;
                }
            }
        }

        int ITicketRepository.GetLastTicketNumber(DateTime date, int departmentId)
        {
            throw new NotImplementedException();
        }

        StatusEnum ITicketRepository.GetStatus(int id)
        {
            throw new NotImplementedException();
        }

        TicketInRooms ITicketRepository.GetTicketInRoomById(int id)
        {
            throw new NotImplementedException();
        }

        List<TicketInRooms> ITicketRepository.GetTodayInProgressTickets()
        {
            throw new NotImplementedException();
        }
    }
}
