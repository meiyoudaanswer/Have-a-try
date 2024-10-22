using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Mvvm.Models;
using Mvvm.Services;

namespace Mvvm.ViewModels;

/*ViewModel:准备数据和功能*/
public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IPoetryStorage _poetryStorage;//这是一个接口
    
    //依赖注入的构造函数，默认的构造函数已经被覆盖，构造时必须传参
    //构造函数注入（Constructor Injection）：通过类的构造函数将依赖的对象作为参数传递进来，并在类的构造函数中进行赋值
    public MainWindowViewModel(IPoetryStorage poetryStorage) {
        _poetryStorage = poetryStorage;
        SayHelloCommand = new RelayCommand(SayHello);//通过调用ICommand接口来调用SayHello函数
        InitializeCommand = new AsyncRelayCommand(InitializeAsync);
        InsertCommand = new AsyncRelayCommand(InsertAsync);
        ListCommand = new AsyncRelayCommand(ListAsync);
    } 
    
    private string _message;
    
    //将私有成员封装成公开的属性
    public string Message {
        get => _message;
        set => SetProperty(ref _message, value); //要赋值的地址和要赋的值
    }
    
    public ICommand SayHelloCommand { get; }
    
    public void SayHello() {
        Message = "Hello, Avalonia!";
    }

    //建数据库
    public async Task InitializeAsync() {
        await _poetryStorage.InitializeAsync();
    }
    
    public ICommand InitializeCommand { get; }

    //插入数据
    public async Task InsertAsync() {
        await _poetryStorage.InsertAsync(new Poetry {
            Name = "Name" + new Random().Next()
        });
    }
    
    public ICommand InsertCommand { get; }

    //展示数据 
    public ObservableCollection<Poetry> Poetries { get; set; } = new();
    
    public async Task ListAsync() {
        var poetries = await _poetryStorage.ListAsync();
        Poetries.Clear();
        foreach (var poetry in poetries) {
            Poetries.Add(poetry);
        }
    }

    public ICommand ListCommand { get; set; }
}