using System.Collections.Generic;
using System.Threading.Tasks;
using Mvvm.Helpers;
using Mvvm.Models;
using SQLite;

namespace Mvvm.Services;

public class PoetryStorage : IPoetryStorage
{

    //此处的“.sqlite3”是老师自己随便起的拓展名
    public const string DbName = "poetrydb.sqlite3";

    //const 常量是程序编译前就确定不变的值，一开始就是写死的，static readonly 是编译时不知道值，编译之后、运行时确定不变的值
    public static readonly string PoetryDbPath = PathHelper.GetLocalFilePath(DbName);

    private SQLiteAsyncConnection _connection;

    private SQLiteAsyncConnection Connection =>
        _connection ??= new SQLiteAsyncConnection(PoetryDbPath);
    /*
    此处 ??= 符号是C#的语法糖（即简便写法），等价于
    if(_connection != null) {
    return _connection;
    }
    _connection = new SQLiteAsyncConnection(PoetryDbPath);
    return _connection;
    */


    public async Task<Poetry> GetRandomPoetryAsync()
    {
        throw new System.NotImplementedException();
    }

    //.net是先连接数据库，再根据提供的实体类创建数据库，java里则是先用SQL语句创建数据库再连接数据库
    //创建数据库
    public async Task InitializeAsync()
    {
        await Connection.CreateTableAsync<Poetry>();
    }

    //向数据库中插入数据
    public async Task InsertAsync(Poetry poetry)
    {
        await Connection.InsertAsync(poetry);
    }

    //展示列表
    public async Task<IList<Poetry>> ListAsync() =>
        await Connection.Table<Poetry>().ToListAsync();

    //查询数据库
    public async Task<IList<Poetry>> QueryAsync(string keyword) =>
        await Connection.Table<Poetry>().Where(p => p.Name.Contains(keyword)).ToListAsync();
}