using System;
using Microsoft.Extensions.DependencyInjection;
using Mvvm.Services;
using Mvvm.ViewModels;

namespace Mvvm;

/*服务定位器模式，引入了依赖注入容器，没有构造参数也没有依赖，只要找到服务定位器就能使用viewmodel的实例*/
public class ServiceLocator
{
    private readonly IServiceProvider _serviceProvider;
    
    public MainWindowViewModel MainWindowViewModel => _serviceProvider.GetService<MainWindowViewModel>();
    //就是return了这个private容器中的实例
    
    public ServiceLocator() {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<MainWindowViewModel>();//注册为单例服务，每次调用时会提供同一个实例，而不会重新创建
        serviceCollection.AddSingleton<IPoetryStorage, PoetryStorage>();
        //注册类型到serviceCollection中

        _serviceProvider = serviceCollection.BuildServiceProvider();
        //提供类型实例（取对象用的）
    }

}