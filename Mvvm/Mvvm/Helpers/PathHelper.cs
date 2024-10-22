using System;
using System.IO;

namespace Mvvm.Helpers;

//使用嵌入式存储，而每个人设备的存储情况都不一样，因此不可以使用固定位置存储数据
public static class PathHelper {
    private static string _localFolder = string.Empty;//此属性为最后的文件储存位置

    private static string LocalFolder {
        get
        {
            if (!string.IsNullOrEmpty(_localFolder)) {
                return _localFolder;
            }

            _localFolder =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder
                        .LocalApplicationData), nameof(Mvvm));
            //寻找当前环境下储存文件的位置+本文件名

            if (!Directory.Exists(_localFolder)) {
                Directory.CreateDirectory(_localFolder);
            }

            return _localFolder;
        }
    }

    public static string GetLocalFilePath(string fileName) {
        return Path.Combine(LocalFolder, fileName);
    }
}
