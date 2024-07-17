# ☕ DotGLFW [![Nuget](https://img.shields.io/nuget/v/DotGLFW)](https://www.nuget.org/packages/DotGLFW) [![Targets](https://img.shields.io/badge/targets-netstandard2.0;net8.0-blue)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) [![Nuget](https://img.shields.io/nuget/dt/DotGLFW)](https://www.nuget.org/packages/DotGLFW) 

A 1:1 mapping of the native GLFW API for .NET, with a lightweight managed wrapper for easy integration in your projects! Includes XML docs for all functions and types from the official GLFW documentation. Supports NativeAot, see [this example](DotGLFW.NugetAotExample/). Ships with pre-compiled binaries for Windows and MacOS.

> [!NOTE]
> Uses the [latest version of GLFW (3.4)](https://github.com/glfw/glfw/releases/tag/3.4).

The library is split into two parts: the *unmanaged API* and the *managed API*.

The unmanaged API is automatically generated using [DotGLFW.Generator](DotGLFW.Generator/). It parses the official GLFW documentation and generates C# code that matches the native API. Functions and types that would otherwise require runtime marshalling are automatically generated with compile-time marshalling to make the library as fast as possible, and to support native AOT compilation.

The managed API is a (mostly) hand-written wrapper around the unmanaged API to make it easier for you to use in your projects. It handles memory management and marshalling of strings, arrays, and other complex types for you. It also provides a more user-friendly interface for many functions using enums and other types instead of raw integers.

## Unmanaged API

The unmanaged API is the static `NativeGlfw` class, which has exact mappings for all GLFW macros (e.g. `GLFW_TRUE`) and functions (e.g. `glfwInit`). Typedefs are also mapped to their equivalents in C#, opaque handles are empty structs, and function pointers are represented as delegates.

> [!IMPORTANT]
> The unmanaged API will require you to perform your own marshalling of strings, arrays, and other complex types. Additionally, you will need to manage memory yourself, including keeping references to callbacks alive. If you don't want to have to do this, use the managed API instead.

## Managed API

The managed API is the static `Glfw` class, which wraps the unmanaged API in a more user-friendly and .NET-y way. It provides compile-time marshalling of strings, arrays, and other complex types, handles memory management for you (including keeping references to callbacks alive) and much more. Many functions (e.g. `glfwWindowHint`) have been overloaded to accept more user-friendly enums and types instead of raw integers.

Take a look in [the provided example](DotGLFW.Example/Program.cs) to see how to use the library with the managed API. It demonstrates how to create a window and set up a very simple rendering loop.