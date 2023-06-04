# LemonUI<br>[![GitHub Actions][actions-img]][actions-url] [![NuGet][nuget-img-2]][nuget-url-2] [![NuGet][nuget-img-3]][nuget-url-3] [![NuGet][nuget-img-f]][nuget-url-f] [![NuGet][nuget-img-r]][nuget-url-r] [![Patreon][patreon-img]][patreon-url] [![PayPal][paypal-img]][paypal-url] [![Discord][discord-img]][discord-url]

LemonUI is a framework for creating UI systems in Grand Theft Auto V that is compatible with FiveM, RageMP, RagePluginHook ScriptHookVDotNet 2 and ScriptHookVDotNet 3. It allows you to create UI Elements with a NativeUI-like style, or you can also create your own UI System from scratch via the resolution-independant classes for Text, Rectangles and Textures.

It was created as a replacement for NativeUI due to being too convoluted to develop and maintain. LemonUI retains most (if not all) of the UI Elements available in NativeUI.

Special thanks to:

* Guad for the original work in NativeUI
* alloc8or for the help with the GTA Online Loading Screen Scaleform
* ikt for helping me to use SET_SCRIPT_GFX_ALIGN and SET_SCRIPT_GFX_ALIGN_PARAMS
* Dot. for the snippet of code used for the item scrolling
* deterministic_bubble for answering some questions about some missing C# classes in FiveM
* PNWParksFan for helping me with some RPH question when I was doing the port

## Download

* [5mods](https://www.gta5-mods.com/scripts/lemonui)
* [GitHub Releases](https://github.com/LemonUIbyLemon/LemonUI/releases)
* [GitHub Actions](https://github.com/LemonUIbyLemon/LemonUI/actions) (experimental versions)

## Installation

> Warning: You don't need to install all of the files. You only need to install the ones for the framework you plan to use. For example, if you want to install SHVDN mods, you don't need to install the RPH version.

### FiveM

You don't need to install any files when using FiveM. When you connect to a server, the resources that need it will automatically provide a copy of LemonUI.

### RageMP

You don't need to install any files when using RageMP. When you connect to a server, the resources that need it will automatically provide a copy of LemonUI when compiling the code.

### RagePluginHook

Copy all of the files from the **RPH** folder inside of the compressed file to the root of your GTA V installation directory.

### ScriptHookVDotNet 2 and ScriptHookVDotNet 3

**PLEASE NOTE THAT THE LAST VERSION THAT SUPPORTED SHVDN2 WAS 1.5.2. [You can download it here]().**

Copy all of the files from the **SHVDN2** and/or **SHVDN2** folder(s) inside of the compressed file to your **scripts** directory.

### Developers

Add any of the NuGet packages linked above and start working in your IDE.

If you are using RagePluginHook or ScriptHookVDotNet, you can disable the copy of the dll in your IDE so your users always have to use the latest version available.

If you are using RageMP, you will need to download the latest release and copy **LemonUI.RageMP.cs** to you client solution.

## Usage

Once installed, the mods that require LemonUI will start working.

If you are a developer, check the [wiki](https://github.com/LemonUIbyLemon/LemonUI/wiki) for information to implement LemonUI in your mod.

[actions-img]: https://img.shields.io/github/actions/workflow/status/LemonUIbyLemon/LemonUI/main.yml?branch=master&label=actions
[actions-url]: https://github.com/LemonUIbyLemon/LemonUI/actions
[nuget-img-2]: https://img.shields.io/nuget/v/LemonUI.SHVDN2?label=nuget%20%28shvdn%202%29
[nuget-url-2]: https://www.nuget.org/packages/LemonUI.SHVDN2/
[nuget-img-3]: https://img.shields.io/nuget/v/LemonUI.SHVDN3?label=nuget%20%28shvdn%203%29
[nuget-url-3]: https://www.nuget.org/packages/LemonUI.SHVDN3/
[nuget-img-f]: https://img.shields.io/nuget/v/LemonUI.FiveM?label=nuget%20%28fivem%29
[nuget-url-f]: https://www.nuget.org/packages/LemonUI.FiveM/
[nuget-img-m]: https://img.shields.io/nuget/v/LemonUI.RageMP?label=nuget%20%28ragemp%29
[nuget-url-m]: https://www.nuget.org/packages/LemonUI.RageMP/
[nuget-img-r]: https://img.shields.io/nuget/v/LemonUI.RagePluginHook?label=nuget%20%28rph%29
[nuget-url-r]: https://www.nuget.org/packages/LemonUI.RagePluginHook/
[patreon-img]: https://img.shields.io/badge/support-patreon-FF424D.svg
[patreon-url]: https://www.patreon.com/lemonchan
[paypal-img]: https://img.shields.io/badge/support-paypal-0079C1.svg
[paypal-url]: https://paypal.me/justalemon
[discord-img]: https://img.shields.io/badge/discord-join-7289DA.svg
[discord-url]: https://discord.gg/Cf6sspj
