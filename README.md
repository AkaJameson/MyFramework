# MyFramework

MyFramework 是一个为 Unity 开发的数字孪生工具框架，旨在简化和加速数字孪生应用的开发。该框架提供了一系列模块化的工具和管理器，帮助开发者更高效地构建复杂的应用程序。

## 📦 模块概览

- **EventCenter**: 事件中心，用于事件的注册和分发，简化事件驱动的开发。
- **Extension**: 扩展方法集合，提供了对 Unity 常用功能的扩展。
- **SoundManager**: 音频管理器，负责音效和背景音乐的播放与管理。
- **UIFrame**: UI 框架，提供了 UI 组件的管理和切换功能。
- **Utilities**: 实用工具集合，包含常用的辅助功能和工具类。
- **AnimatorManager**: 动画管理器，简化动画的控制和状态管理。
- **APIRequest**: API 请求模块，处理网络请求和数据解析。
- **CameraFrame**: 相机框架，提供相机的控制和管理功能。
- **Configuration**: 配置管理模块，处理应用程序的配置项。

## 🚀 快速开始

### 安装

1. 克隆或下载此仓库到你的 Unity 项目中。
2. 将 `MyFramework` 文件夹放置在你的 Unity 项目的 `Assets` 目录下。

### 使用

1. **事件中心**

   ```csharp
   EventCenter.Instance.RegisterEvent("OnPlayerDeath", OnPlayerDeathHandler);
   EventCenter.Instance.TriggerEvent("OnPlayerDeath", playerId);
   ```

2. **音频管理**

   ```csharp
   SoundManager.Instance.PlaySound("Explosion");
   SoundManager.Instance.PlayBackgroundMusic("MainTheme");
   ```

3. **UI 管理**

   ```csharp
   UIFrame.Instance.ShowPanel("MainMenu");
   UIFrame.Instance.HidePanel("Settings");
   ```

4. **API 请求**

   ```csharp
   APIRequest.Instance.Get("https://api.example.com/data", OnDataReceived);
   ```

## 📄 许可证

此项目基于 MIT 许可证开源。详情请参阅 [LICENSE](https://github.com/AkaJameson/MyFramework/blob/main/LICENSE)。

## 🙏 鸣谢

感谢所有为此项目贡献代码和提出建议的开发者们。
