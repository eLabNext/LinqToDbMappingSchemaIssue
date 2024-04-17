using LinqToDB;
using LinqToDB.DataProvider.MySql;
using LinqToDB.Mapping;

var connectionString = "Server=127.0.0.1; Port=13306; User ID=foo; Password=foo; Database=foo; Pooling=true; Connection Idle Timeout=10";

// Using MappingSchema with the default constructor results in an exception.
var dataOptions = new DataOptions()
    .UseDataProvider(MySqlTools.GetDataProvider(ProviderName.MySqlConnector))
    .UseConnectionString(connectionString)
    .UseMappingSchema(new MappingSchema());

// Using MappingSchema using ProviderName.MySqlConnector for the configuration parameter also results in an exception.
// var dataOptions = new DataOptions()
//     .UseDataProvider(MySqlTools.GetDataProvider(ProviderName.MySqlConnector))
//     .UseConnectionString(connectionString)
//     .UseMappingSchema(new MappingSchema(ProviderName.MySqlConnector));

// Using MappingSchema using ProviderName.MySql for the configuration parameter does not result in an exception.
// var dataOptions = new DataOptions()
//     .UseDataProvider(MySqlTools.GetDataProvider(ProviderName.MySqlConnector))
//     .UseConnectionString(connectionString)
//     .UseMappingSchema(new MappingSchema(ProviderName.MySql));

// Not using MappingSchema does not result in an exception.
// var dataOptions = new DataOptions()
//     .UseDataProvider(MySqlTools.GetDataProvider(ProviderName.MySqlConnector))
//     .UseConnectionString(connectionString)
//     .UseMappingSchema(new MappingSchema(ProviderName.MySql));

var dataContext = new DataContext(dataOptions);

_ = dataContext.GetTable<Foo>()
    .Where(foo => (Sql.CurrentTimestamp - foo.DateTime).TotalSeconds < 100)
    .ToList();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

[Table("Foo")]
public class Foo
{
    [Column]
    public DateTime DateTime { get; set; }
}
