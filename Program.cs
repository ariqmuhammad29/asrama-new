using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Mysqlx.Expr;

// Mahasiswa Model
class MahasiswaModel
{
    private string connectionString = "Server=localhost;Database=asrama_puni;User ID=root;Password=;";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    public void Create(string name, long nim, string email, long phone, string address)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string checkNimQuery = "SELECT COUNT(*) FROM mahasiswa_0405 WHERE NIM = @nim";
                using (var checkNimCmd = new MySqlCommand(checkNimQuery, connection))
                {
                    checkNimCmd.Parameters.AddWithValue("@nim", nim);
                    long count = Convert.ToInt64(checkNimCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        Console.WriteLine("NIM sudah digunakan. Data tidak dapat ditambahkan.");
                        return;
                    }
                }

                string getMaxIdQuery = "SELECT IFNULL(MAX(MahasiswaID), 0) FROM mahasiswa_0405";
                long newId;
                using (var getMaxIdCmd = new MySqlCommand(getMaxIdQuery, connection))
                {
                    newId = Convert.ToInt64(getMaxIdCmd.ExecuteScalar()) + 1;
                }

                string query = "INSERT INTO mahasiswa_0405 (MahasiswaID, NamaMahasiswa, NIM, Email, Telepon, Alamat) " +
                               "VALUES (@id, @name, @nim, @email, @telepon, @alamat)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", newId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@nim", nim);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@telepon", phone);
                    cmd.Parameters.AddWithValue("@alamat", address);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Data berhasil ditambahkan!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data: {ex.Message}");
        }
    }


    public void GetData()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM mahasiswa_0405";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nData Mahasiswa:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama | NIM | Email | Telepon | Alamat");
                        Console.WriteLine("-----------------------------------------------------------");
                        while (reader.Read())
                        {
                            long id = reader.GetInt64("MahasiswaID");
                            string name = reader.GetString("NamaMahasiswa");
                            long nim = reader.GetInt64("NIM");
                            string email = reader.GetString("Email");
                            string phone = reader.GetString("Telepon");
                            string address = reader.GetString("Alamat");

                            Console.WriteLine($"{id} | {name} | {nim} | {email} | {phone} | {address}");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data: {ex.Message}");
        }
    }

    public void Delete(long id)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM mahasiswa_0405 WHERE MahasiswaID = @id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data berhasil dihapus!");
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data: {ex.Message}");
        }
    }

    public void Update(long id, string name, long nim, string email, long phone, string address)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE mahasiswa_0405 SET NamaMahasiswa = @name, NIM = @nim, Email = @email, Telepon = @telepon, Alamat = @alamat WHERE MahasiswaID = @id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@nim", nim);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@telepon", phone);
                    cmd.Parameters.AddWithValue("@alamat", address);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data berhasil diperbarui!");
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data: {ex.Message}");
        }
    }

    public void Search(string keyword)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = @"
                SELECT * FROM mahasiswa_0405
                WHERE NamaMahasiswa LIKE @keyword 
                   OR NIM LIKE @keyword 
                   OR Email LIKE @keyword
                   OR Alamat LIKE @keyword";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nHasil Pencarian:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama | NIM | Email | Telepon | Alamat");
                        Console.WriteLine("-----------------------------------------------------------");

                        while (reader.Read())
                        {
                            long id = reader.GetInt64("MahasiswaID");
                            string name = reader.GetString("NamaMahasiswa");
                            long nim = reader.GetInt64("NIM");
                            string email = reader.GetString("Email");
                            string phone = reader.GetString("Telepon");
                            string address = reader.GetString("Alamat");

                            Console.WriteLine($"{id} | {name} | {nim} | {email} | {phone} | {address}");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mencari data: {ex.Message}");
        }
    }
}


// HelpDesk Model
class HelpDeskModel
{
    private string connectionString = "Server=localhost;Database=asrama_puni;User ID=root;Password=;";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    public void Create(string name, string email, long phone)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO helpdesk_0405 (HelpDeskID, NamaHelpDesk, Email, Telepon) VALUES (@id, @name, @email, @telepon)";

                string getMaxIdQuery = "SELECT IFNULL(MAX(HelpDeskID), 0) FROM helpdesk_0405";
                long newId;
                using (var getMaxIdCmd = new MySqlCommand(getMaxIdQuery, connection))
                {
                    newId = Convert.ToInt64(getMaxIdCmd.ExecuteScalar()) + 1;
                }

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", newId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@telepon", phone);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Data berhasil ditambahkan!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data: {ex.Message}");
        }
    }

    public void GetData()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM helpdesk_0405";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nData HelpDesk:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama | Email | Telepon");
                        Console.WriteLine("-----------------------------------------------------------");
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("HelpDeskID");
                            string name = reader.GetString("NamaHelpDesk");
                            string email = reader.GetString("Email");
                            string phone = reader.GetString("Telepon");

                            Console.WriteLine($"{id} | {name} | {email} | {phone}");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data: {ex.Message}");
        }
    }

    public void Update(long id, string name, string email, long phone)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE helpdesk_0405 SET NamaHelpDesk = @name, Email = @email, Telepon = @telepon WHERE HelpDeskID = @id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@telepon", phone);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data berhasil diperbarui!");
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data: {ex.Message}");
        }
    }

    public void Delete(long id)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM helpdesk_0405 WHERE HelpDeskID = @id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data berhasil dihapus!");
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data: {ex.Message}");
        }
    }

    public void Search(string keyword)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM helpdesk_0405 " +
                               "WHERE NamaHelpDesk LIKE @keyword OR Email LIKE @keyword OR Telepon LIKE @keyword";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nHasil Pencarian HelpDesk:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama | Email | Telepon");
                        Console.WriteLine("-----------------------------------------------------------");
                        bool hasResults = false;
                        while (reader.Read())
                        {
                            hasResults = true;
                            int id = reader.GetInt32("HelpDeskID");
                            string name = reader.GetString("NamaHelpDesk");
                            string email = reader.GetString("Email");
                            string phone = reader.GetString("Telepon");

                            Console.WriteLine($"{id} | {name} | {email} | {phone}");
                        }
                        if (!hasResults)
                        {
                            Console.WriteLine("Tidak ada data yang ditemukan.");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mencari data: {ex.Message}");
        }
    }

}

// Kamar Model
class KamarModel
{
    private string connectionString = "Server=localhost;Database=asrama_puni;User ID=root;Password=;";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    public void Create(string name, long capacity, long stock)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO kamarasrama_0405 (KamarID, NamaKamar, Kapasitas, Tersedia) VALUES (@id, @name, @capacity, @stock)";

                string getMaxIdQuery = "SELECT IFNULL(MAX(KamarID), 0) FROM kamarasrama_0405";
                long newId;
                using (var getMaxIdCmd = new MySqlCommand(getMaxIdQuery, connection))
                {
                    newId = Convert.ToInt64(getMaxIdCmd.ExecuteScalar()) + 1;
                }

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", newId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@capacity", capacity);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Data berhasil ditambahkan!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data: {ex.Message}");
        }
    }

    public void GetData()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM kamarasrama_0405";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nData Kamar Asrama:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama Kamar | Kapasitas | Tersedia");
                        Console.WriteLine("-----------------------------------------------------------");
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("KamarID");
                            string name = reader.GetString("NamaKamar");
                            long capacity = reader.GetInt64("Kapasitas");
                            long stock = reader.GetInt64("Tersedia");

                            Console.WriteLine($"{id} | {name} | {capacity} | {stock}");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data: {ex.Message}");
        }
    }

    public void Update(long id, string name, long capacity, long stock)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE kamarasrama_0405 SET NamaKamar = @name, Kapasitas = @capacity, Tersedia = @stock WHERE KamarID = @id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@capacity", capacity);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data berhasil diperbarui!");
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data: {ex.Message}");
        }
    }

    public void Delete(long id)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM kamarasrama_0405 WHERE KamarID = @id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data berhasil dihapus!");
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data: {ex.Message}");
        }
    }

    public void Search(string keyword)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM kamarasrama_0405 " +
                               "WHERE NamaKamar LIKE @keyword OR Kapasitas LIKE @keyword OR Tersedia LIKE @keyword";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nHasil Pencarian Kamar:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama | Kapasitas | Tersedia");
                        Console.WriteLine("-----------------------------------------------------------");
                        bool hasResults = false;
                        while (reader.Read())
                        {
                            hasResults = true;
                            int id = reader.GetInt32("KamarID");
                            string name = reader.GetString("NamaKamar");
                            int capacity = reader.GetInt32("Kapasitas");
                            int stock = reader.GetInt32("Tersedia");

                            Console.WriteLine($"{id} | {name} | {capacity} | {stock}");
                        }
                        if (!hasResults)
                        {
                            Console.WriteLine("Tidak ada data yang ditemukan.");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mencari data: {ex.Message}");
        }
    }

}

// Reservasi Model
class ReservasiModel
{
    private string connectionString = "Server=localhost;Database=asrama_puni;User ID=root;Password=;";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    public void GetDataMahasiswa()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM mahasiswa_0405";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nData Mahasiswa:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama");
                        Console.WriteLine("-----------------------------------------------------------");
                        while (reader.Read())
                        {
                            long id = reader.GetInt64("MahasiswaID");
                            string name = reader.GetString("NamaMahasiswa");

                            Console.WriteLine($"{id} | {name}");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data: {ex.Message}");
        }
    }

    public void GetDataKamar()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM kamarasrama_0405";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nData Kamar Asrama:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama Kamar");
                        Console.WriteLine("-----------------------------------------------------------");
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("KamarID");
                            string name = reader.GetString("NamaKamar");

                            Console.WriteLine($"{id} | {name}");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data: {ex.Message}");
        }
    }

    public void Create(long mahasiswaId, long kamarId, string dateIn, string dateOut, string status)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO reservasikamar_0405 (ReservasiID, MahasiswaID, KamarID, TanggalMasuk, TanggalKeluar, StatusReservasi) VALUES (@id, @mahasiswaId, @kamarId, @tglMasuk, @tglKeluar, @status)";

                string getMaxIdQuery = "SELECT IFNULL(MAX(ReservasiID), 0) FROM reservasikamar_0405";
                long newId;
                using (var getMaxIdCmd = new MySqlCommand(getMaxIdQuery, connection))
                {
                    newId = Convert.ToInt64(getMaxIdCmd.ExecuteScalar()) + 1;
                }

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", newId);
                    cmd.Parameters.AddWithValue("@mahasiswaId", mahasiswaId);
                    cmd.Parameters.AddWithValue("@kamarId", kamarId);
                    cmd.Parameters.AddWithValue("@tglMasuk", dateIn);
                    cmd.Parameters.AddWithValue("@tglKeluar", dateOut);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Data berhasil ditambahkan!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data: {ex.Message}");
        }
    }

    public void GetData()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = @"
            SELECT 
                r.ReservasiID,
                m.NamaMahasiswa,
                k.NamaKamar,
                r.TanggalMasuk,
                r.TanggalKeluar,
                r.StatusReservasi
            FROM 
                reservasikamar_0405 r
            INNER JOIN 
                mahasiswa_0405 m ON r.MahasiswaID = m.MahasiswaID
            INNER JOIN 
                kamarasrama_0405 k ON r.KamarID = k.KamarID";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nData Reservasi Kamar:");
                        Console.WriteLine("----------------------------------------------------------------------------");
                        Console.WriteLine("ID Reservasi | Nama Mahasiswa | Nama Kamar | Tanggal Masuk | Tanggal Keluar | Status");
                        Console.WriteLine("----------------------------------------------------------------------------");

                        while (reader.Read())
                        {
                            long reservasiId = reader.GetInt64("ReservasiID");
                            string mahasiswaName = reader.GetString("NamaMahasiswa");
                            string kamarName = reader.GetString("NamaKamar");

                            string tanggalMasukStr = reader.IsDBNull(reader.GetOrdinal("TanggalMasuk"))
                                ? "NULL"
                                : reader.GetDateTime("TanggalMasuk").ToString("yyyy-MM-dd");
                            string tanggalKeluarStr = reader.IsDBNull(reader.GetOrdinal("TanggalKeluar"))
                                ? "NULL"
                                : reader.GetDateTime("TanggalKeluar").ToString("yyyy-MM-dd");

                            string status = reader.IsDBNull(reader.GetOrdinal("StatusReservasi"))
                                ? "NULL"
                                : reader.GetString("StatusReservasi");

                            Console.WriteLine($"{reservasiId} | {mahasiswaName} | {kamarName} | {tanggalMasukStr} | {tanggalKeluarStr} | {status}");
                        }

                        Console.WriteLine("----------------------------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data reservasi kamar: {ex.Message}");
        }
    }

    public void Search(string keyword)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT 
                r.ReservasiID,
                m.NamaMahasiswa,
                k.NamaKamar,
                r.TanggalMasuk,
                r.TanggalKeluar,
                r.StatusReservasi
            FROM 
                reservasikamar_0405 r
            INNER JOIN 
                mahasiswa_0405 m ON r.MahasiswaID = m.MahasiswaID
            INNER JOIN 
                kamarasrama_0405 k ON r.KamarID = k.KamarID
            WHERE 
                m.NamaMahasiswa LIKE @keyword 
                OR k.NamaKamar LIKE @keyword 
                OR r.StatusReservasi LIKE @keyword";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nHasil Pencarian Reservasi Kamar:");
                        Console.WriteLine("----------------------------------------------------------------------------");
                        Console.WriteLine("ID Reservasi | Nama Mahasiswa | Nama Kamar | Tanggal Masuk | Tanggal Keluar | Status");
                        Console.WriteLine("----------------------------------------------------------------------------");

                        bool hasResults = false;

                        while (reader.Read())
                        {
                            hasResults = true;
                            long reservasiId = reader.GetInt64("ReservasiID");
                            string mahasiswaName = reader.GetString("NamaMahasiswa");
                            string kamarName = reader.GetString("NamaKamar");

                            string tanggalMasukStr = reader.IsDBNull(reader.GetOrdinal("TanggalMasuk"))
                                ? "NULL"
                                : reader.GetDateTime("TanggalMasuk").ToString("yyyy-MM-dd");
                            string tanggalKeluarStr = reader.IsDBNull(reader.GetOrdinal("TanggalKeluar"))
                                ? "NULL"
                                : reader.GetDateTime("TanggalKeluar").ToString("yyyy-MM-dd");

                            string status = reader.IsDBNull(reader.GetOrdinal("StatusReservasi"))
                                ? "NULL"
                                : reader.GetString("StatusReservasi");

                            Console.WriteLine($"{reservasiId} | {mahasiswaName} | {kamarName} | {tanggalMasukStr} | {tanggalKeluarStr} | {status}");
                        }

                        if (!hasResults)
                        {
                            Console.WriteLine("Tidak ada data yang ditemukan.");
                        }

                        Console.WriteLine("----------------------------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mencari data reservasi: {ex.Message}");
        }
    }


    public void Update(long reservasiId, long mahasiswaId, long kamarId, string dateIn, string dateOut, string status)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = @"
            UPDATE reservasikamar_0405 
            SET 
                MahasiswaID = @mahasiswaId,
                KamarID = @kamarId,
                TanggalMasuk = @tanggalMasuk,
                TanggalKeluar = @tanggalKeluar,
                StatusReservasi = @status
            WHERE 
                ReservasiID = @reservasiId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@reservasiId", reservasiId);
                    cmd.Parameters.AddWithValue("@mahasiswaId", mahasiswaId);
                    cmd.Parameters.AddWithValue("@kamarId", kamarId);

                    if (!string.IsNullOrEmpty(dateIn))
                    {
                        cmd.Parameters.AddWithValue("@tanggalMasuk", dateIn);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@tanggalMasuk", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(dateOut))
                    {
                        cmd.Parameters.AddWithValue("@tanggalKeluar", dateOut);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@tanggalKeluar", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@status", status);

                    // Menjalankan query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data reservasi berhasil diperbarui!");
                    }
                    else
                    {
                        Console.WriteLine("Reservasi dengan ID tersebut tidak ditemukan.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data reservasi: {ex.Message}");
        }
    }


    public void Delete(long id)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM reservasikamar_0405 WHERE ReservasiID = @id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data berhasil dihapus!");
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data: {ex.Message}");
        }
    }
}

// Pengaduan Model
class PengaduanModel
{
    private string connectionString = "Server=localhost;Database=asrama_puni;User ID=root;Password=;";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    public void GetDataMahasiswa()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM mahasiswa_0405";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nData Mahasiswa:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama");
                        Console.WriteLine("-----------------------------------------------------------");
                        while (reader.Read())
                        {
                            long id = reader.GetInt64("MahasiswaID");
                            string name = reader.GetString("NamaMahasiswa");

                            Console.WriteLine($"{id} | {name}");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data: {ex.Message}");
        }
    }

    public void GetDataHelpdesk()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM helpdesk_0405";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nData HelpDesk:");
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("ID | Nama");
                        Console.WriteLine("-----------------------------------------------------------");
                        while (reader.Read())
                        {
                            long id = reader.GetInt64("HelpDeskID");
                            string name = reader.GetString("NamaHelpDesk");

                            Console.WriteLine($"{id} | {name}");
                        }
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data: {ex.Message}");
        }
    }

    public void Create(long mahasiswaId, long helpDeskId, string date, string description, string status)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO pengaduanhelpdesk_0405 (PengaduanID, MahasiswaID, TanggalPengaduan, Deskripsi, StatusPengaduan, HelpDeskID) VALUES (@id, @mahasiswaId, @tgl, @deskripsi, @status, @helpdeskId)";

                string getMaxIdQuery = "SELECT IFNULL(MAX(PengaduanID), 0) FROM pengaduanhelpdesk_0405";
                long newId;
                using (var getMaxIdCmd = new MySqlCommand(getMaxIdQuery, connection))
                {
                    newId = Convert.ToInt64(getMaxIdCmd.ExecuteScalar()) + 1;
                }

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", newId);
                    cmd.Parameters.AddWithValue("@mahasiswaId", mahasiswaId);
                    cmd.Parameters.AddWithValue("@tgl", date);
                    cmd.Parameters.AddWithValue("@deskripsi", description);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@helpdeskId", helpDeskId);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Data berhasil ditambahkan!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data: {ex.Message}");
        }
    }

    public void GetData()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = @"
            SELECT 
                p.PengaduanID,
                m.NamaMahasiswa,
                h.NamaHelpDesk,
                p.TanggalPengaduan,
                p.Deskripsi,
                p.StatusPengaduan
            FROM 
                pengaduanhelpdesk_0405 p
            INNER JOIN 
                mahasiswa_0405 m ON p.MahasiswaID = m.MahasiswaID
            INNER JOIN 
                helpdesk_0405 h ON p.HelpDeskID = h.HelpDeskID";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nData Pengaduan HelpDesk:");
                        Console.WriteLine("---------------------------------------------------------------------------------------------");
                        Console.WriteLine("ID Pengaduan | Nama Mahasiswa | Nama HelpDesk | Tanggal Pengaduan | Deskripsi | Status");
                        Console.WriteLine("---------------------------------------------------------------------------------------------");

                        while (reader.Read())
                        {
                            long pengaduanId = reader.GetInt64("PengaduanID");
                            string mahasiswaName = reader.GetString("NamaMahasiswa");
                            string helpDeskName = reader.GetString("NamaHelpDesk");
                            string tanggalPengaduan = reader.IsDBNull(reader.GetOrdinal("TanggalPengaduan"))
                                ? "NULL"
                                : reader.GetDateTime("TanggalPengaduan").ToString("yyyy-MM-dd");
                            string deskripsi = reader.GetString("Deskripsi");
                            string status = reader.GetString("StatusPengaduan");
                            Console.WriteLine($"{pengaduanId} | {mahasiswaName} | {helpDeskName} | {tanggalPengaduan} | {deskripsi} | {status}");
                        }

                        Console.WriteLine("---------------------------------------------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data pengaduan helpdesk: {ex.Message}");
        }
    }

    public void Update(long pengaduanId, long mahasiswaId, long helpDeskId, string date, string description, string status)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = @"UPDATE pengaduanhelpdesk_0405 
                                SET MahasiswaID = @mahasiswaId, 
                                    HelpDeskID = @helpDeskId, 
                                    TanggalPengaduan = @tgl, 
                                    Deskripsi = @deskripsi, 
                                    StatusPengaduan = @status
                                WHERE PengaduanID = @pengaduanId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@pengaduanId", pengaduanId);
                    cmd.Parameters.AddWithValue("@mahasiswaId", mahasiswaId);
                    cmd.Parameters.AddWithValue("@helpDeskId", helpDeskId);
                    cmd.Parameters.AddWithValue("@tgl", date);
                    cmd.Parameters.AddWithValue("@deskripsi", description);
                    cmd.Parameters.AddWithValue("@status", status);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data berhasil diperbarui!");
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data: {ex.Message}");
        }
    }


    public void Delete(long id)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM pengaduanhelpdesk_0405 WHERE PengaduanID = @id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data berhasil dihapus!");
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            mainOptions();
        }
    }

    public static void mainOptions()
    {
        Console.WriteLine("");
        Console.WriteLine("--Selamat Datang di Asrama--");
        Console.WriteLine("1.Mahasiswa");
        Console.WriteLine("2.Helpdesk");
        Console.WriteLine("3.Kamar Asrama");
        Console.WriteLine("4.Reservasi Kamar Asrama");
        Console.WriteLine("5.Pengaduan");

        Console.Write("Pilih Opsi:");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Options.Mahasiswa();
                break;
            case "2":
                Options.HelpDesk();
                break;
            case "3":
                Options.Kamar();
                break;
            case "4":
                Options.Reservasi();
                break;
            case "5":
                Options.Pengaduan();
                break;

            default:
                Console.WriteLine("Opsi yang dimasukkan tidak valid!");
                break;
        }
    }
}

class Options
{

    // Mahasiswa Options
    public static void Mahasiswa()
    {
        Console.WriteLine("");
        Console.WriteLine("--Opsi Mahasiswa--");
        Console.WriteLine("1.Tampilkan Data");
        Console.WriteLine("2.Tambah Data");
        Console.WriteLine("3.Cari Data");
        Console.WriteLine("4.Ubah Data");
        Console.WriteLine("5.Hapus Data");
        Console.WriteLine("6.Kembali ke Halaman Awal");

        Console.Write("Pilih Opsi:");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                MahasiswaFunction.HandleGetMahasiswa(true);
                break;
            case "2":
                MahasiswaFunction.HandleCreateMahasiswa();
                break;
            case "3":
                MahasiswaFunction.HandleSearchMahasiswa();
                break;
            case "4":
                MahasiswaFunction.HandleUpdateMahasiswa();
                break;
            case "5":
                MahasiswaFunction.HandleDeleteMahasiswaById();
                break;
            case "6":
                Program.mainOptions();
                break;
            default:
                Console.WriteLine("Pilihan tidak valid!");
                break;
        }
    }

    // Helpdesk Options
    public static void HelpDesk()
    {
        Console.WriteLine("");
        Console.WriteLine("--Opsi Helpdesk--");
        Console.WriteLine("1.Tampilkan Data");
        Console.WriteLine("2.Tambah Data");
        Console.WriteLine("3.Search Data");
        Console.WriteLine("4.Ubah Data");
        Console.WriteLine("5.Hapus Data");
        Console.WriteLine("6.Kembali ke Halaman Awal");

        Console.Write("Pilih Opsi:");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                HelpDeskFunction.HandleHGet(true);
                break;
            case "2":
                HelpDeskFunction.HandleCreate();
                break;
            case "3":
                HelpDeskFunction.HandleSearch();
                break;
            case "4":
                HelpDeskFunction.HandleUpdate();
                break;
            case "5":
                HelpDeskFunction.HandleDelete();
                break;
            case "6":
                Program.mainOptions();
                break;
            default:
                Console.WriteLine("Pilihan tidak valid!");
                break;
        }
    }

    // Kamar Options
    public static void Kamar()
    {
        Console.WriteLine("");
        Console.WriteLine("--Opsi Kamar Asrama--");
        Console.WriteLine("1.Tampilkan Data");
        Console.WriteLine("2.Tambah Data");
        Console.WriteLine("3.Search Data");
        Console.WriteLine("4.Ubah Data");
        Console.WriteLine("5.Hapus Data");
        Console.WriteLine("6.Kembali ke Halaman Awal");

        Console.Write("Pilih Opsi:");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                KamarFunction.HandleHGet(true);
                break;
            case "2":
                KamarFunction.HandleCreate();
                break;
            case "3":
                KamarFunction.HandleSearch();
                break;
            case "4":
                KamarFunction.HandleUpdate();
                break;
            case "5":
                KamarFunction.HandleDelete();
                break;
            case "6":
                Program.mainOptions();
                break;
            default:
                Console.WriteLine("Pilihan tidak valid!");
                break;
        }
    }

    // Reservasi Options
    public static void Reservasi()
    {
        Console.WriteLine("");
        Console.WriteLine("--Opsi Reservasi Kamar Asrama--");
        Console.WriteLine("1.Tampilkan Data");
        Console.WriteLine("2.Tambah Data");
        Console.WriteLine("3.Search Data");
        Console.WriteLine("4.Ubah Data");
        Console.WriteLine("5.Hapus Data");
        Console.WriteLine("6.Kembali ke Halaman Awal");

        Console.Write("Pilih Opsi:");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                ReservasiFunction.HandleHGet(true);
                break;
            case "2":
                ReservasiFunction.HandleCreate();
                break;
            case "3":
                ReservasiFunction.HandleSearch();
                break;
            case "4":
                ReservasiFunction.HandleUpdate();
                break;
            case "5":
                ReservasiFunction.HandleDelete();
                break;
            case "6":
                Program.mainOptions();
                break;
            default:
                Console.WriteLine("Pilihan tidak valid!");
                break;
        }
    }

    // Pengaduan Options
    public static void Pengaduan()
    {
        Console.WriteLine("");
        Console.WriteLine("--Opsi Pengaduan--");
        Console.WriteLine("1.Tampilkan Data");
        Console.WriteLine("2.Tambah Data");
        Console.WriteLine("3.Ubah Data");
        Console.WriteLine("4.Hapus Data");
        Console.WriteLine("5.Kembali ke Halaman Awal");

        Console.Write("Pilih Opsi:");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                PengaduanFunction.HandleHGet(true);
                break;
            case "2":
                PengaduanFunction.HandleCreate();
                break;
            case "3":
                PengaduanFunction.HandleUpdate();
                break;
            case "4":
                PengaduanFunction.HandleDelete();
                break;
            case "5":
                Program.mainOptions();
                break;
            default:
                Console.WriteLine("Pilihan tidak valid!");
                break;
        }
    }
}

// Mahasiswa Function
class MahasiswaFunction
{
    public static void HandleCreateMahasiswa()
    {
        try
        {
            var model = new MahasiswaModel();
            string name = Validation.GetStringInput("Name");
            long nim = Validation.GetIntegerInput("NIM");
            string email = Validation.GetStringInput("Email");
            long phone = Validation.GetIntegerInput("Telepon");
            string address = Validation.GetStringInput("Alamat");
            model.Create(name, nim, email, phone, address);
            var choice = Helper.TurnBackOptions("Mahasiswa");
            if (choice == "2")
            {
                Options.Mahasiswa();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data Mahasiswa: {ex.Message}");
        }
    }

    public static void HandleGetMahasiswa(bool turnback = false)
    {
        try
        {
            var model = new MahasiswaModel();
            model.GetData();
            if (turnback)
            {
                var choice = Helper.TurnBackOptions("Mahasiswa");
                if (choice == "2")
                {
                    Options.Mahasiswa();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data Mahasiswa: {ex.Message}");
        }
    }

    public static void HandleDeleteMahasiswaById()
    {
        try
        {
            HandleGetMahasiswa();
            var model = new MahasiswaModel();
            long id = Validation.GetIntegerInput("Id Mahasiswa yang akan dihapus");
            model.Delete(id);
            var choice = Helper.TurnBackOptions("Mahasiswa");
            if (choice == "2")
            {
                Options.Mahasiswa();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data Mahasiswa: {ex.Message}");
        }
    }

    public static void HandleUpdateMahasiswa()
    {
        try
        {
            HandleGetMahasiswa();
            var model = new MahasiswaModel();
            long id = Validation.GetIntegerInput("Id Mahasiswa yang akan diperbarui");
            string name = Validation.GetStringInput("Nama baru");
            long nim = Validation.GetIntegerInput("NIM baru");
            string email = Validation.GetStringInput("Email baru");
            long phone = Validation.GetIntegerInput("Telepon baru");
            string address = Validation.GetStringInput("Alamat baru");
            model.Update(id, name, nim, email, phone, address);
            var choice = Helper.TurnBackOptions("Mahasiswa");
            if (choice == "2")
            {
                Options.Mahasiswa();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data Mahasiswa: {ex.Message}");
        }
    }

    public static void HandleSearchMahasiswa()
    {
        try
        {
            var model = new MahasiswaModel();
            string keyword = Validation.GetStringInput("Masukkan keyword untuk mencari (Nama, NIM, Email, Alamat)");
            model.Search(keyword);
            var choice = Helper.TurnBackOptions("Mahasiswa");
            if (choice == "2")
            {
                Options.Mahasiswa();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mencari data Mahasiswa: {ex.Message}");
        }
    }
}


// HelpDesk Function
class HelpDeskFunction
{
    public static void HandleHGet(bool turnback = false)
    {
        try
        {
            var model = new HelpDeskModel();
            model.GetData();
            if (turnback)
            {
                var choice = Helper.TurnBackOptions("HelpDesk");
                if (choice == "2")
                {
                    Options.HelpDesk();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data HelpDesk: {ex.Message}");
        }
    }

    public static void HandleCreate()
    {
        try
        {
            var model = new HelpDeskModel();
            string name = Validation.GetStringInput("Name");
            string email = Validation.GetStringInput("Email");
            long phone = Validation.GetIntegerInput("Telepon");
            model.Create(name, email, phone);
            var choice = Helper.TurnBackOptions("HelpDesk");
            if (choice == "2")
            {
                Options.HelpDesk();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data HelpDesk: {ex.Message}");
        }
    }

    public static void HandleUpdate()
    {
        try
        {
            HandleHGet();
            var model = new HelpDeskModel();
            long id = Validation.GetIntegerInput("Id HelpDesk yang akan diperbarui");
            string name = Validation.GetStringInput("Nama baru");
            string email = Validation.GetStringInput("Email baru");
            long phone = Validation.GetIntegerInput("Telepon baru");
            model.Update(id, name, email, phone);
            var choice = Helper.TurnBackOptions("HelpDesk");
            if (choice == "2")
            {
                Options.HelpDesk();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data HelpDesk: {ex.Message}");
        }
    }

    public static void HandleDelete()
    {
        try
        {
            HandleHGet();
            var model = new HelpDeskModel();
            long id = Validation.GetIntegerInput("Id HelpDesk yang akan dihapus");
            model.Delete(id);
            var choice = Helper.TurnBackOptions("HelpDesk");
            if (choice == "2")
            {
                Options.HelpDesk();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data HelpDesk: {ex.Message}");
        }
    }

    public static void HandleSearch()
    {
        try
        {
            var model = new HelpDeskModel();
            string keyword = Validation.GetStringInput("Masukkan keyword pencarian (nama/email/telepon)");
            model.Search(keyword);

            var choice = Helper.TurnBackOptions("HelpDesk");
            if (choice == "2")
            {
                Options.HelpDesk();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mencari data HelpDesk: {ex.Message}");
        }
    }

}

// Kamar Function
class KamarFunction
{
    public static void HandleHGet(bool turnback = false)
    {
        try
        {
            var model = new KamarModel();
            model.GetData();
            if (turnback)
            {
                var choice = Helper.TurnBackOptions("Kamar Asrama");
                if (choice == "2")
                {
                    Options.Kamar();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data Kamar Asrama: {ex.Message}");
        }
    }

    public static void HandleCreate()
    {
        try
        {
            var model = new KamarModel();
            string name = Validation.GetStringInput("Name");
            long capacity = Validation.GetIntegerInput("Kapasitas");
            long stock = Validation.GetIntegerInput("Tersedia");
            model.Create(name, capacity, stock);
            var choice = Helper.TurnBackOptions("Kamar Asrama");
            if (choice == "2")
            {
                Options.Kamar();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data Kamar Asrama: {ex.Message}");
        }
    }

    public static void HandleSearch()
    {
        try
        {
            var model = new KamarModel();
            string keyword = Validation.GetStringInput("Masukkan keyword pencarian (nama/kapasitas/tersedia)");
            model.Search(keyword);

            var choice = Helper.TurnBackOptions("Kamar Asrama");
            if (choice == "2")
            {
                Options.Kamar();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mencari data Kamar Asrama: {ex.Message}");
        }
    }


    public static void HandleUpdate()
    {
        try
        {
            HandleHGet();
            var model = new KamarModel();
            long id = Validation.GetIntegerInput("Id Kamar Asrama yang akan diperbarui");
            string name = Validation.GetStringInput("Nama baru");
            long capacity = Validation.GetIntegerInput("Kapasitas baru");
            long stock = Validation.GetIntegerInput("Tersedia baru");
            model.Update(id, name, capacity, stock);
            var choice = Helper.TurnBackOptions("Kamar Asrama");
            if (choice == "2")
            {
                Options.Kamar();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data Kamar Asrama: {ex.Message}");
        }
    }

    public static void HandleDelete()
    {
        try
        {
            HandleHGet();
            var model = new KamarModel();
            long id = Validation.GetIntegerInput("Id Kamar yang akan dihapus");
            model.Delete(id);
            var choice = Helper.TurnBackOptions("Kamar Asrama");
            if (choice == "2")
            {
                Options.Kamar();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data Kamar: {ex.Message}");
        }
    }
}

// Reservasi Function
class ReservasiFunction
{
    public static void HandleHGet(bool turnback = false)
    {
        try
        {
            var model = new ReservasiModel();
            model.GetData();
            if (turnback)
            {
                var choice = Helper.TurnBackOptions("Reservasi Kamar Asrama");
                if (choice == "2")
                {
                    Options.Reservasi();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data Reservasi Kamar Asrama: {ex.Message}");
        }
    }

    public static void HandleSearch()
    {
        try
        {
            var model = new ReservasiModel();
            string keyword = Validation.GetStringInput("Masukkan keyword pencarian (Nama Mahasiswa, Nama Kamar, Status)");
            model.Search(keyword);

            var choice = Helper.TurnBackOptions("Reservasi Kamar");
            if (choice == "2")
            {
                Options.Reservasi();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat melakukan pencarian: {ex.Message}");
        }
    }


    public static void HandleCreate()
    {
        try
        {
            var model = new ReservasiModel();

            model.GetDataMahasiswa();
            long mahasiswaId = Validation.GetIntegerInput("ID Mahasiswa");
            model.GetDataKamar();
            long kamarId = Validation.GetIntegerInput("ID Kamar");
            Console.WriteLine("//Format(yyyy-mm-dd) *2024-12-01");
            string dateIn = Validation.GetDateInput("Tanggal Masuk");
            Console.WriteLine("//Format(yyyy-mm-dd) *2024-12-01");
            string dateOut = Validation.GetDateInput("Tanggal Keluar");
            Console.WriteLine("*Aktif/Selesai/Dibatalkan");
            string status = Validation.GetStringInput("Status Reservasi");
            model.Create(mahasiswaId, kamarId, dateIn, dateOut, status);
            var choice = Helper.TurnBackOptions("Reservasi Kamar Asrama");
            if (choice == "2")
            {
                Options.Reservasi();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data Reservasi Kamar Asrama: {ex.Message}");
        }
    }

    public static void HandleUpdate()
    {
        try
        {
            HandleHGet();
            var model = new ReservasiModel();
            long id = Validation.GetIntegerInput("Id Reservasi Kamar Asrama yang akan diperbarui");
            model.GetDataMahasiswa();
            long mahasiswaId = Validation.GetIntegerInput("ID Mahasiswa");
            model.GetDataKamar();
            long kamarId = Validation.GetIntegerInput("ID Kamar");
            Console.WriteLine("//Format(yyyy-mm-dd) *2024-12-01");
            string dateIn = Validation.GetDateInput("Tanggal Masuk");
            Console.WriteLine("//Format(yyyy-mm-dd) *2024-12-01");
            string dateOut = Validation.GetDateInput("Tanggal Keluar");
            Console.WriteLine("*Aktif/Selesai/Dibatalkan");
            string status = Validation.GetStringInput("Status Reservasi");
            model.Update(id, mahasiswaId, kamarId, dateIn, dateOut, status);
            var choice = Helper.TurnBackOptions("Reservasi Kamar Asrama");
            if (choice == "2")
            {
                Options.Reservasi();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data Reservasi Kamar Asrama: {ex.Message}");
        }
    }

    public static void HandleDelete()
    {
        try
        {
            HandleHGet();
            var model = new ReservasiModel();
            long id = Validation.GetIntegerInput("Id Reservasi Kamar Asrama yang akan dihapus");
            model.Delete(id);
            var choice = Helper.TurnBackOptions("Reservasi Kamar Asrama");
            if (choice == "2")
            {
                Options.Reservasi();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data Kamar: {ex.Message}");
        }
    }
}

// Pengaduan Function
class PengaduanFunction
{
    public static void HandleHGet(bool turnback = false)
    {
        try
        {
            var model = new PengaduanModel();
            model.GetData();
            if (turnback)
            {
                var choice = Helper.TurnBackOptions("Pengaduan");
                if (choice == "2")
                {
                    Options.Pengaduan();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat mengambil data Reservasi Kamar Asrama: {ex.Message}");
        }
    }

    public static void HandleCreate()
    {
        try
        {
            var model = new PengaduanModel();

            model.GetDataMahasiswa();
            long mahasiswaId = Validation.GetIntegerInput("ID Mahasiswa");
            model.GetDataHelpdesk();
            long helpdeskId = Validation.GetIntegerInput("ID HelpDesk");
            Console.WriteLine("//Format(yyyy-mm-dd) *2024-12-01");
            string datePengaduan = Validation.GetDateInput("Tanggal Pengaduan");
            string description = Validation.GetStringInput("Deskripsi");
            Console.WriteLine("*Diajukan/Diproses/Diterima/Ditolak");
            string status = Validation.GetStringInput("Status Pengajuan");
            model.Create(mahasiswaId, helpdeskId, datePengaduan, description, status);
            var choice = Helper.TurnBackOptions("Pengaduan");
            if (choice == "2")
            {
                Options.Pengaduan();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menambahkan data Pengaduan: {ex.Message}");
        }
    }

    public static void HandleUpdate()
    {
        try
        {
            HandleHGet();
            var model = new PengaduanModel();
            long id = Validation.GetIntegerInput("Id Pengaduan yang akan diperbarui");
            model.GetDataMahasiswa();
            long mahasiswaId = Validation.GetIntegerInput("ID Mahasiswa");
            model.GetDataHelpdesk();
            long helpdeskId = Validation.GetIntegerInput("ID Helpdesk");
            Console.WriteLine("//Format(yyyy-mm-dd) *2024-12-01");
            string datePengaduan = Validation.GetDateInput("Tanggal Pengaduan");
            string description = Validation.GetStringInput("Deskripsi");
            Console.WriteLine("*Diajukan/Diproses/Diterima/Ditolak");
            string status = Validation.GetStringInput("Status Pengajuan");
            model.Update(id, mahasiswaId, helpdeskId, datePengaduan, description, status);
            var choice = Helper.TurnBackOptions("Pengaduan");
            if (choice == "2")
            {
                Options.Pengaduan();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat memperbarui data Pengaduan: {ex.Message}");
        }
    }

    public static void HandleDelete()
    {
        try
        {
            HandleHGet();
            var model = new ReservasiModel();
            long id = Validation.GetIntegerInput("Id Pengaduan yang akan dihapus");
            model.Delete(id);
            var choice = Helper.TurnBackOptions("Pengaduan");
            if (choice == "2")
            {
                Options.Pengaduan();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menghapus data Pengaduan: {ex.Message}");
        }
    }
}

class Validation
{
    public static long GetIntegerInput(string prompt)
    {
        long result;
        while (true)
        {
            Console.Write($"{prompt}: ");
            string input = Console.ReadLine();

            if (long.TryParse(input, out result) && input.Length <= 20)
                return result;

            Console.WriteLine("Harap masukkan angka yang valid dengan panjang maksimal 20 digit.");
        }
    }

    public static string GetStringInput(string prompt)
    {
        Console.Write($"{prompt}: ");
        return Console.ReadLine();
    }

    public static string GetDateInput(string fieldName)
    {
        while (true)
        {
            string input = GetStringInput(fieldName);

            // Validasi format tanggal menggunakan regex
            if (System.Text.RegularExpressions.Regex.IsMatch(input, "^\\d{4}-\\d{2}-\\d{2}$"))
            {
                // Coba parsing tanggal untuk memastikan validitasnya
                if (DateTime.TryParse(input, out _))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Tanggal tidak valid. Pastikan sesuai dengan format yyyy-mm-dd.");
                }
            }
            else
            {
                Console.WriteLine("Format tanggal salah. Pastikan sesuai dengan format yyyy-mm-dd.");
            }
        }
    }
}

class Helper
{
    public static string TurnBackOptions(string name)
    {
        Console.WriteLine("");
        Console.WriteLine("1.Kembali ke opsi utama");
        Console.WriteLine("2.Kembali ke opsi " + name);
        Console.Write("Piih Opsi:");
        string choice = Console.ReadLine();
        return choice;
    }
}