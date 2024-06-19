using System.Data.SqlClient;

/* first we need CONNECTION*/
string connectionString = "Server=.;Database=LabSoftUni; User=DESKTOP-SENJ7PO;Password=...;TrustServerCertificate=true";

/* then we need QUERY to execute*/
string query = "SELECT EmployeeID, FirstName, LastName, JobTitle, FROM Employees WHERE DepartmentID = @departmentId";

/* add parameter */
int departmentId = 3;

/* create connection*/
using SqlConnection connection = new SqlConnection(connectionString);

SqlCommand cmd = new SqlCommand(query, connection);
cmd.Parameters.AddWithValue("@departmentId", departmentId);

try
{
    /*  open connection */
    connection.Open();

    /* last we create READER */
    SqlDataReader reader = cmd.ExecuteReader();

    while (reader.Read())
    {
        Console.WriteLine($"{reader[1]} {reader[2]}: {reader[3]}");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

}
