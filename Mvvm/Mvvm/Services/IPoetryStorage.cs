using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mvvm.Models;

namespace Mvvm.Services;

public interface IPoetryStorage
{
    Task InitializeAsync();
    
    Task InsertAsync(Poetry poetry);
    
    Task<IList<Poetry>> ListAsync();
    
    Task<IList<Poetry>> QueryAsync(string keyword);
}