using ADONetDemo;
using ADONetDemo.DbConfigLib;
using MySql.Data.MySqlClient;

var accounts = new List<Account>();

var connectionString = DbConfig.ImportFromJsonConfig().ToString();

var db = new MySqlConnection(connectionString);
db.Open();

var sql = """
          SELECT tab_accounts.id AS 'id',
                 tab_accounts.login AS 'login',
                 tab_accounts.password AS 'password',
                 tab_role_types.name AS 'role',
                 tab_users.first_name AS 'first_name',
                 tab_users.last_name AS 'last_name',
                 tab_users.email AS 'email',
                 tab_users.phone AS 'phone'
          FROM tab_accounts
          JOIN tab_users 
              ON tab_accounts.id = tab_users.id
          JOIN tab_roles 
              ON tab_accounts.id = tab_roles.account_id
          JOIN tab_role_types 
              ON tab_roles.role_type_id = tab_role_types.id
          WHERE is_active = TRUE
          ORDER BY last_name, first_name;
          """;

var command = new MySqlCommand()
{
    Connection = db,
    CommandText = sql
};
var result = command.ExecuteReader();
if (result.HasRows)
{
    while (result.Read())
    {
        accounts.Add(new Account()
        {
            Id = result.GetInt32("id"),
            Login = result.GetString("login"),
            Password = result.GetString("password"),
            Role = result.GetString("role"),
            FirstName = result.GetString("first_name"),
            LastName = result.GetString("last_name"),
            Email = result.GetString("email"),
            Phone = result.GetString("phone")
        });
    }
}
else
{
    Console.WriteLine("!!! ERROR !!!");
    return;
}

db.Close();

foreach (var a in accounts)
{
    Console.WriteLine($"{a.Id}: {a.FullName}, {a.Login}, {a.Email}, {a.Phone}");
}