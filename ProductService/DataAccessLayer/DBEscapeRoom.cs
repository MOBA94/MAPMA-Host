using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ModelLayer;
using System.Data;

namespace ProductService.DataAccessLayer {
    class DBEscapeRoom : IESCAPEROOM<EscapeRoom> {

        private string _connectionString;

        public DBEscapeRoom() {
            _connectionString = DB.DbConnectionString;
        }

        public void Create ( string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId, byte[] img )
        {
            EscapeRoom escapeRoom = new EscapeRoom();
            String tempCheck;
            DBEmployee DBemp = new DBEmployee();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (IDbTransaction tran = connection.BeginTransaction())
                    try
                    {
                        using (SqlCommand cmdReadERs = connection.CreateCommand())
                        {
                            cmdReadERs.CommandText = "INSERT INTO EscapeRoom(EsName, EsDescription, MaxClearTime, CleanTime, Price, Rating, EmployeeID, Image)" +
                            "VALUES(@EsName, @EsDescription, @MaxClearTime, @CleanTime, @Price, @Rating, @EmployeeID, @Image)";
                            cmdReadERs.Transaction = tran as SqlTransaction;
                            cmdReadERs.Parameters.AddWithValue("EsName", name);
                            cmdReadERs.Parameters.AddWithValue("EsDescription", description);
                            cmdReadERs.Parameters.AddWithValue("MaxClearTime", maxClearTime);
                            cmdReadERs.Parameters.AddWithValue("CleanTime", cleanTime);
                            cmdReadERs.Parameters.AddWithValue("Price", price);
                            cmdReadERs.Parameters.AddWithValue("Rating", rating);
                            cmdReadERs.Parameters.AddWithValue("EmployeeId", empId);
                            if (img != null && img.Length > 0) {
                                cmdReadERs.Parameters.AddWithValue("Image", img);
                            }
                            else {
                                cmdReadERs.Parameters.Add("Image", System.Data.SqlDbType.VarBinary, -1);
                                cmdReadERs.Parameters["Image"].Value = DBNull.Value;
                            }
                            cmdReadERs.ExecuteNonQuery();
                            tran.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Console.WriteLine(e);
                        Console.ReadLine();
                    }
            }
        }

        public void Delete(int id) {
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdReadERs = connection.CreateCommand()) {
                    cmdReadERs.CommandText = "DELETE FROM EscapeRoom WHERE EscapeRoomID = @EscapeRoomID";
                    cmdReadERs.Parameters.AddWithValue("EscapeRoomID", id);
                    cmdReadERs.ExecuteNonQuery();
                }
            }
        }

        public EscapeRoom GetForOwner(int ER_ID) {
            EscapeRoom escapeRoom = new EscapeRoom();
            String tempCheck;
            DBEmployee DBemp = new DBEmployee();

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdReadERs = connection.CreateCommand()) {


                    cmdReadERs.CommandText = "SELECT EscapeRoom.*, CheckList.CheckList From EscapeRoom LEFT JOIN CheckList ON " +
                        "EscapeRoom.EscapeRoomID = CheckList.EscapeRoomID WHERE EscapeRoom.EscapeRoomID =@EscapeRoomID";
                    cmdReadERs.Parameters.AddWithValue("EscapeRoomID", ER_ID);
                    SqlDataReader reader = cmdReadERs.ExecuteReader();
                    if (reader.Read()) {

                        escapeRoom.escapeRoomID = reader.GetInt32(reader.GetOrdinal("EscapeRoomID"));
                        escapeRoom.name = reader.GetString(reader.GetOrdinal("EsName"));
                        escapeRoom.price = reader.GetDecimal(reader.GetOrdinal("Price"));
                        escapeRoom.maxClearTime = reader.GetInt32(reader.GetOrdinal("MaxClearTime"));
                        escapeRoom.cleanTime = reader.GetInt32(reader.GetOrdinal("CleanTime"));
                        escapeRoom.description = reader.GetString(reader.GetOrdinal("EsDescription"));
                        escapeRoom.rating = reader.GetDecimal(reader.GetOrdinal("Rating"));
                        escapeRoom.emp = DBemp.Get(reader.GetInt32(reader.GetOrdinal("EmployeeID")));
                        escapeRoom.Image = (byte[])reader.GetSqlBinary(reader.GetOrdinal("Image"));


                        int i = 0;

                        //while (reader.GetString(reader.GetOrdinal("CheckList")).Length >= i) {
                        //    tempCheck = reader.GetString(reader.GetOrdinal("CheckList"));
                        //    escapeRoom.AddToList(tempCheck.ToString());
                        //    i++;
                        //}


                    }
                }
            }
            return escapeRoom;
        }

        public IEnumerable<EscapeRoom> GetAllForOwner() {
            List<EscapeRoom> EscapeRooms = new List<EscapeRoom>();
            EscapeRoom tempER;
            String tempCheck;
            DBEmployee EmpDB = new DBEmployee();
            ;

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdReadAllEs = connection.CreateCommand()) {

                    cmdReadAllEs.CommandText = "SELECT EscapeRoom.*, CheckList.CheckList FROM EscapeRoom LEFT JOIN CheckList ON EscapeRoom.EscapeRoomID = CheckList.EscapeRoomID ";
                    SqlDataReader reader = cmdReadAllEs.ExecuteReader();

                    while (reader.Read()) {
                        tempER = new EscapeRoom();
                        tempER.escapeRoomID = reader.GetInt32(reader.GetOrdinal("EscapeRoomID"));
                        tempER.name = reader.GetString(reader.GetOrdinal("EsName"));
                        tempER.price = reader.GetDecimal(reader.GetOrdinal("Price"));
                        tempER.maxClearTime = reader.GetInt32(reader.GetOrdinal("MaxClearTime"));
                        tempER.cleanTime = reader.GetInt32(reader.GetOrdinal("CleanTime"));
                        tempER.description = reader.GetString(reader.GetOrdinal("EsDescription"));
                        tempER.rating = reader.GetDecimal(reader.GetOrdinal("Rating"));
                        tempER.emp = EmpDB.Get(reader.GetInt32(reader.GetOrdinal("EmployeeID")));
                        tempER.Image = (byte[])reader.GetSqlBinary(reader.GetOrdinal("Image"));



                        int i = 0;

                        //while (reader.GetString(reader.GetOrdinal("CheckList")).Length > i) {
                        //    tempCheck = reader.GetString(reader.GetOrdinal("CheckList"));
                        //    tempER.AddToList(tempCheck);
                        //    i++;
                        //}

                        EscapeRooms.Add(tempER);
                    }

                }
            }
            return EscapeRooms;
        }

        public void Update(EscapeRoom ER) {

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (IDbTransaction tran = connection.BeginTransaction())
                    try
                    {
                        using (SqlCommand cmdUpdateRoom = connection.CreateCommand())
                        {

                            cmdUpdateRoom.CommandText = "UPDATE EscapeRoom SET EsName = @EsName, EsDescription = @EsDescription, Price = @Price, MaxClearTime = @MaxClearTime, CleanTime = @CleanTime, Rating = @Rating, EmployeeID = @EmployeeID, Image = @Image  WHERE EscapeRoomID = @EscapeRoomID";
                            cmdUpdateRoom.Transaction = tran as SqlTransaction;
                            cmdUpdateRoom.Parameters.AddWithValue("EsName", ER.name);
                            cmdUpdateRoom.Parameters.AddWithValue("EsDescription", ER.description);
                            cmdUpdateRoom.Parameters.AddWithValue("Price", ER.price);
                            cmdUpdateRoom.Parameters.AddWithValue("MaxClearTime", ER.maxClearTime);
                            cmdUpdateRoom.Parameters.AddWithValue("CleanTime", ER.cleanTime);
                            cmdUpdateRoom.Parameters.AddWithValue("Rating", ER.rating);
                            cmdUpdateRoom.Parameters.AddWithValue("EmployeeID", ER.emp.employeeID);
                            cmdUpdateRoom.Parameters.AddWithValue("EscapeRoomID", ER.escapeRoomID);
                            if (ER.Image != null)
                            {
                                cmdUpdateRoom.Parameters.AddWithValue("Image", ER.Image);
                            }
                            else {
                                cmdUpdateRoom.Parameters.Add("Image", System.Data.SqlDbType.VarBinary, -1);
                                cmdUpdateRoom.Parameters["Image"].Value = DBNull.Value;
                            }
                            cmdUpdateRoom.ExecuteNonQuery();
                            tran.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Console.WriteLine(e);
                        Console.ReadLine();
                    }
            }
        }
    }
}
